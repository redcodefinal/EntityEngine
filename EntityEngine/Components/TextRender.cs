using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Components
{
    public class TextRender : IComponent
    {
        public Entity Entity { get; private set; }
        public SpriteFont Font { get; private set; }
        public string Text;
        public Color Color =  Color.White;
        public float Layer;
        public float Alpha = 1f;
        public float Scale = 1f;
        public virtual Vector2 Origin
        {
            get { return Vector2.Zero; }
        }

        public TextRender(Entity e, SpriteFont sf, string text)
        {
            Entity = e;
            Text = text;
            Font = sf;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(Font, Text, Entity.Body.Position, Color * Alpha, Entity.Body.Angle,Origin, Scale, SpriteEffects.None, Layer);
        }

        public void Destroy()
        {
        }
    }
}
