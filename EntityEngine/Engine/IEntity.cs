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
        Health Health { get; }
        List<Entity> Targets { get; set; }
        List<Entity> Group { get; set; }

        EntityGame GameRef { get; }

        void AddEntity(Entity e);
        void Destroy(Entity e = null);
        void Update();
        void Draw(SpriteBatch sb);
    }
}
