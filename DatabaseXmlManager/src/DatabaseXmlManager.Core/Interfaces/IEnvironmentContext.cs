using DatabaseXmlManager.Core.Enums;
using DatabaseXmlManager.Core.Models;
namespace DatabaseXmlManager.Core.Interfaces;
public interface IEnvironmentContext
{
    EnvironmentProfile Current { get; }
    event EventHandler<EnvironmentProfile>? EnvironmentChanged;
    Task ChangeAsync(DatabaseEnvironment environment, CancellationToken cancellationToken = default);
}
