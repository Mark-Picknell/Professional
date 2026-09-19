namespace DatabaseXmlManager.Core.Models;
public sealed class Record
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public byte[]? RowVersion { get; set; }
}
