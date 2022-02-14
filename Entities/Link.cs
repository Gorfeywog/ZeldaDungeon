using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class Link : ILink
{
	private LinkStateMachine stateMachine;
	private ISprite linkSprite;
	private IItem heldItem; // May be null or hold stale data; check the state to see if it's good
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

	public void UseItem(IItem item)
    {
		this.heldItem = item;
		// put code here to make the item do a thing?
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

		// logic for link holding stuff out
		if (stateMachine.CurrentState == LinkStateMachine.LinkState.UsingItem
			|| stateMachine.CurrentState == LinkStateMachine.LinkState.Attacking)
        {
			Point itemPos = Position; // default value so compiler doesn't complain; should be replaced
			switch (stateMachine.CurrentDirection)
            {
				case Direction.Up: itemPos = new Point(Position.X, Position.Y - 32);
					break;
				case Direction.Down:
					itemPos = new Point(Position.X, Position.Y + 16);
					break;
				case Direction.Left:
					itemPos = new Point(Position.X - 32, Position.Y);
					break;
				case Direction.Right:
					itemPos = new Point(Position.X + 16, Position.Y);
					break;
			}
			if (stateMachine.CurrentState == LinkStateMachine.LinkState.UsingItem)
            {
				heldItem.CurrentPoint = itemPos;
				// yes, this moves the actual item.
				// yes, there is probably a better way to do that.
				heldItem.Draw(spriteBatch);
			}
			else
            {
				ISprite sword = ItemSpriteFactory.Instance.CreateSword(Direction);
				sword.Draw(spriteBatch, itemPos);
            }
        }
		linkSprite.Draw(spriteBatch, Position); 
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
