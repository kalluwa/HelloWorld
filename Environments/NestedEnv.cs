using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptIn14Days.Environments
{
    /// <summary>
    /// this class is a local Environment
    /// </summary>
    class NestedEnv : ScriptIn14Days.Environments.Environment
    {
        //outer environment
        ScriptIn14Days.Environments.Environment OuterEnv;

        /// <summary>
        /// local values
        /// </summary>
        protected Dictionary<string, object> values;

        public NestedEnv() { values = new Dictionary<string, object>(); }

        public void setOuter(ScriptIn14Days.Environments.Environment outer)
        {
            OuterEnv = outer;
        }

        public override void Add(string name, object value)
        {
            if (values.Keys.Contains(name))
                values[name] = value;
            else
            {
                values.Add(name, value);
            }
        }
        public override object Get(string name)
        {
            if (values.Keys.Contains(name))
                return values[name];
            else
            {
                //#######################
                return OuterEnv.Get(name);
                //#######################
            }
        }
    }
}
