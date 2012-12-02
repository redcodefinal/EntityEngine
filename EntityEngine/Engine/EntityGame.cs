using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Engine
{
    public class EntityGame
    {
        public bool Paused { get; protected set; }
        public List<Entity> Entities { get; protected set; }
        public List<Entity> NewEntities { get; protected set; }

        public Rectangle Viewport { get; private set; }
        public Game Game;
        public SpriteBatch SpriteBatch;

        public EntityGame(Game game, GraphicsDeviceManager g, Rectangle viewport, SpriteBatch spriteBatch)
        {
            Game = game;
            SpriteBatch = spriteBatch;

            game.Components.Add(new InputHandler(game));

            Entities = new List<Entity>();
            NewEntities = new List<Entity>();
            Paused = false;
            
            Viewport = viewport;
            MakeWindow(g);
        }

        private void MakeWindow(GraphicsDeviceManager g)
        {
            if ((Viewport.Width > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) ||
                (Viewport.Height > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)) return;
            g.PreferredBackBufferWidth = Viewport.Width;
            g.PreferredBackBufferHeight = Viewport.Height;
            g.IsFullScreen = false;
            g.ApplyChanges();
        }

        public virtual void StartGame()
        {
            Entities = NewEntities;
        }

        public virtual void ResetGame()
        {
            Entities = new List<Entity>();
            NewEntities = new List<Entity>();
        }

        public virtual void Update()
        {
            //Replace the old entities
            Entities = NewEntities.ToList();

            foreach (var e in Entities)
                e.Update();
        }

        public virtual void Draw()
        {
            foreach (var e in Entities)
                e.Draw(SpriteBatch);
        }

        public virtual void AddEntity(Entity entity)
        {
            //Subscribe to the destory event
            entity.DestroyEvent += RemoveEntity;
            entity.CreateEvent += AddEntity;

            NewEntities.Add(entity);
        }

        public virtual void RemoveEntity(Entity entity)
        {
            //Unsubscribe from the destroy event
            NewEntities.Remove(entity);
        }

        public virtual void Pause()
        {
            Paused = true;
        }

        public virtual void Unpause()
        {
            Paused = false;
        }
    }
}
