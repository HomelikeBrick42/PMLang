using System;

namespace PMCompiler.CodeAnalysis.Syntax
{
    internal static class SyntaxFacts
    {
        public static int GetUnaryOperatorPresedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                case SyntaxKind.ExclamationToken:
                    return 5;
                    
                default:
                    return 0;
            }
        }

        public static int GetBinaryOperatorPresedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.AsteriskToken:
                case SyntaxKind.ForwardSlashToken:
                    return 4;

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 3;

                case SyntaxKind.AmpersandAmpersandToken:
                    return 2;

                case SyntaxKind.PipePipeToken:
                    return 1;

                default:
                    return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            switch (text)
            {
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "false":
                    return SyntaxKind.FalseKeyword;

                default:
                    return SyntaxKind.IdentifierToken;
            }
        }
    }
}
