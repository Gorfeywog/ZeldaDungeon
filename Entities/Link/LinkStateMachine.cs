using System;
using System.Diagnostics;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Link
{
	public class LinkStateMachine
	{
		public enum LinkActionState { PickingUp, UsingItem, Walking, Idle, Attacking };
		private bool damaged;
		public bool Damaged
		{
			get => damaged;
			private set
            {
				HasNewSprite = (value != damaged);
				damaged = value;
            }
		}
		public bool FullHealth { get => CurrentHealth == MaxHealth; }
		public int CurrentHealth { get; private set; } // measured in *half hearts*
		public int MaxHealth { get; private set; } // measured in *half hearts*
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
				HasNewSprite = true;
				currentState = value;
			}
		}
		public bool HasNewSprite { get; private set; } // used to avoid generating new sprite every frame


		public LinkStateMachine()
		{
			CurrentState = LinkActionState.Idle;
			currentDirection = Direction.Right;
			HasNewSprite = true;
			MaxHealth = SpriteUtil.LINK_MAX_HEALTH;
			CurrentHealth = MaxHealth;
		}

		public void ChangeDirection(Direction newDirection)
		{
			CurrentDirection = newDirection;
		}

		public void UseItem()
		{
			if (CurrentState != LinkActionState.UsingItem) CurrentState = LinkActionState.UsingItem;
			itemUseCountdown = itemUseDelay;
		}
		public void PickUp()
		{
			if (CurrentState != LinkActionState.PickingUp) CurrentState = LinkActionState.PickingUp;
			itemUseCountdown = SpriteUtil.LINK_PICKUP_TIME;
		}

		public void Attack()
		{
			if (CurrentState != LinkActionState.Attacking) CurrentState = LinkActionState.Attacking;
			itemUseCountdown = itemUseDelay;
		}

		public void TakeDamage()
		{
			if (damageCountdown == 0)
            {
				CurrentHealth--;
				damageCountdown = damageDelay;
				Damaged = true;
			}
			if (CurrentHealth == 0)
			{
				Debug.WriteLine("You Died!");
				SoundManager.Instance.PlaySound("RupeesDecreasing");
			}
		}

		public void Idle()
		{
			if (CurrentState == LinkActionState.Walking)
			{
				CurrentState = LinkActionState.Idle;
			}
		}

		public void Walking()
		{
			if (CurrentState == LinkActionState.Idle)
			{
				CurrentState = LinkActionState.Walking;
			}
		}

		private static readonly int damageDelay = 80;
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
				}
			}
			if (itemUseCountdown > 0)
			{
				itemUseCountdown--;
				if (itemUseCountdown == 0)
				{
					CurrentState = LinkActionState.Idle; // ignores current state, unlike Idle()
				}
			}
		}
		public ISprite LinkSprite()
		{
			HasNewSprite = false;
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
				case LinkActionState.UsingItem:
					return CurrentDirection switch
					{
						Direction.Up => fac.CreateUIUpLink(d),
						Direction.Down => fac.CreateUIDownLink(d),
						Direction.Left => fac.CreateUILeftLink(d),
						Direction.Right => fac.CreateUIRightLink(d),
						_ => throw new ArgumentOutOfRangeException()
					};
				case LinkActionState.PickingUp:
					return fac.CreatePickupLink(d);
				default: throw new ArgumentOutOfRangeException();
			}
		}
	}
}
