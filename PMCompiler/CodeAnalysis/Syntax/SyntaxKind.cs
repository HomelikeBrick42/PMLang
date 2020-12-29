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
        IdentifierToken,
        ExclamationToken,
        EqualsEqualsToken,
        AmpersandAmpersandToken,
        ExclamationEqualsToken,
        PipePipeToken,
        OpenParenthesesToken,
        CloseParenthesesToken,

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
