using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class LinkStateMachine
{
	public enum LinkActionState { UsingPickup, Walking, Idle, Attacking };
	public bool Damaged { get; private set; }
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
	private LinkActionState currentState;
	public LinkActionState CurrentState
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
		CurrentState = LinkActionState.Idle;
		currentDirection = Direction.Right;
		HasNewSprite = true;
    }

	public void ChangeDirection(Direction newDirection)
	{
		CurrentDirection = newDirection;
	}

	public void UsePickup() 
	{
		if (CurrentState != LinkActionState.UsingPickup) CurrentState = LinkActionState.UsingPickup;
		itemUseCountdown = itemUseDelay;
	}

	public void Attack()
	{
		if (CurrentState != LinkActionState.Attacking) CurrentState = LinkActionState.Attacking;
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
		CurrentState = LinkActionState.Idle;
    }

	public void Walking()
    {
		CurrentState = LinkActionState.Walking;
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
		bool d = Damaged; 
		switch (CurrentState)
        {
			case LinkActionState.Idle:
				return CurrentDirection switch
					{
						Direction.Up => fac.CreateIdleUpLink(d),
						Direction.Down => fac.CreateIdleDownLink(d),
						Direction.Left => fac.CreateIdleLeftLink(d),
						Direction.Right => fac.CreateIdleRightLink(d),
						_ => throw new ArgumentOutOfRangeException()
					};
			case LinkActionState.Walking:
				return CurrentDirection switch
				{
					Direction.Up => fac.CreateWalkingUpLink(d),
					Direction.Down => fac.CreateWalkingDownLink(d),
					Direction.Left => fac.CreateWalkingLeftLink(d),
					Direction.Right => fac.CreateWalkingRightLink(d),
					_ => throw new ArgumentOutOfRangeException()
				};
			case LinkActionState.Attacking:
			case LinkActionState.UsingPickup:
				return CurrentDirection switch
				{
					Direction.Up => fac.CreateUIUpLink(d),
					Direction.Down => fac.CreateUIDownLink(d),
					Direction.Left => fac.CreateUILeftLink(d),
					Direction.Right => fac.CreateUIRightLink(d),
					_ => throw new ArgumentOutOfRangeException()
				};
			default: throw new ArgumentOutOfRangeException();
		}
    }
}
