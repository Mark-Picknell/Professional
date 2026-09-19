using DatabaseXmlManager.Core.Interfaces;
using Microsoft.Data.SqlClient;
namespace DatabaseXmlManager.SqlServer;
public sealed class SqlConnectionFactory(IEnvironmentContext environmentContext) : ISqlConnectionFactory
{
    public async Task<SqlConnection> OpenAsync(CancellationToken cancellationToken = default)
    {
        var profile = environmentContext.Current;
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = profile.Server,
            InitialCatalog = profile.Database,
            IntegratedSecurity = true,
            Encrypt = SqlConnectionEncryptOption.Mandatory,
            TrustServerCertificate = false
        };
        var connection = new SqlConnection(builder.ConnectionString);
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
        return connection;
    }
}
