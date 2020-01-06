using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Util.Extension
{
    public static class ExpressionExtension
    {
        public class RebuildExpression : ExpressionVisitor
        {
            private readonly ParameterExpression _left;
            private readonly ParameterExpression _right;

            public RebuildExpression(ParameterExpression left, ParameterExpression right)
            {
                _left = left;
                _right = right;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (_left == node)
                {
                    return _right;
                }
                return base.VisitParameter(node);
            }
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            var rightBody = new RebuildExpression(right.Parameters[0], left.Parameters[0]).Visit(right.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, rightBody), left.Parameters);
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, [NotNull] Expression<Func<T, bool>> right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            var rightBody = new RebuildExpression(right.Parameters[0], left.Parameters[0]).Visit(right.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, rightBody), left.Parameters); 
        }
    }
}
