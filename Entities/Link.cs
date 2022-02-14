using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class Link : ILink
{
	private LinkStateMachine stateMachine;
	private ISprite linkSprite;
	public Point Position { get; private set; }
	public Direction Direction { get => stateMachine.CurrentDirection;  }

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
		if (stateMachine.HasNewSprite)
		{
			linkSprite = stateMachine.LinkSprite(); // only get a new sprite if we need to
		}
		linkSprite.Update();
		if (stateMachine.CurrentState == LinkStateMachine.LinkState.Walking)
        {
			// move forward
			int speed = 1; // TODO - handle speed better
			Point newPos = stateMachine.CurrentDirection switch
			{
				Direction.Up => new Point(Position.X, Position.Y - speed),
				Direction.Down => new Point(Position.X, Position.Y + speed),
				Direction.Left => new Point(Position.X - speed, Position.Y),
				Direction.Right => new Point(Position.X + speed, Position.Y),
				_ => throw new ArgumentException() // bad direction!
			};
			Position = newPos;
        }
	}

	public void Draw(SpriteBatch spriteBatch)
    {
		linkSprite.Draw(spriteBatch, Position); 
		// this is an issue since some sprites have the top left somewhere other than Link's top left,
		// in particular the sword swing left. Best solution is probably to replace those with
		// overlapped sprites, or something to that effect.
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
