﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon
{
    public enum GameState
    {
        Normal, RoomTransition, PauseMenu, PauseMenuTransitionTo, PauseMenuTransitionAway, LinkDying, GameOver, WinTriforce, WinTower
        // TODO - implement states for main menu and gameover?
    }
}
