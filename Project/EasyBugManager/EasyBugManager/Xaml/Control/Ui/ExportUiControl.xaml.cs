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
    /// ExportUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class ExportUiControl : UserControl
    {
        /* 属性: ExportLocation(导出位置)
                 TipString(提示的文字)

           事件: ClickBrowseButton(当点击[浏览]按钮时)
                 ClickYesButton(当点击[确认]按钮时)
                 ClickNoButton(当点击[取消]按钮时)*/



        public ExportUiControl()
        {
            InitializeComponent();
        }



        #region 依赖项属性：ExportLocation
        /// <summary>
        /// 依赖项属性：导出位置
        /// </summary>
        public static DependencyProperty ExportLocationProperty;

        /// <summary>
        /// 公开属性：导出位置
        /// </summary>
        public string ExportLocation
        {
            get { return (string)GetValue(ExportLocationProperty); }
            set { SetValue(ExportLocationProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ExportLocationProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnExportLocationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
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
            args.RoutedEvent = ExportUiControl.ClickBrowseButtonEvent;

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
            args.RoutedEvent = ExportUiControl.ClickYesButtonEvent;

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
            args.RoutedEvent = ExportUiControl.ClickNoButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion




        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static ExportUiControl()
        {
            /*注册依赖项属性*/
            //注册ExportLocationProperty
            ExportLocationProperty = DependencyProperty.Register(
                "ExportLocation", //属性的名字
                typeof(string), //属性的类型
                typeof(ExportUiControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                    //初始值
                    (string)"",
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnExportLocationChanged))
            );

            //注册TipStringProperty
            TipStringProperty = DependencyProperty.Register(
                "TipString", typeof(string), typeof(ExportUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnTipStringChanged)));





            /*注册路由事件*/
            //注册ClickBrowseButtonEvent
            ClickBrowseButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickBrowseButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(ExportUiControl) //这个路由事件属于哪个控件？
            );

            //注册ClickYesButtonEvent
            ClickYesButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickYesButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ExportUiControl));

            //注册ClickNoButtonEvent
            ClickNoButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickNoButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ExportUiControl));

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
    }
}
