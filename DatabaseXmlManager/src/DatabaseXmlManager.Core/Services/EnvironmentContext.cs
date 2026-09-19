using DatabaseXmlManager.Core.Enums;
using DatabaseXmlManager.Core.Interfaces;
using DatabaseXmlManager.Core.Models;
namespace DatabaseXmlManager.Core.Services;
public sealed class EnvironmentContext : IEnvironmentContext
{
    private readonly IReadOnlyDictionary<DatabaseEnvironment, EnvironmentProfile> _profiles;
    public EnvironmentContext(IEnumerable<EnvironmentProfile> profiles, DatabaseEnvironment initialEnvironment = DatabaseEnvironment.DEV)
    {
        _profiles = profiles.ToDictionary(profile => profile.Environment);
        Current = Resolve(initialEnvironment);
    }
    public EnvironmentProfile Current { get; private set; }
    public event EventHandler<EnvironmentProfile>? EnvironmentChanged;
    public Task ChangeAsync(DatabaseEnvironment environment, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var next = Resolve(environment);
        if (next == Current) return Task.CompletedTask;
        Current = next;
        EnvironmentChanged?.Invoke(this, next);
        return Task.CompletedTask;
    }
    private EnvironmentProfile Resolve(DatabaseEnvironment environment) => _profiles.TryGetValue(environment, out var profile)
        ? profile : throw new InvalidOperationException($"No profile is configured for {environment}.");
}
