using DatabaseXmlManager.Core.Enums;
namespace DatabaseXmlManager.Core.Models;
public sealed record EnvironmentProfile
{
    public required DatabaseEnvironment Environment { get; init; }
    public required string Server { get; init; }
    public required string Database { get; init; }
    public bool AllowWrites { get; init; }
    public bool IsProduction => Environment == DatabaseEnvironment.PROD;
}
