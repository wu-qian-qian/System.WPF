using HttpFactory;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Tools.Local.Service;

namespace System.UI.Services
{
    public class MenuService:IService
    {
        private HttpClientFactory _httpfactory;
        public MenuService(HttpClientFactory httpfactory) 
        {
            _httpfactory=httpfactory;
        }

        public List<MenuModel> GetMenuModels(string parameter)
        {
            if (parameter == "1")
            {
                return GetSystemMenuModels();
            }
            else
            {
                return GetPeopleMenuModels();
            }
        }

        private List<MenuModel> GetSystemMenuModels()
        {
            var menuList = new List<MenuModel>()
            {
                new MenuModel()
                {
                    Name="人事系统"
                },
                  new MenuModel()
                {
                    Name="财务系统"
                },
                    new MenuModel()
                {
                    Name="库存管理"
                },
                new MenuModel()
                {
                    Name="基础数据"
                },
                  new MenuModel()
                {
                    Name="邮件"
                },
                    new MenuModel()
                {
                    Name="聊天"
                },

                new MenuModel()
                {
                    Name="其他"
                }
            };
            return menuList;
        }
        private List<MenuModel> GetPeopleMenuModels()
        {
            var menuList = new List<MenuModel>()
            {
                new MenuModel()
                {
                    Name="人事管理",
                    Url="PersonnelManagementVM"
                },
                  new MenuModel()
                {
                    Name="考勤管理",
                     Url="AttendanceManagementVM"
                },
                new MenuModel()
                {
                    Name="其他",
                    Url="PersonnelManagementVM"
                },  new MenuModel()
                {
                    Name="其他",
                    Url="PersonnelManagementVM"
                },  new MenuModel()
                {
                    Name="其他",
                    Url="PersonnelManagementVM"
                },  new MenuModel()
                {
                    Name="其他",
                    Url="PersonnelManagementVM"
                },  new MenuModel()
                {
                    Name="其他",
                    Url="PersonnelManagementVM"
                },  new MenuModel()
                {
                    Name="其他",
                    Url="PersonnelManagementVM"
                },
            };
            return menuList;
        }
    }
}
