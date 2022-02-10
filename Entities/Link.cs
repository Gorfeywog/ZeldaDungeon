using System;
using ZeldaDungeon.Entities;

public class Link : ILink
{
	private LinkStateMachine stateMachine;

	public Link()
	{
		stateMachine = new LinkStateMachine();
	}
	public void ChangeDirection(LinkStateMachine.LinkDirection nextDirection)
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
	}

    public void Draw()
    {
        throw new NotImplementedException();
    }

    public void UpdateSprite()
    {
        throw new NotImplementedException();
    }
}
