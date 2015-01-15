using System;
using System.Collections.Generic;
using ScriptIn14Days.src;

namespace ScriptIn14Days.ASTrees
{
	public class BinaryExpression : ASTList
	{
		public BinaryExpression(List<ASTree> tree)
				:base(tree)
		{
				
		}
		
		/// <summary>
		/// Left tree
		/// </summary>
		public ASTree left()
		{
			return Children[0];
		}
		
		/// <summary>
		/// Right tree
		/// </summary>
		public ASTree right()
		{
			return Children[2];
		}
		
		/// <summary>
		/// Operator
		/// </summary>
		public String Op()
		{
			return ((ASTLeaf)Children[1]).getToken().getText();
		}

        public override string ToString()
        {
            return "["+ left().ToString()+" OP["+Op()+"] "+right().ToString()+"]";
        }

        public override object eval(Environments.Environment env)
        {
            string op = Op();
            if (op == "=")
            {
                object rightValue;
                if (right() is FunStmnt)
                {
                    //closure function
                    rightValue = right();
                }
                else
                {
                    //1 like: sum=3;
                    rightValue = right().eval(env);
                }
                return computAssign(env, rightValue);
            }
            else
            {
                //2 like: sum * sum+1
                object leftValue = left().eval(env);
                object rightValue = right().eval(env);

                return computeOp(leftValue,op, rightValue);
            }
        }

        private object computeOp(object leftValue, string op, object rightValue)
        {
            //if all are numbers
            if (leftValue is int && rightValue is int)//TODO:change accompany with Token getNumber
            {
                return ComputNumber((int)leftValue, op, (int)rightValue);
            }
            else
            {
                if (string.Equals("+", op))
                    return leftValue.ToString() + rightValue.ToString();
                else if (string.Equals("==", op))
                {
                    if (leftValue == null)
                        return rightValue == null ? true : false;
                    else
                        return leftValue.Equals(rightValue);

                }
                else
                    throw new StoneException("Unknown Operator for Text operation");
            }

        }

        private object ComputNumber(int l, string op, int r)
        {
            if (op.Equals("+"))
                return l + r;
            else if (op.Equals("-"))
                return l - r;
            else if (op.Equals("*"))
                return l * r;
            else if (op.Equals("/"))
            {
                if (r == 0)
                    throw new StoneException("Cannot divide Zero");
                return l / r;
            }
            else if (op.Equals("%"))
                return l % r;
            else if (op.Equals("=="))
                return l == r ? Environments.Environment.TRUE :
                    Environments.Environment.FALSE;
            else if (op.Equals(">"))
                return l > r ? Environments.Environment.TRUE :
                    Environments.Environment.FALSE;
            else if (op.Equals("<"))
                return l < r ? Environments.Environment.TRUE :
                    Environments.Environment.FALSE;

            else
                throw new StoneException("UnHandled Operator for Number");
        }

        private object computAssign(Environments.Environment env, object rightValue)
        {
            ASTree l=left();
            
            //in order to remove extra bracket
            //like this:  (k)=3  error
            //  ------->   k =3  pass
            while (!(l is ASTLeaf) && l.numChildren()>0)
                l = l.child(0);

            if (l is Name)
            {
                env.Add(((Name)l).name(), rightValue);
                return rightValue;
            }

            throw new StoneException("assign error");

        }
	}
}

