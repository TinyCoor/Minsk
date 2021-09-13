using System.Collections.Generic;
using System.Linq;

namespace Minsk.CodeAnalysis.Syntax
{
    public sealed class SyntaxTree
    {

        public SyntaxTree(IEnumerable<Diagnostic> diagnostics, ExpressionSyntax root, SyntaxToken endOfFile)
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOfFile = endOfFile;
        }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }

        public IReadOnlyList<Diagnostic> Diagnostics { get; }
        public ExpressionSyntax Root { get; }
        public SyntaxToken EndOfFile { get; }
    }
    
}