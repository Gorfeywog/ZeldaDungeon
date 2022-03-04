using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
    private const int heldItemMaxTime = 10; // how long he holds an item up
    private int heldItemCountDown;
	private LinkInventory inv { get; set; }
    private Game1 g;

    private IList<IEntity> EntityList { get => g.CurrentRoom.roomEntities; }
    private CollisionHandler collision;
    
    public Rectangle CurrentLoc { get; set; }
    public Point Center { get => CurrentLoc.Center; } // used to center projectiles, new Point(Current.X + width / 2, Position.Y + height / 2)
    public Direction Direction { get => stateMachine.CurrentDirection; }


    public Link(Point position, Game1 g)
    {
        stateMachine = new LinkStateMachine();
        linkSprite = LinkSpriteFactory.Instance.CreateIdleLeftLink();
        int width = (int)SpriteUtil.SpriteSize.LinkX;
        int height = (int)SpriteUtil.SpriteSize.LinkY;
        CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
        collision = new CollisionHandler(EntityList, this);
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
        pickup.PickUp(this); // should maybe reevaluate the naming scheme of these methods. they're kinda horrible.
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

    private int speed = 2; // this should maybe be more dynamic
    public void Update()
    {
        stateMachine.Update();
        if (stateMachine.HasNewSprite)
        {
            linkSprite = stateMachine.LinkSprite(); // only get a new sprite if we need to
        }
        linkSprite.Update();

        collision.changeRooms(EntityList);
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
            Rectangle itemPos = new Rectangle(EntityUtils.Offset(CurrentLoc.Center, Direction, 6 * SpriteUtil.SCALE_FACTOR), size);
            ISprite sword = ItemSpriteFactory.Instance.CreateSword(Direction);
            // TODO - align sword with link's center, not his top-left.
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
