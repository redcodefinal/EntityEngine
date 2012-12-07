using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Data
{
    public class Animation
    {
        public Texture2D Texture;
        public Vector2 TileSize;
        public int FramesPerSecond;
        public int CurrentFrame { get; set; }
        public string Key;

        public AnimationRender AnimationRender;
        public Timer FrameTimer;

        public int Tiles
        {
            get { return Texture.Width / (int)TileSize.X; }
        }

        public int MillisecondsPerFrame
        {
            get { return 1000 / FramesPerSecond; }
        }

        public Rectangle CurrentFrameRect
        {
            get
            {
                return new Rectangle((int)(TileSize.X * CurrentFrame),0,(int) TileSize.X, (int) TileSize.Y);
            }
        }

        public Rectangle DrawRect
        {
            get
            {
                return new Rectangle((int) AnimationRender.Entity.Body.Position.X, (int) AnimationRender.Entity.Body.Position.Y, (int) TileSize.X, (int) TileSize.Y);
            }
        }

        public Animation(AnimationRender ar, Texture2D texture, Vector2 tileSize, int framesPerSecond, string key)
        {
            AnimationRender = ar;
            Texture = texture;
            TileSize = tileSize;
            FramesPerSecond = framesPerSecond;
            Key = key;

            FrameTimer = new Timer(AnimationRender.Entity);
            FrameTimer.Milliseconds = MillisecondsPerFrame;
            FrameTimer.Start();
            FrameTimer.LastEvent += AdvanceNextFrame;
        }

        public void Update()
        {
            FrameTimer.Update();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, DrawRect, CurrentFrameRect, AnimationRender.Color, AnimationRender.Entity.Body.Angle,
                    new Vector2(DrawRect.Width/2f, DrawRect.Height/2f), SpriteEffects.None, 0f);
        }

        public void AdvanceNextFrame()
        {
            CurrentFrame++;
            if (CurrentFrame >= Tiles)
                CurrentFrame = 0;
        }
    }
}
