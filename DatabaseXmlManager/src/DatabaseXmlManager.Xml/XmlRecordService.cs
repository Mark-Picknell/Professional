using System.Xml;
using System.Xml.Linq;
using DatabaseXmlManager.Core.Interfaces;
using DomainRecord = DatabaseXmlManager.Core.Models.Record;
namespace DatabaseXmlManager.Xml;
public sealed class XmlRecordService : IXmlRecordService
{
    public async Task<IReadOnlyList<DomainRecord>> ReadAsync(string filename, CancellationToken cancellationToken = default)
    {
        await using var stream = File.OpenRead(filename);
        var document = await XDocument.LoadAsync(stream, LoadOptions.SetLineInfo, cancellationToken).ConfigureAwait(false);
        return document.Root?.Elements("Record").Select(Parse).ToArray() ?? [];
    }
    public async Task WriteAsync(string filename, IEnumerable<DomainRecord> records, CancellationToken cancellationToken = default)
    {
        var document = new XDocument(new XElement("Records", records.Select(record => new XElement("Record",
            new XAttribute("Id", record.Id), new XElement("Name", record.Name),
            record.Description is null ? null : new XElement("Description", record.Description)))));
        await using var stream = File.Create(filename);
        await using var writer = XmlWriter.Create(stream, new XmlWriterSettings { Async = true, Indent = true });
        await document.SaveAsync(writer, SaveOptions.None, cancellationToken).ConfigureAwait(false);
    }
    private static DomainRecord Parse(XElement element) => new()
    {
        Id = (int?)element.Attribute("Id") ?? throw new FormatException("Record Id is required."),
        Name = (string?)element.Element("Name") ?? throw new FormatException("Record Name is required."),
        Description = (string?)element.Element("Description")
    };
}
