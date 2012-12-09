using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EntityEngine.Input
{
    public class DoubleInput : Input
    {
        public KeyboardInput Key;
        public GamePadInput Button;

        public DoubleInput(Keys key, Buttons button, PlayerIndex pi)
        {
            Key = new KeyboardInput(key);
            Button = new GamePadInput(button, pi);
        }

        public override bool Pressed()
        {
            return Key.Pressed() || Button.Pressed();
        }

        public override bool Released()
        {
            return Key.Released() || Button.Released();
        }

        public override bool Down()
        {
            return Key.Down() || Button.Down();
        }

        public override bool Up()
        {
            return Key.Up() && Button.Up();
        }
    }
}
