using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngineTestBed.Objects
{
    public class EnemyShip : Ship
    {
        private float _turndirection = 1.0f;
        private Random _rand;

        public EnemyShip(Texture2D shiptexture, Texture2D bullettexture, EntityGame eg) : base(shiptexture,bullettexture,eg)
        {
            _rand = new Random(DateTime.Now.Millisecond);
            Body.Position = new Vector2(_rand.Next(40, eg.Viewport.Right - 40), _rand.Next(40, eg.Viewport.Bottom - 40));
        }

        protected override void ControlShip()
        {
            if (_rand.NextDouble() < 0.1f) _turndirection = -_turndirection;
            Body.Angle += _turndirection * 0.05f;
            Physics.Thrust(-(float)_rand.NextDouble() * .25f);
            Emitter.Emit(1);
            if (_rand.NextDouble() < 0.05) Weapon.Fire();
        }
    }
}
