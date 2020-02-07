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
    /// ImageUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageUiControl : UserControl
    {
        /* 属性: ImagePath(图片的路径)

           事件: ClickCloseButton(当点击[关闭]按钮时)
                 ClickFileButton(当点击[文件]按钮时)*/




        public ImageUiControl()
        {
            InitializeComponent();
        }



        #region 依赖项属性：ImagePath
        /// <summary>
        /// 依赖项属性：图片的路径
        /// </summary>
        public static DependencyProperty ImagePathProperty;

        /// <summary>
        /// 公开属性：图片的路径
        /// </summary>
        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ImagePathProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnImagePathChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
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
            args.RoutedEvent = ImageUiControl.ClickCloseButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickFileButton
        /// <summary>
        /// 路由事件：ClickFileButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickFileButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickFileButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickFileButton
        {
            //添加一条事件
            add { AddHandler(ClickFileButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickFileButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickFileButton 路由事件
        /// </summary>
        private void OnClickFileButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = ImageUiControl.ClickFileButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion



        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static ImageUiControl()
        {
            /*注册依赖项属性*/
            //注册ImagePathProperty
            ImagePathProperty = DependencyProperty.Register(
                "ImagePath", //属性的名字
                typeof(string), //属性的类型
                typeof(ImageUiControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                    //初始值
                    (string)"",
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnImagePathChanged))
            );






            /*注册路由事件*/
            //注册ClickCloseButtonEvent
            ClickCloseButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickCloseButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(ImageUiControl) //这个路由事件属于哪个控件？
            );

            //注册ClickFileButtonEvent
            ClickFileButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickFileButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ImageUiControl));


        }
        #endregion



        #region [事件 - 按钮]
        /// <summary>
        /// 当点击[关闭]按钮时
        /// </summary>
        private void CloseButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickCloseButton();//触发事件
        }

        /// <summary>
        /// 当点击[文件]按钮时
        /// </summary>
        private void FileButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickFileButton();//触发事件
        }
        #endregion
    }
}
