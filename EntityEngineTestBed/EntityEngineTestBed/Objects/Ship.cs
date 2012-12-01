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
    public class Ship : Entity
    {
        private KeyboardInput _attackkey, _upkey, _leftkey, _rightkey, _downkey;
        public Weapon Weapon { get; protected set; }

        public Ship(Texture2D shiptexture, Texture2D bullettexture, EntityGame eg) : base(eg)
        {
            Body = new Body(this, new Vector2(eg.Viewport.Width/2 - shiptexture.Width/2, eg.Viewport.Height/2 - shiptexture.Height/2),
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
                Physics.Thrust(-.25f);
            else if (_downkey.Down())
                Physics.Thrust(.25f);

            if (_leftkey.Down())
                Body.Angle += -0.05f;
            if (_rightkey.Down())
                Body.Angle += 0.05f;

            if (_attackkey.RapidFire(150))
                Weapon.Fire();
        }
    }
}
