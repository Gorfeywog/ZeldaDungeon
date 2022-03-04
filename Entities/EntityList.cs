using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities.Enemies;

namespace ZeldaDungeon.Entities
{
    public class EntityList : IEnumerable<IEntity>
    {
        IList<IEntity> entityList;
        public EntityList(IList<IEntity> entityList)
        {
            this.entityList = entityList;
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

        public void UpdateList(EntityList roomEntities)
        {
            foreach (IEntity ent in roomEntities){
                if (ent is IEnemy enemy) enemy.UpdateList(roomEntities);
            }
        }
    }
}