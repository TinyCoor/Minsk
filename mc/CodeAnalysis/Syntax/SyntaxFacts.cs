using System;

namespace Minsk.CodeAnalysis.Syntax
{
    internal static class SyntaxFacts
    {
        public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 1;
                case SyntaxKind.SlashToken:
                case SyntaxKind.StarToken:
                    return 2;
                default:
                    return 0;
            }
        }

        public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 3;
                default:
                    return 0;
            }
        }

        internal static SyntaxKind GetKeyWordKind(string text)
        {
            switch (text)
            {
                case "true":
                    return SyntaxKind.TrueKeyWord;
                case "false":
                    return SyntaxKind.FalseKeyWord;
                default:
                    return SyntaxKind.IdentifierToken;
            }
        }
    }

}