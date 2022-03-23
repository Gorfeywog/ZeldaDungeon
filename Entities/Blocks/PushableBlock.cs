using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Blocks
{
    public class PushableBlock : IBlock
    {
        private ISprite sprite = BlockSpriteFactory.Instance.CreatePushableBlock();
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public CollisionHandler Collision { get; set; }
        public EntityList roomEntities;
        private bool canBeMoved;
        private int AmountMoved;
        private int lengthHeld;

        public DrawLayer Layer { get => DrawLayer.Normal; }
        public Rectangle CurrentLoc { get; set; }
        private Rectangle newLoc;
        public PushableBlock(Point position, Room r)
        {
            int width = (int)SpriteUtil.SpriteSize.GenericBlockX;
			int height = (int)SpriteUtil.SpriteSize.GenericBlockY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            Collision = new CollisionHandler(r, this);
            canBeMoved = true;
            AmountMoved = 0;
        }

        //public void UpdateList(EntityList roomEntities)
        //{
        //    this.roomEntities = roomEntities;
        //    Collision.ChangeRooms(roomEntities);
        //}

        public void Move(Direction direction)
        {
                newLoc = new Rectangle(EntityUtils.Offset(CurrentLoc.Location, direction, SpriteUtil.SCALE_FACTOR), CurrentLoc.Size);
                CurrentLoc = newLoc;

    

        }

        public void InitMovement(Direction direction)
        {
            if (canBeMoved && AmountMoved < (int)SpriteUtil.SpriteSize.GenericBlockY && lengthHeld == 60)
            {
                Move(direction);
                AmountMoved++;
            }
            else if (AmountMoved == (int)SpriteUtil.SpriteSize.GenericBlockY)
            {
                canBeMoved = false;
            }
            else if (lengthHeld < 60)
            {
                lengthHeld++;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update()
        {
            sprite.Update();
        }
            
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;
    }
}

