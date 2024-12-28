using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.Tools.Local.Converter;

namespace System.CustomControl.Converter
{

    public class BoolToVisibilityConverter : BaseValueConverter
    {
        public bool IsReverse { get; set; }

        public bool UseHidden { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool @bool = false;
            try
            {
                @bool= System.Convert.ToBoolean(value);
            }
            catch
            {
                if(value!=null)
                    @bool = true;
            }

            if (IsReverse) @bool = !@bool;

            if (@bool) return Visibility.Visible;
            else return UseHidden ? Visibility.Hidden : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vis = (Visibility)value;

            bool res = vis != Visibility.Visible;
            if (IsReverse) res = !res;

            return res;
        }
    }

}
