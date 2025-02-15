﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPF.Tools.Local.Converter
{
    public abstract class BaseValueConverter : MarkupExtension, IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

     public abstract class BaseMultiValueConverter : MarkupExtension, IMultiValueConverter
    {
        public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values;
        }

        public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
