using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AmazedExtension.Extension
{
    /// <summary>
    /// 表达式树扩展
    /// </summary>
    public static class ExpressionExtension
    {
        /// <summary>
        /// 且操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            var rightBody = new RebuildExpression(right.Parameters[0], left.Parameters[0]).Visit(right.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, rightBody), left.Parameters);
        }
        /// <summary>
        /// 或操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
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
    /// <summary>
    /// 重建表达式树（替换表达式参数）
    /// </summary>
    public class RebuildExpression : ExpressionVisitor
    {
        private readonly ParameterExpression _left;
        private readonly ParameterExpression _right;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public RebuildExpression(ParameterExpression left, ParameterExpression right)
        {
            _left = left;
            _right = right;
        }
        /// <summary>
        /// 重写
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_left == node)
            {
                return _right;
            }
            return base.VisitParameter(node);
        }
    }
}
