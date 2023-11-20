using System.Data;
using static Dapper.SqlMapper;

namespace Infrastructure
{
    public interface IDapperRepository
    {
        Task<T> GetAsync<T>(string query, object param = null, CommandType commandType = CommandType.Text);
        Task<IEnumerable<T>> GetAllAsync<T>(string query, object param = null, CommandType commandType = CommandType.Text);
        Task<dynamic> GetMultipleAsync<T1, T2, T3>(string sp, object parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
