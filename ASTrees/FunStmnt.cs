using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptIn14Days.Environments;

namespace ScriptIn14Days.ASTrees
{
    class FunStmnt : ASTList
    {
        NestedEnv localEnv;

        public FunStmnt(List<ASTree> list) : base(list) { localEnv = new NestedEnv(); }

        public ASTree FunName()
        {
            return Children[0].child(0);

        }
        public List<ASTree> ParamList()
        {
            return Children[0].child(1).children();
        }

        public int ParamCount()
        {
            return Children[0].child(1).children().Count;
        }

        public BlockStmnt Body()
        {
            return (BlockStmnt)Children[1];
        }

        public override string ToString()
        {
            return "Fun " + FunName().ToString() + "( " + Children[0].child(1).ToString() + " )" +
                Body().ToString();
        }

        public override object eval(Environments.Environment env)
        {
            //store this function
            env.Add(FunName().ToString().Replace("(", "").Replace(")", ""), this);
            
            localEnv.setOuter(env);
            //set local input value
            for (int i = 0; i < ParamCount(); i++)
            {
                //get leaf
                ASTree node = Children[0].child(1).child(i);
                while (!(node is ASTLeaf))
                    node = node.child(0);

                ScriptIn14Days.src.Token t=((ASTLeaf)node).getToken();
                if (!(t.isIdentifier()))
                {
                    throw new ScriptIn14Days.src.StoneException("Function Parameter Error at Line " +
                        t.getLineNumber());
                }
                localEnv.Add(t.getText(), 0);
            }
            //run in local env

            return Body().eval(localEnv);
        }

        public object call(Environments.Environment env,List<object> pas)
        {
            localEnv.setOuter(env);

            //set local input value
            for (int i = 0; i < pas.Count; i++)
            {
                //get leaf
                ASTree node = Children[0].child(1).child(i);
                while (!(node is ASTLeaf))
                    node = node.child(0);

                localEnv.Add(((ASTLeaf)node).getToken().getText(), pas[i]);
            }
            //run in local env

            return Body().eval(localEnv);
        }
    }
}
