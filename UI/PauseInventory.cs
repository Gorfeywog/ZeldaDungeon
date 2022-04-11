using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Entities.Link;

namespace ZeldaDungeon.UI
{
    class PauseInventory
    {
        private ISprite boomerang;
        private ISprite bow;
        private ISprite bomb;
        private ISprite specialBoomerang;
        private ISprite compass;
        private ISprite map;
        private ISprite rCandle;
        private ISprite bCandle;
        private IItem boomerangI;
        private IItem bowI;
        private IItem bombI;
        private IItem specialBoomerangI;
        private IItem compassI;
        private IItem mapI;
        private IItem rCandleI;
        private IItem bCandleI;
        private bool special = true;
        private bool redCandle = true;
        private IDictionary<IItem, int> itemDict;
        private LinkInventory inventory;
        private Game1 g;
        private Point mapTopLeft = new Point((int)SpriteUtil.MAP_POS_X * SpriteUtil.SCALE_FACTOR, (int)SpriteUtil.MAP_POS_Y * SpriteUtil.SCALE_FACTOR);
        private Point compassTopLeft = new Point((int)SpriteUtil.COMPASS_POS_X * SpriteUtil.SCALE_FACTOR, (int)SpriteUtil.COMPASS_POS_Y * SpriteUtil.SCALE_FACTOR);
        private Point activeItemTopLeft = new Point((int)SpriteUtil.ACTIVE_ITEM_X * SpriteUtil.SCALE_FACTOR, (int)SpriteUtil.ACTIVE_ITEM_Y * SpriteUtil.SCALE_FACTOR);
        
        public PauseInventory(Game1 g)
        {
            this.g = g;
            boomerang = ItemSpriteFactory.Instance.CreateWoodenBoomerang();
            bow = ItemSpriteFactory.Instance.CreateBow();
            specialBoomerang = EnemySpriteFactory.Instance.CreateStaticMagicBoomerangSprite();
            compass = ItemSpriteFactory.Instance.CreateCompass();
            map = ItemSpriteFactory.Instance.CreateMap();
            rCandle = ItemSpriteFactory.Instance.CreateCandle(redCandle);
            bCandle = ItemSpriteFactory.Instance.CreateCandle(!redCandle);
            bomb = ItemSpriteFactory.Instance.CreateBomb();
            boomerangI = new BoomerangItem(g, !special);
            bowI = new BowItem();
            specialBoomerangI = new BoomerangItem(g, special);
            compassI = new CompassItem();
            mapI = new MapItem();
            rCandleI = new CandleItem(g, redCandle);
            bCandleI = new CandleItem(g, !redCandle);
            bombI = new BombItem(g);
            inventory = g.Player.GetInv();
        }

        public void Draw(SpriteBatch spriteBatch, Point itemTopLeft)
        {
            itemDict = inventory.GetDict();
            int scaledWidth;
            int scaledHeight;
            int i = 0;
            int j = 0;
            Point dest;


            if (itemDict.ContainsKey(bowI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BowWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BowLength * SpriteUtil.SCALE_FACTOR;
                dest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y);
                if (j > 0)
                {
                    dest += new Point(0, (SpriteUtil.PAUSE_ITEM_Y_GAP));
                }
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(dest, size);
                bow.Draw(spriteBatch, destRect);
                i++;
                if (i > 4)
                {
                    j = 1;
                    i = 0;
                }
            }
            if (itemDict.ContainsKey(boomerangI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BoomerangX * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BoomerangY * SpriteUtil.SCALE_FACTOR;
                dest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y);
                if (j > 0)
                {
                    dest += new Point(0, (SpriteUtil.PAUSE_ITEM_Y_GAP));
                }
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(dest, size);
                boomerang.Draw(spriteBatch, destRect);
                i++;
                if (i > 4)
                {
                    j = 1;
                    i = 0;
                }
            }
            if (itemDict.ContainsKey(specialBoomerangI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BoomerangX * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BoomerangY * SpriteUtil.SCALE_FACTOR;
                dest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y);
                if (j > 0)
                {
                    dest += new Point(0, (SpriteUtil.PAUSE_ITEM_Y_GAP));
                }
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(dest, size);
                specialBoomerang.Draw(spriteBatch, destRect);
                i++;
                if (i > 4)
                {
                    j = 1;
                    i = 0;
                }
            }
            if (itemDict.ContainsKey(compassI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.CompassWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.CompassLength * SpriteUtil.SCALE_FACTOR;
                dest = compassTopLeft;
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(dest, size);
                compass.Draw(spriteBatch, destRect);
            }
            if (itemDict.ContainsKey(mapI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.MapWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.MapLength * SpriteUtil.SCALE_FACTOR;
                dest = mapTopLeft;
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(dest, size);
                map.Draw(spriteBatch, destRect);
            }
            if (itemDict.ContainsKey(rCandleI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.CandleWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.CandleLength * SpriteUtil.SCALE_FACTOR;
                dest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y);
                if (j > 0)
                {
                    dest += new Point(0, (SpriteUtil.PAUSE_ITEM_Y_GAP));
                }
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(dest, size);
                rCandle.Draw(spriteBatch, destRect);
                i++;
                if (i > 4)
                {
                    j = 1;
                    i = 0;
                }
            }
            if (itemDict.ContainsKey(bCandleI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.CandleWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.CandleLength * SpriteUtil.SCALE_FACTOR;
                dest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y);
                if (j > 0)
                {
                    dest += new Point(0, (SpriteUtil.PAUSE_ITEM_Y_GAP));
                }
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(dest, size);
                bCandle.Draw(spriteBatch, destRect);
                i++;
                if (i > 4)
                {
                    j = 1;
                    i = 0;
                }
            }
            if (itemDict.ContainsKey(bombI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BombWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BombLength * SpriteUtil.SCALE_FACTOR;
                dest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y);
                if (j > 0)
                {
                    dest += new Point(0, (SpriteUtil.PAUSE_ITEM_Y_GAP));
                }
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(dest, size);
                bomb.Draw(spriteBatch, destRect);
                i++;
                if (i > 4)
                {
                    j = 1;
                    i = 0;
                }
            }


        }

        public void Update()
        {

        }
    }
}
