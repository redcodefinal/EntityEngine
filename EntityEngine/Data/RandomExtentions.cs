using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EntityEngine.Data
{
    public static class RandomExtentions
    {
        public static bool RandomBool(this Random rand)
        {
            return (rand.Next(0, 2) == 0) ? true : false;
        }

        public static Color RandomColor(this Random rand)
        {
            int r = rand.Next(5, 256);
            int g = rand.Next(5, 256);
            int b = rand.Next(5, 256);
            return new Color(r,g,b);
        }
    }
}
