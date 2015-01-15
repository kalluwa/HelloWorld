using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptIn14Days.Environments
{
    public abstract class Environment
    {
        public static int TRUE = 1;
        public static int FALSE = 0;

        public abstract void Add(string name, object value);
        public abstract object Get(string name);
    }
}
