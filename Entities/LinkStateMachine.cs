using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class LinkStateMachine
{
	// public enum LinkDirection { Left, Right, Up, Down }; // MOVED TO ZeldaDungeon.Entities.Direction
	public Direction currentDirection = Direction.Right;
	public enum LinkState { UsingItem, Damaged, Walking, Idle, Attacking };
	// should it be possible to have a Damaged, Walking link? maybe change Damaged to a boolean?
	public LinkState currentState = LinkState.Idle;

	public void ChangeDirection(Direction newDirection)
	{
		currentDirection = newDirection;
	}

	public void UseItem() 
	{
		if (currentState != LinkState.UsingItem) currentState = LinkState.UsingItem;
	}

	public void Attack()
	{
		if (currentState != LinkState.Attacking) currentState = LinkState.Attacking;
	}

	public void TakeDamage()
	{
		if (currentState != LinkState.Damaged) currentState = LinkState.Damaged;
	} 

	public void Idle()
    {
		currentState = LinkState.Idle; // TODO - shouldn't there be more logic here?
    }

	public void Walking()
    {
		currentState = LinkState.Walking; // TODO - shouldn't there be more logic here?
	}


	public void Update()
    {

    }
	public ISprite LinkSprite()
    {
		LinkSpriteFactory fac = LinkSpriteFactory.Instance;
		switch (currentState)
        {
			case LinkState.Idle:
				return currentDirection switch
					{
						Direction.Up => fac.CreateIdleUpLink(),
						Direction.Down => fac.CreateIdleDownLink(),
						Direction.Left => fac.CreateIdleLeftLink(),
						Direction.Right => fac.CreateIdleRightLink(),
						_ => throw new ArgumentOutOfRangeException() // if not a valid direction - throw an exception!
					};
			case LinkState.Walking:
				return currentDirection switch
				{
					Direction.Up => fac.CreateWalkingUpLink(),
					Direction.Down => fac.CreateWalkingDownLink(),
					Direction.Left => fac.CreateWalkingLeftLink(),
					Direction.Right => fac.CreateWalkingRightLink(),
					_ => throw new ArgumentOutOfRangeException()
				};
			case LinkState.Attacking:
				return currentDirection switch
				{
					Direction.Up => fac.CreateAttackingUpLink(),
					Direction.Down => fac.CreateAttackingDownLink(),
					Direction.Left => fac.CreateAttackingLeftLink(),
					Direction.Right => fac.CreateAttackingRightLink(),
					_ => throw new ArgumentOutOfRangeException()
				};
			case LinkState.UsingItem:
				return currentDirection switch
				{
					Direction.Up => fac.CreateUIUpLink(),
					Direction.Down => fac.CreateUIDownLink(),
					Direction.Left => fac.CreateUILeftLink(),
					Direction.Right => fac.CreateUIRightLink(),
					_ => throw new ArgumentOutOfRangeException()
				};
			case LinkState.Damaged:
				return currentDirection switch
				{
					Direction.Up => fac.CreateDamagedUpLink(),
					Direction.Down => fac.CreateDamagedDownLink(),
					Direction.Left => fac.CreateDamagedLeftLink(),
					Direction.Right => fac.CreateDamagedRightLink(),
					_ => throw new ArgumentOutOfRangeException()
				};
			default: throw new ArgumentOutOfRangeException();
		}
    }
}
