using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon
{
    public interface IController
    {
        public void UpdateState();
        public void ExecuteCommands();
    }
}
