using System;
using ScriptIn14Days.src;
using Environment = ScriptIn14Days.Environments.Environment;

namespace ScriptIn14Days.ASTrees
{
	/// <summary>
	/// Number literal.
	/// </summary>
	public class NumberLiteral : ASTLeaf
	{
		public NumberLiteral (Token t)
			:base(t)
		{
		}
		
		public int value(){ return token.getNumber ();}

        public override object eval(Environment env)
        {
            return token.getNumber() ;
        }
	}
	
	public class Name : ASTLeaf
	{
		public Name (Token t)
			:base(t)
		{
		}
		
		public string name(){ return token.getText ();}

        public override object eval(Environment env)
        {
            object ob = env.Get(name());
            if (ob == null)
                throw new StoneException("Can't find ¡¾ " + name()+" ¡¿at line "+token.getLineNumber().ToString());
            else
                return ob; 
        }
	}
	
	public class StringLiteral : ASTLeaf
	{
		public StringLiteral (Token t)
			:base(t)
		{
		}
		
		public string Text(){ return token.getText ();}

        public override object eval(Environment env)
        {
            return token.getText();
        }
	}
	
}

