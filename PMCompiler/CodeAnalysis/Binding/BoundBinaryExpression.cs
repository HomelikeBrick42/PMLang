using System;

namespace PMCompiler.CodeAnalysis.Binding
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right)
        {
            Left = left;
            Operator = op;
            Right = right;
        }

        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;
        public override Type Type => Operator.ResultType;
        public BoundExpression Left { get; }
        public BoundBinaryOperator Operator { get; }
        public BoundExpression Right { get; }
    }
}
