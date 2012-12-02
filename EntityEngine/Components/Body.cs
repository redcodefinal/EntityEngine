using System;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;

namespace EntityEngine.Components
{
    public class Body : Component
    {
        public Vector2 Position;
        public Vector2 Bounds;
        public float Angle;

        public Rectangle BoundingBox { get {return new Rectangle((int) Position.X, (int) Position.Y, 
            (int)( Bounds.X * Entity.Render.Scale), (int)(Bounds.Y * Entity.Render.Scale));}}

        public Body(Entity e, Vector2 position, Vector2 bounds) : base(e)
        {
            Position = position;
            Bounds = bounds;
        }

        public bool TestCollision(Entity entity)
        {
            //Distance Formula
            var dotproduct = Math.Abs(
                Math.Sqrt(
                Math.Pow(Position.X - entity.Body.Position.X, 2) +
                Math.Pow(Position.Y - entity.Body.Position.Y, 2)));
            return (dotproduct <= Bounds.X / 2 + entity.Body.Bounds.X / 2);
        }
    }
}
