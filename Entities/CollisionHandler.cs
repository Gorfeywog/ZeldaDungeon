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
        private bool XCollision;
        private bool YCollision;

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
                // delete this comment
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
            //You've got to be kidding me
            return rectangle1.Intersects(rectangle2);

        }

        public Direction DetectDirection(IEntity CurrentEntity)
        {
            
            if (CurrentEntity.CurrentLoc.X < ActualEntity.CurrentLoc.X)
            {
                dx = ActualEntity.CurrentLoc.X - CurrentEntity.CurrentLoc.X - CurrentEntity.CurrentLoc.Width;
            } else
            {
                dx = ActualEntity.CurrentLoc.X + ActualEntity.CurrentLoc.Width - CurrentEntity.CurrentLoc.X;
            }

            if (CurrentEntity.CurrentLoc.Y < ActualEntity.CurrentLoc.Y)
            {
                dy = ActualEntity.CurrentLoc.Y - CurrentEntity.CurrentLoc.Y - CurrentEntity.CurrentLoc.Height;
            }
            else
            {
                dy = ActualEntity.CurrentLoc.Y + ActualEntity.CurrentLoc.Height - CurrentEntity.CurrentLoc.Y;
            }
            // If dx > dy, we know it's either a top or bottom collision.
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                // If dy is positive, the actualEntity was hit from the top.
                // If negative, it was hit from the bottom.
                if (dy < 0) return Direction.Up;
                else return Direction.Down;
            }
            // If dy > dx, we know it's either a left or right collision.
            else
            {
                // If dx is positive, the actualEntity was hit from the left.
                // If negative, it was hit from the right.
                if (dx < 0) return Direction.Left;
                else return Direction.Right;
            }
        }

        public void trapUpdate()
        {

            foreach (IEntity en in roomEntities)
            {
                if (en is Trap trap)
                {
                    DetectCollision(ActualEntity.CurrentLoc, trap.CurrentLoc);
                    if (XCollision || YCollision)
                    {
                        trap.Moving = true;
                        trap.Move();

                    }
                }
                
                
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
