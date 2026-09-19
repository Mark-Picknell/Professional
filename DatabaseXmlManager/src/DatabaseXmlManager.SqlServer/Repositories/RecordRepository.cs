using System.Data;
using DatabaseXmlManager.Core.Interfaces;
using Microsoft.Data.SqlClient;
using DomainRecord = DatabaseXmlManager.Core.Models.Record;
namespace DatabaseXmlManager.SqlServer.Repositories;
public sealed class RecordRepository(ISqlConnectionFactory connectionFactory) : IRecordRepository
{
    public async Task<IReadOnlyList<DomainRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var records = new List<DomainRecord>();
        await using var connection = await connectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var command = new SqlCommand("SELECT RecordId, Name, Description, RowVersion FROM dbo.Record ORDER BY RecordId;", connection);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false)) records.Add(Map(reader));
        return records;
    }
    public async Task<DomainRecord?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = await connectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var command = new SqlCommand("SELECT RecordId, Name, Description, RowVersion FROM dbo.Record WHERE RecordId = @RecordId;", connection);
        command.Parameters.Add("@RecordId", SqlDbType.Int).Value = id;
        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        return await reader.ReadAsync(cancellationToken).ConfigureAwait(false) ? Map(reader) : null;
    }
    public Task<int> InsertAsync(DomainRecord record, CancellationToken cancellationToken = default) => throw PendingSchema();
    public Task UpdateAsync(DomainRecord record, CancellationToken cancellationToken = default) => throw PendingSchema();
    public Task DeleteAsync(int id, byte[] rowVersion, CancellationToken cancellationToken = default) => throw PendingSchema();
    private static NotImplementedException PendingSchema() => new("Finalize the target schema before implementing writes.");
    private static DomainRecord Map(SqlDataReader reader) => new()
    {
        Id = reader.GetInt32(0), Name = reader.GetString(1),
        Description = reader.IsDBNull(2) ? null : reader.GetString(2),
        RowVersion = reader.GetFieldValue<byte[]>(3)
    };
}
