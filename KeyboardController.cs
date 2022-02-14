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
        private KeyboardState oldState;
        private IDictionary<Keys, ICommand> regCommands;
        private IDictionary<Keys, Tuple<ICommand, ICommand>> holdCommands;
        public KeyboardController()
        {
            UpdateState(); // don't let state be null
            regCommands = new Dictionary<Keys, ICommand>();
        }
        public void UpdateState()
        {
            if (state == null)
            {
                oldState = Keyboard.GetState();
            }
            else
            {
                oldState = state;
            }
            state = Keyboard.GetState();
        }
        public void ExecuteCommands()
        {
            foreach (Keys k in state.GetPressedKeys())
            {
                // handle traditional commands
                if (regCommands.ContainsKey(k) && oldState.IsKeyUp(k)) // check old state so we only press it once
                {
                    regCommands[k].Execute();
                }
                // handle hold commands being pressed
                if (holdCommands.ContainsKey(k) && oldState.IsKeyUp(k))
                {
                    holdCommands[k].Item1.Execute();
                }
            }
            // check for released hold commands
            foreach (Keys k in holdCommands.Keys)
            {
                if (oldState.IsKeyDown(k) && state.IsKeyUp(k))
                {
                    holdCommands[k].Item2.Execute();
                }
            }
        }
        public void RegisterCommand(Keys k, ICommand c)
        {
            regCommands[k] = c;
        }
        public void RegisterHoldCommand(Keys c, ICommand onPress, ICommand onRelease)
        {

        }

    }
}
