using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Commands;
using ZeldaDungeon.Entities;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon
{
    public class ControllerManager
    {
        private Game1 g;
        private MouseController mouseCon;
        private KeyboardController keyboardCon;
        // constants used for specific commands:
        private static readonly int xDoorSize = (int)SpriteUtil.SpriteSize.DoorX * SpriteUtil.SCALE_FACTOR;
        private static readonly int yDoorSize = (int)SpriteUtil.SpriteSize.DoorY * SpriteUtil.SCALE_FACTOR;
        private static readonly int xCenter = SpriteUtil.X_POS_CENTER * SpriteUtil.SCALE_FACTOR;
        private static readonly int yCenter = (SpriteUtil.HUD_HEIGHT + SpriteUtil.Y_POS_CENTER) * SpriteUtil.SCALE_FACTOR;
        private static readonly int yTop = (SpriteUtil.HUD_HEIGHT + SpriteUtil.Y_POS_TOP) * SpriteUtil.SCALE_FACTOR;
        private static readonly int yBottom = (SpriteUtil.HUD_HEIGHT + SpriteUtil.Y_POS_BOTTOM) * SpriteUtil.SCALE_FACTOR;
        private static readonly int xLeft = SpriteUtil.X_POS_LEFT * SpriteUtil.SCALE_FACTOR;
        private static readonly int xRight = SpriteUtil.X_POS_RIGHT * SpriteUtil.SCALE_FACTOR;
        public ControllerManager(Game1 g)
        {
            this.g = g;
            mouseCon = new MouseController();
            keyboardCon = new KeyboardController();
        }
        public void Reset()
        {
            mouseCon = new MouseController();
            keyboardCon = new KeyboardController();
        }
        public void Update()
        {
            mouseCon.UpdateState();
            keyboardCon.UpdateState();
            mouseCon.ExecuteCommands();
            keyboardCon.ExecuteCommands();
        }
        public void RegisterCommands()
        {
            keyboardCon.RegisterCommand(Keys.Q, new Quit(g));
            keyboardCon.RegisterCommand(Keys.R, new Reset(g));
            keyboardCon.RegisterCommand(Keys.Escape, new OpenMenu(g));
            ICommand linkUp = new MoveLink(g, Direction.Up);
            ICommand linkDown = new MoveLink(g, Direction.Down);
            ICommand linkLeft = new MoveLink(g, Direction.Left);
            ICommand linkRight = new MoveLink(g, Direction.Right);
            ICommand linkStopUp = new StopLink(g, Direction.Up);
            ICommand linkStopDown = new StopLink(g, Direction.Down);
            ICommand linkStopLeft = new StopLink(g, Direction.Left);
            ICommand linkStopRight = new StopLink(g, Direction.Right);
            ICommand linkAttack = new LinkAttack(g);
            keyboardCon.RegisterHoldCommand(Keys.W, linkUp, linkStopUp);
            keyboardCon.RegisterHoldCommand(Keys.Up, linkUp, linkStopUp);
            keyboardCon.RegisterHoldCommand(Keys.A, linkLeft, linkStopLeft);
            keyboardCon.RegisterHoldCommand(Keys.Left, linkLeft, linkStopLeft);
            keyboardCon.RegisterHoldCommand(Keys.S, linkDown, linkStopDown);
            keyboardCon.RegisterHoldCommand(Keys.Down, linkDown, linkStopDown);
            keyboardCon.RegisterHoldCommand(Keys.D, linkRight, linkStopRight);
            keyboardCon.RegisterHoldCommand(Keys.Right, linkRight, linkStopRight);
            keyboardCon.RegisterCommand(Keys.Z, linkAttack);
            keyboardCon.RegisterCommand(Keys.N, linkAttack);
            keyboardCon.RegisterCommand(Keys.O, new DecRoom(g));
            keyboardCon.RegisterCommand(Keys.P, new IncRoom(g));
            keyboardCon.RegisterCommand(Keys.D1, new LinkUseItem(g, new BombItem(g)));
            keyboardCon.RegisterCommand(Keys.D2, new LinkUseItem(g, new ArrowItem(g, false)));
            keyboardCon.RegisterCommand(Keys.D3, new LinkUseItem(g, new ArrowItem(g, true)));
            keyboardCon.RegisterCommand(Keys.D4, new LinkUseItem(g, new CandleItem(g, true)));
            keyboardCon.RegisterCommand(Keys.D5, new LinkUseItem(g, new BoomerangItem(g, false)));
            keyboardCon.RegisterCommand(Keys.D6, new LinkUseItem(g, new BoomerangItem(g, true)));
            keyboardCon.RegisterCommand(Keys.D7, new LinkUseItem(g, new CandleItem(g, false)));
            keyboardCon.RegisterCommand(Keys.M, new MuteBackground(g));
            keyboardCon.RegisterCommand(Keys.L, new DamageLink(g));
            keyboardCon.RegisterCommand(Keys.X, new LinkUseSelectedItem(g));
            keyboardCon.RegisterCommand(Keys.OemOpenBrackets, new IncItemSelect(g));
            keyboardCon.RegisterCommand(Keys.OemCloseBrackets, new DecItemSelect(g));

            //Sets up locations to click to move between doors with mouse
            mouseCon.RegisterCommand(new Rectangle(xCenter, yTop, xDoorSize, yDoorSize), new LinkUseDoor(g, Direction.Up));
            mouseCon.RegisterCommand(new Rectangle(xCenter, yBottom, xDoorSize, yDoorSize), new LinkUseDoor(g, Direction.Down));
            mouseCon.RegisterCommand(new Rectangle(xLeft, yCenter, xDoorSize, yDoorSize), new LinkUseDoor(g, Direction.Left));
            mouseCon.RegisterCommand(new Rectangle(xRight, yCenter, xDoorSize, yDoorSize), new LinkUseDoor(g, Direction.Right));
        }
    }
}
