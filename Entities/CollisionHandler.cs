using Microsoft.Xna.Framework;
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
        }

        public void ChangeRooms(EntityList newList)
        {
            roomEntities = newList;

        }
        public bool WillHitBlock(Rectangle nextLoc)
        {
            foreach (IEntity en in roomEntities)
            {
                if (en is PushableBlock pb && ActualEntity is ILink && DetectCollision(nextLoc, en.CurrentLoc))
                {
                    pb.InitMovement(DetectDirection(pb));
                    return true;
                }
                else if (en is IBlock block && DetectCollision(nextLoc, block.CurrentLoc) && height <= block.Height)
                {
                    return true;
                }   
                else if (en is Trap trap && !ActualEntity.Equals(trap) && DetectCollision(nextLoc, trap.CurrentLoc))
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

        private void HandleCollisionPlayerProjectile(ILink player, IProjectile proj)
        {
            // Will be changed, implement after refactoring.
            if (DetectCollision(player.CurrentLoc, proj.CurrentLoc))
            {
                proj.OnHit(player);
            }
        }

        private void HandleCollisionEnemyProjectile(IEnemy enemy, IProjectile proj)
        {
            // Will be changed, implement after refactoring.
            // Might be changed to EnemyProjectile.
            if (DetectCollision(enemy.CurrentLoc, proj.CurrentLoc))
            {
                proj.OnHit(enemy);
            }
        }

        
        private bool DetectCollision(Rectangle rectangle1, Rectangle rectangle2)
        {
            // If entity1 starts before entity2 finishes and vice versa, you know theres an x-value that matches. If the same thing happens with the y-values, there is collision.
            // Easier to visualize in a picture. Also idk if it matters to do this big if statement or boolean variables.
            XCollision = (rectangle1.X < (rectangle2.X + rectangle2.Width - (2 * SpriteUtil.SCALE_FACTOR))
                && rectangle2.X < (rectangle1.X + rectangle1.Width - (2 * SpriteUtil.SCALE_FACTOR)));
            YCollision = (rectangle1.Y < (rectangle2.Y + rectangle2.Height - (2 * SpriteUtil.SCALE_FACTOR))
                    && rectangle2.Y < (rectangle1.Y + rectangle1.Height) - (2 * SpriteUtil.SCALE_FACTOR));

                if (XCollision && YCollision)
                {
                    return true;
                }
                return false;
        }

        public Direction DetectDirection(IEntity CurrentEntity)
        {
            bool actBigX = false;
            bool actBigY = false;
            if (CurrentEntity.CurrentLoc.X < ActualEntity.CurrentLoc.X)
            {
                dx = - ActualEntity.CurrentLoc.X + CurrentEntity.CurrentLoc.X + CurrentEntity.CurrentLoc.Width;
            } else
            {
                actBigX = true;
                dx = ActualEntity.CurrentLoc.X + ActualEntity.CurrentLoc.Width - CurrentEntity.CurrentLoc.X;
            }

            if (CurrentEntity.CurrentLoc.Y < ActualEntity.CurrentLoc.Y)
            {
                dy = - ActualEntity.CurrentLoc.Y + CurrentEntity.CurrentLoc.Y + CurrentEntity.CurrentLoc.Height;
            }
            else
            {
                actBigY = true;
                dy = ActualEntity.CurrentLoc.Y + ActualEntity.CurrentLoc.Height - CurrentEntity.CurrentLoc.Y;
            }
            // If dx > dy, we know it's either a top or bottom collision.
            if (dx > dy)
            {
                // If dy is positive, the actualEntity was hit from the top.
                // If negative, it was hit from the bottom.
                if (!actBigY) return Direction.Up;
                else return Direction.Down;
            }
            // If dy > dx, we know it's either a left or right collision.
            else
            {
                // If dx is positive, the actualEntity was hit from the left.
                // If negative, it was hit from the right.
                if (!actBigX) return Direction.Left;
                else return Direction.Right;
            }
        }

        public void TrapUpdate()
        {

            foreach (IEntity en in roomEntities)
            {
                if (en is Trap trap)
                {
                    DetectCollision(ActualEntity.CurrentLoc, trap.CurrentLoc);
                    if (XCollision || YCollision)
                    {
                        trap.Attack();
                    }
                }
                
                
            }

        }

        public void Update()
        {
            if (ActualEntity is ILink player)
            {
                foreach (IEnemy enemy in roomEntities.Enemies())
                {
                    HandleCollisionPlayerEnemy(player, enemy);
                }
                foreach(IProjectile proj in roomEntities.Projectiles())
                {
                    HandleCollisionPlayerProjectile(player, proj);
                }
            }
            else if (ActualEntity is IEnemy enemy)
            {
                foreach (IProjectile proj in roomEntities.Projectiles())
                {
                    HandleCollisionEnemyProjectile(enemy, proj);
                }
            }
        }
    }
}
