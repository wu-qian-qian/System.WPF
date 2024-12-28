using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using Panuon.UI.Silver;
using Panuon.UI.Silver.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.UI.Local.Statics.UI;
using System.Windows;

namespace System.UI.Core
{
    /// <summary>
    /// 依赖中介类
    /// </summary>
    public class Intermediary
    {
        public static Intermediary Instance { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }

        public DynamicTheme.Core.ThemeManager _themeManager;
        public static Intermediary CreatInstance()
        {
            if (Instance == null)
                Instance = new Intermediary();
            return Instance;
        }

        public Intermediary()
        {
            InitLoding = UIShowTool.InitLoding;
            UpdateLoding = UIShowTool.UpdateLoding;
            CloseLoading = UIShowTool.CloseLoding;

            OpenTips = UIShowTool.OpenTips;
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        #region 提示面板事件,后续的所有事件使用封装的事件处理
        /// <summary>
        /// 关闭系统事件
        /// </summary>
        public Action CloseSystem;
        /// <summary>
        /// 关闭登入面板
        /// </summary>
        public Action HideLogion;

        /// <summary>
        /// 加载等待面板
        /// </summary>
        public Func<string, string, bool, Window?, int, Task<IPendingHandler>>? InitLoding;
        /// <summary>
        /// 修改等待面板
        /// </summary>
        public Func<IPendingHandler, string, int, ValueTask>? UpdateLoding;
        /// <summary>
        /// 关闭等待面板
        /// </summary>
        public Action<IPendingHandler>? CloseLoading;
        /// <summary>
        /// 打开标签
        /// </summary>
        public Action<string, string, Window, MessageBoxButton, int>? OpenTips;
        #endregion




    }
}
