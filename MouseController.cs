using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Commands;

namespace ZeldaDungeon
{
    public class MouseController : IController
    {
        private MouseState state = Mouse.GetState();
        private MouseState oldState;
        private IDictionary<Rectangle, ICommand> commands;
        public MouseController()
        {
            commands = new Dictionary<Rectangle, ICommand>();
        }
        public void UpdateState()
        {
            oldState = state;
            state = Mouse.GetState();
        }
        public void ExecuteCommands()
        {
            if (state.LeftButton == ButtonState.Pressed // only do stuff on buttonpress
                && oldState.LeftButton == ButtonState.Released) {
                Point pos = state.Position;
                Point oldPos = oldState.Position;
                foreach (Rectangle r in commands.Keys)
                {
                    if (r.Contains(pos))
                    {
                        commands[r].Execute();
                    }
                }
            }
        }
        public void RegisterCommand(Rectangle r, ICommand c) // activates on left click in r
        {
            commands[r] = c;
        }

    }
}
