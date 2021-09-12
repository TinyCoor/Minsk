﻿using Minsk.CodeAnalysis.Syntax;
using System;

namespace mc.CodeAnalysis.Binding
{
    internal sealed class BoundBinaryOperator
    {
        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type leftType,Type rightType, Type resultType)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            LeftType = leftType;
            RightType = rightType;
            Type = resultType;
        }
    
        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind operandType, Type type)
            :this(syntaxKind,operandType,type,type,type)
        {
        }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type operandType,Type resultType)
        : this(syntaxKind, kind, operandType, operandType, resultType)
        {
        }


        public SyntaxKind SyntaxKind { get; }
        public BoundBinaryOperatorKind Kind { get; }
        public Type LeftType { get; }
        public Type RightType { get; }
        public Type Type { get; }

        private static BoundBinaryOperator[] _operators = {
            new BoundBinaryOperator(SyntaxKind.PlusToken,BoundBinaryOperatorKind.Addition,typeof(int)),
            new BoundBinaryOperator(SyntaxKind.MinusToken,BoundBinaryOperatorKind.Subtraction,typeof(int)),
            new BoundBinaryOperator(SyntaxKind.StarToken,BoundBinaryOperatorKind.Multiplication,typeof(int)),
            new BoundBinaryOperator(SyntaxKind.StarToken,BoundBinaryOperatorKind.Division,typeof(int)),
            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken,BoundBinaryOperatorKind.Equals,typeof(int),typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.BangEqualsToken,BoundBinaryOperatorKind.NotEquals,typeof(int),typeof(bool)),

            new BoundBinaryOperator(SyntaxKind.AmpersandAmpersandToken,BoundBinaryOperatorKind.LogicalAnd,typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.PipePipeToken,BoundBinaryOperatorKind.LogicalOr,typeof(bool)),
               new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken,BoundBinaryOperatorKind.Equals,typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.BangEqualsToken,BoundBinaryOperatorKind.NotEquals,typeof(bool)),

        };

        public static BoundBinaryOperator Bind(SyntaxKind syntaxKind, Type letfType,Type rightType)
        {
            foreach (var op in _operators)
            {
                if (op.SyntaxKind == syntaxKind && op.LeftType == letfType && op.RightType == rightType)
                {
                    return op;
                }
            }
            return null;
        }
    }
}
