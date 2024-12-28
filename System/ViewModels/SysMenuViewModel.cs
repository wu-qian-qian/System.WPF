using CommunityToolkit.Mvvm.Input;
using DynamicTheme.Core;
using EventModule.Local;
using Model;
using Model.Enum;
using System.Collections.ObjectModel;
using System.UI.Core;
using System.UI.Core.Region;
using System.UI.Services;
using System.UI.ViewModels;
using System.UI.ViewModels.Model;
using System.Windows.Controls;

namespace System.UI.Local.ViewModels
{
    [EventName("Login")]
    [EventName("Close")]
    public partial class SysMenuViewModel : ObservableObject, IEventHandler
    {
        private readonly IregionManager _regionManager;
        private readonly MenuService _menuService;
        private readonly IEventBus _eventBus;

        [ObservableProperty]
        private ObservableCollection<MenuModel> menus;

        [ObservableProperty]
        private MenuModel selectMenus;

        [ObservableProperty]
        private UserViewModel userModel;

        [ObservableProperty]
        private bool isLoadModules = false;
        public SysMenuViewModel(IregionManager regionManager, MenuService menuService, IEventBus eventBus)
        {
            _regionManager = regionManager;
            _menuService = menuService;
            _eventBus = eventBus;
            Init();


        }

        void Init()
        {
            var menuList = _menuService.GetMenuModels("1");
            Menus = new ObservableCollection<MenuModel>();
            foreach (var item in menuList)
            {
                Menus.Add(item);
            }
        }
        
        [RelayCommand]
        private async Task ReLoaing()
        {
           await Intermediary.Instance.InitLoding("登入提示", "填充数据！！！", true, null, 2210);
           Init();
        }
        [RelayCommand]
        private async Task LoginModules()
        {
            IsLoadModules = true;
            SelectMenus = null;
            _regionManager.Active(nameof(PeopleViewModel));
        }
        [RelayCommand]
        private async Task BackGoto()
        {
            _regionManager.GoBack();
            IsLoadModules = false;
        }

        /// <summary>
        /// region来开启修改密码TODO
        /// </summary>
        private void UpdatePassword()
        {

        }
        /// <summary>
        /// region来开启修改用户数据TODO
        /// </summary>
        private void UpdateUserDataBox()
        {

        }

        public async Task Handle<T>(T eventData)
        {
            if (typeof(T) == typeof(UserModel))
            {
                var user = eventData as UserModel;
                CreatOrUpdateUser(user);  
            }
            if (eventData is ValueType @enum)
            {
                switch (@enum)
                {
                    case OnlineState.Close:
                    //TODO 程序退出向服务器发送一些数据信息
                        break;
                }
            }
            await Task.CompletedTask;
        }

        private void CreatOrUpdateUser(UserModel user)
        {
            if (UserModel == null|| UserModel.Equals(user)==false)
            {
                UserModel = new UserViewModel(user,_eventBus);
                UserModel.updatePassword = UpdatePassword;
                userModel.updateUserDataBox= UpdateUserDataBox;
            }
        }
    }
}
