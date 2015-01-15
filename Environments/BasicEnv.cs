using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptIn14Days.Environments
{
    class BasicEnv : ScriptIn14Days.Environments.Environment
    {
        protected Dictionary<string, object> values;
        public BasicEnv() { values = new Dictionary<string, object>(); }

        public override void Add(string name, object value)
        {
            if (values.Keys.Contains(name))
                values[name] = value;
            else
                values.Add(name, value);
        }
        public override object Get(string name)
        {
            if (values.Keys.Contains(name))
                return values[name];
            else
                return null;
        }
    }
}
