﻿using System.Collections.Generic;
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
        public Collision Collision { get; protected set; }
        public EntityState StateRef { get; private set; }

        public List<IComponent> Components { get; protected set; }

        public string Name;

        public Entity(EntityState es)
        {
            StateRef = es;
            Components = new List<IComponent>();
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
            foreach (var component in Components.ToList())
            {
                component.Destroy();
            }
        }

        virtual public void Update()
        {
            foreach (var component in Components.ToList().Where(component => component != null))
            {
                component.Update();
            }
        }

        virtual public void Draw(SpriteBatch sb)
        {
            foreach (var component in Components.ToList().Where(component => component != null))
            {
                component.Draw(sb);
            }
        }
    }
}