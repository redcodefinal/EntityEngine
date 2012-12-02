using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using EntityEngine.Engine;
using EntityEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EntityEngineTestBed.Objects
{
    public class Asteroid : AsteroidEntity
    {
        private static readonly Random Rand = new Random(DateTime.Now.Millisecond);
        private KeyboardInput _debugkey = new KeyboardInput(Keys.L);

        public Asteroid(Texture2D asteroidtexture, EntityGame eg) : base(eg)
        {
            Body = new Body(this, new Vector2(Rand.Next(0, eg.Viewport.Right), Rand.Next(0, eg.Viewport.Bottom)), new Vector2(asteroidtexture.Width,asteroidtexture.Height));
            Components.Add(Body);
            Render = new Render(this, asteroidtexture) { Scale = .75f };
            Components.Add(Render);
            Physics = new Physics(this) { Velocity = new Vector2((float)Rand.NextDouble() * Rand.Next(-1, 2), (float)Rand.NextDouble() * Rand.Next(-1, 2)), Drag = 1.0f };
            Components.Add(Physics);
            Health = new Health(this, 3);
            Components.Add(Health);
            Health.HurtEvent += SplitAsteroid;
            Health.DiedEvent += Destroy;
        }

        public override void Update()
        {
            base.Update();
            foreach (var entity in Targets.Where(entity => Body.TestCollision(entity)))
            {
                entity.Health.Hurt(1);
                Destroy();
                return;
            }
        }

        private void SplitAsteroid(Entity e = null)
        {
            if (!Health.Alive) return; // Stop it from adding new asteroids if it is already dead!

            Render.Scale *= 0.75f;

            var a = new Asteroid(Render.Texture, GameRef);
            a.Targets = Targets;
            a.Body.Position = Body.Position;
            a.Render.Scale = Render.Scale;
            a.Health.HitPoints = Health.HitPoints;

            Physics.Velocity = new Vector2((float) Rand.NextDouble()*Rand.Next(-2, 3) + .1f,
                                   (float) Rand.NextDouble()*Rand.Next(-2, 3) + .1f);

            foreach (var t in Targets)
                t.Targets.Add(a);

            AddEntity(a);
            
        }
    }
}
