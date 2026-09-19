using System.Windows;
using DatabaseXmlManager.App.ViewModels;
namespace DatabaseXmlManager.App;
public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel) { InitializeComponent(); DataContext = viewModel; }
}
