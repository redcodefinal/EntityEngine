using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;

namespace EntityEngineTestBed.Objects
{
    public class AsteroidEntity : Entity
    {
        public AsteroidEntity(EntityGame eg) : base(eg)
        {
            
        }

        public override void Update()
        {
            base.Update();
            const int buffer = 20;
            if (Body.Position.X < -buffer)
                Body.Position.X = GameRef.Viewport.Right + buffer;
            else if (Body.Position.X > GameRef.Viewport.Right + buffer)
                Body.Position.X = -buffer;

            if (Body.Position.Y < -buffer)
                Body.Position.Y = GameRef.Viewport.Bottom + buffer;
            else if (Body.Position.Y > GameRef.Viewport.Bottom + buffer)
                Body.Position.Y = -buffer;
        }
    }
}
