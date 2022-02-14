using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class LinkStateMachine
{
	// public enum LinkDirection { Left, Right, Up, Down }; // MOVED TO ZeldaDungeon.Entities.Direction
	public enum LinkState { UsingItem, Damaged, Walking, Idle, Attacking };
	// should it be possible to have a Damaged, Walking link? maybe change Damaged to a boolean?
	// would complicate spriting a bit, but it would not be too hard to handle that from a sprite side
	private Direction currentDirection;
	public Direction CurrentDirection
    {
		get
        {
			return currentDirection;
        }
		private set
		{
			HasNewSprite = true;
			currentDirection = value;
        }
    }
	private LinkState currentState;
	public LinkState CurrentState
    {
		get
        {
			return currentState;
        }
		private set
        {
			HasNewSprite = true; // new state usually means new sprite
			currentState = value;
        }
    }
	public bool HasNewSprite { get; private set; } // used to avoid generating new sprite every frame


	public LinkStateMachine()
    {
		CurrentState = LinkState.Idle;
		currentDirection = Direction.Right;
		HasNewSprite = true;
    }

	public void ChangeDirection(Direction newDirection)
	{
		CurrentDirection = newDirection;
	}

	public void UseItem() 
	{
		if (CurrentState != LinkState.UsingItem) CurrentState = LinkState.UsingItem;
	}

	public void Attack()
	{
		if (CurrentState != LinkState.Attacking) CurrentState = LinkState.Attacking;
	}

	public void TakeDamage()
	{
		if (CurrentState != LinkState.Damaged) CurrentState = LinkState.Damaged;
	} 

	public void Idle()
    {
		CurrentState = LinkState.Idle; // TODO - shouldn't there be more logic here?
    }

	public void Walking()
    {
		CurrentState = LinkState.Walking; // TODO - shouldn't there be more logic here?
	}


	public void Update()
    {

    }
	public ISprite LinkSprite()
    {
		HasNewSprite = false; // just generated a sprite; it must be up to date!
		LinkSpriteFactory fac = LinkSpriteFactory.Instance;
		switch (CurrentState)
        {
			case LinkState.Idle:
				return CurrentDirection switch
					{
						Direction.Up => fac.CreateIdleUpLink(),
						Direction.Down => fac.CreateIdleDownLink(),
						Direction.Left => fac.CreateIdleLeftLink(),
						Direction.Right => fac.CreateIdleRightLink(),
						_ => throw new ArgumentOutOfRangeException() // if not a valid direction - throw an exception!
					};
			case LinkState.Walking:
				return CurrentDirection switch
				{
					Direction.Up => fac.CreateWalkingUpLink(),
					Direction.Down => fac.CreateWalkingDownLink(),
					Direction.Left => fac.CreateWalkingLeftLink(),
					Direction.Right => fac.CreateWalkingRightLink(),
					_ => throw new ArgumentOutOfRangeException()
				};
			case LinkState.Attacking:
				/*
				return CurrentDirection switch
				{
					Direction.Up => fac.CreateAttackingUpLink(),
					Direction.Down => fac.CreateAttackingDownLink(),
					Direction.Left => fac.CreateAttackingLeftLink(),
					Direction.Right => fac.CreateAttackingRightLink(),
					_ => throw new ArgumentOutOfRangeException()
				};
				*/ // those sprites are obseleted! attacking is like using an item! fallthrough!
			case LinkState.UsingItem:
				return CurrentDirection switch
				{
					Direction.Up => fac.CreateUIUpLink(),
					Direction.Down => fac.CreateUIDownLink(),
					Direction.Left => fac.CreateUILeftLink(),
					Direction.Right => fac.CreateUIRightLink(),
					_ => throw new ArgumentOutOfRangeException()
				};
			case LinkState.Damaged:
				return CurrentDirection switch
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
