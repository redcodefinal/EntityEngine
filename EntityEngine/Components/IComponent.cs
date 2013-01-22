using EntityEngine.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Components
{
    public interface IComponent
    {
        Entity Entity { get; }

        void Update();
        void Draw(SpriteBatch sb);
        void Destroy();
        void ParseXml(XmlParser xmlparser, string nodename);
    }
}