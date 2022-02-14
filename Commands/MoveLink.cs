﻿using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Commands
{
    public class MoveLink : ICommand
    {
        private ILink link;
        private Direction dir;
        public MoveLink(ILink link, Direction dir)
        {
            this.link = link;
            this.dir = dir;
        }

        public void Execute()
        {
            link.ChangeDirection(dir);
            link.StartWalking();
        }
    }
}
