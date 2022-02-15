using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class LinkStateMachine
{
	// public enum LinkDirection { Left, Right, Up, Down }; // MOVED TO ZeldaDungeon.Entities.Direction
	public enum LinkState { UsingItem, Walking, Idle, Attacking };
	public bool Damaged { get; private set; }
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
		itemUseCountdown = itemUseDelay;
	}

	public void Attack()
	{
		if (CurrentState != LinkState.Attacking) CurrentState = LinkState.Attacking;
		itemUseCountdown = itemUseDelay;
	}

	public void TakeDamage()
	{
		Damaged = true;
		damageCountdown = damageDelay;
		HasNewSprite = true;
	} 

	public void Idle()
    {
		CurrentState = LinkState.Idle;
    }

	public void Walking()
    {
		CurrentState = LinkState.Walking; // TODO - shouldn't there be more logic here?
	}

	private static readonly int damageDelay = 80; // chosen by magic
	private int damageCountdown = 0;
	private static readonly int itemUseDelay = 20; // also used for attacks
	private int itemUseCountdown = 0;
	public void Update()
    {
		if (Damaged)
        {
			damageCountdown--;
			if (damageCountdown == 0)
            {
				Damaged = false;
				HasNewSprite = true;
            }
        }
		if (itemUseCountdown > 0)
		{
			itemUseCountdown--;
			if (itemUseCountdown == 0)
			{
				Idle();
			}
		}
	}
	public ISprite LinkSprite()
    {
		HasNewSprite = false; // just generated a sprite; it must be up to date!
		LinkSpriteFactory fac = LinkSpriteFactory.Instance;
		bool d = Damaged; // for brevity
		switch (CurrentState)
        {
			case LinkState.Idle:
				return CurrentDirection switch
					{
						Direction.Up => fac.CreateIdleUpLink(d),
						Direction.Down => fac.CreateIdleDownLink(d),
						Direction.Left => fac.CreateIdleLeftLink(d),
						Direction.Right => fac.CreateIdleRightLink(d),
						_ => throw new ArgumentOutOfRangeException() // if not a valid direction - throw an exception!
					};
			case LinkState.Walking:
				return CurrentDirection switch
				{
					Direction.Up => fac.CreateWalkingUpLink(d),
					Direction.Down => fac.CreateWalkingDownLink(d),
					Direction.Left => fac.CreateWalkingLeftLink(d),
					Direction.Right => fac.CreateWalkingRightLink(d),
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
					Direction.Up => fac.CreateUIUpLink(d),
					Direction.Down => fac.CreateUIDownLink(d),
					Direction.Left => fac.CreateUILeftLink(d),
					Direction.Right => fac.CreateUIRightLink(d),
					_ => throw new ArgumentOutOfRangeException()
				};
				/*
			case LinkState.Damaged:
				return CurrentDirection switch
				{
					Direction.Up => fac.CreateDamagedUpLink(),
					Direction.Down => fac.CreateDamagedDownLink(),
					Direction.Left => fac.CreateDamagedLeftLink(),
					Direction.Right => fac.CreateDamagedRightLink(),
					_ => throw new ArgumentOutOfRangeException()
				};
				*/ // Obsoleted! Damaged is now a modifier!
			default: throw new ArgumentOutOfRangeException();
		}
    }
}
