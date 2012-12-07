using System;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Components
{
    public class Render : Component
    {
        public Texture2D Texture { get; private set; }

        public Color Color = Color.White;

        public float Alpha = 1f;
        public float Scale = 1f;

        public virtual Vector2 Origin { get { return new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f); } }

        public virtual Rectangle DrawRect { get { return Entity.Body.BoundingBox; } }

        public Render(Entity e, Texture2D texture)
            : base(e)
        {
            Texture = texture;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, DrawRect, null, Color * Alpha, Entity.Body.Angle,
                Origin, SpriteEffects.None, 0f);
        }
    }
}