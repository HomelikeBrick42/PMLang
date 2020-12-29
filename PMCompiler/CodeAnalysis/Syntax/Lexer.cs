using System.Collections.Generic;

namespace PMCompiler.CodeAnalysis.Syntax
{
    internal sealed class Lexer
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public Lexer(string text)
        {
            _text = text;
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private char Current => Peek(0);
        private char LookAhead => Peek(1);

        private char Peek(int offset)
        {
            var index = _position + offset;

            if (index >= _text.Length)
                return '\0';

            return _text[index];
        }

        private void Next()
        {
            _position++;
        }

        public SyntaxToken Lex()
        {
            var start = _position;

            if (_position >= _text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, start, "\0", null);

            if (char.IsDigit(Current))
            {
                while (char.IsDigit(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);

                if (!int.TryParse(text, out var value))
                    _diagnostics.Add($"ERROR: The number {_text} isn't a valid Int32.");

                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                while (char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }

            if (char.IsLetter(Current))
            {
                while (char.IsLetter(Current))
                    Next();
                    
                var length = _position - start;
                var text = _text.Substring(start, length);
                var kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);
            }

            switch (Current)
            {
                case '+':
                {
                    _position += 1;
                    return new SyntaxToken(SyntaxKind.PlusToken, start, "+", null);
                }
                case '-':
                {
                    _position += 1;
                    return new SyntaxToken(SyntaxKind.MinusToken, start, "-", null);
                }
                case '*':
                {
                    _position += 1;
                    return new SyntaxToken(SyntaxKind.AsteriskToken, start, "*", null);
                }
                case '/':
                {
                    _position += 1;
                    return new SyntaxToken(SyntaxKind.ForwardSlashToken, start, "/", null);
                }
                case '(':
                {
                    _position += 1;
                    return new SyntaxToken(SyntaxKind.OpenParenthesesToken, start, "(", null);
                }
                case ')':
                {
                    _position += 1;
                    return new SyntaxToken(SyntaxKind.CloseParenthesesToken, start, ")", null);
                }
                case '!':
                {
                    _position += 1;
                    return new SyntaxToken(SyntaxKind.ExclamationToken, start, "!", null);
                }
                case '&':
                {
                    if (LookAhead == '&')
                    {
                        _position += 2;
                        return new SyntaxToken(SyntaxKind.AmpersandAmpersandToken, start, "&&", null);
                    }
                    else break;
                }
                case '|':
                {
                    if (LookAhead == '|')
                    {
                        _position += 2;
                        return new SyntaxToken(SyntaxKind.PipePipeToken, start, "||", null);
                    }
                    else break;
                }
            }

            _diagnostics.Add($"ERROR: Bad character in input: '{Current}'");
            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(start, 1), null);
        }
    }
}
