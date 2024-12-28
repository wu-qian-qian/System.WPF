using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.Tools.Local.Converter;

namespace System.CustomControl.Converter
{
    /// <summary>
    /// 多数据绑定
    /// </summary>
    public class MultiBindingConverter : BaseMultiValueConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
         
            return values.ToArray();
        }
    }
}
