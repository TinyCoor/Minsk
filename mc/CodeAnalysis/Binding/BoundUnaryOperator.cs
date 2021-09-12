using Minsk.CodeAnalysis.Syntax;
using System;

namespace mc.CodeAnalysis.Binding
{
    internal sealed class BoundUnaryOperator
    {
        private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind,Type operandType,Type resultType)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            OperandType = operandType;
            ResultType = resultType;
        }

        private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType)
            :this(syntaxKind,kind,operandType,operandType)
        {
        }

        public SyntaxKind SyntaxKind { get; }
        public BoundUnaryOperatorKind Kind { get; }
        public Type OperandType { get; }
        public Type ResultType { get; }

        private static BoundUnaryOperator[] _operators = {
            new BoundUnaryOperator(SyntaxKind.BangToken,BoundUnaryOperatorKind.LogicNegation,typeof(bool)),
            new BoundUnaryOperator(SyntaxKind.BangToken,BoundUnaryOperatorKind.Addition,typeof(int)),
            new BoundUnaryOperator(SyntaxKind.BangToken,BoundUnaryOperatorKind.Negation,typeof(int)),
        };

        public static BoundUnaryOperator Bind(SyntaxKind syntaxKind,Type operandType)
        {
            foreach ( var op in _operators)
            {
                if(op.SyntaxKind==syntaxKind && op.OperandType == operandType)
                {
                    return op;
                }
            }
            return null;
        }
    }
}
