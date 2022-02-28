using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    class HandleCollision
    {
        List<IEntity> levelObjects;
        List<IEntity> potentialCollision;
        IDictionary<IEntity, Direction> collisions;
        IEntity actualEntity;
        int dx;
        int dy;
        public HandleCollision(List<IEntity> levelObjects, IEntity actualEntity)
        {
            this.levelObjects = levelObjects;
            this.actualEntity = actualEntity;
            collisions = new Dictionary<IEntity, Direction>();
        }

        private void parseForProximity()
        {
            foreach (IEntity currentEntity in levelObjects)
            {
                // If entity1 starts before entity2 finishes and vice versa, you know theres an x-value that matches.
                // Easier to visualize in a picture.
                if (actualEntity.CurrentLoc.X < (currentEntity.CurrentLoc.X + currentEntity.CurrentLoc.Width)
                    && currentEntity.CurrentLoc.X < (actualEntity.CurrentLoc.X + actualEntity.CurrentLoc.Width))
                {
                    potentialCollision.Add(currentEntity);
                }
            }
        }

        private void detectCollision()
        {
            foreach (IEntity currentEntity in potentialCollision)
            {
                if (actualEntity.CurrentLoc.Y < (currentEntity.CurrentLoc.Y + currentEntity.CurrentLoc.Height)
                    && currentEntity.CurrentLoc.Y < (actualEntity.CurrentLoc.Y + actualEntity.CurrentLoc.Height))
                {
                    dx = actualEntity.CurrentLoc.X - currentEntity.CurrentLoc.X;
                    dy = actualEntity.CurrentLoc.Y - currentEntity.CurrentLoc.Y;
                    // If dx > dy, we know it's either a top or bottom collision.
                    if (Math.Abs(dx) > Math.Abs(dy))
                    {
                        // If dy is positive, the actualEntity was hit from the top.
                        // If negative, it was hit from the bottom.
                        if (dy > 0) collisions.Add(currentEntity, Direction.Up);
                        else collisions.Add(currentEntity, Direction.Down);
                    } 
                    // If dy > dx, we know it's either a left or right collision.
                    else
                    {
                        // If dx is positive, the actualEntity was hit from the left.
                        // If negative, it was hit from the right.
                        if (dx > 0) collisions.Add(currentEntity, Direction.Left);
                        else collisions.Add(currentEntity, Direction.Right);
                    }
                }
            }
        }

        public void Update()
        {
            parseForProximity();
            detectCollision();
            foreach (KeyValuePair<IEntity, Direction> collision in collisions)
            {
                Console.WriteLine("Collided with " + collision.Key.ToString() +" from the " + collision.Value.ToString() +".\n");
            }
        }
        // TODO:
        // Handle entities that can be walked through
        // Create actual reactions to the collision, right now it just detects it
        // Handle what happens when dx = dy
        // Implement this to every dynamic entity's update method
    }
}
