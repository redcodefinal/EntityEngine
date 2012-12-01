using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEngine.Engine
{
    public class TileEntity : Entity
    {
        public int Index;

        public TileEntity(EntityGame eg) : base(eg)
        {
        }
    }
}
