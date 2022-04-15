using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class DummyCommand : ICommand
    {
        // when you need an ICommand as a placeholder
        public DummyCommand() { }
        public void Execute() { }
    }
}
