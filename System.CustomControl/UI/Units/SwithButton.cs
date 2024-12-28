using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace System.CustomControl.UI.Units
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:System.CustomControl.UI.Units"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:System.CustomControl.UI.Units;assembly=System.CustomControl.UI.Units"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:SwithButton/>
    ///
    /// </summary>
    public class SwithButton : RadioButton
    {
        static SwithButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwithButton), new FrameworkPropertyMetadata(typeof(SwithButton)));
        }



        public Brush CheckBackground
        {
            get { return (Brush)GetValue(CheckProperty); }
            set { SetValue(CheckProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CheckBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckProperty =
            DependencyProperty.Register("CheckBackground", typeof(Brush), typeof(SwithButton), new PropertyMetadata(null));



        public Brush UnCheckBackground
        {
            get { return (Brush)GetValue(UnCheckProperty); }
            set { SetValue(UnCheckProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnCheckBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnCheckProperty =
            DependencyProperty.Register("UnCheckBackground", typeof(Brush), typeof(SwithButton), new PropertyMetadata(null));



        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(SwithButton), new PropertyMetadata(null));




    }
}
