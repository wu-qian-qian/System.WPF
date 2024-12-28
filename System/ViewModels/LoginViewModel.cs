
using CommunityToolkit.Mvvm.Input;
using EventModule.Local;
using Extension;
using Model;
using System.IO;
using System.UI.Core;
using System.UI.Core.Region;
using System.UI.Local.Config;
using System.UI.Thread.Base;
using System.Windows.Controls;

namespace System.UI.Local.ViewModels
{
    [EventName("SelectBackground")]
    internal partial class LoginViewModel : ObservableObject, IEventHandler
    {
        private readonly IregionManager _regionManager;

        private readonly IEventBus _eventBus;

        private readonly ITaskHelper _taskHelper;

        private SysMenuViewModel viewModel;
        public LoginViewModel(IregionManager regionManager,IEventBus eventBus, ITaskHelper taskHelper)
        {
            _regionManager = regionManager;
            _eventBus = eventBus;
            _taskHelper = taskHelper;
               //默认添加自动登入
            LocalUserData localUser = FileHelper.ReadConfigJons<LocalUserData>("Assets/Config/Local.json");
            Handle<string>(localUser.SelectBackground).GetAwaiter().GetResult();
            //但是这样做有无法获取异常
            Task task = _taskHelper.CraetTask(
             () =>
            {
                //内部涉及到控件的使用所以需要回到UI线程
                Execute.OnUIThread(() =>
                {
                    //不能阻塞，会死锁
                    Login(new object[2] { localUser.UserName, localUser.Pwd }).ConfigureAwait(false);
                }, true);
            });
            task.GetAwaiter().GetResult();
        }

        [RelayCommand]
        private async Task Login(object info)
        {
            object[] objs = info as object[];
            var res=await Intermediary.Instance.InitLoding("登入提示", "正在登入。。。。", true, null,10);
            if (objs[0].ToString() == "admin" && objs[1].ToString() == "qwe")
            {
                res.UpdateMessage("登入成功");
                Intermediary.Instance.HideLogion();
                var u = new UserModel
                {
                    Name = "小吴",
                    Role = "主管",
                    BackgroudColor = new List<string> { "深色", "浅色" },
                    IndexBackgroudColor=1,
                    IconUrl = "https://webstatic.mihoyo.com/upload/contentweb/2022/08/15/ab72edd8acc105904aa50da90e4e788e_2299455865599609620.jpg"
                };
                  _regionManager.Active(nameof(SysMenuViewModel));
                //发送一个事件
                  await _eventBus.PublishClient<UserModel>("Login", u);
            }
            else
            {
                res.Close();
                Intermediary.Instance.OpenTips("登入失败", "登入失败\n账户或密码出现错误！！", null, Windows.MessageBoxButton.YesNo, 5);
            }
        }

        public  Task Handle<T>(T josn)
        {
            switch (josn.ToString())
            {
                case "深色":
                    Intermediary.Instance._themeManager.ApplyTheme("Dark");
                    break;
                case "浅色":
                    Intermediary.Instance._themeManager.ApplyTheme("Light");
                    break;
            }
            return Task.CompletedTask;
        }

    }
}
