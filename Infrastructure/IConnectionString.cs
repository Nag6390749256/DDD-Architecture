namespace Infrastructure
{
    public interface IConnectionString
    {
        string connectionString { get; set; }
    }
    public class ConnectionProvidor : IConnectionString
    {
        public string connectionString { get; set; } = string.Empty;
    }
}
