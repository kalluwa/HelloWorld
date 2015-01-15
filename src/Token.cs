using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptIn14Days.src
{
    
    public class Token
    {
        //special token
        public static Token EOF = new Token(-1) { }; //end of file
        public static string EOL = "\\n"; //end of line

        protected int LineNumber;

        protected Token(int line)
        {
            LineNumber = line;
        }

        #region Method

        public int getLineNumber() { return LineNumber; }
        public virtual bool isIdentifier() { return false; }
        public virtual bool isNumber() { return false; }
        public virtual bool isString() { return false; }
        public virtual int getNumber() { throw new StoneException("Not Number Token! Can't convert it to Number"); }
        public virtual string getText() { return "TEXT"; }

        #endregion
    }

    public class IdToken : Token
    {
        public string IDentifier;

        public IdToken(int lineNo, string id)
            :base(lineNo)
        {
            IDentifier = id;
        }

        public override bool isIdentifier()
        {
            return true;
        }
		
		public override string getText ()
		{
			return IDentifier;
		}
		
        public override string ToString()
        {
            return "【line:"+LineNumber.ToString()+" ID: " + IDentifier+"】";
        }
    }

    public class StringToken : Token
    {
        public string Text;

        public StringToken(int lineNo, string text)
            : base(lineNo)
        {
            Text =text;
        }

        public override bool isString()
        {
            return true;
        }
		
		public override string getText ()
		{
			return Text;
		}
		
        public override string ToString()
        {
            return "【line:" + LineNumber.ToString() + " text: " + Text+"】";
        }
    }

    public class NumToken : Token
    {
        public int Number;

        public NumToken(int lineNo, int num)
            : base(lineNo)
        {
            Number = num;
        }

        public override bool isNumber()
        {
            return true;
        }

        public override int getNumber()
        {
            return Number;
        }
		public override string getText ()
		{
			return Number.ToString();
		}

        public override string ToString()
        {
            return "【line:" + LineNumber.ToString() + " number: " + Number+"】";
        }
    }
}
