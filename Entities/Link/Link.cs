using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities.Pickups;
using ZeldaDungeon.Entities.Projectiles;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Rooms;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Link
{
    public class Link : ILink
    {

        private LinkStateMachine stateMachine;
        private ISprite linkSprite;
        private IPickup heldItem; // null if he has never held an item up, may hold stale data
                                  // note that it's a pickup and not a real item
        private LinkInventory inv { get; set; }
        private Game1 g;
        public Room CurrentRoom { get => g.CurrentRoom; }

        private CollisionHandler collision;
        private SoundManager sound;
        public CollisionHeight Height { get => CollisionHeight.Normal; }
        public DrawLayer Layer { get => DrawLayer.Normal; }
        public Rectangle CurrentLoc { get; set; }
        public Point Center { get => CurrentLoc.Center; }
        public Direction Direction { get => stateMachine.CurrentDirection; }
        public int CurrentHealth { get => stateMachine.CurrentHealth; }
        public int MaxHealth { get => stateMachine.MaxHealth; }


        public Link(Point position, Game1 g)
        {
            this.g = g;
            inv = new LinkInventory();
            stateMachine = new LinkStateMachine();
            linkSprite = LinkSpriteFactory.Instance.CreateIdleLeftLink();
            int width = (int)SpriteUtil.SpriteSize.LinkX;
            int height = (int)SpriteUtil.SpriteSize.LinkY;
            CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
            sound = SoundManager.Instance;
            collision = new CollisionHandler(CurrentRoom, this);
        }

        public void ChangeRoom(Room r)
        {
            collision = new CollisionHandler(r, this);
        }

        public void ChangeDirection(Direction nextDirection)
        {
            stateMachine.ChangeDirection(nextDirection);
        }

        public void TakeDamage(int amt = 1)
        {
            if (!stateMachine.Damaged)
            {
                sound.PlaySound("PlayerHurt");
                stateMachine.TakeDamage(amt);
            }
        }

        public void Heal(int amt = 1) => stateMachine.Heal(amt);
        public void Heal() => stateMachine.Heal();
        public void UseHeartContainer() => stateMachine.UseHeartContainer();

        public void PickUp(IPickup pickup)
        {
            if (pickup.HoldsUp)
            {
                stateMachine.PickUp();
                heldItem = pickup;
                if (pickup is TriforcePiecePickup)
                {
                    sound.PlaySound("TriforcePieceObtained");
                }
                else
                {
                    sound.PlaySound("ItemReceived");
                }
            }
            sound.PlaySound("ItemObtained");
            pickup.PickUp(this);
        }
        public bool CanPickUp() => stateMachine.CurrentState == LinkStateMachine.LinkActionState.Idle
            || stateMachine.CurrentState == LinkStateMachine.LinkActionState.Walking;
        public void AddItem(IItem item, int quantity = 1)
        {
            inv.AddItem(item, quantity);

        }

        public void UseItem(IItem item)
        {
            if (inv.HasItem(item))
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

        public LinkInventory GetInv()
        {
            return inv;
        }


        public void Attack()
        {
            bool success = stateMachine.Attack();
            if (success)
            {
                // TODO - make both of these spawn in a better centered way 
                g.CurrentRoom.RegisterEntity(new StaticSword(Center, Direction, g));
                if (stateMachine.FullHealth)
                {
                    g.CurrentRoom.RegisterEntity(new ThrownSword(Center, Direction, g));
                }
            }

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
            collision.SpecialTriggerUpdate();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            /*
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
            */
            if (stateMachine.CurrentState == LinkStateMachine.LinkActionState.PickingUp)
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
}
