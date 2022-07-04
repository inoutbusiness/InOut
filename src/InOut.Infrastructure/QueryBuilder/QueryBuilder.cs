using FastMember;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.QueryBuilder.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace InOut.Infrastructure.QueryBuilder
{
    public class QueryBuilder : IQueryBuilder
    {
        #region Async

        public async Task<IEnumerable<T>> ExecQueryBuilderAsync<T>(InOutContext context, string querySQL) where T : class, new()
        {
            var entities = new List<T>();

            try
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = querySQL;
                    command.CommandType = System.Data.CommandType.Text;

                    await context.Database.OpenConnectionAsync();

                    using (SqlDataReader result = (SqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await result.ReadAsync())
                            entities.Add(await ConvertToObjectAsync<T>(result));
                    }

                    return entities;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                await context.Database.CloseConnectionAsync();
            }
        }

        private async Task<T> ConvertToObjectAsync<T>(SqlDataReader reader) where T : class, new()
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var members = accessor.GetMembers();
            var entity = new T();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (!await reader.IsDBNullAsync(i))
                {
                    var fieldName = reader.GetName(i);

                    if (members.Any(x => string.Equals(x.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        accessor[entity, fieldName] = reader.GetValue(i);
                    }
                }
            }
            return entity;
        }

        #endregion

        #region Sync

        public IEnumerable<T> ExecQueryBuilder<T>(InOutContext context, string querySQL) where T : class, new()
        {
            var entities = new List<T>();

            try
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = querySQL;
                    command.CommandType = System.Data.CommandType.Text;

                    context.Database.OpenConnection();

                    using (SqlDataReader result = (SqlDataReader)command.ExecuteReader())
                    {
                        while (result.Read())
                            entities.Add(ConvertToObject<T>(result));
                    }

                    return entities;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }

        private T ConvertToObject<T>(SqlDataReader reader) where T : class, new()
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var members = accessor.GetMembers();
            var entity = new T();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (!reader.IsDBNull(i))
                {
                    var fieldName = reader.GetName(i);

                    if (members.Any(x => string.Equals(x.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        accessor[entity, fieldName] = reader.GetValue(i);
                    }
                }
            }
            return entity;
        }

        #endregion
    }
}
