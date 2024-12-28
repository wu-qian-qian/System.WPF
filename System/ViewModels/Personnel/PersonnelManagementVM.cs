using Model.Model.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.UI.Services;

namespace System.UI.ViewModels.Personnel
{
    public partial class PersonnelManagementVM:ObservableObject
    {
        private readonly PersonnelService _personnelService;

        public PersonnelManagementVM(PersonnelService personnelService)
        {
            _personnelService = personnelService;
            peopleDataDtos= _personnelService.GetPeopleDataDtos();
        }

        [ObservableProperty]
        private List<PeopleDataDto> peopleDataDtos;
    }
}
