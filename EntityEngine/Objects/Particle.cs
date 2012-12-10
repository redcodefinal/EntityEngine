using EntityEngine.Components;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;

namespace EntityEngine.Objects
{
    public class Particle : Entity
    {
        public int TimeToLive { get; set; }
        public int MaxTimeToLive { get; private set; }
        public Emitter Emitter;
        private TileRender _tr;
        public int Index
        {
            get { return _tr.Index; }
            set { _tr.Index = value; }
        }

        public Particle(int index, Vector2 position, int ttl, Emitter e)
            : base(e.Entity.StateRef)
        {
            
            Body = new Body(this, position, e.TileSize);
            Components.Add(Body);

            _tr = new TileRender(this, e.Texture, e.TileSize);
            Render = _tr;
            Components.Add(Render);
            Index = index;

            Physics = new Physics(this);
            Components.Add(Physics);

            Emitter = e;
            TimeToLive = ttl;
            MaxTimeToLive = TimeToLive;
        }

        public override void Update()
        {
            base.Update();

            TimeToLive--;
            if (TimeToLive <= 0)
                Destroy();
        }
    }

    public class FadeParticle : Particle
    {
        public int FadeAge;

        public FadeParticle(int index, Vector2 position, int fadeage, int ttl, Emitter e)
            : base(index, position, ttl, e)
        {
            FadeAge = fadeage;
        }

        public override void Update()
        {
            base.Update();
            if (TimeToLive < FadeAge)
            {
                Render.Alpha -= 1f / FadeAge;
            }
        }
    }
}