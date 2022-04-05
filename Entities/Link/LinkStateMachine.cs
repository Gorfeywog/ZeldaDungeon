using System;
using System.Collections;
using System.Collections.Generic;
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
		private static readonly int maxMaxHealth = 20; // can only fit so much health on the screen
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
		private bool CanInterrupt { get => currentState == LinkActionState.Idle || currentState == LinkActionState.Walking; }


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

		public bool UseItem()
		{
			if (CanInterrupt)
			{
				CurrentState = LinkActionState.UsingItem;
				itemUseCountdown = itemUseDelay;
				return true;
			}
			return false;
		}
		public bool PickUp()
		{
			if (CanInterrupt)
			{
				CurrentState = LinkActionState.PickingUp;
				itemUseCountdown = SpriteUtil.LINK_PICKUP_TIME;
				return true;
			}
			return false;
		}

		public bool Attack()
		{
			if (CanInterrupt)
			{
				CurrentState = LinkActionState.Attacking;
				itemUseCountdown = itemUseDelay;
				return true;
			}
			return false;
		}

		public void TakeDamage(int amt = 1)
		{
			if (damageCountdown == 0)
            {
				CurrentHealth -= amt;
				damageCountdown = damageDelay;
				Damaged = true;
			}
			if (CurrentHealth <= 0)
			{
				Debug.WriteLine("You Died!");
				SoundManager.Instance.PlaySound("RupeesDecreasing");
				// TODO - game over screen happens here
			}
		}
		public void Heal(int amt)
        {
			CurrentHealth = Math.Min(CurrentHealth + amt, MaxHealth);
        }
		public void Heal() => Heal(MaxHealth);
		public void UseHeartContainer()
        {
			MaxHealth = Math.Min(MaxHealth + 2, maxMaxHealth);
			Heal(); // heart container confers a full heal
        }

		public bool Idle()
		{
			if (CanInterrupt)
			{
				CurrentState = LinkActionState.Idle;
				return true;
			}
			return false;
		}

		public bool Walking()
		{
			if (CanInterrupt)
			{
				CurrentState = LinkActionState.Walking;
				return true;
			}
			return false;
		}

		private static readonly int damageDelay = 80;
		private int damageCountdown = 0;
		public static readonly int itemUseDelay = 20; // also used for attacks
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
