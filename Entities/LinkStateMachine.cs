using System;

public class LinkStateMachine
{
	public enum LinkDirection { Left, Right, Up, Down };
	public LinkDirection currentDirection = LinkDirection.Right;
	public enum LinkState { UsingItem, Damaged, Walking, Idle, Attacking };
	public LinkState currentState = LinkState.Idle;

	public void ChangeDirection(LinkDirection newDirection)
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

	public void Update()
    {

    }
}
