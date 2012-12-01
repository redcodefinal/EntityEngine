using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;

namespace EntityEngine.Objects
{
    public class Particle : TileEntity
    {
        public Particle(int index, Vector2 position, int ttl, Emitter e, EntityGame eg) : base(eg)
        {
            Index = index;
            Body = new Body(this, position, e.TileSize);
            Render = new TileRender(this, e.Texture, e.TileSize);
            Physics = new Physics(this);
        }

        public override void Update()
        {
            
        }
    }
}
