using EntityEngine.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Components
{
    public class Component : IComponent
    {
        public Entity Entity { get; private set; }

        public Component(Entity e)
        {
            Entity = e;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(SpriteBatch sb)
        {
        }
    }
}