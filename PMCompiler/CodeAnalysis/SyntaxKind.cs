namespace PMCompiler.CodeAnalysis
{
    enum SyntaxKind
    {
        // Tokens
        NumberToken,
        WhitespaceToken,
        PlusToken,
        MinusToken,
        AsteriskToken,
        ForwardSlashToken,
        OpenParenthesesToken,
        CloseParenthesesToken,
        BadToken,
        EndOfFileToken,

        // Expressions
        NumberExpression,
        BinaryExpression,
        ParenthesizedExpression
    }
}