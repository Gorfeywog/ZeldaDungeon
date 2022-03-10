using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ZeldaDungeon;
using ZeldaDungeon.Entities;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Sprites;

public class Link : ILink
{

    private LinkStateMachine stateMachine;
    private ISprite linkSprite;
	private IPickup heldItem; // null if he has never held an item up, may hold stale data
                              // note that it's a pickup and not a real item
	private LinkInventory inv { get; set; }
   // private Game1 g;

    public EntityList roomEntities { get; set; }
    private CollisionHandler collision;
    public CollisionHeight Height { get => CollisionHeight.Normal; }
    public Rectangle CurrentLoc { get; set; }
    public Point Center { get => CurrentLoc.Center; }
    public Direction Direction { get => stateMachine.CurrentDirection; }
    

    public Link(Point position, EntityList roomEntities)
    {
       // this.g = g;
        inv = new LinkInventory();
        stateMachine = new LinkStateMachine();
        linkSprite = LinkSpriteFactory.Instance.CreateIdleLeftLink();
        int width = (int)SpriteUtil.SpriteSize.LinkX;
        int height = (int)SpriteUtil.SpriteSize.LinkY;
        CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
        this.roomEntities = roomEntities;
        collision = new CollisionHandler(this.roomEntities, this);
    }

    public void UpdateList(EntityList roomEntities)
    {
        this.roomEntities = roomEntities;
        collision.ChangeRooms(roomEntities);
    }

    public void ChangeDirection(Direction nextDirection)
    {
        stateMachine.ChangeDirection(nextDirection);
    }

    public void TakeDamage()
    {
        stateMachine.TakeDamage();
    }

    public void PickUp(IPickup pickup)
    {
        if (pickup.HoldsUp)
        {
            stateMachine.PickUp();
            heldItem = pickup;
        }
        pickup.PickUp(this); 
    }
    public bool CanPickUp() => stateMachine.CurrentState == LinkStateMachine.LinkActionState.Idle
        || stateMachine.CurrentState == LinkStateMachine.LinkActionState.Walking;
    public void AddItem(IItem item)
    {
        inv.AddItem(item);	
    }

    public void UseItem(IItem item)
    {
        if (inv.HasItem(item)) // check if the item is ready to use? (like, arrows need a bow, etc.)
        {
            stateMachine.UseItem();
            inv.UseItem(item);
            item.UseOn(this);
        }
    }

    public bool HasItem(IItem item)
    {
        return inv.HasItem(item);
    }


    public void Attack()
    {
        stateMachine.Attack();
    }

    private int speed = SpriteUtil.SCALE_FACTOR; // this should maybe be more dynamic
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
            Rectangle newPos = new Rectangle(EntityUtils.Offset(CurrentLoc.Location, Direction, speed), CurrentLoc.Size);
            if (!collision.WillHitBlock(newPos)) CurrentLoc = newPos;
            
        }
        collision.Update();
        collision.TrapUpdate();
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
            int itemOffset = 12 * SpriteUtil.SCALE_FACTOR;
            itemPos.Location = EntityUtils.Offset(itemPos.Location, Direction, itemOffset);
            ISprite sword = ItemSpriteFactory.Instance.CreateSword(Direction);

            sword.Draw(spriteBatch, itemPos);
        }
		else if (stateMachine.CurrentState == LinkStateMachine.LinkActionState.PickingUp)
        {
            int height = heldItem.CurrentLoc.Height;
            Point destPoint = new Point(CurrentLoc.X, CurrentLoc.Y - height);
            Rectangle oldPos = heldItem.CurrentLoc;
            oldPos.X = CurrentLoc.X;
            oldPos.Y = CurrentLoc.Y - height;
            heldItem.CurrentLoc = oldPos;
            heldItem.Draw(spriteBatch);
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
