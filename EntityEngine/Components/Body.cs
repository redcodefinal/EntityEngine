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
            if (Entity.Render.DrawRect.Intersects(entity.Render.DrawRect))
            {
                //Get the area of intersection
                var intersection = new Rectangle();
                intersection.Y = Math.Max(Entity.Render.DrawRect.Top, entity.Render.DrawRect.Top);
                intersection.Height = Math.Min(Entity.Render.DrawRect.Bottom, entity.Render.DrawRect.Bottom) - intersection.Y;
                intersection.X = Math.Max(Entity.Render.DrawRect.Left, entity.Render.DrawRect.Left);
                intersection.Width = Math.Min(Entity.Render.DrawRect.Right, entity.Render.DrawRect.Right) - intersection.X;

                for (var y = intersection.Y; y < intersection.Bottom; y++)
                {
                    for (var x = intersection.X; x < intersection.Right; x++)
                    {
                        var color1 = Entity.Render.ColorData[(x - Entity.Render.DrawRect.Left) + (y - Entity.Render.DrawRect.Top) * Entity.Render.DrawRect.Width];
                        var color2 = entity.Render.ColorData[(x - entity.Render.DrawRect.Left) + (y - entity.Render.DrawRect.Top) * entity.Render.DrawRect.Width];

                        if (color1.A != 0 && color2.A != 0)
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
