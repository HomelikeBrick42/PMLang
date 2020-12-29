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

        public object Evalulate()
        {
            return EvalulateExpression(_root);
        }

        private object EvalulateExpression(BoundExpression root)
        {
            switch (root.Kind)
            {
                case BoundNodeKind.LiteralExpression:
                {
                    var numberExpression = (BoundLiteralExpression)root;
                    return numberExpression.Value;
                }

                case BoundNodeKind.UnaryExpression:
                {
                    var unaryExpression = (BoundUnaryExpression)root;

                    var operand = EvalulateExpression(unaryExpression.Operand);
                    switch (unaryExpression.OperatorKind)
                    {
                        case BoundUnaryOperatorKind.Identity:
                            return +(int)operand;
                        case BoundUnaryOperatorKind.Negation:
                            return -(int)operand;

                        default:
                            throw new Exception($"Unexpected unary operator {unaryExpression.OperatorKind}.");
                    }
                }

                case BoundNodeKind.BinaryExpression:
                {
                    var binaryExpression = (BoundBinaryExpression)root;

                    var left = (int)EvalulateExpression(binaryExpression.Left);
                    var right = (int)EvalulateExpression(binaryExpression.Right);

                    switch (binaryExpression.OperatorKind)
                    {
                        case BoundBinaryOperatorKind.Addition:
                            return left + right;
                        case BoundBinaryOperatorKind.Subtraction:
                            return left - right;
                        case BoundBinaryOperatorKind.Multiplication:
                            return left * right;
                        case BoundBinaryOperatorKind.Division:
                            return left / right;

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
