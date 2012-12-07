using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;

namespace EntityEngine.Components
{
    public class Collision : Component
    {
        public List<Entity> Partners = new List<Entity>();
        protected  List<Entity> NewPartners = new List<Entity>(); 
        public List<Entity> CollidedWith = new List<Entity>();
        public bool Colliding
        {
            get { return (CollidedWith.Count > 0); }
        }
        public event Entity.EventHandler CollideEvent;

        public Collision(Entity e) : base(e)
        {
            Entity.StateRef.EntityRemoved += RemovePartner;
            Partners = NewPartners;
        }

        public override void Update()
        {
            Partners = NewPartners.ToList();

            //Erase the collided with list every frame
            CollidedWith = new List<Entity>();
            foreach (var p in Partners)
            {
                if (TestCollision(p))
                {
                    CollidedWith.Add(p);
                    if(CollideEvent != null)
                        CollideEvent(p);
                }
            }
        }

        public override void Destroy()
        {
            Partners = new List<Entity>();
            NewPartners = new List<Entity>();
        }

        virtual public bool TestCollision(Entity e)
        {
            return (Entity.Body.BoundingBox.Intersects(e.Body.BoundingBox));
        }

        public void AddPartner(Entity e)
        {
            NewPartners.Add(e);
        }

        public void RemovePartner(Entity e)
        {
            NewPartners.Remove(e);
        }
    }
}
