using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Xaml.Behaviors.Core;


namespace System.CustomControl.UI.Units
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///    xmlns:MyNamespace="clr-namespace:System.CustomControl.UI.Units"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///    xmlns:MyNamespace="clr-namespace:System.CustomControl.UI.Units;assembly=System.CustomControl.UI.Units"
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
    ///    <MyNamespace:PageIndex/>
    ///
    /// </summary>
    public class PageIndex : Control
    {
        /// <summary>
        /// 模板中文本框控件的名字
        /// </summary>
        const string PART_BtnPagePanel = "PART_BtnPagePanel";
        /// <summary>
        /// 显示项的面板
        /// </summary>
        private Panel _btnPagePanel = null;
        static PageIndex()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PageIndex), new FrameworkPropertyMetadata(typeof(PageIndex)));
        }
        public PageIndex()
        {
            BtnClickCommand = new ActionCommand(GotoPage);
        }
        
        public override void OnApplyTemplate()
        {
            if (_btnPagePanel == null)
                _btnPagePanel = GetTemplateChild(PART_BtnPagePanel) as Panel;
            base.OnApplyTemplate();
        }
        private void UpdatePagePanel()
        {
            _btnPagePanel.Children.Clear();
            int index = (DataIndex % PageCount);
            //最大页数
            index = index > 0 ? DataIndex / PageCount + 1 : DataIndex / PageCount;
            //开始变化的页面阈值
            int maxShow = index % 6;
            int maxValue = maxShow;
            //大于等于2才有按钮
            if (index > 9)
            {
                Button btn1 = new Button();
                btn1.Content = "上一页";
                btn1.Command = BtnClickCommand;
                btn1.CommandParameter = btn1.Content;
                _btnPagePanel.Children.Add(btn1);
                for (int i = 0; i < 8; i++)
                {
                    Button btn = new Button();
                    btn.Command = BtnClickCommand;
                    _btnPagePanel.Children.Add(btn);
                    if (i == 0)
                    {
                        btn.Content = "首页";
                        btn.CommandParameter = btn.Content;
                        continue;
                    }
                    if (i == 7)
                    {
                        btn.Content = "尾页";
                        btn.CommandParameter = btn.Content;
                        continue;
                    }
                    if (PageNumber > maxShow)
                    {
                       if (index - PageNumber < maxShow)
                        {
                            btn.Content = (index - maxValue).ToString();
                            btn.CommandParameter = btn.Content;
                           maxValue--;
                        }
                        else
                        {
                            btn.Content = (i + PageNumber - 2).ToString();
                            btn.CommandParameter = btn.Content;
                        }
                    }
                    else
                    {
                        btn.Content = i.ToString();
                    }
                    if (btn.Content.ToString() == PageNumber.ToString())
                        btn.Background = Brushes.Black;
                    btn.CommandParameter = btn.Content;
                }
                Button btn2 = new Button();
                btn2.Content = "下一页";
                btn2.Command = BtnClickCommand;
                btn2.CommandParameter = btn2.Content;
                _btnPagePanel.Children.Add(btn2);
            }
            else if (index < 9 && index > 1)
            {
                Button btn1 = new Button();
                btn1.Content = "上一页";
                btn1.Command = BtnClickCommand;
                btn1.CommandParameter = btn1.Content;
                _btnPagePanel.Children.Add(btn1);
                for (int i = 1; i <= index; i++)
                {
                    Button btn = new Button();
                    btn.Content = i.ToString();
                    btn.Command = BtnClickCommand;
                    btn.CommandParameter = btn.Content;
                    if (btn.Content.ToString() == PageNumber.ToString())
                        btn.Background = Brushes.Black;
                    _btnPagePanel.Children.Add(btn);
                }
                Button btn2 = new Button();
                btn2.Content = "下一页";
                btn2.Command = BtnClickCommand;
                btn2.CommandParameter = btn2.Content;
                _btnPagePanel.Children.Add(btn2);
            }
        }
        private void GotoPage(object content)
        {
            int index = (DataIndex % PageCount);
            //最大页数
            index = index > 0 ? DataIndex / PageCount + 1 : DataIndex / PageCount;
            if (content == "下一页" && PageNumber < index)
            {

                PageNumber += 1;
            }
            else if (content == "上一页" && PageNumber > 1)
            {
                PageNumber += 1;
            }
            else if (content == "首页")
            {
                PageNumber = 1;
            }
            else if (content == "尾页")
            {
                PageNumber = index;
            }
            else
            {
             if (int.TryParse(content.ToString(), out var pagenum))
                {
                    if(pagenum>0&&pagenum<=index)
                        PageNumber = pagenum;
                }
              
            }
            UpdatePagePanel();
        }

        /// <summary>
        /// 当前页的条目数
        /// </summary>
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IndexTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountProperty =
             DependencyProperty.Register("PageCount", typeof(int), typeof(PageIndex), new PropertyMetadata(0, ChagePageCountCallBack));

        private static void ChagePageCountCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PageIndex pg = d as PageIndex;

            if (e.OldValue != e.NewValue)
            {
                pg.PageNumber = 1;
                pg.PageCount = (int)e.NewValue;
                pg.UpdatePagePanel();
            }
        }

        /// <summary>
        /// 总条目
        /// </summary>
        public int DataIndex
        {
            get { return (int)GetValue(DataIndexProperty); }
            set { SetValue(DataIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IndexTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataIndexProperty =
            DependencyProperty.Register("DataIndex", typeof(int), typeof(PageIndex), new PropertyMetadata(0));

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageNumber
        {
            get { return (int)GetValue(PageNumberProperty); }
            set { SetValue(PageNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IndexTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageNumberProperty =
             DependencyProperty.Register("PageNumber", typeof(int), typeof(PageIndex), new PropertyMetadata(1, ChagePageNumberCallBack));

        private static void ChagePageNumberCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PageIndex pg = d as PageIndex;
            object o = new object[] { pg.PageNumber, pg.PageCount };
            pg.PageGotoCommand.Execute(o);
        }

        /// <summary>
        /// 页面跳转命令
        /// 外部调用
        /// </summary>
        public ICommand PageGotoCommand
        {
            get { return (ICommand)GetValue(PageGotoCommandProperty); }
            set { SetValue(PageGotoCommandProperty, value); }
        }

        public static readonly DependencyProperty PageGotoCommandProperty =
             DependencyProperty.Register("PageGotoCommand", typeof(ICommand), typeof(PageIndex), new PropertyMetadata(null));

        /// <summary>
        /// 页面跳转命令
        /// </summary>
        private ICommand BtnClickCommand
        {
            get { return (ICommand)GetValue(BtnClickCommandProperty); }
            set { SetValue(BtnClickCommandProperty, value); }
        }

        private static readonly DependencyProperty BtnClickCommandProperty =
            DependencyProperty.Register("BtnClickCommand", typeof(ICommand), typeof(PageIndex), new PropertyMetadata(null));
    }
}