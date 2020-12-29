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
                    switch (unaryExpression.Operator.Kind)
                    {
                        case BoundUnaryOperatorKind.Identity:
                            return +(int)operand;
                        case BoundUnaryOperatorKind.Negation:
                            return -(int)operand;

                        case BoundUnaryOperatorKind.LogicalNegation:
                            return !(bool)operand;

                        default:
                            throw new Exception($"Unexpected unary operator {unaryExpression.Operator.Kind}.");
                    }
                }

                case BoundNodeKind.BinaryExpression:
                {
                    var binaryExpression = (BoundBinaryExpression)root;

                    var left = EvalulateExpression(binaryExpression.Left);
                    var right = EvalulateExpression(binaryExpression.Right);

                    switch (binaryExpression.Operator.Kind)
                    {
                        case BoundBinaryOperatorKind.Addition:
                            return (int)left + (int)right;
                        case BoundBinaryOperatorKind.Subtraction:
                            return (int)left - (int)right;
                        case BoundBinaryOperatorKind.Multiplication:
                            return (int)left * (int)right;
                        case BoundBinaryOperatorKind.Division:
                            return (int)left / (int)right;

                        case BoundBinaryOperatorKind.LogicalAnd:
                            return (bool)left && (bool)right;
                        case BoundBinaryOperatorKind.LogicalOr:
                            return (bool)left || (bool)right;

                        case BoundBinaryOperatorKind.LogicalEquals:
                            return Equals(left, right);
                        case BoundBinaryOperatorKind.LogicalNotEquals:
                            return !Equals(left, right);

                        default:
                            throw new Exception($"Unexpected binary operator {binaryExpression.Operator.Kind}.");
                    }
                }
                
                default:
                    throw new Exception($"Unexpected node {root.Kind}.");
            }
        }
    }
}
