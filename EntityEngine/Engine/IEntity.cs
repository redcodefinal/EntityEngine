using System.Collections.Generic;
using EntityEngine.Components;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Engine
{
    public interface IEntity
    {
        Body Body { get; }

        Render Render { get; }

        Physics Physics { get; }

        Collision Collision { get; }

        Health Health { get; }

        EntityState StateRef { get; }

        List<IComponent> Components { get; } 

        void AddEntity(Entity e);

        void Destroy(Entity e = null);

        void Update();

        void Draw(SpriteBatch sb);
    }
}