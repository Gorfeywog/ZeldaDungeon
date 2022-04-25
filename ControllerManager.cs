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
            keyboardCon.RegisterHoldCommand(Keys.W, linkUp, linkStopUp);
            keyboardCon.RegisterHoldCommand(Keys.Up, linkUp, linkStopUp);
            keyboardCon.RegisterHoldCommand(Keys.A, linkLeft, linkStopLeft);
            keyboardCon.RegisterHoldCommand(Keys.Left, linkLeft, linkStopLeft);
            keyboardCon.RegisterHoldCommand(Keys.S, linkDown, linkStopDown);
            keyboardCon.RegisterHoldCommand(Keys.Down, linkDown, linkStopDown);
            keyboardCon.RegisterHoldCommand(Keys.D, linkRight, linkStopRight);
            keyboardCon.RegisterHoldCommand(Keys.Right, linkRight, linkStopRight);
            keyboardCon.RegisterCommand(Keys.M, new MuteBackground(g));
            keyboardCon.RegisterCommand(Keys.K, new LinkUseSelectedItem(g));
            keyboardCon.RegisterCommand(Keys.X, new LinkUseSelectedItem(g));
            keyboardCon.RegisterCommand(Keys.OemOpenBrackets, new IncItemSelect(g));
            keyboardCon.RegisterCommand(Keys.OemCloseBrackets, new DecItemSelect(g));

        }
        public void RegisterMainMenuCommands(bool mainMenu)
        {
            ICommand linkAttack = new LinkAttack(g);
            if (mainMenu)
            {
                keyboardCon.RegisterCommand(Keys.Z, new DummyCommand());
                keyboardCon.RegisterCommand(Keys.J, new DummyCommand());
                keyboardCon.RegisterCommand(Keys.D1, new StartDungeon(g, Direction.Up));
                keyboardCon.RegisterCommand(Keys.D2, new StartTower(g, Direction.Right));

            } 
            else
            {
                keyboardCon.RegisterCommand(Keys.Z, linkAttack);
                keyboardCon.RegisterCommand(Keys.J, linkAttack);
                keyboardCon.RegisterCommand(Keys.D1, new DummyCommand());
                keyboardCon.RegisterCommand(Keys.D2, new DummyCommand());
            }
        }
    }
}
