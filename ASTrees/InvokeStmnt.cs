using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptIn14Days.src;

namespace ScriptIn14Days.ASTrees
{
    class InvokeStmnt : ASTList
    {
        public InvokeStmnt(List<ASTree> list) : base(list) { }

        public string ClassName()
        {
            ASTree node = Children[0];
            while (!(node is ASTLeaf))
                node = node.child(0);

            return node.ToString();
        }

        public string MethodName()
        {
            ASTree node = Children[1];
            while (!(node is ASTLeaf))
                node = node.child(0);

            return node.ToString();
        }

        public object[] Parameters()
        {
            //get num of children
            int numOfChild = numChildren();

            if (numOfChild < 2)
                return null;

            List<object> currentParams = new List<object>();
            for (int i = 2; i < numOfChild; i++)
            {
                ASTree node = Children[i];
                while (!(node is ASTLeaf))
                    node = node.child(0);
                currentParams.Add((((ASTLeaf)node).getToken().isNumber()) ? (object)((ASTLeaf)node).getToken().getNumber() : node.ToString());
            }


            return currentParams.ToArray();
        }
        public override object eval(Environments.Environment env)
        {
            #region special case

            if (ClassName().Equals("Print",StringComparison.CurrentCultureIgnoreCase))
                ScriptIn14Days.Form1.OutputStatic(MethodName());
            else if (ClassName().Equals("Time", StringComparison.CurrentCultureIgnoreCase))
                ScriptIn14Days.Form1.OutputStatic(DateTime.Now.ToString());
            #endregion
            else

                BasicInvoker.InvokeStatic(ClassName(), MethodName(), Parameters());
            
            return "invoke success";
        }
    }
}
