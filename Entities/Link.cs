using Microsoft.Xna.Framework.Graphics;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class Link : ILink
{
	private LinkStateMachine stateMachine;
	private ISprite linkSprite;

	public Link()
	{
		stateMachine = new LinkStateMachine();
		linkSprite = LinkSpriteFactory.Instance.CreateIdleLeftLink();
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

	public void Draw(SpriteBatch spriteBatch)
    {
		linkSprite.Draw(spriteBatch);
    }
}
