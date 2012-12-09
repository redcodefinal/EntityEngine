using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.GUI
{
    public class Image : Entity
    {
        public Image(EntityState es, Texture2D texture, Vector2 position) : base(es)
        {
            Body = new Body(this, position, new Vector2(texture.Width, texture.Height));
            Components.Add(Body);

            Physics = new Physics(this);
            Components.Add(Physics);

            Render = new Render(this, texture);
            Components.Add(Render);
        }
    }
}
