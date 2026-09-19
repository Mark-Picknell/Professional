using DatabaseXmlManager.Core.Enums;
using DatabaseXmlManager.Core.Models;
using DatabaseXmlManager.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace DatabaseXmlManager.Tests.Core;
[TestClass]
public sealed class EnvironmentContextTests
{
    [TestMethod]
    public async Task ChangeAsync_ChangesCurrentProfileAndRaisesEvent()
    {
        var context = new EnvironmentContext([Profile(DatabaseEnvironment.DEV), Profile(DatabaseEnvironment.UAT)]);
        EnvironmentProfile? observed = null;
        context.EnvironmentChanged += (_, profile) => observed = profile;
        await context.ChangeAsync(DatabaseEnvironment.UAT);
        Assert.AreEqual(DatabaseEnvironment.UAT, context.Current.Environment);
        Assert.AreSame(context.Current, observed);
    }
    [TestMethod]
    public void ProductionProfile_IsProduction()
    {
        Assert.IsTrue(Profile(DatabaseEnvironment.PROD).IsProduction);
        Assert.IsFalse(Profile(DatabaseEnvironment.DEV).IsProduction);
    }
    private static EnvironmentProfile Profile(DatabaseEnvironment environment) => new()
    {
        Environment = environment, Server = $"{environment}-SERVER", Database = "TEST", AllowWrites = environment != DatabaseEnvironment.PROD
    };
}
