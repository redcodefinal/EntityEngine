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
        public int TimeToLive { get; protected set; }
        public int MaxTimeToLive { get; private set; }
        public Particle(int index, Vector2 position, int ttl, Emitter e, EntityGame eg) : base(eg)
        {
            Index = index;
            Body = new Body(this, position, e.TileSize);
            Components.Add(Body);
            Render = new TileRender(this, e.Texture, e.TileSize);
            Components.Add(Render);
            Physics = new Physics(this);
            Components.Add(Physics);

            TimeToLive = ttl;
            MaxTimeToLive = TimeToLive;
        }

        public override void Update()
        {
            base.Update();

            TimeToLive--;
            if(TimeToLive <= 0)
                Destroy();
        }
    }
}
