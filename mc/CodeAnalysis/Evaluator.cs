using System;
using mc.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis
{
  
    internal class Evaluator
    {
        private  BoundExpression _root { get; }
        public Evaluator(BoundExpression root)
        {
            _root = root;
        }

        public object Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private object EvaluateExpression(BoundExpression node)
        {
            //binary 
            if (node is BoundLiteralExpression n)
            {
                return n.Value;
            }
            if (node is BoundUnaryExpression u)
            {
                var operand =(int)EvaluateExpression(u.Operand);

                if (u.OperatorKind == BoundUnaryOperatorKind.Negation)
                {
                    return -operand;
                }
                else if (u.OperatorKind == BoundUnaryOperatorKind.Addition)
                {
                    return operand;
                }
                else
                    throw new Exception($"Unexpected Unary Operator Token Kind {u.Kind}");
            }
            if (node is BoundBinaryExpression b)
            {
                var left = (int)EvaluateExpression(b.Left);
                var right = (int)EvaluateExpression(b.Right);

                if (b.OperatorKind == BoundBinaryOperatorKind.Addition)
                    return left + right;

                else if (b.OperatorKind == BoundBinaryOperatorKind.Subtraction)
                    return left - right;

                else if (b.OperatorKind== BoundBinaryOperatorKind.Multiplication)
                    return left * right;

                else if (b.OperatorKind == BoundBinaryOperatorKind.Division)
                    return left / right;

                else
                    throw new Exception($"Unexpected binary Operator");
            }

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
    
}