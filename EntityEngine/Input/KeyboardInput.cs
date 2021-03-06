﻿using Microsoft.Xna.Framework.Input;

namespace EntityEngine.Input
{
    public sealed class KeyboardInput : Input
    {
        private Keys _key;

        public Keys Key
        {
            get
            {
                return _key;
            }

            set
            {
                _key = value;
                InputHandler.Flush();
            }
        }

        public KeyboardInput(Keys key)
        {
            _key = key;
        }

        public override bool Pressed()
        {
            return InputHandler.KeyPressed(Key);
        }

        public override bool Released()
        {
            return InputHandler.KeyReleased(Key);
        }

        public override bool Down()
        {
            return InputHandler.KeyDown(Key);
        }

        public override bool Up()
        {
            return InputHandler.KeyUp(Key);
        }
    }
}