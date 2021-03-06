using System.Collections.Generic;

namespace PMCompiler.CodeAnalysis.Syntax
{
    internal sealed class Parser
    {
        private readonly SyntaxToken[] _tokens;

        private List<string> _diagnostics = new List<string>();
        private int _position;

        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();

            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.Lex();

                if (token.Kind != SyntaxKind.WhitespaceToken &&
                    token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }
            } while(token.Kind != SyntaxKind.EndOfFileToken);

            _tokens = tokens.ToArray();
            _diagnostics.AddRange(lexer.Diagnostics);
        }
        
        public IEnumerable<string> Diagnostics => _diagnostics;

        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];

            return _tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken MatchToken(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();

            _diagnostics.Add($"ERROR: Unepected token <{Current.Kind}>, expected <{kind}>.");
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        public SyntaxTree Parse()
        {
            var expression = ParseExpression();
            var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(_diagnostics, expression, endOfFileToken);
        }

        private ExpressionSyntax ParseExpression(int parentPresedence = 0)
        {
            ExpressionSyntax left;

            var unaryOperatorPresedence = Current.Kind.GetUnaryOperatorPresedence();
            if (unaryOperatorPresedence != 0 && unaryOperatorPresedence >= parentPresedence)
            {
                var operatorToken = NextToken();
                var operand = ParseExpression(unaryOperatorPresedence);
                left = new UnaryExpressionSyntax(operatorToken, operand);
            }
            else
                left = ParsePrimaryExpression();

            while (true)
            {
                var presedence = Current.Kind.GetBinaryOperatorPresedence();
                if (presedence == 0 || presedence <= parentPresedence)
                    break;

                var operatorToken = NextToken();
                var right = ParseExpression(presedence);
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            switch (Current.Kind)
            {
                case SyntaxKind.OpenParenthesesToken:
                {
                    var left = NextToken();
                    var expression = ParseExpression();
                    var right = MatchToken(SyntaxKind.CloseParenthesesToken);
                    return new ParenthesizedExpressionSyntax(left, expression, right);
                }

                case SyntaxKind.TrueKeyword:
                case SyntaxKind.FalseKeyword:
                {
                    var keyworkToken = NextToken();
                    var value = keyworkToken.Kind == SyntaxKind.TrueKeyword;
                    return new LiteralExpressionSyntax(keyworkToken, value);
                }

                default:
                {
                    var numberToken = MatchToken(SyntaxKind.NumberToken);
                    return new LiteralExpressionSyntax(numberToken);
                }
            }
        }
    }
}
