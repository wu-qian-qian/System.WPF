using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace System.CustomControl.UI.Helper
{
    public static class PasswordBoxHelper
    {
        static bool _isInit = false;
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata("", OnUpdataPasswordPropertyCallBack));

        private static void OnUpdataPasswordPropertyCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            if (!_isInit)
            {
                _isInit = true;
                passwordBox.PasswordChanged += PasswordBoxPasswordChanged;
            }
        }

        static void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            if (GetPassword(passwordBox)!= passwordBox.Password)
            {
                SetPassword(passwordBox, passwordBox.Password);
            }
           
        }
    }
}
