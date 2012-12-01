using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using EntityEngine.Engine;

namespace EntityEngineTestBed.Objects
{
    public class Weapon : Component
    {
        public Weapon(Entity e) : base(e)
        {}

        public virtual void Fire()
        {
        }
    }
}
