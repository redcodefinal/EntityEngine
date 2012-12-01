using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;
using EntityEngineTestBed.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngineTestBed
{
    public class AsteroidGame : EntityGame
    {
        private Ship _ship;

        public AsteroidGame(Game game, GraphicsDeviceManager g, SpriteBatch sb)
            : base(game, g, new Rectangle(0, 0, 600, 600), sb)
        {
            _ship = new Ship(game.Content.Load<Texture2D>("ship"), game.Content.Load<Texture2D>("bullet"), this);
            AddEntity(_ship);
            StartGame();
        }
    }
}
