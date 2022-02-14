using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class Link : ILink
{
	private LinkStateMachine stateMachine;
	private ISprite linkSprite; // this definitely needs to interact with the state machine somehow
	private Point position;

	public Link()
	{
		stateMachine = new LinkStateMachine();
		linkSprite = LinkSpriteFactory.Instance.CreateIdleLeftLink();
	}
	public void ChangeDirection(Direction nextDirection)
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
		linkSprite.Update();
		stateMachine.Update();
	}

	public void Draw(SpriteBatch spriteBatch)
    {
		linkSprite.Draw(spriteBatch, position); 
		// this is an issue since some sprites have the top left somewhere other than Link's top left,
		// in particular the sword swing left. Best solution is probably to add code in such sprite classes, but
		// that will be easier to approach once we have a little more to the game
    }

    public void StartWalking()
    {
		stateMachine.Walking();
    }

    public void StopWalking()
    {
		stateMachine.Idle();
    }
}
