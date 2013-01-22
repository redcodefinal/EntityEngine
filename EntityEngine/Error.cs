using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEngine.Engine;

namespace EntityEngine
{
    public static class Error
    {
        public static void Warning(string message)
        {
            Console.WriteLine("Warning: " + message);
        }

        public static void Warning(string message, Entity sender)
        {
            Console.WriteLine("Warning: " + message + " from " + sender.Name);
        }
    }
}
