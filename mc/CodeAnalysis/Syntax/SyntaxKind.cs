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
        IdentifierToken,


        //expression
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,

        //key word
        TrueKeyWord,
        FalseKeyWord,
    }
}