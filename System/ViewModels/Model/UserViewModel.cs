using CommunityToolkit.Mvvm.Input;
using EventModule.Local;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace System.UI.ViewModels.Model
{
    public partial class UserViewModel: ObservableObject
    {
        private readonly IEventBus _eventBus;
        public Action updatePassword;
        public Action updateUserDataBox;
        public string Name { get; set; }

        public string Role { get; set; }

        public List<string> BackgroudColor { get; set; }

        public string IconUrl { get; set; }

        /// <summary>
        /// 选中的颜色
        /// </summary>
        public int IndexBackgroudColor { get; set; }
        public UserViewModel(UserModel userModel, IEventBus eventBus)
        {
            Name=userModel.Name;
            Role=userModel.Role;
            BackgroudColor = userModel.BackgroudColor;
            IconUrl = userModel.IconUrl;
            IndexBackgroudColor=userModel.IndexBackgroudColor;
            _eventBus= eventBus;
        }
        [RelayCommand]
        public async void SelectBackground(string item)
        {

            await _eventBus.PublishClient<string>("SelectBackground", item);
        }
        [RelayCommand]
        public void UpdatePwd()
        {
            updatePassword?.Invoke();
        }
        [RelayCommand]
        public void UpdateUser()
        {
            updateUserDataBox?.Invoke();
        }
        public bool Equals(UserModel user)
        {
            return Name == user.Name && Role == user.Role && IndexBackgroudColor == user.IndexBackgroudColor;
        }
    }
}
