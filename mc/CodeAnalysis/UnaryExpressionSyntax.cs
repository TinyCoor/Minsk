using System.Collections.Generic;

namespace Minsk.CodeAnalysis
{
    public sealed class UnaryExpressionSyntax : ExpressionSyntax
    {
        public UnaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax operand)
        {
            OperatorToken = operatorToken;
            Operand = operand;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OperatorToken;
            yield return Operand;

        }

        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;
        public ExpressionSyntax Left { get; }
        public ExpressionSyntax Right { get; }
        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Operand { get; }
    }

}