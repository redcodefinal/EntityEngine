using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;
using Microsoft.Xna.Framework.Audio;

namespace EntityEngine.Components
{
    public class Sound : Component
    {
        /// <summary>
        /// The sound to process
        /// </summary>
        public SoundEffectInstance SoundEffect;

        /// <summary>
        /// The volume of the sound
        /// </summary>
        public float Volume
        {
            get { return SoundEffect.Volume; }
            set { SoundEffect.Volume = value; }
        }

        /// <summary>
        /// The pitch of the sound
        /// </summary>
        public float Pitch
        {
            get { return SoundEffect.Pitch; }
            set { SoundEffect.Pitch = value; }
        }

        /// <summary>
        /// The panning of the sound
        /// </summary>
        public float Pan
        {
            get { return SoundEffect.Pan; }
            set { SoundEffect.Pan = value; }
        }

        /// <summary>
        /// If the track should loop or not.
        /// </summary>
        public bool Loop
        {
            get { return SoundEffect.IsLooped; }
            set { SoundEffect.IsLooped = value; }
        }
        
        public bool IsPlaying { get; private set; }
        public bool IsPaused { get; private set; }

        public Sound(Entity e) : base(e)
        {
        }

        public Sound(Entity e, SoundEffect _sound) : base(e)
        {
            SoundEffect = _sound.CreateInstance();
            Volume = 1.0f;
            Pitch = 0.0f;
            Pan = 0.0f;
            Loop = false;
        }

        /// <summary>
        /// Plays this instance.
        /// </summary>
        public void Play()
        {
            SoundEffect.Play();
            IsPlaying = true;
            IsPaused = false;
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            SoundEffect.Pause();
            IsPlaying = false;
            IsPaused = true;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            SoundEffect.Stop();
            IsPlaying = false;
            IsPaused = false;
        }

        public SoundEffect LoadSound(string location)
        {
            return Entity.StateRef.GameRef.Game.Content.Load<SoundEffect>(location);
        }

        public override void ParseXml(XmlParser xmlparser, string path)
        {
            string rootnode = xmlparser.GetRootNode();
            rootnode = rootnode + "->" + path + "->";

            Volume = xmlparser.GetFloat(rootnode + "Volume");
            Pan = xmlparser.GetFloat(rootnode + "Pan");
            Pitch = xmlparser.GetFloat(rootnode + "Pitch");
            Loop = xmlparser.GetBool(rootnode + "Loop");
            SoundEffect = LoadSound(rootnode + "SoundEffect").CreateInstance();
        }
    }
}
