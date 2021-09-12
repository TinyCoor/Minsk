namespace Minsk.CodeAnalysis
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
        BinaryExpression,
        ParenthesizedExpression,

    }
}