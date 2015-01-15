using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptIn14Days.ASTrees
{
    class IfStmnt : ASTList
    {
        public IfStmnt(List<ASTree> child) : base(child) { }
        //if
        public ASTree Condition(){return child(0);}
        //then
        public ASTree ThenBlock(){return child(1);}
        //else
        public ASTree ElseBlock()
        {
            if(numChildren()<=2)
            {
                return null;
            }

            return child(2);
        }

        public override string ToString()
        {
            return "(if " + Condition().ToString() + " " + ThenBlock().ToString() + 
                ((numChildren()>2)?" else " +ElseBlock().ToString():"")+")";
        }

        public override object eval(Environments.Environment env)
        {
            object condition = Condition().eval(env);
            if (condition is int && (int)condition != Environments.Environment.FALSE)
            {
                return ThenBlock().eval(env);
            }
            else if (numChildren()>2)
            {
                return ElseBlock().eval(env);
            }

            return 0;
        }
    }
}
