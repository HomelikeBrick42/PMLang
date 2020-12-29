using System;

using PMCompiler.CodeAnalysis.Syntax;

namespace PMCompiler.CodeAnalysis
{
    public sealed class Evalulator
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
                case SyntaxKind.LiteralExpression:
                {
                    var numberExpression = (LiteralExpressionSyntax)root;
                    return (int)numberExpression.LiteralToken.Value;
                }

                case SyntaxKind.UnaryExpression:
                {
                    var unaryExpression = (UnaryExpressionSyntax)root;

                    var operand = EvalulateExpression(unaryExpression.Operand);
                    switch (unaryExpression.OperatorToken.Kind)
                    {
                        case SyntaxKind.PlusToken:
                            return +operand;
                        case SyntaxKind.MinusToken:
                            return -operand;

                        default:
                            throw new Exception($"Unexpected unary operator {unaryExpression.OperatorToken.Kind}.");
                    }
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
