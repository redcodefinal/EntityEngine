using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Objects
{
    public class Emitter : Component
    {
        public Texture2D Texture { get; protected set; }
        public Vector2 TileSize { get; protected set; }

        public bool AutoEmit;
        public int EmitAmount = 1;

        

        public Emitter(Entity e, Texture2D texture, Vector2 tilesize) : base(e)
        {
            Texture = texture;
            TileSize = tilesize;
        }

        public override void Update()
        {
            if(AutoEmit)
                Emit(EmitAmount);
        }

        protected virtual Particle GenerateNewParticle()
        {
            var p = new Particle(0, Entity.Body.Position/2, 30, this) {Physics = {Velocity = Vector2.UnitY}};
            return p;
        }

        public virtual void Emit(int amount)
        {
            for(var i = 0; i < amount; i++)
               Entity.AddEntity(GenerateNewParticle());
        }
    }
}
