using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Data;
using EntityEngine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Components
{
    public class AnimationRender : Render
    {
        public Dictionary<string, Animation> Animations;
        public Animation CurrentAnimation;

        public AnimationRender(Entity e) : base(e, null)
        {
            Animations =  new Dictionary<string, Animation>();
        }

        public override void Update()
        {
            CurrentAnimation.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            CurrentAnimation.Draw(sb);
        }

        public void AddAnimation(Animation a)
        {
            Animations.Add(a.Key, a);
        }

        public void RemoveAnimation(Animation a)
        {
            Animations.Remove(a.Key);
        }

        public void ShowAnimation(Animation a)
        {
            CurrentAnimation = Animations[a.Key];
        }

        public void ShowAnimation(string key)
        {
            CurrentAnimation = Animations[key];
        }
    }
}
