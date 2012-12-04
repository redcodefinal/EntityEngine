using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;
using EntityEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EntityEngineTestBed
{
    public class AsteroidGame : EntityGame
    {
        private GameState _gs;
        private PauseState _pausestate;
        private KeyboardInput _switchkey = new KeyboardInput(Keys.Space);

        public AsteroidGame(Game game, GraphicsDeviceManager g, SpriteBatch sb) : base(game, g, new Rectangle(0,0,600,600), sb)
        {
            _gs = new GameState(this);
            CurrentState = _gs;

            _pausestate = new PauseState(this);

        }

        public override void Update()
        {
            base.Update();

            if (CurrentState == _pausestate && _switchkey.Pressed())
            {
                _gs.Show();
            }
             else if (CurrentState == _gs && _switchkey.Pressed())
            {
                _pausestate.Show();
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (CurrentState == _pausestate)
                _gs.Draw(SpriteBatch);
        }
    }
}
