using EntityEngine.Engine;

namespace EntityEngine.Components
{
    public class Health : Component
    {
        public Health(Entity e, int hp)
            : base(e)
        {
            HitPoints = hp;
        }

        public float HitPoints { get; set; }

        public bool Alive
        {
            get { return !(HitPoints <= 0); }
        }

        public event Entity.EventHandler HurtEvent;

        public event Entity.EventHandler DiedEvent;

        public void Hurt(float points)
        {
            if (!Alive) return;

            HitPoints -= points;
            if (HurtEvent != null)
                HurtEvent(Entity);

            if (!Alive)
            {
                if (DiedEvent != null)
                    DiedEvent(Entity);
            }
        }
    }
}