using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    class CollisionHandler
    {
        List<IEntity> LevelObjects;
        IDictionary<IEntity, Direction> Collisions;
        IEntity ActualEntity;
        int dx, dy;

        public CollisionHandler(List<IEntity> LevelObjects, IEntity ActualEntity)
        {
            this.LevelObjects = LevelObjects;
            this.ActualEntity = ActualEntity;
            Collisions = new Dictionary<IEntity, Direction>();
        }

        private void HandleCollision(ILink player, KeyValuePair<IEntity, Direction> collision, IEnemy type)
        {
            // Link takes damage
        }

        private void HandleCollision(ILink player, KeyValuePair<IEntity, Direction> collision, IBlock type)
        {
            // Stop movement
        }

        private void HandleCollision(ILink player, KeyValuePair<IEntity, Direction> collision, IItem type)
        {
            // if the item is on the floor, pick up the item.
        }
        private void HandleCollision(IEnemy enemy, KeyValuePair<IEntity, Direction> collision, IBlock type)
        {
            // Stop movement
        }

        private void HandleCollision(IEnemy enemy, KeyValuePair<IEntity, Direction> collision, IItem type)
        {
            // If the item was thrown by Link, take damage. Knockback??
        }

        private void HandleCollision(IBlock PushedBlock, KeyValuePair<IEntity, Direction> collision, IBlock type)
        {
            // Stop movement
        }





        private void HandleCollision(IItem item, KeyValuePair<IEntity, Direction> collision, IBlock type)
        {
            // Basically only for boomerangs, stop the movement early and return.
        }

        private void DetectCollision()
        {
            foreach (IEntity CurrentEntity in LevelObjects)
            {
                // If entity1 starts before entity2 finishes and vice versa, you know theres an x-value that matches. If the same thing happens with the y-values, there is collision.
                // Easier to visualize in a picture. Also idk if it matters to do this big if statement or boolean variables.
                if (ActualEntity.CurrentLoc.X < (CurrentEntity.CurrentLoc.X + CurrentEntity.CurrentLoc.Width)
                    && CurrentEntity.CurrentLoc.X < (ActualEntity.CurrentLoc.X + ActualEntity.CurrentLoc.Width)
                    && ActualEntity.CurrentLoc.Y < (CurrentEntity.CurrentLoc.Y + CurrentEntity.CurrentLoc.Height)
                    && CurrentEntity.CurrentLoc.Y < (ActualEntity.CurrentLoc.Y + ActualEntity.CurrentLoc.Height))
                {
                    Collisions.Add(CurrentEntity, DetectDirection(CurrentEntity));
                }
            }
        }

        private Direction DetectDirection(IEntity CurrentEntity)
        {
            dx = ActualEntity.CurrentLoc.X - CurrentEntity.CurrentLoc.X;
            dy = ActualEntity.CurrentLoc.Y - CurrentEntity.CurrentLoc.Y;
            // If dx > dy, we know it's either a top or bottom collision.
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                // If dy is positive, the actualEntity was hit from the top.
                // If negative, it was hit from the bottom.
                if (dy > 0) return Direction.Up;
                else return Direction.Down;
            } 
            // If dy > dx, we know it's either a left or right collision.
            else
            {
                // If dx is positive, the actualEntity was hit from the left.
                // If negative, it was hit from the right.
                if (dx > 0) return Direction.Left;
                else return Direction.Right;
            }
        }

        // TODO:
        // Handle entities that can be walked through
        // Create an actual collision handler, rename this
        // Handle what happens when dx = dy
        // Implement this to every dynamic entity's update method

        // NOTE:
        // Haven't been able to test it but I think detection is implemented correctly. I wanted to 
        // discuss how you guys want to go about implementing the actual handler. Also, I feel like
        // I should just make this class a utility class for the handler and take out the constructor.

        public void Update()
        {
            DetectCollision();
            foreach (KeyValuePair<IEntity, Direction> Collision in Collisions){
               // HandleCollision(Collision.Key.GetType, )
            }
        }


    }
}
