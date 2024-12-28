using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.UI.Local.Config
{
    public record LocalUserData
    {
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string SelectBackground { get; set; }
    }
}
