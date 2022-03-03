using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class Link : ILink
{
    private static int height = 32;
    private static int width = 32;
    private LinkStateMachine stateMachine;
    private ISprite linkSprite;
    public List<IEntity> entityList { get; set; }
    private CollisionHandler collision;
    
    public Rectangle CurrentLoc { get; set; }
    public Point Center { get => CurrentLoc.Center; } // used to center projectiles, new Point(Current.X + width / 2, Position.Y + height / 2)
    public Direction Direction { get => stateMachine.CurrentDirection; }

    public Link(Point pos, List<IEntity> entityList)
    {
        stateMachine = new LinkStateMachine();
        linkSprite = LinkSpriteFactory.Instance.CreateIdleLeftLink();
        CurrentLoc = new Rectangle(0, 0, 16, 16);
        this.entityList = entityList;
        collision = new CollisionHandler(entityList, this);
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

        collision.changeRooms(entityList);
        if (stateMachine.CurrentState == LinkStateMachine.LinkActionState.Walking)
        {
            if (!collision.WillHitBlock(new Rectangle(EntityUtils.Offset(CurrentLoc.Location, Direction, speed), CurrentLoc.Size)))
            {
                CurrentLoc = new Rectangle(EntityUtils.Offset(CurrentLoc.Location, Direction, speed), CurrentLoc.Size);
            }
            
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (stateMachine.CurrentState == LinkStateMachine.LinkActionState.Attacking)
        {
            Point size;
            if (Direction == Direction.Left || Direction == Direction.Right)
            {
                size = new Point(16, 7);
            }
            else
            {
                size = new Point(7, 16);
            }
            Rectangle itemPos = new Rectangle(EntityUtils.Offset(CurrentLoc.Center, Direction, 24), size);
            ISprite sword = ItemSpriteFactory.Instance.CreateSword(Direction);
            // TODO - align sword with link's center, not his top-left.
            sword.Draw(spriteBatch, itemPos);
        }
        linkSprite.Draw(spriteBatch, CurrentLoc);
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
