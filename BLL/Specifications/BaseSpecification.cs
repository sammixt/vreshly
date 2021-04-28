using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace BLL.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public System.Linq.Expressions.Expression<Func<T, bool>> Criteria { get; }

        public System.Collections.Generic.List<System.Linq.Expressions.Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
