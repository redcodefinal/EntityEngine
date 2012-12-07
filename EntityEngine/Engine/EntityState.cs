using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Engine
{
    public class EntityState
    {
        public List<Entity> Entities { get; protected set; }
        public List<Entity> NewEntities { get; protected set; }
        public EntityGame GameRef { get; private set; }

        public event Entity.EventHandler EntityRemoved;
        public event Entity.EventHandler EntityAdded;

        public EntityState(EntityGame eg)
        {
            GameRef = eg;
            NewEntities = new List<Entity>();
            Entities = NewEntities;
        }

        public void Start()
        {
        }

        public virtual void Reset()
        {
            Entities = new List<Entity>();
            NewEntities = new List<Entity>();
        }

        public virtual void Update()
        {
            //Replace the old entities
            Entities = NewEntities.ToList();

            foreach (var e in Entities)
                e.Update();
        }

        public virtual void Draw(SpriteBatch sb)
        {
            foreach (var e in Entities)
                e.Draw(sb);
        }

        public void AddEntity(Entity entity)
        {
            //Subscribe to the destory event
            entity.DestroyEvent += RemoveEntity;
            entity.CreateEvent += AddEntity;
            NewEntities.Add(entity);
            if (EntityAdded != null)
                EntityAdded(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            //Unsubscribe from the destroy event
            NewEntities.Remove(entity);
            if (EntityRemoved != null)
                EntityRemoved(entity);
        }

        public virtual void Show()
        {
            GameRef.CurrentState = this;
        }
    }
}