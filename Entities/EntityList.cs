using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities.Blocks;
using ZeldaDungeon.Entities.Enemies;
using ZeldaDungeon.Rooms;

namespace ZeldaDungeon.Entities
{
    public class EntityList : IEnumerable<IEntity>
    {
        public int Count { get => entityList.Count; }
        private IList<IEntity> entityList;
        public EntityList(IList<IEntity> entityList)
        {
            this.entityList = entityList;
        }
        public EntityList()
        {
            this.entityList = new List<IEntity>();
        }

        public void Remove(IEntity entity)
        {
            entityList.Remove(entity);
        }

        public void Add(IEntity entity)
        {
            entityList.Add(entity);
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            return entityList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerable<IEnemy> Enemies()
        {
            foreach (IEntity en in entityList)
            {
                if (en is IEnemy enemy)
                {
                    yield return enemy;
                }
            }
        }
        public IEnumerable<IProjectile> Projectiles()
        {
            foreach (IEntity en in entityList)
            {
                if (en is IProjectile proj)
                {
                    yield return proj;
                }
            }
        }
        public IEnumerable<IBlock> Blocks()
        {
            foreach (IEntity en in entityList)
            {
                if (en is IBlock block)
                {
                    yield return block;
                }
            }
        }
        public IEnumerable<IPickup> Pickups()
        {
            foreach (IEntity en in entityList)
            {
                if (en is IPickup pickup)
                {
                    yield return pickup;
                }
            }
        }
        public IEnumerable<Door> Doors()
        {
            foreach (IEntity en in entityList)
            {
                if (en is Door d)
                {
                    yield return d;
                }
            }
        }
        public void UpdateList(EntityList roomEntities)
        {
            foreach (IEntity ent in roomEntities){
                if (ent is IEnemy enemy) enemy.UpdateList(roomEntities);
                if (ent is PushableBlock pb) pb.UpdateList(roomEntities);
            }
        }
    }
}