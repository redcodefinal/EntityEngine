using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;
using EntityEngine.Input;
using EntityEngineTestBed.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EntityEngineTestBed
{
    public sealed class AsteroidGame : EntityGame
    {
        private Ship _ship;
        private EnemyShip _enemyship;
        private KeyboardInput _resetkey = new KeyboardInput(Keys.F11);
        public List<Entity> Enemies = new List<Entity>();
        private SpriteFont font;
        private bool _gameover;
        private string _gameovertext;

        public AsteroidGame(Game game, GraphicsDeviceManager g, SpriteBatch sb)
            : base(game, g, new Rectangle(0, 0, 600, 600), sb)
        {
            StartGame();
        }

        public override void StartGame()
        {
            _ship = new Ship(Game.Content.Load<Texture2D>("ship"), Game.Content.Load<Texture2D>("bullet"), this);
            AddEntity(_ship);

            _enemyship = new EnemyShip(Game.Content.Load<Texture2D>("enemyship"), Game.Content.Load<Texture2D>("bullet"), this);
            AddEntity(_enemyship);
            Enemies.Add(_enemyship);

            _ship.Targets.Add(_enemyship);
            _enemyship.Targets.Add(_ship);

            for (var i = 0; i < 10; i++)
            {
                var a = new Asteroid(Game.Content.Load<Texture2D>("asteroid"), this);
                _ship.Targets.Add(a);
                a.Targets.Add(_ship);
                Enemies.Add(a);
                AddEntity(a);
            }

            font = Game.Content.Load<SpriteFont>("font");

            base.StartGame();

            Unpause();
        }

        public override void ResetGame()
        {
            base.ResetGame();
            _gameover = false;
        }

        public override void Update()
        {
            if(!Paused)
                base.Update();

            if (_resetkey.Pressed())
            {
                ResetGame();
                StartGame();
            }

            if(_ship.Targets.Count == 0)
                GameOver(true);
            if(!_ship.Health.Alive)
                GameOver(false);
        }

        public void GameOver(bool won)
        {
            _gameover = true;
            _gameovertext = won ? "You Won!" : "Game Over!";
        }

        public override void Draw()
        {
            base.Draw();
            if (_gameover)
            {
                SpriteBatch.DrawString(font, _gameovertext, Vector2.One * 10, Color.White);
            }
        }

    }
}
