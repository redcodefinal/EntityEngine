using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Components
{
    public class TileRender : Render
    {
        public Vector2 TileSize { get; private set; }

        public int Index;

        public int Columns { get { return (int)(Texture.Width / TileSize.X); } }

        public int Rows { get { return (int)(Texture.Height / TileSize.Y); } }

        public override Rectangle DrawRect
        {
            get
            {
                return new Rectangle((int) (Entity.Body.Position.X + Origin.X*Scale.X),
                                     (int) (Entity.Body.Position.Y + Origin.Y*Scale.Y), (int) (TileSize.X*Scale.X),
                                     (int) (TileSize.Y*Scale.Y));
            }
        }

        public Rectangle SourceRectangle
        {
            get
            {
                var r = new Rectangle();
                for (var i = 0; i <= Index; i += Columns)
                {
                    var ypos = Index - i;

                    if (ypos >= Columns) continue;

                    var p = new Point { Y = (i / Columns) * (int)TileSize.Y, X = ypos * (int)TileSize.X };
                    r = new Rectangle(p.X, p.Y, (int)TileSize.X, (int)TileSize.Y);
                }
                return r;
            }
        }

        public TileRender(Entity e, Texture2D texture, Vector2 tilesize)
            : base(e, texture)
        {
            TileSize = tilesize;
            Origin = new Vector2(TileSize.X/2.0f, TileSize.Y/2.0f);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, DrawRect, SourceRectangle, Color * Alpha, Entity.Body.Angle, Origin, Flip, Layer);
        }
    }
}