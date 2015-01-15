using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptIn14Days.ASTrees
{
    class WhileStmnt : ASTList
    {
        public WhileStmnt(List<ASTree> child) : base(child) { }
        //while
        public ASTree Condition(){return child(0);}
        //then
        public ASTree Body() { return child(1); }

        public override string ToString()
        {
            return "(while " + Condition().ToString() + " " + Body().ToString() + ")";
        }

        public override object eval(Environments.Environment env)
        {
            object condition = Condition().eval(env);

            object result = 0;
            while (condition is int && (int)condition != Environments.Environment.FALSE)
            {
                result = Body().eval(env);
                condition = Condition().eval(env);
            }

            return result;
        }
    }
}
