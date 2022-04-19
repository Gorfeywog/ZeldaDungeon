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
        private int bowX = 0, bowY = 0;
        private int boomX = 0, boomY = 0;
        private int specBoomX = 0, specBoomY = 0;
        private int rCandleX = 0, rCandleY = 0;
        private int bCandleX = 0, bCandleY = 0;
        private int bombX = 0, bombY = 0;
        private ItemSelect itemSelect;

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
            bowI = new BowItem(g);
            specialBoomerangI = new BoomerangItem(g, special);
            compassI = new CompassItem();
            mapI = new MapItem();
            rCandleI = new CandleItem(g, redCandle);
            bCandleI = new CandleItem(g, !redCandle);
            bombI = new BombItem(g);
            inventory = g.Player.GetInv();
            itemSelect = new ItemSelect(g.Player);
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
                    bowX = i;
                    bowY = j;
                    i++;
                }
                bowDest = itemTopLeft + new Point(bowX * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, bowY * ((int)SpriteUtil.PAUSE_ITEM_OFFSET_Y + SpriteUtil.PAUSE_ITEM_Y_GAP));
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
                    boomX = i;
                    boomY = j;
                    i++;
                }
                boomDest = itemTopLeft + new Point(boomX * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, boomY * ((int)SpriteUtil.PAUSE_ITEM_OFFSET_Y + SpriteUtil.PAUSE_ITEM_Y_GAP));
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
                    specBoomX = i;
                    specBoomY = j;
                    i++;
                }
                specBoomDest = itemTopLeft + new Point(specBoomX * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, specBoomY * ((int)SpriteUtil.PAUSE_ITEM_OFFSET_Y + SpriteUtil.PAUSE_ITEM_Y_GAP));
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(specBoomDest, size);
                specialBoomerang.Draw(spriteBatch, destRect);
                specialBoomerangDrawn = true;

            }
            if (itemDict.ContainsKey(rCandleI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.CandleWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.CandleLength * SpriteUtil.SCALE_FACTOR;
                if (!rCandleDrawn)
                {
                    rCandleX = i;
                    rCandleY = j;
                    i++;
                }
                rCandleDest = itemTopLeft + new Point(rCandleX * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, rCandleY * ((int)SpriteUtil.PAUSE_ITEM_OFFSET_Y + SpriteUtil.PAUSE_ITEM_Y_GAP));
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
                    bCandleX = i;
                    bCandleY = j;
                    i++;
                }
                bCandleDest = itemTopLeft + new Point(bCandleX * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, bCandleY * ((int)SpriteUtil.PAUSE_ITEM_OFFSET_Y + SpriteUtil.PAUSE_ITEM_Y_GAP));
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
                    bombX = i;
                    bombY = j;
                    i++;
                }
                bombDest = itemTopLeft + new Point(bombX * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, bombY * ((int)SpriteUtil.PAUSE_ITEM_OFFSET_Y + SpriteUtil.PAUSE_ITEM_Y_GAP));
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(bombDest, size);
                bomb.Draw(spriteBatch, destRect);
                bombDrawn = true;
            }

            if (itemDict.ContainsKey(compassI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.CompassWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.CompassLength * SpriteUtil.SCALE_FACTOR;
                compDest = itemTopLeft - compassTopLeft;
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(compDest, size);
                compass.Draw(spriteBatch, destRect);
            }
            if (itemDict.ContainsKey(mapI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.MapWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.MapLength * SpriteUtil.SCALE_FACTOR;
                mapDest = itemTopLeft - mapTopLeft;
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(mapDest, size);
                map.Draw(spriteBatch, destRect);
            }
            DrawSelectionCursor();
            if (i > 4)
            {
                j = 1;
                i = 0;
            }
            


        }
        private void DrawSelectionCursor()
        {
            ISprite cursor = ItemSpriteFactory.Instance.CreateSelectionIndicator();
            var selected = itemSelect.SelectedItem();
            if (selected == null) { return; }
            Point loc;
            int scaledWidth;
            int scaledHeight;
            if (selected.Equals(bowI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BowWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BowLength * SpriteUtil.SCALE_FACTOR;
                loc = bowDest;
            }
            else if (selected.Equals(boomerangI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BoomerangX * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BoomerangY * SpriteUtil.SCALE_FACTOR;
                loc = boomDest
            }
            else if (selected.Equals(specialBoomerangI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BoomerangX * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BoomerangY * SpriteUtil.SCALE_FACTOR;
                loc = specBoomDest;
            }
            else if (selected.Equals(rCandleI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.CandleWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.CandleLength * SpriteUtil.SCALE_FACTOR;
                loc = rCandleDest;
            }
            else if (selected.Equals(bCandleI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.CandleWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.CandleLength * SpriteUtil.SCALE_FACTOR;
                loc = bCandleDest;
            }
            else if (selected.Equals(bombI))
            {
                scaledWidth = (int)SpriteUtil.SpriteSize.BombWidth * SpriteUtil.SCALE_FACTOR;
                scaledHeight = (int)SpriteUtil.SpriteSize.BombLength * SpriteUtil.SCALE_FACTOR;
                loc = bombDest;
            }
            else
            {
                return;
            }
        }
        public void Update()
        {

        }
    }
}
