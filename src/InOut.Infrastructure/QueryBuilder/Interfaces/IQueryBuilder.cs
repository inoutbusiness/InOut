using InOut.Infrastructure.Context;

namespace InOut.Infrastructure.QueryBuilder.Interfaces
{
    public interface IQueryBuilder
    {
        Task<IEnumerable<T>> ExecQueryBuilderAsync<T>(InOutContext context, string querySQL) where T : class, new();
        IEnumerable<T> ExecQueryBuilder<T>(InOutContext context, string querySQL) where T : class, new();
    }
}
