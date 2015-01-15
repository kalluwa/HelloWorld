using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptIn14Days.src;
using System.Reflection;

namespace ScriptIn14Days.ASTrees
{
    //this class is base class of any ASTree
    public abstract class Element
    {
        /// <summary>
        /// parse lexer current element adn add it to exist ASTree
        /// </summary>
        /// <param name="lexer"></param>
        /// <param name="tree"></param>
        public abstract void Parse(Lexer lexer, List<ASTree> tree);

        /// <summary>
        /// is the lexer token match this element?
        /// </summary>
        /// <param name="lexer"></param>
        /// <returns></returns>
        public abstract bool Match(Lexer lexer);
    }
    /// <summary>
    /// this class is used to parse tokens to basic sentence
    /// </summary>
    public class Parser
    {
        #region Basic of Basic Element

        /// <summary>
        /// find a number token
        /// </summary>
        class NumToken : Element
        {
            public override void Parse(Lexer lexer, List<ASTree> tree)
            {

                if (Match(lexer))
                {
                    //Console.WriteLine("Match Num"+t.getText());
                    tree.Add(new NumberLiteral(lexer.read()));
                }
            }

            public override bool Match(Lexer lexer)
            {
                return lexer.peek(0).isNumber();
            }
        }
        /// <summary>
        /// find a string token
        /// </summary>
        class StrToken : Element
        {
            public override void Parse(Lexer lexer, List<ASTree> tree)
            {
                //Token t = lexer.read();
                if (Match(lexer))
                tree.Add(new StringLiteral(lexer.read()));
            }

            public override bool Match(Lexer lexer)
            {
                return lexer.peek(0).isString();
            }
        }

        /// <summary>
        /// find a id name
        /// </summary>
        class IdToken : Element
        {
            /// <summary>
            /// store exist ids
            /// </summary>
            HashSet<string> reserved;

            public IdToken(HashSet<string> r)
            {
                reserved = r;
            }

            public override void Parse(Lexer lexer, List<ASTree> tree)
            {
                //oken t = lexer.read();
                if (Match(lexer))
                tree.Add(new Name(lexer.read()));
            }

            public override bool Match(Lexer lexer)
            {
                Token t = lexer.peek(0);
                return t.isIdentifier() && !reserved.Contains(t.getText());
            }
        }

        /// <summary>
        /// find a skip[like  '{' '}']
        /// </summary>
        class Leaf : Element
        {
            /// <summary>
            /// contain what may act like skip token
            /// 
             /// </summary>
            string[] tokens;

            public Leaf(string[] ts)
            {
                tokens = ts;
            }


            public override void Parse(Lexer lexer, List<ASTree> tree)
            {
                if (Match(lexer))
                //we won't add it to the ASTree in skip
                //tree.Add(new NumberLiteral(lexer.read()));
                {
                    Token t = lexer.read();
                    //doesn't add it
                    //so we do nothing here in skip
                    addNode(tree, t);
                }
            }

            protected virtual void addNode(List<ASTree> tree,Token t)
            {
                tree.Add(new ASTLeaf(t));
            }
            public override bool Match(Lexer lexer)
            {
                Token t = lexer.peek(0);
                if(t.isIdentifier())
                    for (int i = 0; i < tokens.Length; i++)
                    {
                        if (tokens[i] == t.getText())
                            return true;
                    }

                return false;
            }
        }

        class Skip : Leaf
        {
            public Skip(string[] seps)
                :base(seps)
            {
            }
            protected override void addNode(List<ASTree> tree, Token t)
            {
                //skip this 'add'
            }
        }

        /// <summary>
        /// record single operator's attribute
        /// </summary>
        public class Precedence
        {
            public int value;//==?-?/?*?==?....
            public bool leftAssoc;// =:right ohters:left
            public Precedence(int v, bool left)
            {
                value = v;
                leftAssoc = left;
            }
        }
        /// <summary>
        /// Operators
        /// </summary>
        public class Operators 
        {
            Dictionary<string, Precedence> ops;

            public static bool LEFT = true;
            public static bool RIGHT = false;

            /// <summary>
            /// add a operator
            /// </summary>
            /// <param name="name">operator symbol</param>
            /// <param name="prec">priority???</param>
            /// <param name="left">calculation order</param>
            public void add(string name, int prec, bool left)
            {
                if (ops == null)
                    ops = new Dictionary<string, Precedence>();

                ops.Add(name,new Precedence(prec,left));
            }

            public Precedence get(string name)
            {
                if (!ops.Keys.Contains(name))
                    return null;

                return ops[name];
            }
        }
        #endregion

        #region Or/Repeat sentence

        /// <summary>
        /// abstract tree
        /// </summary>
        public class Tree : Element
        {
            protected Parser parser;

            public Tree(Parser p) { parser = p; }

            public override void Parse(Lexer lexer, List<ASTree> tree)
            {
                //add sub tree
                if(Match(lexer))
                    tree.Add(parser.Parse(lexer));
            }

            public override bool Match(Lexer lexer)
            {
                return parser.Match(lexer);
            }
        }

        public class OrTree : Element
        {
            //has more than one choice
            List<Parser> parsers;
            public OrTree(Parser[] ps) { parsers = ps.ToList(); }
            public OrTree(List<Parser> ps) { parsers = ps; }
            public OrTree(Parser p) 
            { 
                if(parsers==null)
                    parsers = new List<Parser>();

                parsers.Add(p);
            }
            public override void Parse(Lexer lexer, List<ASTree> tree)
            {
                //find the first most fitted one and parse it
                Parser parser = ChooseParser(lexer);
                if (parser == null)
                {
                    //Console.WriteLine("find no parser matched in orTree");
                    //throw new StoneException("find no parser matched in orTree");
                }
                else
                {
                    ASTree t = parser.Parse(lexer);
                    tree.Add(t);
                    //Console.WriteLine(t.ToString());
                }
            }

            public override bool Match(Lexer lexer)
            {
                return ChooseParser(lexer) != null;
            }
            public Parser ChooseParser(Lexer lexer)
            {
                for (int i = 0; i < parsers.Count; i++)
                {
                    if (parsers[i].Match(lexer))
                        return parsers[i];
                }

                return null;
            }

            public void InsertChoice(Parser parser)
            {
                parsers.Add(parser);
            }
        }

        /// <summary>
        /// repeat Tree
        /// </summary>
        public class RepeatTree : Element
        {
            Parser parser;
            bool onlyOnce;

            public RepeatTree(Parser p, bool _onlyOnce)
            {
                parser = p;
                onlyOnce = _onlyOnce;
            }

            public override void Parse(Lexer lexer, List<ASTree> tree)
            {

                //Console.WriteLine("Match Repeat");
                while(parser.Match(lexer))
                {
                    //read this and move to next
                    ASTList t = (ASTList)parser.Parse(lexer);
                    //if(!(t is ASTList)||t.numChildren()>0)
                    tree.Add(t);

                    if (onlyOnce)
                        break;
                }
            }

            public override bool Match(Lexer lexer)
            {
                return parser.Match(lexer);
            }
        }

        #endregion

        #region Advanced Sentence

        /// <summary>
        /// expression statement
        /// </summary>
        public class Expr : Element
        {
            //factor {op factor}
            public Operators ops;
            Parser factor;

            public Expr(Parser p,Operators op)
            {
                //set pattern
                factor = p;
                
                ops = op;

                typeForBinary = null;
            }

            Type typeForBinary;

            public Expr(Type typeFromASTList, Parser p, Operators op)
            {
                typeForBinary = typeFromASTList;
                factor = p;
                ops = op;
            }
            public override void Parse(Lexer lexer, List<ASTree> tree)
            {
                ASTree right = factor.Parse(lexer);
                Precedence prec;
                while ((prec = nextOperator(lexer)) != null)
                {
                    //continue to parse

                    //read an operator and identifier/other token
                    right = doShift(lexer, right, prec.value);
                }

                tree.Add(right);
            }
            /// <summary>
            /// read an operator and identifier/other token
            /// </summary>
            /// <param name="lexer"></param>
            /// <param name="right"></param>
            /// <param name="prec"></param>
            /// <returns></returns>
            private ASTree doShift(Lexer lexer, ASTree left, int prec)
            {
                List<ASTree> list = new List<ASTree>();
                list.Add(left);//add left factor
                Token op = lexer.read();
                if (op.getText().Equals("++") || op.getText().Equals("--"))
                {
                    //add 
                    //left = left +/- 1
                    List<ASTree> right = new List<ASTree>();
                    right.Add(left);
                    right.Add(new StringLiteral(new StringToken(op.getLineNumber(), op.getText().Equals("++") ? "+" : "-")));
                    right.Add(new NumberLiteral(new ScriptIn14Days.src.NumToken(op.getLineNumber(), 1)));

                    
                    list.Add(new StringLiteral(new StringToken(op.getLineNumber(),"=")));

                    ConstructorInfo ctor = typeForBinary.GetConstructor(new[] { typeof(List<ASTree>) });
                    ASTList rightNode = (ASTList)ctor.Invoke(new object[] { right });
                    list.Add(rightNode);
                
                }
                else
                {
                    list.Add(new ASTLeaf(op));//add operator
                    ASTree right = factor.Parse(lexer);//read next factor

                    Precedence next;
                    while ((next = nextOperator(lexer)) != null &&
                        rightIsExpr(prec, next))
                        right = doShift(lexer, right, next.value);

                    list.Add(right);
                }
                ASTList node ;
                if (typeForBinary == null)
                    node = new ASTList(list);
                else
                {
                    ConstructorInfo ctor = typeForBinary.GetConstructor(new[]{typeof(List<ASTree>)});
                    node = (ASTList)ctor.Invoke(new object[] { list });
                }
                return node;
            }

            /// <summary>
            /// this class is used to seprate */  from +-
            /// like    : 3+4*5-4
            /// priority:  1 2 1
            /// it means that 4*5 is an expression
            /// </summary>
            /// <param name="prec"></param>
            /// <param name="next"></param>
            /// <returns></returns>
            private bool rightIsExpr(int prec, Precedence next)
            {
                if (next.leftAssoc)// like * / + -
                    return prec < next.value;
                else // only =
                    return prec <= next.value;
            }

            /// <summary>
            /// test next token is a operator
            /// </summary>
            /// <param name="lexer"></param>
            /// <returns></returns>
            private Precedence nextOperator(Lexer lexer)
            {
                Token t = lexer.peek(0);
                if (t.isIdentifier())
                    return ops.get(t.getText());
                else
                    return null;
            }

            /// <summary>
            /// match>> factor{ op factor} >>
            /// if factor match then return success
            /// </summary>
            /// <param name="lexer"></param>
            /// <returns></returns>
            public override bool Match(Lexer lexer)
            {
                return factor.Match(lexer);
            }
        }
        #endregion

        #region Variables and internal functions
        
        protected List<Element> elements= new List<Element>();

        //constructor
        public Parser()
        {
            reset();
        }

        Type astTreeType;

        public Parser(Type type)
        {
            //type : 
            //TODO: how to judge type is ASTList
            //if ((type is ASTList))
            //    throw new StoneException("Error in create Parser,Type Error");
            reset();
            astTreeType = type;

        }
        protected Parser(Parser p)
        {
            elements = p.elements;
        }

        public Parser reset()
        {
            elements.Clear();
            astTreeType = null;
            return this;
        }

        //important two functions
        public ASTree Parse(Lexer lexer)
        {
            List<ASTree> tree = new List<ASTree>();
            //add node to tree

            int currentLine = 0;
            for (int i = 0; i < elements.Count; i++)
            {
                if(lexer.peek(0).getLineNumber()>=0)
                    currentLine = lexer.peek(0).getLineNumber();
                elements[i].Parse(lexer, tree);
            }

            if (tree.Count == 0)
                throw new StoneException("Parse Error! at line " + currentLine.ToString());
            //make them to children of new tree
            ASTList root ;
            if (null == astTreeType)
                if (tree.Count > 1 || tree[0] is ASTLeaf)
                    root = new ASTList(tree);
                else
                    root = (ASTList)tree[0];
            else
            {
                if (tree.Count > 1 || tree[0] is ASTLeaf)
                {
                    ConstructorInfo ctor = astTreeType.GetConstructor(new[] { typeof(List<ASTree>) });
                    root = (ASTList)ctor.Invoke(new object[] { tree });
                }
                else
                {
                    
                    ConstructorInfo ctor = astTreeType.GetConstructor(new[] { typeof(List<ASTree>) });
                    if(tree[0] is BinaryExpression)
                        root = (ASTList)ctor.Invoke(new object[] { tree});
                    else 
                        root = (ASTList)ctor.Invoke(new object[] { tree[0].children() });
                }
            }

            return root;
        }

        public bool Match(Lexer lexer)
        {
            if(elements.Count<=0)
                return true;
            else
            {
                return elements[0].Match(lexer);
            }
        }


        public static Parser rule()
        {
            return new Parser();
        }
        public static Parser rule(Type typeFromASTList)
        {
            return new Parser(typeFromASTList);
        }
        #endregion

        #region quick add functions

        public Parser addNumber()
        {
            elements.Add(new NumToken());
            return this;
        }

        public Parser addString()
        {
            elements.Add(new StrToken());
            return this;
        }

        public Parser addId(HashSet<string> reserved)
        {
            elements.Add(new IdToken(reserved));
            return this;
        }

        //operator like ' . ; .,
        //add specified token

        //should exist in code
        //but will be added to ASTree
        public Parser addToken(string[] pat)
        {
            elements.Add(new Leaf(pat));
            return this;
        }

        //should exist in code
        //but won't be added to ASTree
        public Parser addSkip(string[] pat)
        {
            elements.Add(new Skip(pat));
            return this;
        }

        //add a new node
        public Parser addAst(Parser p)
        {
            elements.Add(new Tree(p));
            return this;
        }

        public Parser addExpr(Parser p,Operators ops)
        {
            elements.Add(new Expr(p, ops));
            return this;
        }

        public Parser addExpr(Type t,Parser p, Operators ops)
        {
            elements.Add(new Expr(t,p, ops));
            return this;
        }
        //or choice
        public Parser or(Parser[] p)
        {
            elements.Add(new OrTree(p));
            return this;
        }

        public Parser or(Parser p1,Parser p2)
        {
            Parser[] p = { p1, p2 };
            elements.Add(new OrTree(p));
            return this;
        }

        public Parser maybe(Parser p)
        {
            //empty or a way
            Parser[] parsers= { new Parser(),p};

            elements.Add(new OrTree(parsers));
            return this;
        }

        public Parser repeat(Parser p)
        {
            //one or more
            elements.Add(new RepeatTree(p,false));

            return this;
        }

        //0~1 times
        public Parser option(Parser p)
        {
            elements.Add(new RepeatTree(p, true));
            return this;
        }

        public Parser insertChoice(Parser p)
        {
            Element e = elements[0];

            if (e is OrTree)
            {
                ((OrTree)e).InsertChoice(p);
            }
            else
            {
                Parser otherwise = new Parser(this);
                reset();//clear elements
                or(otherwise, p);//elements.add(or(1,2))  numElements = 1
            }

            return this;
        }
        #endregion

        #region Debug

        /// <summary>
        /// print ast tree for debug
        /// </summary>
        /// <param name="root"></param>
        public static void PrintASTree(ASTree root)
        {
            if(root!=null)
            {
                Console.WriteLine(root.ToString());

                //if (root.numChildren() != 0)
                //{
                //    for(int i=0;i<root.numChildren();i++)
                //        PrintASTree(root.child(i));
                //}
            }
        }
        #endregion
    }
}
