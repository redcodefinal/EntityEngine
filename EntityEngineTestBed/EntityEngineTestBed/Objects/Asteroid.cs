using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using EntityEngine.Engine;
using EntityEngine.Input;
using EntityEngine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EntityEngineTestBed.Objects
{
    public class Asteroid : AsteroidEntity
    {
        private static readonly Random Rand = new Random(DateTime.Now.Millisecond);
        private KeyboardInput _debugkey = new KeyboardInput(Keys.L);
        private readonly Emitter _emitter;
        private HitEmitter _hitemitter;

        public Asteroid(Texture2D asteroidtexture, EntityState es) : base(es)
        {
            Body = new Body(this, new Vector2(Rand.Next(0, es.GameRef.Viewport.Right), Rand.Next(0, es.GameRef.Viewport.Bottom)), new Vector2(asteroidtexture.Width,asteroidtexture.Height));
            Components.Add(Body);
            Render = new Render(this, asteroidtexture) { Scale = .75f };
            Components.Add(Render);
            Physics = new Physics(this) { Velocity = new Vector2((float)Rand.NextDouble() * Rand.Next(-2, 3) + .5f, (float)Rand.NextDouble() * Rand.Next(-2, 3) +.5f), Drag = 1.0f };
            Components.Add(Physics);
            Health = new Health(this, 3);
            Components.Add(Health);
            _emitter = new HurtEmitter(this, StateRef.GameRef.Game.Content.Load<Texture2D>(@"particles/thrustparticle"));
            Components.Add(_emitter);
            _hitemitter = new AsteroidHitEmitter(this, StateRef.GameRef.Game.Content.Load<Texture2D>(@"particles/thrustparticle"));
            Components.Add(_hitemitter);
            Health.HurtEvent += EmitEventHandler;
            Health.HurtEvent += SplitAsteroid;
            Health.DiedEvent += Destroy;
        }

        public override void Update()
        {
            base.Update();
            foreach (var entity in Targets.Where(entity => Body.TestCollision(entity)))
            {
                entity.Health.Hurt(1);
                if (!entity.Health.Alive) return;
                
                _hitemitter.Emit(10);
                Destroy();
                return;
            }
        }

        public void EmitEventHandler(Entity e = null)
        {
            _emitter.Emit(10 * Health.HitPoints);
        }


        private void SplitAsteroid(Entity e = null)
        {
            if (!Health.Alive) return; // Stop it from adding new asteroids if it is already dead!

            Render.Scale *= 0.75f;

            var a = new Asteroid(Render.Texture, StateRef);
            a.Targets = Targets;
            a.Body.Position = Body.Position;
            a.Render.Scale = Render.Scale;
            a.Health.HitPoints = Health.HitPoints;

            Physics.Velocity = new Vector2((float) Rand.NextDouble()*Rand.Next(-2, 3) + .5f,
                                   (float) Rand.NextDouble()*Rand.Next(-2, 3) + .5f);

            foreach (var t in Targets)
                t.Targets.Add(a);

            AddEntity(a);
            
        }
    }

    class HurtEmitter : Emitter
    {
        public HurtEmitter(Entity e, Texture2D texture) : base(e, texture, Vector2.One * 5)
        {

        }

        public override void Emit(int amount)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            float pislice = MathHelper.TwoPi/amount;
            for (int i = 0; i < amount; i++)
            {
                int index = random.Next(0, 3);

                float angle = pislice*i;
                Vector2 position = Entity.Body.Position;

                var p = new FadeParticle(index,position,40,this);
                p.Body.Angle = angle;
                p.TimeToLive = 50 + Entity.Health.HitPoints*20;
                p.Physics.Drag = 0.99f;
                p.Physics.Thrust(random.Next(1,4));
                p.Render.Scale = (float)random.NextDouble() + .5f;
                Entity.AddEntity(p);
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }

    class AsteroidHitEmitter : HitEmitter
    {
        public AsteroidHitEmitter(Entity e, Texture2D particletexture) : base(e, particletexture)
        {
            TileSize = Vector2.One*5;
        }

        private Random _rand = new Random(DateTime.Now.Millisecond);
        protected override Particle GenerateNewParticle()
        {
            int index = _rand.Next(0, 5);

            Particle p = new FadeParticle(index, Entity.Body.Position, 40, this);
            p.TimeToLive = 400;
            float angle = Math.Abs(Entity.Targets[0].Body.Angle) - MathHelper.Pi;
            float anglev = (float)((_rand.NextDouble() - .5f)*1.25f);
            p.Body.Angle = angle - anglev;
            p.Physics.Thrust((float)_rand.NextDouble() * 2);
            p.Render.Scale = .25f * Entity.Health.HitPoints + .75f;
            return p;
        }
    }
}
