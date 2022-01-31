using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Commands;

namespace ZeldaDungeon
{
    public class KeyboardController : IController
    {
        private KeyboardState state;
        private IDictionary<Keys, ICommand> commands;
        public KeyboardController()
        {
            UpdateState(); // don't let state be null
            commands = new Dictionary<Keys, ICommand>();
        }
        public void UpdateState()
        {
            state = Keyboard.GetState();
        }
        public void ExecuteCommands()
        {
            foreach (Keys k in state.GetPressedKeys())
            {
                if (commands.ContainsKey(k))
                {
                    commands[k].Execute();
                }
            }
        }
        public void RegisterCommand(Keys k, ICommand c)
        {
            commands[k] = c;
        }

    }
}
