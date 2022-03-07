﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ZeldaDungeon.Entities.Blocks;
using ZeldaDungeon.Entities.Enemies;
using ZeldaDungeon.Sprites;


namespace ZeldaDungeon.Entities
{
    public class CollisionHandler
    {
        private EntityList roomEntities;
        private IList<IBlock> blockEntities;
        private IDictionary<IEntity, Direction> Collisions;
        private IEntity ActualEntity;
        private CollisionHeight height;
        private int dx, dy;

        public CollisionHandler(EntityList roomEntities, IEntity ActualEntity)
        {
            this.roomEntities = roomEntities;
            this.ActualEntity = ActualEntity;
            Collisions = new Dictionary<IEntity, Direction>();
            if (ActualEntity is IEnemy e)
            {
                height = e.Height;
            }
            else if (ActualEntity is ILink player)
            {
                height = player.Height;
            }
            else
            {
                height = CollisionHeight.Normal;
            }
            blockEntities = new List<IBlock>();
        }

        public void ChangeRooms(EntityList newList)
        {
            roomEntities = newList;
            blockEntities.Clear();
            foreach (IEntity ent in roomEntities)
            {
                if (ent is IBlock block) blockEntities.Add(block);
            }
        }
        public bool WillHitBlock(Rectangle nextLoc)
        {
            foreach (IBlock block in blockEntities)
            {
                /*                if (isFlying) 
                                {
                                    if (DetectCollision(nextLoc, block.CurrentLoc) && block is BlueUnwalkableGapBlock) 
                                    {
                                        return true;
                                    }
                                    else return false;
                                }*/
                if (DetectCollision(nextLoc, block.CurrentLoc) && height <= block.Height)
                {
                    return true;
                }
                
            }
            return false;
        }
        private void HandleCollisionPlayerEnemy(ILink player, IEnemy enemy)
        {
            if (DetectCollision(player.CurrentLoc, enemy.CurrentLoc))
            {
                player.TakeDamage();
            }
            // This is all this does, right?
        }

        private void HandleCollisionPlayerProjectile(ILink player, IProjectile item)
        {
            // Will be changed, implement after refactoring.
            if (DetectCollision(player.CurrentLoc, item.CurrentLoc))
            {
                player.TakeDamage();
                item.DespawnEffect();
            }
        }

        private void HandleCollisionEnemyProjectile(IEnemy enemy, IProjectile item)
        {
            // Will be changed, implement after refactoring.
            // Might be changed to EnemyProjectile.
            if (DetectCollision(enemy.CurrentLoc, item.CurrentLoc))
            {
                enemy.TakeDamage();
                item.DespawnEffect();
            }
        }

        private bool DetectCollision(Rectangle rectangle1, Rectangle rectangle2)
        {
                // If entity1 starts before entity2 finishes and vice versa, you know theres an x-value that matches. If the same thing happens with the y-values, there is collision.
                // Easier to visualize in a picture. Also idk if it matters to do this big if statement or boolean variables.
                if (rectangle1.X < (rectangle2.X + rectangle2.Width - (2 * SpriteUtil.SCALE_FACTOR))
                    && rectangle2.X < (rectangle1.X + rectangle1.Width - (2 * SpriteUtil.SCALE_FACTOR))
                    && rectangle1.Y < (rectangle2.Y + rectangle2.Height - (2 * SpriteUtil.SCALE_FACTOR))
                    && rectangle2.Y < (rectangle1.Y + rectangle1.Height) - (2 * SpriteUtil.SCALE_FACTOR))
                {
                    return true;
                }
                return false;
        }

        private Direction DetectDirection(IEntity CurrentEntity)
        {
            dx = ActualEntity.CurrentLoc.X - CurrentEntity.CurrentLoc.X;
            dy = ActualEntity.CurrentLoc.Y - CurrentEntity.CurrentLoc.Y;
            // If dx > dy, we know it's either a top or bottom collision.
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                // If dy is positive, the actualEntity was hit from the top.
                // If negative, it was hit from the bottom.
                if (dy > 0) return Direction.Up;
                else return Direction.Down;
            }
            // If dy > dx, we know it's either a left or right collision.
            else
            {
                // If dx is positive, the actualEntity was hit from the left.
                // If negative, it was hit from the right.
                if (dx > 0) return Direction.Left;
                else return Direction.Right;
            }
        }

        public void Update()
        {
            foreach (IEntity ent in roomEntities)
            {
                if (ActualEntity is ILink player)
                {
                    if (ent is IEnemy enemy)
                    {
                        HandleCollisionPlayerEnemy(player, enemy);
                    }

                    else if (ent is IProjectile item)
                    {
                        HandleCollisionPlayerProjectile(player, item);
                    }
                }

                else if (ActualEntity is IEnemy enemy)
                {
                    if (ent is IProjectile item)
                    {
                        HandleCollisionEnemyProjectile(enemy, item);
                    }
                }
         
            }
        }
    }
}
