using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Commands;

namespace ZeldaDungeon
{
    public class KeyboardController : IController
    {
        private KeyboardState state = Keyboard.GetState();
        private KeyboardState oldState;
        private IDictionary<Keys, ICommand> regCommands;
        private IDictionary<Keys, (ICommand, ICommand)> holdCommands;
        public KeyboardController()
        {
            regCommands = new Dictionary<Keys, ICommand>();
            holdCommands = new Dictionary<Keys, (ICommand, ICommand)>();
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
                if (regCommands.ContainsKey(k) && oldState.IsKeyUp(k)) 
                {
                    regCommands[k].Execute();
                }
                if (holdCommands.ContainsKey(k) && oldState.IsKeyUp(k))
                {
                    holdCommands[k].Item1.Execute();
                }
            }
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
        public void RegisterHoldCommand(Keys k, ICommand onPress, ICommand onRelease)
        {
            holdCommands[k] = (onPress, onRelease);
        }

    }
}
