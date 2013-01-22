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
        public Vector2 Scale = Vector2.One;
        public float Layer;
        public SpriteEffects Flip = SpriteEffects.None;

        public Vector2 Origin { get; set; }

        public virtual Rectangle DrawRect 
        { 
            get
            {
                return new Rectangle(
                    (int)(Entity.Body.Position.X + Origin.X*Scale.X), 
                    (int)(Entity.Body.Position.Y + Origin.Y*Scale.Y), 
                    (int)(Texture.Width * Scale.X),
                    (int)(Texture.Height * Scale.Y));
            } 
        }
        
        public Render(Entity e)
            : base(e)
        {
            Origin = Vector2.Zero;
        }

        public Render(Entity e, Texture2D texture)
            : base(e)
        {
            Texture = texture;
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, DrawRect, null, Color * Alpha, Entity.Body.Angle,
                Origin, Flip, Layer);
        }

        public Texture2D LoadTexture(string location)
        {
            return Entity.StateRef.GameRef.Game.Content.Load<Texture2D>(location);
        }

        public override void ParseXml(XmlParser xmlparser, string nodename)
        {
            string rootnode = xmlparser.GetRootNode() + "->Render->";
            Texture = LoadTexture(xmlparser.GetString(rootnode + "Texture"));

            Color = xmlparser.GetColor(rootnode + "Color");
            Alpha = xmlparser.GetFloat(rootnode + "Alpha");
            Scale = xmlparser.GetVector2(rootnode + "Scale");
            Layer = xmlparser.GetFloat(rootnode + "Layer");
        }
    }
}