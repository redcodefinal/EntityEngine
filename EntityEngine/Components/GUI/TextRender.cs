using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Components.GUI
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
        public SpriteEffects Flip = SpriteEffects.None;
        public Rectangle DrawRectangle
        {
            get
            {
                return new Rectangle((int)Entity.Body.Position.X, (int)Entity.Body.Position.Y, (int)Font.MeasureString(Text).X, (int)Font.MeasureString(Text).Y);
            }
        }
        public virtual Vector2 Origin
        {
            //TODO Implement origin!
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
            sb.DrawString(Font, Text, Entity.Body.Position, Color * Alpha, Entity.Body.Angle,Origin, Scale, Flip, Layer);
        }

        public void Destroy()
        {
        }
    }
}
