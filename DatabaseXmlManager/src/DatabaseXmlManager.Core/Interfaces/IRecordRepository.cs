using DomainRecord = DatabaseXmlManager.Core.Models.Record;
namespace DatabaseXmlManager.Core.Interfaces;
public interface IRecordRepository
{
    Task<IReadOnlyList<DomainRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DomainRecord?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<int> InsertAsync(DomainRecord record, CancellationToken cancellationToken = default);
    Task UpdateAsync(DomainRecord record, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, byte[] rowVersion, CancellationToken cancellationToken = default);
}
