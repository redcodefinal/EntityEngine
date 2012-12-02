using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;

namespace EntityEngine.Components
{
    public class Physics : Component
    {
        public float AngularVelocity;
        public Vector2 Velocity = Vector2.Zero;
        public float Drag = 1f;
        public Physics(Entity e) : base(e)
        {
        }

        public override void Update()
        {
            Velocity *= Drag;
            Entity.Body.Position += Velocity;
            Entity.Body.Angle += AngularVelocity;
        }

        public void Thrust(float power)
        {
            Velocity.X += (float)Math.Sin(-Entity.Body.Angle) * power;
            Velocity.Y += (float)Math.Cos(-Entity.Body.Angle) * power;
        }

        public void Thrust(float power, float angle)
        {
            Velocity.X += (float)Math.Sin(-angle) * power;
            Velocity.Y += (float)Math.Cos(-angle) * power;
        }
    }
}
