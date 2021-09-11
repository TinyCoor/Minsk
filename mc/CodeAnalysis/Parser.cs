using System.Collections.Generic;

namespace Minsk.CodeAnalysis
{
    class Parser
{
    private readonly SyntaxToken[] _tokens;
    private int _position;
    private List<string> _diagnostics = new List<string>();
    public Parser(string _text)
    {
        var lexer = new Lexer(_text);
        var tokens = new List<SyntaxToken>();
        SyntaxToken token;
        do
        {
            token = lexer.NextToken();
            if (token.Kind != SyntaxKind.WhiteSpaceToken &&
                token.Kind != SyntaxKind.BadToken)
            {
                tokens.Add(token);
            }

        } while (token.Kind != SyntaxKind.EndOfFileToken);

        _tokens = tokens.ToArray();
        _diagnostics.AddRange(lexer.Diagnostics);
    }

    public IEnumerable<string> Diagnostics => _diagnostics;

    private SyntaxToken NextToken()
    {
        var current = Current;
        _position++;
        return current;
    }

    private SyntaxToken Match(SyntaxKind kind)
    {
        if(Current.Kind == kind)
            return NextToken();


        _diagnostics.Add($"ERROR: Unexpoected Token <{Current.Kind}>, expected <{kind}>");
        return new SyntaxToken(kind,Current.Position,null,null);    
    }
    public SyntaxTree Parse()
    {
        var expression =  ParseTerm();
        var endOfFile = Match(SyntaxKind.EndOfFileToken);
        return new SyntaxTree(_diagnostics, expression, endOfFile);
    }

    private ExpressionSyntax ParseTerm() {
        var left = ParseFactor();
        while (Current.Kind == SyntaxKind.PlusToken ||
            Current.Kind == SyntaxKind.MinusToken   || 
            Current.Kind == SyntaxKind.StarToken    || 
            Current.Kind == SyntaxKind.SlashToken
            )
        {
            var opToken = NextToken();
            var right = ParseFactor();
            left = new BinaryExpressionSyntax(left, opToken, right);
        }
        return left;
    }

    private ExpressionSyntax ParseFactor()
    {
        var left = ParsePrimaryExpression();
        while (Current.Kind == SyntaxKind.StarToken ||
            Current.Kind == SyntaxKind.SlashToken
            )
        {
            var opToken = NextToken();
            var right = ParsePrimaryExpression();
            left = new BinaryExpressionSyntax(left, opToken, right);
        }
        return left;
    }

    private ExpressionSyntax ParseExPression()
    {
        return ParseTerm();
    }

    private ExpressionSyntax ParsePrimaryExpression(){

        if(Current.Kind == SyntaxKind.OpenParenthesisToken)
        {
            var left = NextToken();
            var expression = ParseExPression();
            var right = Match(SyntaxKind.CloseParenthesisToken);
            return new ParenthesizedExpressionSyntax(left, expression, right);
        }
        var  numberToken = Match(SyntaxKind.NumberToken);
        return new NumberExpressionSyntax(numberToken);

    }

    private SyntaxToken Peek(int offset)
    {
        var index = _position + offset;
        if (index >= _tokens.Length)
        {
            return _tokens[_tokens.Length - 1];
        }
        return _tokens[index];
    }

    private SyntaxToken Current => Peek(0);
}
    
}