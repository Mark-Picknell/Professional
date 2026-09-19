using DatabaseXmlManager.Xml;
using DomainRecord = DatabaseXmlManager.Core.Models.Record;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace DatabaseXmlManager.Tests.Xml;
[TestClass]
public sealed class XmlRecordServiceTests
{
    [TestMethod]
    public async Task WriteThenRead_RoundTripsRecords()
    {
        var filename = Path.Combine(Path.GetTempPath(), $"DatabaseXmlManager-{Guid.NewGuid():N}.xml");
        try
        {
            var service = new XmlRecordService();
            await service.WriteAsync(filename, [new DomainRecord { Id = 7, Name = "Example", Description = "Round trip" }]);
            var records = await service.ReadAsync(filename);
            Assert.AreEqual(1, records.Count);
            Assert.AreEqual(7, records[0].Id);
            Assert.AreEqual("Example", records[0].Name);
            Assert.AreEqual("Round trip", records[0].Description);
        }
        finally { if (File.Exists(filename)) File.Delete(filename); }
    }
}
