using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Util.Data.Repository
{
    public static class ExpressionExtension
    {
        //public class ParameterRebinder : ExpressionVisitor
        //{
        //    private readonly Dictionary<ParameterExpression, ParameterExpression> map;
        //    public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        //    {
        //        this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        //    }
        //    public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        //    {
        //        return new ParameterRebinder(map).Visit(exp);
        //    }
        //    protected override Expression VisitParameter(ParameterExpression p)
        //    {
        //        ParameterExpression replacement;
        //        if (map.TryGetValue(p, out replacement))
        //        {
        //            p = replacement;
        //        }
        //        return base.VisitParameter(p);
        //    }
        //}
        //public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        //{
        //    var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
        //    var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
        //    return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        //}
        //public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        //{
        //    return first.Compose(second, Expression.And);
        //}
        //public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        //{
        //    return first.Compose(second, Expression.Or);
        //}

        //----------------------------------------------------------------------------------
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
        //-----------------------------------------------------------------------------------
        ///// <summary> The Predicate Operator </summary>
        //public enum PredicateOperator
        //{
        //    /// <summary> The "Or" </summary>
        //    Or,

        //    /// <summary> The "And" </summary>
        //    And
        //}

        ///// <summary>
        ///// See http://www.albahari.com/expressions for information and examples.
        ///// </summary>
        //public static class PredicateBuilder
        //{
        //    private class RebindParameterVisitor : ExpressionVisitor
        //    {
        //        private readonly ParameterExpression _oldParameter;
        //        private readonly ParameterExpression _newParameter;

        //        public RebindParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        //        {
        //            _oldParameter = oldParameter;
        //            _newParameter = newParameter;
        //        }

        //        protected override Expression VisitParameter(ParameterExpression node)
        //        {
        //            if (node == _oldParameter)
        //            {
        //                return _newParameter;
        //            }

        //            return base.VisitParameter(node);
        //        }
        //    }

        //    /// <summary> Start an expression </summary>
        //    public static ExpressionStarter<T> New<T>(Expression<Func<T, bool>> expr = null) { return new ExpressionStarter<T>(expr); }

        //    /// <summary> Create an expression with a stub expression true or false to use when the expression is not yet started. </summary>
        //    public static ExpressionStarter<T> New<T>(bool defaultExpression) { return new ExpressionStarter<T>(defaultExpression); }

        //    /// <summary> Always true </summary>
        //    [Obsolete("Use PredicateBuilder.New() instead.")]
        //    public static Expression<Func<T, bool>> True<T>() { return new ExpressionStarter<T>(true); }

        //    /// <summary> Always false </summary>
        //    [Obsolete("Use PredicateBuilder.New() instead.")]
        //    public static Expression<Func<T, bool>> False<T>() { return new ExpressionStarter<T>(false); }

        //    /// <summary> OR </summary>
        //    public static Expression<Func<T, bool>> Or<T>([NotNull] this Expression<Func<T, bool>> expr1, [NotNull] Expression<Func<T, bool>> expr2)
        //    {
        //        var expr2Body = new RebindParameterVisitor(expr2.Parameters[0], expr1.Parameters[0]).Visit(expr2.Body);
        //        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, expr2Body), expr1.Parameters);
        //    }

        //    /// <summary> AND </summary>
        //    public static Expression<Func<T, bool>> And<T>([NotNull] this Expression<Func<T, bool>> expr1, [NotNull] Expression<Func<T, bool>> expr2)
        //    {
        //        var expr2Body = new RebindParameterVisitor(expr2.Parameters[0], expr1.Parameters[0]).Visit(expr2.Body);
        //        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, expr2Body), expr1.Parameters);
        //    }

        //    /// <summary>
        //    /// Extends the specified source Predicate with another Predicate and the specified PredicateOperator.
        //    /// </summary>
        //    /// <typeparam name="T">The type</typeparam>
        //    /// <param name="first">The source Predicate.</param>
        //    /// <param name="second">The second Predicate.</param>
        //    /// <param name="operator">The Operator (can be "And" or "Or").</param>
        //    /// <returns>Expression{Func{T, bool}}</returns>
        //    public static Expression<Func<T, bool>> Extend<T>([NotNull] this Expression<Func<T, bool>> first, [NotNull] Expression<Func<T, bool>> second, PredicateOperator @operator = PredicateOperator.Or)
        //    {
        //        return @operator == PredicateOperator.Or ? first.Or(second) : first.And(second);
        //    }

        //    /// <summary>
        //    /// Extends the specified source Predicate with another Predicate and the specified PredicateOperator.
        //    /// </summary>
        //    /// <typeparam name="T">The type</typeparam>
        //    /// <param name="first">The source Predicate.</param>
        //    /// <param name="second">The second Predicate.</param>
        //    /// <param name="operator">The Operator (can be "And" or "Or").</param>
        //    /// <returns>Expression{Func{T, bool}}</returns>
        //    public static Expression<Func<T, bool>> Extend<T>([NotNull] this ExpressionStarter<T> first, [NotNull] Expression<Func<T, bool>> second, PredicateOperator @operator = PredicateOperator.Or)
        //    {
        //        return @operator == PredicateOperator.Or ? first.Or(second) : first.And(second);
        //    }
        //}
    }
}
