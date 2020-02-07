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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyBugManager
{
    /// <summary>
    /// NoButtonControl.xaml 的交互逻辑
    /// </summary>
    public partial class NoButtonControl : UserControl
    {
        /* 属性: PressAnimationSize(按下按钮时 动画缩小的尺寸)
                 MouseEnterImage(鼠标进入时的图片) 
                 MouseLeaveImage(鼠标移出时的图片)

           事件: OnClick(当点击按钮的时候)*/


        public NoButtonControl()
        {
            InitializeComponent();
        }




        #region 依赖项属性：PressAnimationSize

        /// <summary>
        /// 依赖项属性：按下按钮时 动画缩小的尺寸
        /// </summary>
        public static DependencyProperty PressAnimationSizeProperty;

        /// <summary>
        /// 公开属性：图标的宽度
        /// </summary>
        public Point PressAnimationSize
        {
            get { return (Point)GetValue(PressAnimationSizeProperty); }
            set { SetValue(PressAnimationSizeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IconHeightProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnPressAnimationSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：MouseEnterImage
        /// <summary>
        /// 依赖项属性：鼠标进入时的图片
        /// </summary>
        public static DependencyProperty MouseEnterImageProperty;

        /// <summary>
        /// 公开属性：鼠标进入时的图片
        /// </summary>
        public ImageBrush MouseEnterImage
        {
            get { return (ImageBrush)GetValue(MouseEnterImageProperty); }
            set { SetValue(MouseEnterImageProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ContentProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnMouseEnterImageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：MouseLeaveImage
        /// <summary>
        /// 依赖项属性：鼠标移出时的图片
        /// </summary>
        public static DependencyProperty MouseLeaveImageProperty;

        /// <summary>
        /// 公开属性：鼠标移出时的图片
        /// </summary>
        public ImageBrush MouseLeaveImage
        {
            get { return (ImageBrush)GetValue(MouseLeaveImageProperty); }
            set { SetValue(MouseLeaveImageProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ContentProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnMouseLeaveImageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion



        #region 路由事件：Click
        /// <summary>
        /// 路由事件：ClickEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickEvent;


        /// <summary>
        /// 路由事件的属性：Click
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> Click
        {
            //添加一条事件
            add { AddHandler(ClickEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 Click 路由事件
        /// </summary>
        private void OnClick()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = NoButtonControl.ClickEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion



        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static NoButtonControl()
        {
            /*注册依赖项属性*/
            //注册MouseLeaveImageProperty
            MouseLeaveImageProperty = DependencyProperty.Register(
                "MouseLeaveImage", //属性的名字
                typeof(ImageBrush), //属性的类型
                typeof(NoButtonControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (ImageBrush)null,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnMouseLeaveImageChanged))
            );

            //注册MouseEnterImageProperty
            MouseEnterImageProperty = DependencyProperty.Register(
                "MouseEnterImage", typeof(ImageBrush), typeof(NoButtonControl),
                new FrameworkPropertyMetadata((ImageBrush)null,
                    new PropertyChangedCallback(OnMouseEnterImageChanged)));

            //注册PressAnimationSizeProperty
            PressAnimationSizeProperty = DependencyProperty.Register(
                "PressAnimationSize", typeof(Point), typeof(NoButtonControl),
                new FrameworkPropertyMetadata((Point)new Point(0.75,0.75), new PropertyChangedCallback(OnPressAnimationSizeChanged)));





            /*注册路由事件*/
            //注册ClickEvent
            ClickEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "Click", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(NoButtonControl) //这个路由事件属于哪个控件？
            );

        }
        #endregion



        #region 事件 -[点击按钮动画]
        //当鼠标进入[按钮]的时候，触发此方法
        private void BaseButton_OnMouseEnter(object sender, MouseEventArgs e)
        {
            /* 更改按钮的背景 */
            //创建绑定数据
            Binding _binding = new Binding()
            {
                ElementName = "NoButtonUserControl",//要绑定的控件名称
                Path = new PropertyPath("MouseEnterImage"),// 需绑定的属性名
            };
            //重新绑定
            this.ButtonImageBorder.SetBinding(BackgroundProperty, _binding);
        }

        //当鼠标离开[按钮]的时候，触发此方法
        private void BaseButton_OnMouseLeave(object sender, MouseEventArgs e)
        {
            /* 更改按钮的背景 */
            //创建绑定数据
            Binding _binding = new Binding()
            {
                ElementName = "NoButtonUserControl",//要绑定的控件名称
                Path = new PropertyPath("MouseLeaveImage"),// 需绑定的属性名
            };
            //重新绑定
            this.ButtonImageBorder.SetBinding(BackgroundProperty, _binding);
        }


        //当鼠标在[按钮]上点击的时候，触发此方法
        private void Button_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            /* 获取"按下动画"，并播放动画 */
            AnimationTool.PlayButtonAnimation(true, this.PressAnimationSize, this.BaseButtonScaleTransform);
        }


        //当鼠标在[按钮]上抬起的时候，触发此方法
        private void Button_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            /* 获取"抬起动画"，并播放动画 */
            AnimationTool.PlayButtonAnimation(false, this.PressAnimationSize, this.BaseButtonScaleTransform);
        }


        //当鼠标在[按钮]上按下的时候，触发此方法
        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            //触发点击事件
            OnClick();
        }
        #endregion
    }
}
