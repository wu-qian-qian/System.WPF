using HttpFactory;
using Model.Model.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Tools.Local.Service;

namespace System.UI.Services
{
    public class PersonnelService:IService
    {
        private HttpClientFactory _httpfactory;
        public PersonnelService(HttpClientFactory httpfactory)
        {
            _httpfactory = httpfactory;
        }

        public List<PeopleDataDto> GetPeopleDataDtos()
        {
            return new List<PeopleDataDto>
            {
                new PeopleDataDto
                {
                  
                },
                 new PeopleDataDto
                {

                },
                 new PeopleDataDto {},
                 new PeopleDataDto {},
                 new PeopleDataDto {},
                 new PeopleDataDto {},
                 new PeopleDataDto {},
                 new PeopleDataDto {},
                 new PeopleDataDto {},
                  new PeopleDataDto {},
                 new PeopleDataDto {},
                 new PeopleDataDto {},
                 new PeopleDataDto {},
                 new PeopleDataDto {},
                 new PeopleDataDto {},
            };
        }
    }
}
