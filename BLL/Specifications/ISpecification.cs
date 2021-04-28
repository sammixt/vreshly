using System;
using System.Linq.Expressions;
using System.Collections.Generic;
namespace BLL.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get;  }
    }

}
