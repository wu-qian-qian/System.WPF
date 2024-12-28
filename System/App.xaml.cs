using Dependency;
using DynamicTheme.Core;
using EventModule.Local;
using HttpExtensions;
using Infrastructures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Enum;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.UI;
using System.UI.Core;
using System.UI.Core.Region;
using System.UI.Local.ViewModels;
using System.UI.Main;
using System.UI.Main.Personnel;
using System.UI.ViewModels;
using System.Windows;

namespace System
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IEventBus _eventBus;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Intermediary.CreatInstance().CloseSystem = Close;
            var container = new ServiceCollection();
            var serviceProvider= this.Initialize(container);
            _eventBus= serviceProvider.GetRequiredService<IEventBus>();
        }
        private void Close()
        {
            Application.Current.Shutdown();
            _eventBus.Publish<OnlineState>("Close",OnlineState.Close);
        }
    }
}