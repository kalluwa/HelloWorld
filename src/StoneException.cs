using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptIn14Days.src
{
    public class StoneException : Exception
    {
        public StoneException(string errorText)
            :base(errorText)
        { 
        }

        public StoneException(string errorText, int pos)
        :base(errorText+pos.ToString())
        {
        }
    }
}
