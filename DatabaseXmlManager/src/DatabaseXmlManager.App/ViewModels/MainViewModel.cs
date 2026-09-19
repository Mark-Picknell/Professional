using System.Windows;
using DatabaseXmlManager.Core.Enums;
using DatabaseXmlManager.Core.Interfaces;
namespace DatabaseXmlManager.App.ViewModels;
public sealed class MainViewModel : ViewModelBase
{
    private readonly IEnvironmentContext _environmentContext;
    public MainViewModel(IEnvironmentContext environmentContext) { _environmentContext = environmentContext; _environmentContext.EnvironmentChanged += (_, _) => Refresh(); }
    public IReadOnlyList<DatabaseEnvironment> Environments { get; } = Enum.GetValues<DatabaseEnvironment>();
    public DatabaseEnvironment SelectedEnvironment { get => _environmentContext.Current.Environment; set => _ = _environmentContext.ChangeAsync(value); }
    public string Status => $"Selected: {_environmentContext.Current.Environment} | Server: {_environmentContext.Current.Server} | Database: {_environmentContext.Current.Database}";
    public Visibility ProductionBannerVisibility => _environmentContext.Current.IsProduction ? Visibility.Visible : Visibility.Collapsed;
    private void Refresh() { OnPropertyChanged(nameof(SelectedEnvironment)); OnPropertyChanged(nameof(Status)); OnPropertyChanged(nameof(ProductionBannerVisibility)); }
}
