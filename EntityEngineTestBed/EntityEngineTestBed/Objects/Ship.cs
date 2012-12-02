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
    public class Ship : AsteroidEntity
    {
        private KeyboardInput _attackkey, _upkey, _leftkey, _rightkey, _downkey;
        public Weapon Weapon { get; protected set; }
        public Emitter Emitter;
        public Ship(Texture2D shiptexture, Texture2D bullettexture, EntityGame eg) : base(eg)
        {
            Body = new Body(this, new Vector2(eg.Viewport.Width/2.0f, eg.Viewport.Height/2.0f),
                            new Vector2(shiptexture.Width, shiptexture.Height));
            Components.Add(Body);

            Physics = new Physics(this) { Drag = 0.9f };
            Components.Add(Physics);

            Render = new Render(this, shiptexture) { Scale = .5f };
            Components.Add(Render);

            Weapon = new Gun(this, bullettexture);
            Components.Add(Weapon);

            Health = new Health(this, 5);
            Health.DiedEvent += Destroy;
            Components.Add(Health);

            Emitter = new ShipEmitter(this, eg.Game.Content.Load<Texture2D>(@"particles/shipparticle"));
            Components.Add(Emitter);

            _attackkey = new KeyboardInput(Keys.Enter);
            _upkey = new KeyboardInput(Keys.W);
            _downkey = new KeyboardInput(Keys.S);
            _leftkey = new KeyboardInput(Keys.A);
            _rightkey = new KeyboardInput(Keys.D);

        }

        public override void Update()
        {
            base.Update();
            
            ControlShip();
        }

        virtual protected void ControlShip()
        {
            if (_upkey.Down())
            {
                Physics.Thrust(-.25f);
                Emitter.Emit(1);
            }
            else if (_downkey.Down())
            {
                Physics.Thrust(.13f);
            }

            if (_leftkey.Down())
                Body.Angle += -0.05f;
            if (_rightkey.Down())
                Body.Angle += 0.05f;

            if (_attackkey.RapidFire(150))
                Weapon.Fire();
        }
    }

    class ShipEmitter : Emitter
    {
        Random _rand = new Random(DateTime.Now.Millisecond);
        public ShipEmitter(Entity e, Texture2D particletexture) : base(e, particletexture, Vector2.One*5)
        {
            
        }

        protected override Particle GenerateNewParticle()
        {
            int index = _rand.Next(0, 3);
            int ttl = _rand.Next(10, 16);

            //Rotate the point based on the center of the sprite
            // p = unrotated point, o = rotation origin
            //p'x = cos(theta) * (px-ox) - sin(theta) * (py-oy) + ox
            //p'y = sin(theta) * (px-ox) + cos(theta) * (py-oy) + oy

            Vector2 origin = Entity.Body.Position;

            Vector2 unrotatedposition = new Vector2(
                Entity.Body.BoundingBox.X,
                Entity.Body.BoundingBox.Bottom - 10);
            float angle = Entity.Body.Angle;
            Vector2 position = new Vector2(
                (float)(Math.Cos(angle) * (unrotatedposition.X - origin.X) - Math.Sin(angle) * (unrotatedposition.Y - origin.Y) + origin.X),
                (float)(Math.Sin(angle) * (unrotatedposition.X - origin.X) + Math.Cos(angle) * (unrotatedposition.Y - origin.Y) + origin.Y)
            );

            

            Particle p = new Particle(index, position, ttl, this, Entity.GameRef);

            float anglev = (float)_rand.NextDouble() - .5f;
            p.Physics.Thrust((float)_rand.NextDouble(), angle - anglev);
            return p;
        }
    }
}
