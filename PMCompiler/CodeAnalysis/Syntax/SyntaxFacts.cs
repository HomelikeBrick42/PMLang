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
                    return 3;

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
                    return 2;

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 1;

                default:
                    return 0;
            }
        }
    }
}
