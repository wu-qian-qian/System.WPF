using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using WPF.Tools.Local.Service;
using System.Windows.Navigation;

namespace System.UI.Core.Region
{

    /// <summary>
    /// 该类不允许被外部实例
    /// </summary>
    public sealed partial class RegionManager
    {
        private RegionManager()
        {

        }
        public static RegionManager CreatRegionManager()
        {
            RegionManager region = new RegionManager();
            return region;
        }

        /// <summary>
        /// Fram 属性的操作 默认名为Region不可更改
        /// Fram使用反射操作
        /// 并且使用RegionNode 保存当前激活的界面、和上一级节点
        /// </summary>
        /// <param name="viewModel"></param>
        public void Active(string viewModel,bool newFram)
        {
            var region = _pageManager[viewModel];
            if (region.IsIns == false)
            {
                region.InsUI((FrameworkElement)services.GetRequiredKeyedService
                  (region.UIType, viewModel));
            }
            var ui = region.UI;
            if (ui is Window win)
            {
                win.Show();
                CurrentWin = new RegionNode();
                CurrentWin.Key = viewModel;
                CurrentWin.CurrentWin = win;
            }
            else if (ui is Page page)
            {
                var fram = CurrentWin.CurrentWin.GetType().GetField(IregionManager.Region, Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.GetField
                  | Reflection.BindingFlags.Default | Reflection.BindingFlags.Instance);
                if (fram != null)
                {
                    BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                    if (fram.GetValue(CurrentWin.CurrentWin) is ContentControl control)
                    {
                        if (newFram == true)
                        {
                            var content = control.GetType().GetProperty(IregionManager.Content, flag);
                            var visibility = control.GetType().GetProperty(IregionManager.Visibility, flag);
                            content.SetValue(control, page);
                            visibility.SetValue(control, Visibility.Visible);

                            RegionNode node = new RegionNode();
                            node.UpNode = CurrentWin;
                            node.CurrentWin = page;
                            CurrentWin = node;
                            CurrentWin.Key = viewModel;
                        }
                        else
                        {
                            var content = control.GetType().GetProperty(IregionManager.Content, flag);
                            var visibility = control.GetType().GetProperty(IregionManager.Visibility, flag);
                            content.SetValue(control, page);
                            visibility.SetValue(control, Visibility.Visible);
                        }
                    }
                }
            }
            else
            {
                ui.Visibility = Visibility.Visible;
            }
            region.InsVM(services.GetRequiredService(region.ViewModel));
        }

        /// <summary>
        /// 返回上一级
        /// 这里只针对隐藏上级的Fram
        /// 隐藏上一级的Fram本级的Page就会被隐藏
        /// </summary>
        public void GoBack()
        {
            var node = CurrentWin.UpNode;
            if (node != null)
            {
                var fram = node.CurrentWin.GetType().GetField(IregionManager.Region, Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.GetField
                  | Reflection.BindingFlags.Default | Reflection.BindingFlags.Instance).GetValue(node.CurrentWin);
                if (fram != null)
                {
                    if (fram is ContentControl control)
                    {
                        BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                        var content = control.GetType().GetProperty(IregionManager.Visibility, flag);
                        content.SetValue(fram, Visibility.Collapsed);
                        CurrentWin.UpNode = null;
                        CurrentWin = null;
                        CurrentWin = node;
                    }
                }
            }
        }

        /// <summary>
        /// 跳转到指定位置
        /// </summary>
        /// <param name="key"></param>
        public void GoIndex(string key)
        {
            var region = GetRegionUIElement(key);
            if (region != null)
            {
                GoBack();
                if (CurrentWin.Key != key)
                {
                    GoIndex(key);
                }
            }
            else
            {
                throw new InvalidOperationException("不存在");
            }
        }
        /// <summary>
        /// 直接异常当前UI
        /// 后续做优化为关系整个窗体
        /// TODO
        /// </summary>
        /// <param name="viewModel"></param>
        public void Hide(string viewModel)
        {
            _pageManager[viewModel].UI.Visibility = Visibility.Collapsed;
        }
        public void Build(IServiceProvider services)
        {
            this.services = services;
        }
        /// <summary>
        /// 获取page/win 静态动画资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Storyboard? GetAnimation(string key)
        {
            return CurrentWin.GetAnimation(key);
        }
        /// <summary>
        /// 或取任意一级的Page/Win
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public UIElement GetRegionUIElement(string key)
        {
            var ui = _pageManager[key].UI;
            return GetRegionUIElement(ui, CurrentWin);
        }
        private UIElement GetRegionUIElement(UIElement ui, RegionNode node)
        {
            if (ui == node.CurrentWin)
            {
                return node.CurrentWin;
            }
            else
            {
                return GetRegionUIElement(ui, node.UpNode);
            }
        }

        /// <summary>
        /// 获取界面展示的任意控件元素
        /// 直到找到第一个匹配的元素否则返回null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UIElement GetControlUIElement(string key)
        {
            var control = CurrentWin.GetUIElement(key);
            if (control == null)
            {
                control = GetControlUIElement(key, CurrentWin.UpNode);
            }
            return control;
        }

        private UIElement GetControlUIElement(string key, RegionNode node)
        {
            if (node != null)
            {
                var control = node.GetUIElement(key);
                if (control == null)
                {
                    control = GetControlUIElement(key, CurrentWin.UpNode);
                }
                return control;
            }
            return default;
        }
        public static IServiceCollection Register(IServiceCollection services, Type view, Type viewModel)
        {
            services.AddKeyedScoped(view, viewModel.Name);
            services.AddScoped(viewModel);
            _pageManager.Add(viewModel.Name, new Region(view, viewModel));
            return services;
        }
    }
}