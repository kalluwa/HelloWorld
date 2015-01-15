using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptIn14Days.ASTrees
{
    class PrimaryStmnt : ASTList
    {
        public PrimaryStmnt(List<ASTree> list) : base(list) { }

        /// <summary>
        /// main part
        /// </summary>
        /// <returns></returns>
        public ASTree MainPart()
        {
            return Children[0];
        }
        /// <summary>
        /// parameters
        /// </summary>
        /// <returns></returns>
        public bool HasPostfix()
        {
            if (Children.Count > 1)
                return true;

            return false;
        }
        //postfix
        public ASTree Postfix(int i)
        {
            return Children[1].child(i);
        }

        public int PostfixCount()
        {
            return Children[1].numChildren();
        }

        /// <summary>
        /// run function or get expression result
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public override object eval(Environments.Environment env)
        {
            if (HasPostfix())
            {
                //get leaf
                ASTree node = Children[0];
                while (!(node is ASTLeaf))
                    node = node.child(0);
                //get params
                List<object> objs = new List<object>();
                if (String.IsNullOrEmpty(Postfix(0).ToString().Replace("(", "").Replace(")", "")))
                {
                }
                else
                {
                    for (int i = 0; i < PostfixCount(); i++)
                    {
                        objs.Add(Postfix(i).eval(env)); // "func()" call eval will trigger a exception  
                    }
                }
                //run function
                return ((FunStmnt)env.Get(((ASTLeaf)node).getToken().getText())).call(env,objs);
            }
            else
                return base.eval(env);
        }
    }
}
