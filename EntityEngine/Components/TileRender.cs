using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Components
{
    public class TileRender : Render
    {
        public Vector2 TileSize { get; private set; }
        public TileEntity TileEntity { get; private set; }
        public int Columns { get { return (int)(Texture.Width / TileSize.X); } }
        public int Rows { get { return (int)(Texture.Height / TileSize.Y); } }
        public override Rectangle DrawRect
        {
            get
            {
                return new Rectangle((int)(Entity.Body.Position.X * Scale),(int) (Entity.Body.Position.Y * Scale), (int)(TileSize.X * Scale), (int)(TileSize.Y * Scale));
            }
        }
        public Rectangle SourceRectangle
        {
            get
            {
                for (var i = 0; i <= TileEntity.Index; i += Columns)
                {
                    var ypos = TileEntity.Index - i;

                    if (ypos >= Columns) continue;

                    var p = new Point {Y = (i/Columns)*(int) TileSize.Y, X = ypos*(int) TileSize.X};
                    var r = new Rectangle(p.X, p.Y, (int) TileSize.X, (int) TileSize.Y);
                    return r;
                }
                throw new Exception("Something went wrong when calling TileEntity.SourceRectangle");
            }
        }

        public TileRender(TileEntity e, Texture2D texture, Vector2 tilesize) : base(e, texture)
        {
            TileSize = tilesize;
            TileEntity = e;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, DrawRect, SourceRectangle, Color, Entity.Body.Angle, new Vector2(DrawRect.Width / 2, DrawRect.Height / 2), SpriteEffects.None, 0f);
        }
    }
}
