using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptIn14Days.ASTrees;

namespace ScriptIn14Days.src
{
    /// <summary>
    /// create all parser
    /// </summary>
    class BasicParser
    {
        #region Variables

        HashSet<string> reserved;
        Parser.Operators Ops;

        Parser expr0;
        Parser PrimaryBasic;
        Parser Primary;
        Parser Factor;
        Parser Expr;
        Parser statement0;
        Parser Block;
        Parser simple;
        Parser Statement;
        Parser Program;

        //function 
        Parser Args;
        Parser Postfix;
        Parser Func;

        Parser Invoke;

        #endregion
        public BasicParser()
        {
            //create parser
       
            //reserved ids
            reserved = new HashSet<string>();
            //operator definitions
            Ops = new Parser.Operators();

            /*fill program data*/
            reserved.Add(";");
            reserved.Add(Token.EOL);
            reserved.Add("}");

            Ops.add("=", 1, false); //Parser.Operators.RIGHT//
            Ops.add("==", 2, true);//Parser.Operators.LEFT
            Ops.add(">", 2, true);
            Ops.add("<", 2, true);
            Ops.add("++", 5, true);//not well implement
            Ops.add("--", 5, true);//not well implement
            Ops.add("+", 3, true);
            Ops.add("-", 3, true);
            Ops.add("*", 4, true);
            Ops.add("/", 4, true);
            Ops.add("%", 4, true);
            //language statement

            //null tree
            Expr = Parser.rule();
            Args = Parser.rule(typeof(ArgsStmnt));
            Postfix = Parser.rule().addSkip(new string[] { "(" })
                .option(Parser.rule().addAst(Args))
                .addSkip(new string[] { ")" });
            Func = Parser.rule(typeof(FunStmnt));

            Primary = Parser.rule(typeof(PrimaryStmnt)).or(new Parser[]{
                Parser.rule().addSkip(new string[]{"("}).addAst(Expr).addSkip(new string[]{")"}),
                Parser.rule().addNumber(),
                Parser.rule().addString(),
                Parser.rule().addId(reserved)
            }).option(Postfix);

            //Primary = Parser.rule().addAst(PrimaryBasic).option(Postfix);

            Factor = Parser.rule().or(
                new Parser[]{
                    Func,//check Func first
                    Parser.rule().addSkip(new string[]{"-"}).addAst(Primary),
                    Parser.rule().addAst(Primary),
                });


            
            Expr.addExpr(typeof(BinaryExpression),Factor, Ops);// = Parser.rule().addExpr(Factor, Ops);

            Args.addAst(Expr).repeat(Parser.rule().addSkip(new string[] { "," }).addAst(Expr));

            Statement = Parser.rule();

            Block = Parser.rule(typeof(BlockStmnt))
                .addSkip(new string[]{"{"})
                .repeat(Parser.rule().option(Statement).addSkip(new string[] { ";", Token.EOL }))
                .addSkip(new string[] { "}"});

            simple = Parser.rule().addAst(Expr).addSkip(new string[] { ";", Token.EOL });

            Func.addSkip(new string[] { "fun" })
                .addAst(Primary).addAst(Block);

            Invoke = Parser.rule(typeof(InvokeStmnt));
            Invoke.addSkip(new string[] { "invoke" })
                .addSkip(new string[] { "(" })
                .addAst(Args)
                .addSkip(new string[] { ")" });

            Statement.or(new Parser[]{
                //function 
                Func,
                Invoke,
                //if else statement
                Parser.rule(typeof(IfStmnt)).addSkip(new string[] { "if" })
                .addAst(Expr)
                .addAst(Parser.rule().addAst(Block))
                .option(Parser.rule().addSkip(new string[]{"else"}).addAst(Block)),
                //while statement
                Parser.rule(typeof(WhileStmnt)).addSkip(new string[]{"while"}).addAst(Expr).addAst(Block),
                //block
                Parser.rule().addAst(Block),
                //simple statement
                simple
            });

            
            //single statement
            Program = Parser.rule().option(Statement).addSkip(new string[] { ";", Token.EOL });
        }

        public ASTree Parse(Lexer lexer)
        {
            return Program.Parse(lexer);
        }
    }
}
