using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.UI.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace System.UI.Main
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            Intermediary.Instance.HideLogion = Logion;
        }

        private void Window_DragMove(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Intermediary.Instance.CloseSystem();
        }

        private void Logion()
        {
            var loginAnit = (Storyboard) this.Resources["HideLogin"];
            loginAnit.Begin();
            SysName.Visibility = Visibility.Collapsed;
            login.Visibility = Visibility.Collapsed;
            AniShow.Visibility = Visibility.Collapsed;
            Region.Visibility = Visibility.Visible;
            
        }

        private void CloseMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OpenMax_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
    }
}
