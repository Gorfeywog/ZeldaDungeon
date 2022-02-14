using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class Link : ILink
{
	private LinkStateMachine stateMachine;
	private ISprite linkSprite;
	private Point position;

	public Link()
	{
		stateMachine = new LinkStateMachine();
		linkSprite = LinkSpriteFactory.Instance.CreateIdleLeftLink();
	}
	public void ChangeDirection(Direction nextDirection)
    {
		stateMachine.ChangeDirection(nextDirection);
    }

	public void TakeDamage()
    {
		stateMachine.TakeDamage();
    }

	public void UseItem()
    {
		stateMachine.UseItem();
    }

	public void Attack()
    {
		stateMachine.Attack();
    }

	public void Update()
	{
		stateMachine.Update();
		linkSprite = stateMachine.LinkSprite(); // TODO - make this not break animations!
		linkSprite.Update();
		if (stateMachine.currentState == LinkStateMachine.LinkState.Walking)
        {
			// move forward
			int speed = 1; // TODO - handle speed better
			Point newPos = stateMachine.currentDirection switch
			{
				Direction.Up => new Point(position.X, position.Y - speed),
				Direction.Down => new Point(position.X, position.Y + speed),
				Direction.Left => new Point(position.X - speed, position.Y),
				Direction.Right => new Point(position.X + speed, position.Y),
				_ => throw new ArgumentException() // bad direction!
			};
			position = newPos;
        }
	}

	public void Draw(SpriteBatch spriteBatch)
    {
		linkSprite.Draw(spriteBatch, position); 
		// this is an issue since some sprites have the top left somewhere other than Link's top left,
		// in particular the sword swing left. Best solution is probably to add code in such sprite classes, but
		// that will be easier to approach once we have a little more to the game
    }

    public void StartWalking()
    {
		stateMachine.Walking();
    }

    public void StopWalking()
    {
		stateMachine.Idle();
    }
}
