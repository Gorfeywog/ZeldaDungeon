using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class Link : ILink
{
    private static int height = 32;
    private static int width = 32;
    private LinkStateMachine stateMachine;
    private ISprite linkSprite;
    public Point Position { get; private set; }
    public Point Center { get => new Point(Position.X + width / 2, Position.Y + height / 2); } // used to center projectiles
    public Direction Direction { get => stateMachine.CurrentDirection; }

    public Link(Point pos)
    {
        stateMachine = new LinkStateMachine();
        linkSprite = LinkSpriteFactory.Instance.CreateIdleLeftLink();
        Position = pos;
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
        stateMachine.UseItem();
        item.UseOn(this);
    }

    public void Attack()
    {
        stateMachine.Attack();
    }

    private int speed = 2; // this should maybe be more dynamic
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
            Position = EntityUtils.Offset(Position, Direction, speed);
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
    public void DespawnEffect() { }
    public bool ReadyToDespawn => false;
}
