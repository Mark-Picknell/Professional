using Microsoft.Data.SqlClient;
namespace DatabaseXmlManager.SqlServer;
public interface ISqlConnectionFactory
{
    Task<SqlConnection> OpenAsync(CancellationToken cancellationToken = default);
}
