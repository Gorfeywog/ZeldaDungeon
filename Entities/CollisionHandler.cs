using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ZeldaDungeon.Entities.Blocks;
using ZeldaDungeon.Entities.Enemies;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;


namespace ZeldaDungeon.Entities
{
    
    public class CollisionHandler
    {
        private Room CurrentRoom { get; set; }
        private EntityList RoomEntities { get => CurrentRoom.roomEntities; }
        private IEntity ActualEntity;
        private CollisionHeight height;
        private int dx, dy;
        private bool XCollision;
        private bool YCollision;

        public CollisionHandler(Room room, IEntity ActualEntity)
        {
            CurrentRoom = room;
            this.ActualEntity = ActualEntity;
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

        public void ChangeRooms(Room newRoom)
        {
            CurrentRoom = newRoom;
        }
        public bool WillHitBlock(Rectangle nextLoc)
        {
            if (!nextLoc.Intersects(CurrentRoom.RoomPos))
            {
                return true; // cannot escape the room!
            }
            foreach (Door d in RoomEntities.Doors()) 
            {
                if (DetectCollision(nextLoc, d.CurrentLoc) && ActualEntity is ILink player)
                {
                    HandleCollisionPlayerDoor(player, d);
                    return true;
                }
            }
            foreach (IEntity en in RoomEntities)
            {
                if (DetectCollision(nextLoc, en.CurrentLoc))
                {
                    if (en is PushableBlock pb && ActualEntity is ILink)
                    {
                        pb.InitMovement(DetectDirection(pb.CurrentLoc));
                        return true;
                    }
                    if (en is IBlock block)
                    {
                        if (ActualEntity is IProjectile && block.Height == CollisionHeight.Projectile)
                        {
                            return false;
                        }
                        else if (height <= block.Height)
                        {
                            return true;
                        }

                    }
                    else if (en is Trap trap && !ActualEntity.Equals(trap))
                    {
                        return true;
                    }
                }

            }
            return false;

        }
        private void HandleCollisionPlayerEnemy(ILink player, IEnemy enemy)
        {
            if (DetectCollision(player.CurrentLoc, enemy.CurrentLoc) && !enemy.IsFriendly)
            {
                player.TakeDamage();
            }
        }

        private void HandleCollisionPlayerProjectile(ILink player, IProjectile proj)
        {
            if (DetectCollision(player.CurrentLoc, proj.CurrentLoc))
            {
                proj.OnHit(player);
            }
        }

        private static readonly IItem key = new KeyItem();
        private void HandleCollisionPlayerDoor(ILink player, Door d)
        {
            if (d.State == DoorState.Locked && player.HasItem(key))
            {
                player.UseItem(key);
                CurrentRoom.G.UnlockRoomDoor(d.Dir); // CurrentRoom.G here is bad but the alternatives seem worse
            }
            if (d.CanPass)
            {
                CurrentRoom.G.UseRoomDoor(d.Dir);
            }
        }
        private void HandleCollisionEnemyProjectile(IEnemy enemy, IProjectile proj)
        {
            if (DetectCollision(enemy.CurrentLoc, proj.CurrentLoc))
            {
                proj.OnHit(enemy);
            }
        }

        
        private bool DetectCollision(Rectangle rectangle1, Rectangle rectangle2)
        {
            // If entity1 starts before entity2 finishes and vice versa, you know theres an x-value that matches. If the same thing happens with the y-values, there is collision.
            XCollision = (rectangle1.X < (rectangle2.X + rectangle2.Width - (2 * SpriteUtil.SCALE_FACTOR))
                && rectangle2.X < (rectangle1.X + rectangle1.Width - (2 * SpriteUtil.SCALE_FACTOR)));
            YCollision = (rectangle1.Y < (rectangle2.Y + rectangle2.Height - (2 * SpriteUtil.SCALE_FACTOR))
                    && rectangle2.Y < (rectangle1.Y + rectangle1.Height) - (2 * SpriteUtil.SCALE_FACTOR));

            return XCollision && YCollision;
        }

        public Direction DetectDirection(Rectangle CurrentEntityLoc)
        {
            bool actualEntityBigX = false;
            bool actualEntityBigY = false;
            if (CurrentEntityLoc.X < ActualEntity.CurrentLoc.X)
            {
                dx = - ActualEntity.CurrentLoc.X + CurrentEntityLoc.X + CurrentEntityLoc.Width;
            } else
            {
                actualEntityBigX = true;
                dx = ActualEntity.CurrentLoc.X + ActualEntity.CurrentLoc.Width - CurrentEntityLoc.X;
            }

            if (CurrentEntityLoc.Y < ActualEntity.CurrentLoc.Y)
            {
                dy = - ActualEntity.CurrentLoc.Y + CurrentEntityLoc.Y + CurrentEntityLoc.Height;
            }
            else
            {
                actualEntityBigY = true;
                dy = ActualEntity.CurrentLoc.Y + ActualEntity.CurrentLoc.Height - CurrentEntityLoc.Y;
            }
            // If dx > dy, we know it's either a top or bottom collision.
            if (dx > dy)
            {
                // If dy is positive, the actualEntity was hit from the top.
                // If negative, it was hit from the bottom.
                if (!actualEntityBigY) return Direction.Up;
                else return Direction.Down;
            }
            // If dy > dx, we know it's either a left or right collision.
            else
            {
                // If dx is positive, the actualEntity was hit from the left.
                // If negative, it was hit from the right.
                if (!actualEntityBigX) return Direction.Left;
                else return Direction.Right;
            }
        }

        public void TrapUpdate()
        {

            foreach (IEntity en in RoomEntities)
            {
                if (en is Trap trap)
                {
                    DetectCollision(ActualEntity.CurrentLoc, trap.CurrentLoc);
                    if (XCollision || YCollision)
                    {
                        trap.SpecialTrapAttack();
                    }
                }
                
                
            }

        }
        public void SpecialTriggerUpdate()
        {
            if (!(ActualEntity is ILink))
            {
                return; // only happens for Link
            }
            Rectangle loc = ActualEntity.CurrentLoc;
            foreach (IBlock b in RoomEntities.Blocks())
            {
                if (b is SpecialTrigger st && DetectCollision(loc, b.CurrentLoc) && ActualEntity is ILink)
                {
                    st.Trigger();
                }
            }
        }

        public void Update()
        {
            if (ActualEntity is ILink player)
            {
                foreach (IEntity en in RoomEntities)
                {
                    if (en is IEnemy enemy)
                    {
                        HandleCollisionPlayerEnemy(player, enemy);
                    }
                    else if (en is IProjectile proj)
                    {
                        HandleCollisionPlayerProjectile(player, proj);
                    }
                }
            }
            else if (ActualEntity is IEnemy enemy)
            {
                foreach (IProjectile proj in RoomEntities.Projectiles())
                {
                    HandleCollisionEnemyProjectile(enemy, proj);
                }
            }
        }
    }
}
