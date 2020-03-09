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
    /// ListTipControl.xaml 的交互逻辑
    /// </summary>
    public partial class ListTipControl : UserControl
    {
        /* 属性: IsAddBugCompleteTip(是否是[添加Bug成功]的提示？如果为true，就显示[添加Bug成功]的提示；如果为false，就显示[删除Bug成功]的提示)
                 IsOpen(是否打开界面？)
                 BugData(要操作的Bug数据。如果是可以[删除Bug成功]的话，可以为null)

           事件: ClickLookButton(当点击[查看]按钮时)
                 ClickCloseButton(当点击[关闭]按钮时) */


        public ListTipControl()
        {
            InitializeComponent();
        }



        #region 依赖项属性：IsAddBugCompleteTip
        /// <summary>
        /// 依赖项属性：是否是[添加Bug成功]的提示？(如果为true，就显示[添加Bug成功]的提示；如果为false，就显示[删除Bug成功]的提示)
        /// </summary>
        public static DependencyProperty IsAddBugCompleteTipProperty;

        /// <summary>
        /// 公开属性：是否是[添加Bug成功]的提示？(如果为true，就显示[添加Bug成功]的提示；如果为false，就显示[删除Bug成功]的提示)
        /// </summary>
        public bool IsAddBugCompleteTip
        {
            get { return (bool)GetValue(IsAddBugCompleteTipProperty); }
            set { SetValue(IsAddBugCompleteTipProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsAddBugCompleteTipProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsAddBugCompleteTipChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            ListTipControl _listTipControl = sender as ListTipControl;

            //进行判断
            switch ((bool)e.NewValue)
            {
                //如果是显示[添加Bug成功]的提示
                case true:
                    //打开[添加Bug的文字]，关闭[删除Bug的文字]，打开[关闭按钮]，打开[查看按钮]
                    _listTipControl.CreateBugBorder.Visibility = Visibility.Visible;
                    _listTipControl.DeleteBugBorder.Visibility = Visibility.Collapsed;
                    _listTipControl.LookButton.Visibility = Visibility.Visible;
                    _listTipControl.CloseButton.Visibility = Visibility.Visible;
                    break;

                //如果是显示[删除Bug成功]的提示
                case false:
                    //关闭[添加Bug的文字]，打开[删除Bug的文字]，打开[关闭按钮]，关闭[查看按钮]
                    _listTipControl.CreateBugBorder.Visibility = Visibility.Collapsed;
                    _listTipControl.DeleteBugBorder.Visibility = Visibility.Visible;
                    _listTipControl.LookButton.Visibility = Visibility.Collapsed;
                    _listTipControl.CloseButton.Visibility = Visibility.Visible;
                    break;
            }
        }
        #endregion

        #region 依赖项属性：IsOpen
        /// <summary>
        /// 依赖项属性：是否打开界面？
        /// </summary>
        public static DependencyProperty IsOpenProperty;

        /// <summary>
        /// 公开属性：是否打开界面？
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsOpenProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsOpenChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            ListTipControl _listTipControl = sender as ListTipControl;

            //进行判断
            switch ((bool)e.NewValue)
            {
                //如果是打开Tip
                case true:
                    //打开控件
                    _listTipControl.Visibility = Visibility.Visible;

                    /* 显示这个控件（把Opacity属性设置为1）*/
                    //播放[第1个]动画:把Opacity属性设置为1
                    AnimationTool.PlayGridOpacityAnimation(_listTipControl.BaseGrid, null, 1, 0.25f,
                      (object _sender1, EventArgs _e1) =>
                      {
                          //当[第1个]动画播放完成后，播放[第2个动画]：啥也不干，等待3秒，然后关闭控件
                          AnimationTool.PlayGridOpacityAnimation(_listTipControl.BaseGrid, 1, 1, 3f,
                              (object _sender2, EventArgs _e2) =>
                              {
                                    //当[第2个]动画播放完成后，关闭控件
                                    _listTipControl.IsOpen = false;
                              });

                      });
                    break;

                //如果是关闭Tip
                case false:
                    //隐藏这个控件（把Opacity属性设置为0）
                    AnimationTool.PlayGridOpacityAnimation(_listTipControl.BaseGrid, null, 0, 0.25f,
                        (object _sender1, EventArgs _e1) =>
                        {
                            //当动画播放完成后，关闭控件
                            _listTipControl.Visibility = Visibility.Collapsed;
                        });
                    break;
            }
        }
        #endregion

        #region 依赖项属性：BugData
        /// <summary>
        /// 依赖项属性：要操作的Bug数据 (如果是可以[删除Bug成功]的话，可以为null)
        /// </summary>
        public static DependencyProperty BugDataProperty;

        /// <summary>
        /// 公开属性：要操作的Bug数据 (如果是可以[删除Bug成功]的话，可以为null)
        /// </summary>
        public BugData BugData
        {
            get { return (BugData)GetValue(BugDataProperty); }
            set { SetValue(BugDataProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当BugDataProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnBugDataChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion



        #region 路由事件：ClickLookButton
        /// <summary>
        /// 路由事件：ClickLookButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickLookButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickLookButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickLookButton
        {
            //添加一条事件
            add { AddHandler(ClickLookButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickLookButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickLookButton 路由事件
        /// </summary>
        private void OnClickLookButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListTipControl.ClickLookButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickCloseButton
        /// <summary>
        /// 路由事件：ClickCloseButtonEvent
        /// （当点击按钮时，触发此事件）
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
            args.RoutedEvent = ListTipControl.ClickCloseButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion



        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static ListTipControl()
        {
            /*注册依赖项属性*/
            //注册IsAddBugCompleteTipProperty
            IsAddBugCompleteTipProperty = DependencyProperty.Register(
                "IsAddBugCompleteTip", //属性的名字
                typeof(bool), //属性的类型
                typeof(ListTipControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (bool)false,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnIsAddBugCompleteTipChanged))
            );

            //注册IsOpenProperty
            IsOpenProperty = DependencyProperty.Register(
                "IsOpen", typeof(bool), typeof(ListTipControl),
                new FrameworkPropertyMetadata((bool)false, new PropertyChangedCallback(OnIsOpenChanged)));

            //注册BugDataProperty
            BugDataProperty = DependencyProperty.Register(
                "BugData", typeof(BugData), typeof(ListTipControl),
                new FrameworkPropertyMetadata((BugData)null, new PropertyChangedCallback(OnBugDataChanged)));








            /*注册路由事件*/
            //注册ClickLookButtonEvent
            ClickLookButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickLookButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(ListTipControl) //这个路由事件属于哪个控件？
            );

            //注册ClickCloseButtonEvent
            ClickCloseButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickCloseButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ListTipControl));


        }
        #endregion




        #region [事件 - 按钮]
        /// <summary>
        /// 当点击[查看]按钮时
        /// </summary>
        private void LookButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickLookButton();
        }

        /// <summary>
        /// 当点击[关闭]按钮时
        /// </summary>
        private void CloseButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickCloseButton();
        }
        #endregion

    }
}
