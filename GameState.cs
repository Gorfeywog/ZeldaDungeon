using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon
{
    public enum GameState
    {
        Normal, RoomTransition, PauseMenu, PauseMenuTransitionTo, PauseMenuTransitionAway, LinkDying, GameOver, WinTriforce, WinTower
    }
}
