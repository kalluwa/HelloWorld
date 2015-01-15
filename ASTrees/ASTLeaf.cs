using System;
using System.Collections.Generic;
using ScriptIn14Days.src;

namespace ScriptIn14Days.ASTrees
{
	public class ASTLeaf : ASTree
	{
		private List<ASTree> Empty = new List<ASTree>();
		
		protected Token token;
		
		public ASTLeaf (Token t) {   	token = t;		}
		
		public Token getToken()
		{
			return token;
		}
		
		public override string ToString ()
		{
			return token.getText();//toString
		}
		/// <summary>
		/// interface implements
		/// </summary>
		public ASTree child (int i)
		{
			//no child should exist here
			throw new StoneException("access invalid child at line "+token.getLineNumber().ToString());
		}
		
		public List<ASTree> children ()
		{
			return Empty;
		}
		
		public int numChildren ()
		{
			return 0;
		}
		
		/// <summary>
		/// Location of this node's first leaf's Token.
		/// </summary>
		public string location ()
		{
			return token.getLineNumber().ToString();
		}

        /// <summary>
        /// get leaf value
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public virtual object eval(ScriptIn14Days.Environments.Environment env)
        {
            throw new StoneException("ASTLeaf base class cannot eval,at line "+token.getLineNumber());
        }
	}
}

