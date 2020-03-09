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
    /// MainUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class MainUiControl : UserControl
    {
        /*  事件： ClickMinimizeButton（当点击[最小化]按钮时）
                 ClickCloseButton（当点击[关闭]按钮时）
                 ClickSettingButton（当点击[设置]按钮时）
                 ClickCreateProjectButton（当点击[创建项目]按钮时）
                 ClickOpenProjectButton（当点击[打开项目]按钮时）
                 ClickLatelyButton（当点击[最近]按钮时）*/


        public MainUiControl()
        {
            InitializeComponent();
        }



        #region 路由事件：ClickMinimizeButton
        /// <summary>
        /// 路由事件：ClickMinimizeButtonEvent
        /// （当点击[最小化]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickMinimizeButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickMinimizeButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickMinimizeButton
        {
            //添加一条事件
            add { AddHandler(ClickMinimizeButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickMinimizeButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickMinimizeButton 路由事件
        /// </summary>
        private void OnClickMinimizeButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = MainUiControl.ClickMinimizeButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickCloseButton
        /// <summary>
        /// 路由事件：ClickCloseButtonEvent
        /// （当点击[最小化]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickCloseButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickCloseButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickCloseButton
        {
            //添加一条事件
            add { AddHandler(ClickCloseButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickCloseButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickCloseButton 路由事件
        /// </summary>
        private void OnClickCloseButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = MainUiControl.ClickCloseButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickSettingButton
        /// <summary>
        /// 路由事件：ClickSettingButtonEvent
        /// （当点击[设置]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickSettingButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickSettingButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickSettingButton
        {
            //添加一条事件
            add { AddHandler(ClickSettingButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickSettingButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickSettingButton 路由事件
        /// </summary>
        private void OnClickSettingButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = MainUiControl.ClickSettingButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickCreateProjectButton
        /// <summary>
        /// 路由事件：ClickCreateProjectButtonEvent
        /// （当点击[创建项目]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickCreateProjectButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickCreateProjectButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickCreateProjectButton
        {
            //添加一条事件
            add { AddHandler(ClickCreateProjectButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickCreateProjectButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickCreateProjectButton 路由事件
        /// </summary>
        private void OnClickCreateProjectButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = MainUiControl.ClickCreateProjectButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickOpenProjectButton
        /// <summary>
        /// 路由事件：ClickOpenProjectButtonEvent
        /// （当点击[打开项目]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickOpenProjectButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickOpenProjectButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickOpenProjectButton
        {
            //添加一条事件
            add { AddHandler(ClickOpenProjectButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickOpenProjectButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickOpenProjectButton 路由事件
        /// </summary>
        private void OnClickOpenProjectButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = MainUiControl.ClickOpenProjectButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickLatelyButton
        /// <summary>
        /// 路由事件：ClickLatelyButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickLatelyButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickLatelyButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickLatelyButton
        {
            //添加一条事件
            add { AddHandler(ClickLatelyButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickLatelyButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickLatelyButton 路由事件
        /// </summary>
        private void OnClickLatelyButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = MainUiControl.ClickLatelyButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion



        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static MainUiControl()
        {
            /*注册路由事件*/
            //注册ClickMinimizeButtonEvent
            ClickMinimizeButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickMinimizeButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(MainUiControl) //这个路由事件属于哪个控件？
            );

            //注册ClickCloseButtonEvent
            ClickCloseButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickCloseButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(MainUiControl));

            //注册ClickSettingButtonEvent
            ClickSettingButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickSettingButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(MainUiControl));

            //注册ClickCreateProjectButtonEvent
            ClickCreateProjectButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickCreateProjectButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(MainUiControl));

            //注册ClickOpenProjectButtonEvent
            ClickOpenProjectButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickOpenProjectButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(MainUiControl));

            //注册ClickLatelyButtonEvent
            ClickLatelyButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickLatelyButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(MainUiControl));
        }
        #endregion




        #region [事件]
        /// <summary>
        /// 当点击[最小化]按钮时
        /// </summary>
        private void MinimizeButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickMinimizeButton();//触发[点击最小化按钮]的事件
        }

        /// <summary>
        /// 当点击[关闭]按钮时
        /// </summary>
        private void CloseButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickCloseButton();//触发[点击关闭按钮]的事件
        }

        /// <summary>
        /// 当点击[设置]按钮时
        /// </summary>
        private void SettingButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickSettingButton();//触发[点击设置按钮]的事件
        }

        /// <summary>
        /// 当点击[创建项目]按钮时
        /// </summary>
        private void CreateProjectButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickCreateProjectButton();//触发[点击创建项目按钮]的事件
        }

        /// <summary>
        /// 当点击[打开项目]按钮时
        /// </summary>
        private void OpenProjectButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickOpenProjectButton();//触发[点击打开项目按钮]的事件
        }

        /// <summary>
        /// 当点击[最近]按钮时
        /// </summary>
        private void LatelyButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickLatelyButton();//触发[最近按钮]的事件
        }
        #endregion

        #region [事件 - 拖动窗口]

        /// <summary>
        /// 当在窗口顶部的矩形中，按下鼠标左键的时候：拖动窗口的时候
        /// </summary>
        private void WindowTitleBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /* 当点击拖拽区域的时候，让窗口跟着移动 */
            //DragMove();

            /*当点击拖拽区域的时候，让窗口跟着移动（并且阻止[窗口拖到屏幕边缘时 自动最大化]）*/
            AppManager.MainWindow.DragMoveWindow(e);
        }

        #endregion




    }
}
