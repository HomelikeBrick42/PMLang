using System;

namespace PMCompiler.CodeAnalysis
{
    class Evalulator
    {
        private readonly ExpressionSyntax _root;

        public Evalulator(ExpressionSyntax root)
        {
            _root = root;
        }

        public int Evalulate()
        {
            return EvalulateExpression(_root);
        }

        private int EvalulateExpression(ExpressionSyntax root)
        {
            switch (root.Kind)
            {
                case SyntaxKind.NumberExpression:
                {
                    var numberExpression = (NumberExpressionSyntax)root;
                    return (int)numberExpression.NumberToken.Value;
                }

                case SyntaxKind.BinaryExpression:
                {
                    var binaryExpression = (BinaryExpressionSyntax)root;

                    var left = EvalulateExpression(binaryExpression.Left);
                    var right = EvalulateExpression(binaryExpression.Right);

                    switch (binaryExpression.OperatorToken.Kind)
                    {
                        case SyntaxKind.PlusToken:
                            return (int)left + (int)right;
                        case SyntaxKind.MinusToken:
                            return (int)left - (int)right;
                        case SyntaxKind.AsteriskToken:
                            return (int)left * (int)right;
                        case SyntaxKind.ForwardSlashToken:
                            return (int)left / (int)right;

                        default:
                            throw new Exception($"Unexpected binary operator {binaryExpression.OperatorToken.Kind}.");
                    }
                }

                case SyntaxKind.ParenthesizedExpression:
                {
                    var parenthesizedExpression = (ParenthesizedExpressionSyntax)root;
                    return EvalulateExpression(parenthesizedExpression.Expression);
                }
                
                default:
                    throw new Exception($"Unexpected node {root.Kind}.");
            }
        }
    }
}