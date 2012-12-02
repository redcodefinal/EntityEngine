using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngineTestBed.Objects
{
    public class Bullet : AsteroidEntity
    {
        public int Age { get; private set; }
        public Bullet(Texture2D bullettexture, Vector2 position, EntityGame eg) : base(eg)
        {
            Body = new Body(this, position, new Vector2(bullettexture.Width, bullettexture.Height));
            Components.Add(Body);
            Physics = new Physics(this);
            Components.Add(Physics);
            Render = new Render(this, bullettexture);
            Components.Add(Render);
        }

        public override void Update()
        {
            base.Update();

            foreach (var entity in Targets)
            {
                if (!Body.TestCollision(entity)) continue;
                entity.Health.Hurt(1);
                Destroy();
                return;
            }

            Age++;
            if (Age > 30) Render.Alpha -= .1f;
            if (Age > 45) Destroy();
        }
    }
}
