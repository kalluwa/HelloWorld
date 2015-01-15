using System;
using System.Collections.Generic;
using ScriptIn14Days.src;
using Environment = ScriptIn14Days.Environments.Environment;
namespace ScriptIn14Days.ASTrees
{
	public class ASTList : ASTree
	{
		protected List<ASTree> Children;
		
		public ASTList (List<ASTree> list)
		{
			Children = list;
		}
		
		public override string ToString ()
		{
			System.Text.StringBuilder sb =new System.Text.StringBuilder();
			sb.Append("(");
			string sep = "";
			for (int i = 0; i < Children.Count; i++) {
				sb.Append(sep);
				sep=" ";
				sb.Append(Children[i].ToString());
			}
			sb.Append(")");
			return sb.ToString();
		}
		/// <summary>
		/// interface implements
		/// </summary>
		public ASTree child (int i)
		{
			return Children[i];
		}
		
		public List<ASTree> children()
		{
			return Children;
		}
		
		public int numChildren ()
		{
			return Children.Count;
		}
		
		/// <summary>
		/// Location this node's first leaf's Token's linenumber.
		/// </summary>
		public string location ()
		{
			foreach (var child in Children) {
				string s = child.location();
				if(!string.IsNullOrEmpty(s))
					return s;
			}
			return null;
		}

        public virtual object eval(Environment env)
        {
            if (numChildren() == 1)
                return child(0).eval(env);
            else
                throw new StoneException("ASTList cannot eval");
        }
	}
}

