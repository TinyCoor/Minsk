using System.Collections.Generic;

namespace Minsk.CodeAnalysis
{
   public sealed class LiteralExpressionSyntax : ExpressionSyntax
{
    public LiteralExpressionSyntax(SyntaxToken numberToken)
    {
        LiteralToken= numberToken;
    }

    public override IEnumerable<SyntaxNode> GetChildren(){

        yield return LiteralToken;
    }

    public override SyntaxKind Kind => SyntaxKind.NumberExpression;
    public SyntaxToken LiteralToken { get; }
}
    
}