using System.Windows;
using DatabaseXmlManager.App.ViewModels;
using DatabaseXmlManager.Core.Enums;
using DatabaseXmlManager.Core.Interfaces;
using DatabaseXmlManager.Core.Models;
using DatabaseXmlManager.Core.Services;
using DatabaseXmlManager.SqlServer;
using DatabaseXmlManager.SqlServer.Repositories;
using DatabaseXmlManager.Xml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace DatabaseXmlManager.App;
public partial class App : Application
{
    private readonly IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
    {
        var profiles = context.Configuration.GetSection("Environments").Get<List<EnvironmentProfile>>() ?? [];
        services.AddSingleton<IEnvironmentContext>(_ => new EnvironmentContext(profiles, DatabaseEnvironment.DEV));
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddTransient<IRecordRepository, RecordRepository>();
        services.AddTransient<IXmlRecordService, XmlRecordService>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
    }).Build();
    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();
        _host.Services.GetRequiredService<MainWindow>().Show();
        base.OnStartup(e);
    }
    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync(); _host.Dispose(); base.OnExit(e);
    }
}
