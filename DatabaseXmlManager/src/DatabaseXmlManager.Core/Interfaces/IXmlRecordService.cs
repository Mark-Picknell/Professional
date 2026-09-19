using DomainRecord = DatabaseXmlManager.Core.Models.Record;
namespace DatabaseXmlManager.Core.Interfaces;
public interface IXmlRecordService
{
    Task<IReadOnlyList<DomainRecord>> ReadAsync(string filename, CancellationToken cancellationToken = default);
    Task WriteAsync(string filename, IEnumerable<DomainRecord> records, CancellationToken cancellationToken = default);
}
