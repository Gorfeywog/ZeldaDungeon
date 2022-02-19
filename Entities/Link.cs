using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class Link : ILink
{
    private LinkStateMachine stateMachine;
    private ISprite linkSprite;
    private IItem heldItem; // May be null or hold stale data; check the state to see if it's good
    public Point Position { get; private set; }
    public Direction Direction { get => stateMachine.CurrentDirection; }

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

    public void UseItem(IItem item)
    {
        this.heldItem = item;
        stateMachine.UseItem();
        item.UseOn(this);
    }

    public void Attack()
    {
        stateMachine.Attack();
    }

    public void Update()
    {
        stateMachine.Update();
        if (stateMachine.HasNewSprite)
        {
            linkSprite = stateMachine.LinkSprite(); // only get a new sprite if we need to
        }
        linkSprite.Update();
        if (stateMachine.CurrentState == LinkStateMachine.LinkActionState.Walking)
        {
            int speed = 1; // TODO - handle speed better
            Point newPos = EntityUtils.Offset(Position, Direction, speed);
            Position = newPos;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (stateMachine.CurrentState == LinkStateMachine.LinkActionState.Attacking)
        {
            Point itemPos = EntityUtils.Offset(Position, Direction, 32);
            ISprite sword = ItemSpriteFactory.Instance.CreateSword(Direction);
            // TODO - align sword with link's center, not his top-left.
            sword.Draw(spriteBatch, itemPos);
        }
        linkSprite.Draw(spriteBatch, Position);
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
