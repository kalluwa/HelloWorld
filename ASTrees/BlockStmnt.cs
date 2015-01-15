using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptIn14Days.ASTrees
{
    class BlockStmnt : ASTList
    {
        public BlockStmnt(List<ASTree> list) : base(list) { }
        public override object eval(Environments.Environment env)
        {
            object result = 0;
            for (int i = 0; i < numChildren(); i++)
                //if(!(child(i).ToString().Equals("=")))
                result = child(i).eval(env);
            return result;
        }
    }
}
