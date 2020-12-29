namespace PMCompiler.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        // Tokens
        BadToken,
        EndOfFileToken,
        WhitespaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        AsteriskToken,
        ForwardSlashToken,
        OpenParenthesesToken,
        CloseParenthesesToken,
        IdentifierToken,
        ExclamationToken,

        AmpersandAmpersandToken,
        PipePipeToken,

        // Keywords
        TrueKeyword,
        FalseKeyword,

        // Expressions
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
    }
}
