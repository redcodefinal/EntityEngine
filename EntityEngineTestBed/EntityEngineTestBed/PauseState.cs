using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;

namespace EntityEngineTestBed
{
    public class PauseState : EntityState
    {
        public PauseState(EntityGame eg) : base(eg)
        {
            
        }

        public override void Show()
        {
            base.Show();
            Reset();
        }
    }
}
