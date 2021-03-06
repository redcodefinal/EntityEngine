﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Data
{
    public class Animation : Render
    {
        public Vector2 TileSize;
        public int FramesPerSecond;
        public int CurrentFrame { get; set; }
        public string Key;
        public event Timer.TimerEvent LastFrameEvent;

        public AnimationRender AnimationRender;
        public Timer FrameTimer;

        public bool HitLastFrame
        {
            get { return (CurrentFrame >= Tiles-1); }
        }

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

        public override Rectangle DrawRect
        {
            get
            {
                return new Rectangle((int)(Entity.Body.Position.X + Origin.X * Scale.X), (int)(Entity.Body.Position.Y + Origin.Y * Scale.Y), (int)(TileSize.X * Scale.X), (int)(TileSize.Y * Scale.Y));
            }
        }

        public Animation(Entity e, Texture2D texture, Vector2 tileSize, int framesPerSecond, string key) 
            : base(e, texture)
        {
            TileSize = tileSize;
            FramesPerSecond = framesPerSecond;
            Key = key;

            Origin = new Vector2(TileSize.X / 2.0f, TileSize.Y / 2.0f);

            FrameTimer = new Timer(e);
            FrameTimer.Milliseconds = MillisecondsPerFrame;
            FrameTimer.LastEvent += AdvanceNextFrame;
        }

        public Animation(Entity e)
            : base(e)
        {
            Origin = new Vector2(TileSize.X / 2.0f, TileSize.Y / 2.0f);

            FrameTimer = new Timer(e);
            FrameTimer.Milliseconds = MillisecondsPerFrame;
            FrameTimer.LastEvent += AdvanceNextFrame;
        }

        public override void Update()
        {
            FrameTimer.Update();
            if (HitLastFrame)
            {
                if (LastFrameEvent != null)
                    LastFrameEvent();
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, DrawRect, CurrentFrameRect, Color * Alpha, Entity.Body.Angle,
                    Origin, Flip, Layer);
        }

        public void AdvanceNextFrame()
        {
            CurrentFrame++;
            if (CurrentFrame >= Tiles)
                CurrentFrame = 0;
        }
        public void AdvanceLastFrame()
        {
            CurrentFrame--;
            if (CurrentFrame < 0)
                CurrentFrame = Tiles;
        }

        public void Start()
        {
            FrameTimer.Start();
        }

        public void Stop()
        {
            FrameTimer.Stop();
        }

        public override void ParseXml(XmlParser xmlparser, string nodename)
        {
            base.ParseXml(xmlparser, nodename);
            string rootnode = xmlparser.GetRootNode();
            rootnode = rootnode + "->" + nodename + "->";

            TileSize = xmlparser.GetVector2(rootnode + "TileSize");
            FramesPerSecond = xmlparser.GetInt(rootnode + "FramesPerSecond");
            CurrentFrame = xmlparser.GetInt(rootnode + "CurrentFrame");
            Key = xmlparser.GetString(rootnode + "Key");

        }
    }
}
