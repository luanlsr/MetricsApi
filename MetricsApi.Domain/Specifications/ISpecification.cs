using System.Linq.Expressions;

namespace MetricsApi.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}
