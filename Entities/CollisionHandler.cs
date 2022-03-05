﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ZeldaDungeon.Entities.Blocks;
using ZeldaDungeon.Entities.Enemies;

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
        private void HandleCollisionPlayerEnemy(ILink player, KeyValuePair<IEntity, Direction> collision)
        {
            player.TakeDamage();
            // This is all this does, right?
        }

        private void HandleCollisionPlayerItem(ILink player, KeyValuePair<IEntity, Direction> collision)
        {
            // Will be changed, implement after refactoring.
        }

        private void HandleCollisionEnemyItem(IEnemy enemy, KeyValuePair<IEntity, Direction> collision)
        {
            // Will be changed, implement after refactoring.
            // Might be changed to EnemyProjectile.
        }

        private bool DetectCollision(Rectangle rectangle1, Rectangle rectangle2)
        {
                // If entity1 starts before entity2 finishes and vice versa, you know theres an x-value that matches. If the same thing happens with the y-values, there is collision.
                // Easier to visualize in a picture. Also idk if it matters to do this big if statement or boolean variables.
                if (rectangle1.X < (rectangle2.X + rectangle2.Width)
                    && rectangle2.X < (rectangle1.X + rectangle1.Width)
                    && rectangle1.Y < (rectangle2.Y + rectangle2.Height)
                    && rectangle2.Y < (rectangle1.Y + rectangle1.Height))
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

        // TODO:
        // Handle entities that can be walked through
        // Create an actual collision handler, rename this
        // Handle what happens when dx = dy
        // Implement this to every dynamic entity's update method

        // NOTE:
        // Haven't been able to test it but I think detection is implemented correctly. I wanted to 
        // discuss how you guys want to go about implementing the actual handler. Also, I feel like
        // I should just make this class a utility class for the handler and take out the constructor.

        public void Update()
        {
/*            DetectCollision();
            foreach (KeyValuePair<IEntity, Direction> Collision in Collisions)
            {
                if (ActualEntity is ILink)
                {
                    if (Collision.Key is IEnemy)
                    {
                        HandleCollisionPlayerEnemy((ILink)ActualEntity, Collision);
                    }
                    else if (Collision.Key is IItem)
                    {
                        HandleCollisionPlayerItem((ILink)ActualEntity, Collision);
                    }
                }
                else if (ActualEntity is IEnemy && Collision.Key is IItem)
                {
                    //Might be changed to IProjectile
                    HandleCollisionEnemyItem((IEnemy)ActualEntity, Collision);
                }
            }*/

        }
    }
}
