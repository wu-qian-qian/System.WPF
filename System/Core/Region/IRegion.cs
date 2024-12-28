using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Windows.Controls;

namespace System.UI.Core.Region
{
    /// <summary>
    /// 用于导航
    /// </summary>
    public interface IregionManager
    {
        /// <summary>
        /// 导航Frame
        /// </summary>
        public const string Region = "Region";
        /// <summary>
        /// 内容
        /// </summary>
        public const string Content = "Content";

        /// <summary>
        /// 状态
        /// </summary>
        public const string Visibility = "Visibility";
        /// <summary>
        /// Fram的 导航
        /// </summary>
        public const string NavigationUIVisibility = "NavigationUIVisibility";
        /// <summary>
        /// 集货页面展示
        /// </summary>
        /// <param name="viewModel"></param>
        void Active(string viewModel,bool newFram=true);
        /// <summary>
        /// 隐藏某一级的元素
        /// </summary>
        /// <param name="viewModel"></param>
        void Hide(string viewModel);
        /// <summary>
        /// 返回上一级界面page/win
        /// </summary>
        void GoBack();
        /// <summary>
        /// 跳转指定
        /// </summary>
        /// <param name="key"></param>
        void GoIndex(string key);
        /// <summary>
        /// 构建整个区域元素用来实现导航的基本模块
        /// </summary>
        /// <param name="services"></param>
        void Build(IServiceProvider services);
        /// <summary>
        /// 获取动画==key的资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Storyboard? GetAnimation(string key);

        /// <summary>
        /// 获取page/win==key的资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        UIElement GetRegionUIElement(string key);

        /// <summary>
        /// 获取控件元素name==key的控件元素，使用反射查找
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        UIElement GetControlUIElement(string key);

    }
    public partial class RegionManager : IregionManager
    {
        private static Dictionary<string, Region> _pageManager = new Dictionary<string, Region>();

        private RegionNode CurrentWin { get; set; }
        private IServiceProvider services;
        private sealed class Region
        {
            public bool IsIns = false;
            public Type UIType { get; private set; }

            public Type ViewModel { get; private set; }

            public FrameworkElement UI { get; private set; }

            public Region(Type regionType, Type viewModel)
            {
                UIType = regionType;
                ViewModel = viewModel;
            }

            public void InsUI(FrameworkElement element)
            {
                UI = element;
            }
            public void InsVM( object viewModel)
            {
                UI.DataContext = viewModel;
            }
        }

        /// <summary>
        /// 可以添加一个缓存列表
        /// 为当前已经查询过的UI的这样可以避免多次反射获取元素
        /// </summary>
        private sealed class RegionNode
        {

            public string Key { get; set; }
            /// <summary>
            /// 当前界面
            /// </summary>
            public UIElement CurrentWin { get; set; }
            /// <summary>
            /// 上一个界面的节点
            /// </summary>
            public RegionNode UpNode { get; set; }


            /// <summary>
            /// 获取当前界面定义的动画
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public Storyboard? GetAnimation(string key)
            {
                Storyboard storyboard = null;
                if (CurrentWin is FrameworkElement ui)
                {
                    storyboard = (Storyboard)ui.Resources[key];
                }
                return storyboard;
            }

            /// <summary>
            /// 或取当前界面的指定控件元素
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public UIElement? GetUIElement(string key)
            {
                var control = CurrentWin.GetType().GetField(IregionManager.Region, Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.GetField
                | Reflection.BindingFlags.Default | Reflection.BindingFlags.Instance).GetValue(CurrentWin);
                if (control != null)
                {
                    return (UIElement)control;
                }
                return default;
            }
        }
    }
}

