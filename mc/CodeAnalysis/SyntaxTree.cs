using System.Collections.Generic;
using System.Linq;

namespace Minsk.CodeAnalysis
{
    class SyntaxTree
{

    public SyntaxTree(IEnumerable<string> diagnostics,ExpressionSyntax root,SyntaxToken endOfFile)
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

    public IReadOnlyList<string> Diagnostics { get; }
    public ExpressionSyntax Root { get;}
    public SyntaxToken EndOfFile { get; }
}
    
}