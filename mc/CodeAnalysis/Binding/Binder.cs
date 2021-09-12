﻿using Minsk.CodeAnalysis.Syntax;
using System;
using System.Collections.Generic;

namespace mc.CodeAnalysis.Binding
{
    internal sealed class Binder
    {
        private readonly List<string> _diagnostics = new List<string>();

        public IEnumerable<string> Diagnostices =>  _diagnostics;
        public BoundExpression BindExpression(ExpressionSyntax syntax)
        {
            switch (syntax.Kind)
            {
                case SyntaxKind.LiteralExpression:
                    return BindLiteralExpression((LiteralExpressionSyntax)syntax);
                case SyntaxKind.BinaryExpression:
                    return BindBinaryExpression((BinaryExpressionSyntax)syntax);
                case SyntaxKind.UnaryExpression:
                    return BindUnaryExpression((UnaryExpressionSyntax)syntax);

                default:
                    throw new Exception($"Unexpected syntax {syntax.Kind}");
            }
        }

        private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax)
        {

            var value = syntax.Value ?? 0;
            return new BoundLiteralExpression(value);
        }

        private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
        {
            var boundLeft = BindExpression(syntax.Left);
            var boundRight = BindExpression(syntax.Right);
            var boundOperatorKind = BindBinaryOperatorKind(syntax.OperatorToken.Kind,boundLeft.Type,boundRight.Type);
            if (boundOperatorKind == null)
            {
                _diagnostics.Add($"Binary operator syntax `{syntax.OperatorToken.Text}`  is not defined for type {boundLeft.Type} and {boundRight.Type} ");
                return boundLeft;
            }
            return new BoundBinaryExpression(boundLeft,boundOperatorKind.Value, boundRight);

        }

        private BoundBinaryOperatorKind? BindBinaryOperatorKind(SyntaxKind kind,Type leftType,Type rightType)
        {
            if (leftType != typeof(int) || rightType !=typeof(int))
                return null;

            switch (kind)
            {
                case SyntaxKind.PlusToken:
                    return BoundBinaryOperatorKind.Addition;
                case SyntaxKind.MinusToken:
                    return BoundBinaryOperatorKind.Subtraction;
                case SyntaxKind.StarToken:
                    return BoundBinaryOperatorKind.Multiplication;
                case SyntaxKind.SlashToken:
                    return BoundBinaryOperatorKind.Division;

                default:
                    throw new Exception($"Unexpected binary operator {kind}");
            }
        }

        private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
        {
            var boundOperand = BindExpression(syntax.Operand);
            var boundOperatorKind = BindUnaryOperator(syntax.OperatorToken.Kind,boundOperand.Type);
            if(boundOperatorKind == null)
            {
                _diagnostics.Add($"Unary operator syntax `{syntax.OperatorToken.Text}`  is not defined for type {boundOperand.Type}");
                return boundOperand;
            }
            return new BoundUnaryExpression(boundOperatorKind.Value, boundOperand);
        }

        private BoundUnaryOperatorKind? BindUnaryOperator(SyntaxKind kind,Type operandType)
        {
            if (operandType != typeof(int))
                return null;

            switch (kind)
            {
                case SyntaxKind.PlusToken:
                    return BoundUnaryOperatorKind.Addition;
                case SyntaxKind.MinusToken:
                    return BoundUnaryOperatorKind.Negation;

                default:
                    throw new Exception($"Unexpected unary operator {kind}");

            }
        }
    }
}