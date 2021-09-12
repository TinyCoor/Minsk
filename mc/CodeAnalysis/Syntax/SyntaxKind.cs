namespace Minsk.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        //Tokens 
        BadToken,
        EndOfFileToken,
        NumberToken,
        WhiteSpaceToken,
        PlusToken,
        MinusToken,
        SlashToken,
        StarToken,
        OpenParenthesisToken,
        CloseParenthesisToken,

        //expression
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
     
    }
}