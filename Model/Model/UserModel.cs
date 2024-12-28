using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 用户数据模型
    /// </summary>
    public class UserModel
    {
        public string Name { get; set; }

        public string Role { get; set; }

        public List<string> BackgroudColor { get; set; }

        public string IconUrl { get; set; }

        /// <summary>
        /// 选中的颜色
        /// </summary>
        public int IndexBackgroudColor { get; set; }
    }
}
