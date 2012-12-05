using System.Collections.Generic;
using System.Linq;
using EntityEngine.Components;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Engine
{
    public class Entity : IEntity
    {
        public delegate void EventHandler(Entity e);

        public EventHandler CreateEvent;
        public EventHandler DestroyEvent;

        public Body Body { get; protected set; }

        public Render Render { get; protected set; }

        public Physics Physics { get; protected set; }

        public Health Health { get; protected set; }

        public EntityState StateRef { get; private set; }

        public List<Entity> Targets { get; set; }

        public List<Entity> Group { get; set; }

        public List<Component> Components { get; protected set; }

        public Entity(EntityState es)
        {
            Targets = new List<Entity>();
            Group = new List<Entity>();
            Components = new List<Component>();
            StateRef = es;
        }

        virtual public void AddEntity(Entity e)
        {
            if (CreateEvent != null)
                CreateEvent(e);
        }

        virtual public void Destroy(Entity e = null)
        {
            if (DestroyEvent != null)
                DestroyEvent(this);

            foreach (var target in Targets)
                target.Targets.Remove(this);

            foreach (var entity in Group)
                entity.Group.Remove(this);
        }

        virtual public void Update()
        {
            foreach (var component in Components.Where(component => component != null))
            {
                component.Update();
            }
        }

        virtual public void Draw(SpriteBatch sb)
        {
            foreach (var component in Components.Where(component => component != null))
            {
                component.Draw(sb);
            }
        }
    }
}