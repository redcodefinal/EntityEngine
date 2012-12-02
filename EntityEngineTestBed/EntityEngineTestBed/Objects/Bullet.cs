using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using EntityEngine.Engine;
using EntityEngine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngineTestBed.Objects
{
    public class Bullet : AsteroidEntity
    {
        public int Age { get; private set; }
        public Emitter Emitter;
        public Bullet(Texture2D bullettexture, Vector2 position, EntityGame eg) : base(eg)
        {
            Body = new Body(this, position, new Vector2(bullettexture.Width, bullettexture.Height));
            Components.Add(Body);

            Physics = new Physics(this);
            Components.Add(Physics);

            Render = new Render(this, bullettexture);
            Components.Add(Render);

            Emitter = new HitEmitter(this, GameRef.Game.Content.Load<Texture2D>(@"particles/hitparticle"));
            Components.Add(Emitter);
        }

        public override void Update()
        {
            base.Update();

            foreach (var entity in Targets)
            {
                if (!Body.TestCollision(entity)) continue;
                entity.Health.Hurt(1);
                Emitter.Emit(6);
                Destroy();
                return;
            }

            Age++;
            if (Age > 30) Render.Alpha -= .1f;
            if (Age > 45) Destroy();
        }
    }

    internal class HitEmitter : Emitter
    {
        private Random _rand = new Random(DateTime.Now.Millisecond);

        public HitEmitter(Entity e, Texture2D particletexture) : base(e, particletexture, Vector2.One*3)
        {

        }

        protected override Particle GenerateNewParticle()
        {
            int index = _rand.Next(0, 2);
            int ttl = _rand.Next(5, 10);

            Particle p = new Particle(index, Entity.Body.Position, ttl, this, Entity.GameRef);
            float angle = Entity.Body.Angle;
            float anglev = (float)((_rand.NextDouble() - .5f)*1.25f);
            p.Body.Angle = angle - anglev;
            p.Physics.Thrust((float)_rand.NextDouble() * 2);
            return p;
        }
    }
}
