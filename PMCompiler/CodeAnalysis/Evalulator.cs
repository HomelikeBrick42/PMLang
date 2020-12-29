using System;
using PMCompiler.CodeAnalysis.Binding;
using PMCompiler.CodeAnalysis.Syntax;

namespace PMCompiler.CodeAnalysis
{
    internal sealed class Evalulator
    {
        private readonly BoundExpression _root;

        public Evalulator(BoundExpression root)
        {
            _root = root;
        }

        public int Evalulate()
        {
            return EvalulateExpression(_root);
        }

        private int EvalulateExpression(BoundExpression root)
        {
            switch (root.Kind)
            {
                case BoundNodeKind.LiteralExpression:
                {
                    var numberExpression = (BoundLiteralExpression)root;
                    return (int)numberExpression.Value;
                }

                case BoundNodeKind.UnaryExpression:
                {
                    var unaryExpression = (BoundUnaryExpression)root;

                    var operand = EvalulateExpression(unaryExpression.Operand);
                    switch (unaryExpression.OperatorKind)
                    {
                        case BoundUnaryOperatorKind.Identity:
                            return +operand;
                        case BoundUnaryOperatorKind.Negation:
                            return -operand;

                        default:
                            throw new Exception($"Unexpected unary operator {unaryExpression.OperatorKind}.");
                    }
                }

                case BoundNodeKind.BinaryExpression:
                {
                    var binaryExpression = (BoundBinaryExpression)root;

                    var left = EvalulateExpression(binaryExpression.Left);
                    var right = EvalulateExpression(binaryExpression.Right);

                    switch (binaryExpression.OperatorKind)
                    {
                        case BoundBinaryOperatorKind.Addition:
                            return (int)left + (int)right;
                        case BoundBinaryOperatorKind.Subtraction:
                            return (int)left - (int)right;
                        case BoundBinaryOperatorKind.Multiplication:
                            return (int)left * (int)right;
                        case BoundBinaryOperatorKind.Division:
                            return (int)left / (int)right;

                        default:
                            throw new Exception($"Unexpected binary operator {binaryExpression.OperatorKind}.");
                    }
                }
                
                default:
                    throw new Exception($"Unexpected node {root.Kind}.");
            }
        }
    }
}
