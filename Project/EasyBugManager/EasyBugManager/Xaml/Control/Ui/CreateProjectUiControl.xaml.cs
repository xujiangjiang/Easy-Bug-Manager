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

namespace EasyBugManager
{
    /// <summary>
    /// CreateProjectUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class CreateProjectUiControl : UserControl
    {
        /* 属性: ProjectName*(项目名)
                 SaveLocation(保存位置)
                 TipString(提示的文字)

                 IsShowAdvancedOption(是否显示高级选项？)
                 IsCollaborationMode(是否是协同合作模式？)

            事件: ClickBrowseButton(当点击[浏览]按钮时)
                  ClickYesButton(当点击[确认]按钮时)
                  ClickNoButton(当点击[取消]按钮时)*/



        public CreateProjectUiControl()
        {
            InitializeComponent();
        }



        #region 依赖项属性：ProjectName*
        /// <summary>
        /// 公开属性：项目名
        /// </summary>
        public string ProjectName
        {
            get { return this.ProjectNameTextBox.Text; }
            set { this.ProjectNameTextBox.Text = value; }
        }
        #endregion

        #region 依赖项属性：SaveLocation
        /// <summary>
        /// 依赖项属性：保存位置
        /// </summary>
        public static DependencyProperty SaveLocationProperty;

        /// <summary>
        /// 公开属性：保存位置
        /// </summary>
        public string SaveLocation
        {
            get { return (string)GetValue(SaveLocationProperty); }
            set { SetValue(SaveLocationProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当SaveLocationProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnSaveLocationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：TipString
        /// <summary>
        /// 依赖项属性：提示的文字
        /// </summary>
        public static DependencyProperty TipStringProperty;

        /// <summary>
        /// 公开属性：提示的文字
        /// </summary>
        public string TipString
        {
            get { return (string)GetValue(TipStringProperty); }
            set { SetValue(TipStringProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当TipStringProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnTipStringChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion


        #region 依赖项属性：IsShowAdvancedOption
        /// <summary>
        /// 依赖项属性：是否显示高级选项？
        /// </summary>
        public static DependencyProperty IsShowAdvancedOptionProperty;

        /// <summary>
        /// 公开属性：是否显示高级选项？
        /// </summary>
        public bool IsShowAdvancedOption
        {
            get { return (bool)GetValue(IsShowAdvancedOptionProperty); }
            set { SetValue(IsShowAdvancedOptionProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsShowAdvancedOptionProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsShowAdvancedOptionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取到控件
            CreateProjectUiControl _createProjectUiControl = sender as CreateProjectUiControl;

            //比较值
            switch ((bool)e.NewValue)
            {
                //如果要显示[高级选项]
                case true:
                    _createProjectUiControl.BackgroundBorder.Height = 520;
                    _createProjectUiControl.AdvancedOptionsButtonGrid.Visibility = Visibility.Collapsed;
                    _createProjectUiControl.AdvancedOptionsCanvas.Visibility = Visibility.Visible;
                    break;

                //如果不显示[高级选项]
                case false:
                    _createProjectUiControl.BackgroundBorder.Height = 292;
                    _createProjectUiControl.AdvancedOptionsButtonGrid.Visibility = Visibility.Visible;
                    _createProjectUiControl.AdvancedOptionsCanvas.Visibility = Visibility.Collapsed;
                    break;
            }

        }
        #endregion

        #region 依赖项属性：IsCollaborationMode
        /// <summary>
        /// 依赖项属性：是否是协同合作模式？
        /// </summary>
        public static DependencyProperty IsCollaborationModeProperty;

        /// <summary>
        /// 公开属性：是否是协同合作模式？
        /// </summary>
        public bool IsCollaborationMode
        {
            get { return (bool)GetValue(IsCollaborationModeProperty); }
            set { SetValue(IsCollaborationModeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsCollaborationModeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsCollaborationModeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion




        #region 路由事件：ClickBrowseButton
        /// <summary>
        /// 路由事件：ClickBrowseButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickBrowseButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickBrowseButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickBrowseButton
        {
            //添加一条事件
            add { AddHandler(ClickBrowseButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickBrowseButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickBrowseButton 路由事件
        /// </summary>
        private void OnClickBrowseButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = CreateProjectUiControl.ClickBrowseButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickYesButton
        /// <summary>
        /// 路由事件：ClickBrowseButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickYesButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickYesButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickYesButton
        {
            //添加一条事件
            add { AddHandler(ClickYesButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickYesButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickYesButton 路由事件
        /// </summary>
        private void OnClickYesButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = CreateProjectUiControl.ClickYesButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickNoButton
        /// <summary>
        /// 路由事件：ClickNoButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickNoButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickNoButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickNoButton
        {
            //添加一条事件
            add { AddHandler(ClickNoButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickNoButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickNoButton 路由事件
        /// </summary>
        private void OnClickNoButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = CreateProjectUiControl.ClickNoButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion




        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static CreateProjectUiControl()
        {
            /*注册依赖项属性*/
            //注册SaveLocationProperty
            SaveLocationProperty = DependencyProperty.Register(
                "SaveLocation", //属性的名字
                typeof(string), //属性的类型
                typeof(CreateProjectUiControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (string)"",
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnSaveLocationChanged))
            );

            //注册TipStringProperty
            TipStringProperty = DependencyProperty.Register(
                "TipString", typeof(string), typeof(CreateProjectUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnTipStringChanged)));

            //注册IsShowAdvancedOptionProperty
            IsShowAdvancedOptionProperty = DependencyProperty.Register(
                "IsShowAdvancedOption", typeof(bool), typeof(CreateProjectUiControl),
                new FrameworkPropertyMetadata((bool)false, new PropertyChangedCallback(OnIsShowAdvancedOptionChanged)));

            //注册IsCollaborationModeProperty
            IsCollaborationModeProperty = DependencyProperty.Register(
                "IsCollaborationMode", typeof(bool), typeof(CreateProjectUiControl),
                new FrameworkPropertyMetadata((bool)false, new PropertyChangedCallback(OnIsCollaborationModeChanged)));





            /*注册路由事件*/
            //注册ClickBrowseButtonEvent
            ClickBrowseButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickBrowseButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(CreateProjectUiControl) //这个路由事件属于哪个控件？
            );

            //注册ClickYesButtonEvent
            ClickYesButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickYesButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(CreateProjectUiControl));

            //注册ClickNoButtonEvent
            ClickNoButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickNoButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(CreateProjectUiControl));

        }
        #endregion





        #region [事件 - 文本框]
        //当Name文本框里的文字改变时
        private void ProjectNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //取到TextBox控件
            TextBox _textBox = sender as TextBox;

            //如果TextBox的内容为空，就把TextBox的背景设置为透明
            //如果TextBox的内容不为空，把TextBox的背景设置为白色
            AnimationTool.PlayTextChangedAnimation(_textBox);
        }
        #endregion

        #region [事件 - 按钮]
        //当点击[浏览]按钮时
        private void BrowseButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickBrowseButton();//触发事件
        }

        //当点击[确认]按钮时
        private void YesButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickYesButton();//触发事件
        }

        //当点击[取消]按钮时
        private void NoButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickNoButton();//触发事件
        }
        #endregion

        #region [事件 - 高级选项]
        //当点击[高级选项]按钮时
        private void AdvancedOptionsButton_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.IsShowAdvancedOption = true;
        }
        #endregion


    }
}
