using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CustomControl.UI.Helper
{
    public static class PageIndexStaticResource
    {
        public static string PageTitle = "每页显示条目数：";
        public static string DataCount = "共计条目：";
        public static string UpIndex = "上一页";
        public static string NextIndex = "下一页";
        public static string GotoIndex = "跳转到：";
        public static string Goto = "跳转";
        public static IEnumerable<string> PageCountList = new[] { "10", "20", "50", "80", "100" };
    }
}
