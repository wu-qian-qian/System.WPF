using Model;
using System.UI.Core.Region;
using System.UI.Services;

namespace System.UI.ViewModels
{
    public partial class PeopleViewModel: ObservableObject
    {
        private readonly MenuService _menuService;

        private readonly IregionManager _regionManager;

        [ObservableProperty]
        List<MenuModel> menus;

        private  MenuModel selectMenu;
        public MenuModel SelectMenu
        {
            get { return selectMenu; }
            set
            {
                if(selectMenu != value)
                {
                    selectMenu = value;
                    SelectMenuView(selectMenu);
                    OnPropertyChanged(nameof(SelectMenu));
                }
            }
        }

        public PeopleViewModel(MenuService menuService,IregionManager regionManager)
        {
            _menuService= menuService;
            _regionManager= regionManager;
            Menus = _menuService.GetMenuModels("2");
            SelectMenu = Menus[0];
        }
        private void SelectMenuView(MenuModel menu)
        {
            _regionManager.Active(menu.Url, false); 
        }
    }
}
