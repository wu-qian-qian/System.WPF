using DynamicTheme.Core;
using EventModule.Local;
using Infrastructures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.UI.Core;
using System.UI.Core.Region;
using System.UI.Local.ViewModels;
using System.UI.Main.Personnel;
using System.UI.Main;
using System.UI.ViewModels;
using HttpExtensions;
using System.Runtime.InteropServices.JavaScript;
using WPF.Tools.Local.Service;
using System.UI.Thread.Base;
using System.UI.Thread;
using System.UI.ViewModels.Personnel;

namespace System.UI
{
    public static class Startup
    {
        public static IServiceProvider Initialize(this App app, IServiceCollection container)
        {
            InitializeDependency(container);
            RegisterRegion(container);
            Execute.InitializeWithDispatcher();
            RegisterTheme();
            return BuildScopeProvider(container);
        }
        /// <summary>
        /// 背景切换的注册
        /// </summary>
        private static void RegisterTheme()
        {
            Intermediary.Instance._themeManager = new ThemeManager();
            Intermediary.Instance._themeManager.RegisterTheme("Dark", "System.Skins", "Theme/DarkTheme.xaml");
            Intermediary.Instance._themeManager.RegisterTheme("Light", "System.Skins", "Theme/LightTheme.xaml");
        }
        /// <summary>
        /// 界面元素的注入
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterRegion(IServiceCollection container)
        {
            RegionManager.Register(container, typeof(Login), typeof(LoginViewModel));
            RegionManager.Register(container, typeof(SysMenuView), typeof(SysMenuViewModel));
            RegionManager.Register(container, typeof(PeopleControl), typeof(PeopleViewModel));
            RegionManager.Register(container, typeof(PersonnelManagement), typeof(PersonnelManagementVM));
            RegionManager.Register(container, typeof(AttendanceManagement), typeof(AttendanceManagementVM));
            var regionManger = RegionManager.CreatRegionManager();
            container.AddSingleton<IregionManager>(regionManger);
        }

        public static void RegisterService(IServiceCollection container, IEnumerable<Assembly> ass)
        {
            foreach (Assembly assembly in ass)
            {
                var services = assembly.GetTypes().Where(p => !p.IsAbstract && (typeof(IService)).IsAssignableFrom(p));
                foreach (Type service in services)
                {
                    container.AddScoped(service);
                }
            }
        }
        private static void InitializeDependency(IServiceCollection container)
        {
            #region 对配置文件与标记自动注入的类型进行注入
            IConfigurationRoot? configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
            container.AddSingleton<IConfigurationRoot>(configuration);
            //container.AddDependencyInjection(ass );
            #endregion
            container.AddSingleton(Intermediary.Instance);

            IEnumerable<Assembly>? ass = ReflectionHelper.GetAllReferencedAssemblies("System.UI");
            #region    添加事件触发
            List<Type> busType = new List<Type>();
            foreach (Assembly assembly in ass)
            {
                var bus = assembly.GetTypes().Where(p => !p.IsAbstract && (typeof(IEventHandler)).IsAssignableFrom(p));
                busType.AddRange(bus);
            }
            //这里只进行订阅
            container.AddEventSubscribe(busType);
            //一些API服务注入
            RegisterService(container, ass);
            #endregion

            #region 对http的注入
            var httpOptions = configuration.GetSection("HttpOptions").Get<HttpOptions>();
            container.AddHttpClient(httpOptions);
            #endregion

            container.AddSingleton<ITaskHelper>(TaskHelper.Builde());
        }

        /// <summary>
        /// 构建依赖的生命周期
        /// </summary>
        /// <param name="container"></param>
        private static IServiceProvider BuildScopeProvider(IServiceCollection container)
        {
            var serviceProvider = container.BuildServiceProvider();
            Intermediary.Instance.SetServiceProvider(serviceProvider);
            var regionManager = serviceProvider.GetRequiredService<IregionManager>();
            regionManager.Build(serviceProvider);
            regionManager.Active(nameof(LoginViewModel));
            return serviceProvider;
        }
    }
}
