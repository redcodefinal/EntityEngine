using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityEngine.Data
{
    public static class RandomExtentions
    {
        public static bool RandomBool(this Random rand)
        {
            return (rand.Next(0, 2) == 0) ? true : false;
        }
    }
}
