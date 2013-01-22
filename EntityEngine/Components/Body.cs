using System;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;

namespace EntityEngine.Components
{
    public class Body : Component
    {
        public Vector2 Position;
        public Vector2 Bounds;
        public float Angle;

        public virtual Rectangle BoundingBox
        {
            get
            {
                if(Entity.Render != null)
                {
                    return new Rectangle((int)Position.X, (int)Position.Y,
                                         Entity.Render.DrawRect.Width, Entity.Render.DrawRect.Height);
                }
                return new Rectangle((int) Position.X, (int) Position.Y, (int) Bounds.X, (int) Bounds.Y);
            }
        }

        public Body(Entity e)
            : base(e)
        {
        }

        public Body(Entity e, Vector2 position, Vector2 bounds)
            : base(e)
        {
            Position = position;
            Bounds = bounds;
        }

        public override void ParseXml(XmlParser xmlparser, string nodename)
        {
            string rootnode = xmlparser.GetRootNode();
            rootnode = rootnode + "->"+nodename+"->";
            try
            {
                Position = xmlparser.GetVector2(rootnode + "Position");
            }
            catch{}

            try
            {
                Angle = xmlparser.GetFloat(rootnode + "Angle");
            }
            catch{}
            
        }
    }
}