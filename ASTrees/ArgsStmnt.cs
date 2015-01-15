using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptIn14Days.ASTrees
{
    class ArgsStmnt : ASTList
    {
        public ArgsStmnt(List<ASTree> list) : base(list) { }

        public override string ToString()
        {
            return base.ToString();
        }
        public override object eval(Environments.Environment env)
        {
            return 0;
        }
    }
}
