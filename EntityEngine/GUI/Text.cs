using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using EntityEngine.Components.GUI;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.GUI
{
    public class Text : Entity
    {
        public new TextRender Render;
        public Text(EntityState es, Vector2 position, string text, SpriteFont font) : base(es)
        {
            //TODO: Fix the bounds to be the correct height and width.
            Body = new Body(this, position, Vector2.Zero);
            Components.Add(Body);

            Physics= new Physics(this);
            Components.Add(Physics);

            Render = new TextRender(this, font, text);
            Components.Add(Render);
        }
    }
}
