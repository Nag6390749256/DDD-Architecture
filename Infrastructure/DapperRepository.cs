using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using static Dapper.SqlMapper;
using static Infrastructure.IConnectionString;

namespace Infrastructure
{
    public class DapperRepository : IDapperRepository, IDisposable
    {
        private string connectionString;
        private readonly ILogger<DapperRepository> _logger;
        public DapperRepository(IConnectionString connection, ILogger<DapperRepository> logger)
        {
            connectionString = connection.connectionString ?? "SqlConnection";
            _logger = logger;
        }
        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
        public async Task<T> GetAsync<T>(string query, object param = null,CommandType commandType = CommandType.Text)
        {
            T result;
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var response = await db.QueryAsync<T>(query, param,commandType: commandType);
                    result = response.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(exception:ex,ex.Message);
                result = default(T);
            }
            return result;
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string query, object param = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var result = await db.QueryAsync<T>(query, param, commandType: commandType);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return new List<T>();
            }
        }
        public async Task<dynamic> GetMultipleAsync<T1, T2,T3>(string sp, object parms, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var result = await db.QueryMultipleAsync(sp, parms, commandType: commandType).ConfigureAwait(false);
                    var res = new
                    {
                        Table1 = result.Read<T1>(),
                        Table2 = result.Read<T2>(),
                        Table3 = result.Read<T3>()
                    };
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
