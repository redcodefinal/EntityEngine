using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;

namespace EntityEngine.Components
{
    public class Health : Component
    {
        public event Entity.EventHandler HurtEvent;
        public event Entity.EventHandler DiedEvent;

        public int HitPoints { get; set; }
        public bool Alive { get { return !(HitPoints <= 0); } }


        public Health(Entity e, int hp) : base(e)
        {
            HitPoints = hp;
        }

        public void Hurt(int points)
        {
            if(!Alive) return;

            HitPoints -= points;
            if (HurtEvent != null)
                HurtEvent(Entity);

            if (!Alive)
            {
                if (DiedEvent != null)
                    DiedEvent(Entity);
            }
        }

        
    }

}
