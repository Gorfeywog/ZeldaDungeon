using System;
using ZeldaDungeon.Entities;

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
}
