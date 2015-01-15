using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace ScriptIn14Days.src
{
    public class Lexer
    {
        //\s*((//.*)|([0-9]+)|(\"(\"|\\\\|\\n|[^\"])*\")|[A-Z_a-z][A-Z_a-z0-9]*|==|<=|>=|&&|\|\||\p{Punct})?
        /// <summary>
        ///        format[used in stone]    in string[used in java]
        /// space:   \s*   (actual format)
        ///          \\s*----------------------->\\s*
        /// comment: \\.*  (actual format)
        ///          \\.*----------------------->\\\\.*
        /// number:  [0-9]+ -------------------->the same
        /// string:  "\"|\\"|\n|[^"]*"   （------represent in stone
        ///          "\\"|\\\\"|\\n|[^"]*"------>"\\\\"|\\\\\\\\"|\\\\n|[^"]*"
        /// identifier:
        ///          [a-z_A-Z][a-z_A-Z0-9]*
        /// special symbols:        
        ///         ==
        ///         !=
        ///         &&
        ///         ||--------->\|\|-------->\\|\\|
        ///         >=
        ///         <=
        ///         \p{Punct}--------------->\\p{Punct}
        ///  
        /// </summary>
        public static string regexPat = "\\s*" +
            "(" +
                "(//.*)|" +
                "([0-9]+)|" +
                "(\"(" + "\\\\\"|\\\\\\\\\"|\\\\n|[^\"]*" + ")*\")|" +
                "([a-z_A-Z][a-z_A-Z0-9]*)|" +
                "(==|!=|<=|>=|&&|\\|\\||<|>|=|\\*|/|\\+\\+|\\-\\-|\\+|\\-|\\{|\\}|,|;|\\)|\\(|\\||\\&|\\%|\\^|\\$|\\#|\\@|\\!|\\~|\\\\)" +
             ")?";
        //public static string regexPat = @"\s*((//.*)|([0-9]+)|[a-z_A-Z][a-z_A-Z0-9]*|==|!=|<=|>=|&&|(\p{P}))";
        //public static string regexPat = @"\s*((//.*)|([0-9]+))";
        Regex RegexPattern = new Regex(regexPat);
        Queue<Token> Tokens = new Queue<Token>();
        bool HasMore;
        LineNumberReader Reader;

        public Lexer(string source)
        {
            HasMore = true;
            source=source.Replace("\r\n", "\n");
            Reader = new LineNumberReader(source);
			//if(Tokens.Count>0)
			//	Tokens.Clear();
        }

        /// <summary>
        /// read a token and remove it 
        /// </summary>
        /// <returns></returns>
        public Token read()
        {
            if (fillQueue(0))
            {
                return Tokens.Dequeue();
            }
            else
            {
                return Token.EOF;
            }
        }

        public Token peek(int index)
        {
            if (fillQueue(index))
            {
                return Tokens.ElementAt(index);
            }

            return Token.EOF;
        }

        public bool fillQueue(int i)
        {
            while (i >= Tokens.Count)
            {
                if (HasMore)
                    readLine();
                else
                    return false;
            }

            return true;
        }

        public void readLine()
        {
            string line;
            line = Reader.ReadLine();

            if (line == "eof")
            {
                HasMore = false;
                return;
            }

            int lineNo = Reader.GetCurrentLine();
            
            MatchCollection matches = RegexPattern.Matches(line);

            int pos = 0;
            int length = line.Length;
            while (pos < line.Length)
            {
                Match match=RegexPattern.Match(line,pos,length);
                if (match.Success)
                {
                    //Console.Write(match.Value+"#");
                    
                    addToken(lineNo, match);
                    pos = match.Index + match.Length;
                    length = line.Length - pos;
                    continue;
                }
                break;
            }
            
            //Tokens.Enqueue(new IdToken(lineNo,Token.EOL));

            //OutputToken();
        }

        public void addToken(int lineNumber, Match match)
        {
            //foreach (string name in RegexPattern.GetGroupNames())
            //{
            //    Console.WriteLine("  capture group {0} value is:{1}", name, match.Groups[name].Value);
            //}
            //return;
            string m=match.Groups[1].Value;//space
            Token token = Token.EOF;
            if (!string.IsNullOrEmpty(m)) //if not space
            {
                if (string.IsNullOrEmpty(match.Groups[2].Value))//not a comment
                {
                    
                    if (!string.IsNullOrEmpty(match.Groups[3].Value))//number
                        token = new NumToken(lineNumber, Int32.Parse(m));
                     if (!string.IsNullOrEmpty(match.Groups[4].Value))//string
                        token = new StringToken(lineNumber, m.Replace("\"",""));
                     if (!string.IsNullOrEmpty(match.Groups[6].Value))//id
                        token = new IdToken(lineNumber, m);
                     if (!string.IsNullOrEmpty(match.Groups[7].Value))//symbol
                        token = new IdToken(lineNumber, m);
                    
                    if(token!=Token.EOF)
                        Tokens.Enqueue(token);
                }
            }

        }

        public void OutputToken()
        {
            for (int i = 0; i < Tokens.Count; i++)
            {
                Console.Write("@" + Tokens.ElementAt(i).ToString());
            }
        }


    }
}
