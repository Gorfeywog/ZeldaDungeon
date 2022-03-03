﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

public class Link : ILink
{

    private LinkStateMachine stateMachine;
    private ISprite linkSprite;
    
    public Rectangle CurrentLoc { get; set; }
    public Point Center { get => CurrentLoc.Center; } // used to center projectiles, new Point(Current.X + width / 2, Position.Y + height / 2)
    public Direction Direction { get => stateMachine.CurrentDirection; }

    public Link(Point position)
    {
        stateMachine = new LinkStateMachine();
        linkSprite = LinkSpriteFactory.Instance.CreateIdleLeftLink();
        int width = (int)SpriteUtil.SpriteSize.LinkX;
        int height = (int)SpriteUtil.SpriteSize.LinkY;
        CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
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
            CurrentLoc = new Rectangle(EntityUtils.Offset(CurrentLoc.Location, Direction, speed), CurrentLoc.Size);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (stateMachine.CurrentState == LinkStateMachine.LinkActionState.Attacking)
        {
            Point size;
            int width = (int)SpriteUtil.SpriteSize.SwordWidth * SpriteUtil.SCALE_FACTOR;
            int length = (int)SpriteUtil.SpriteSize.SwordLength * SpriteUtil.SCALE_FACTOR;
            if (Direction == Direction.Left || Direction == Direction.Right)
            {
                size = new Point(length, width);
            }
            else
            {
                size = new Point(width, length);
            }
            Rectangle itemPos = new Rectangle(CurrentLoc.Center, size);
            itemPos.Offset(CurrentLoc.Center - itemPos.Center);
            itemPos.Location = EntityUtils.Offset(itemPos.Location, Direction, 12 * SpriteUtil.SCALE_FACTOR);
            ISprite sword = ItemSpriteFactory.Instance.CreateSword(Direction);

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
