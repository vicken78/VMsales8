using Caliburn.Micro;
using System.Windows;
using VMsales8.ViewModels;
namespace VMsales8;

public class Bootstrapper : BootstrapperBase
{
    private readonly SimpleContainer _container = new SimpleContainer();

    protected override void Configure()
    {
        // Register IWindowManager and IEventAggregator with the container
        _container.Singleton<IWindowManager, WindowManager>();
        _container.Singleton<IEventAggregator, EventAggregator>();
    }

    public Bootstrapper()
    {
        Initialize();
    }
    protected override void OnStartup(object sender, StartupEventArgs e)
    {
        DisplayRootViewForAsync<ShellViewModel>();
    }

}