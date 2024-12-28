using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model.People
{
    public class PeopleDataDto
    {
        public int Id { get; set; } = 1;

        public string Name { get; set; }="张三";  

        public string Sex { get; set; }="男";

        public string Phone { get; set; }="123456789";

        public string Address { get; set; }="北京市";

        public string Department { get; set; }="技术部";

        public string JobTitle { get; set; }="工程师";

        public string Description { get; set; }="这是一个人员信息";
    }
}
