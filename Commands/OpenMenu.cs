using System;

public class OpenMenu
{
    private Game1 g;
    public OpenMenu(Game1 g)
    {
        this.g = g;
    }

    public void Execute()
    {
        g.PauseMenu();
    }
}
