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
        private int i = 0;
        private int j = 0;
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
        private bool bowDrawn = false;
        private bool boomerangDrawn = false;
        private bool bombDrawn = false;
        private bool specialBoomerangDrawn = false;
        private bool rCandleDrawn = false;
        private bool bCandleDrawn = false;
        private IDictionary<IItem, int> itemDict;
        private Point bowDest;
        private Point boomDest;
        private Point specBoomDest;
        private Point bombDest;
        private Point rCandleDest;
        private Point bCandleDest;
        private Point mapDest;
        private Point compDest;
        private LinkInventory inventory;
        private Game1 g;
        private Point mapTopLeft = new Point((int)SpriteUtil.MAP_POS_X * SpriteUtil.SCALE_FACTOR, (int)SpriteUtil.MAP_POS_Y * SpriteUtil.SCALE_FACTOR);
        private Point compassTopLeft = new Point((int)SpriteUtil.COMPASS_POS_X * SpriteUtil.SCALE_FACTOR, (int)SpriteUtil.COMPASS_POS_Y * SpriteUtil.SCALE_FACTOR);
        
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


            if (itemDict.ContainsKey(bowI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BowWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BowLength * SpriteUtil.SCALE_FACTOR;
                if (!bowDrawn) 
                {
                    bowDest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y) + new Point(0, j * SpriteUtil.PAUSE_ITEM_Y_GAP);
                    i++;

                }

                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(bowDest, size);
                bow.Draw(spriteBatch, destRect);
                bowDrawn = true;
                
            }
            if (itemDict.ContainsKey(boomerangI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BoomerangX * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BoomerangY * SpriteUtil.SCALE_FACTOR;
                if (!boomerangDrawn)
                {
                    boomDest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y) + new Point(0, j * SpriteUtil.PAUSE_ITEM_Y_GAP);
                    i++;

                }

                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(boomDest, size);
                boomerang.Draw(spriteBatch, destRect);
                boomerangDrawn = true;
            }
            if (itemDict.ContainsKey(specialBoomerangI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BoomerangX * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BoomerangY * SpriteUtil.SCALE_FACTOR;
                if (!specialBoomerangDrawn)
                {
                    specBoomDest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y) + new Point(0, j * SpriteUtil.PAUSE_ITEM_Y_GAP);
                    i++;

                }

                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(specBoomDest, size);
                specialBoomerang.Draw(spriteBatch, destRect);
                specialBoomerangDrawn = true;

            }
            if (itemDict.ContainsKey(compassI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.CompassWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.CompassLength * SpriteUtil.SCALE_FACTOR;
                compDest = compassTopLeft;
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(compDest, size);
                compass.Draw(spriteBatch, destRect);
            }
            if (itemDict.ContainsKey(mapI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.MapWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.MapLength * SpriteUtil.SCALE_FACTOR;
                mapDest = mapTopLeft;
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(mapDest, size);
                map.Draw(spriteBatch, destRect);
            }
            if (itemDict.ContainsKey(rCandleI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.CandleWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.CandleLength * SpriteUtil.SCALE_FACTOR;
                if (!rCandleDrawn)
                {
                    rCandleDest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y) + new Point(0, j * SpriteUtil.PAUSE_ITEM_Y_GAP);
                    i++;
                }

                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(rCandleDest, size);
                rCandle.Draw(spriteBatch, destRect);
                rCandleDrawn = true;
            }
            if (itemDict.ContainsKey(bCandleI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.CandleWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.CandleLength * SpriteUtil.SCALE_FACTOR;
                if (!bCandleDrawn)
                {
                    bCandleDest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y) + new Point(0, j * SpriteUtil.PAUSE_ITEM_Y_GAP);
                    i++;

                }

                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(bCandleDest, size);
                bCandle.Draw(spriteBatch, destRect);
                bCandleDrawn = true;
            }
            if (itemDict.ContainsKey(bombI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BombWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BombLength * SpriteUtil.SCALE_FACTOR;
                if (!bombDrawn)
                {
                    bombDest = itemTopLeft + new Point(i * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_Y) + new Point(0, j * SpriteUtil.PAUSE_ITEM_Y_GAP);
                    i++;
                }

                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(bombDest, size);
                bomb.Draw(spriteBatch, destRect);
                bombDrawn = true;
            }

            if (i > 5)
            {
                j++;
                i = 0;
            }
            


        }

        public void Update()
        {

        }
    }
}
