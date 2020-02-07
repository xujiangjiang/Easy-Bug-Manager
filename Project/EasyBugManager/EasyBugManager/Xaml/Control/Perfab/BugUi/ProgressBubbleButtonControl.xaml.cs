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
    /// ProgressBubbleButtonControl.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressBubbleButtonControl : UserControl
    {
        /* 属性: ProgressType(进度的类型)
                 PressAnimationSize(按下按钮时 动画缩小的尺寸)

                 MouseEnterImage(鼠标进入时的图片) 
                 MouseLeaveImage(鼠标移出时的图片)

                 UndoneImage(未完成的图标)
                 SolvedImage(已解决的图标)
                 DeprecatImage(已废弃的图标)


           事件: Click(当点击按钮的时候)*/




        public ProgressBubbleButtonControl()
        {
            InitializeComponent();
        }



        #region 依赖项属性：ProgressType
        /// <summary>
        /// 依赖项属性：进度的类型
        /// </summary>
        public static DependencyProperty ProgressTypeProperty;

        /// <summary>
        /// 公开属性：进度的类型
        /// </summary>
        public ProgressType ProgressType
        {
            get { return (ProgressType)GetValue(ProgressTypeProperty); }
            set { SetValue(ProgressTypeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ProgressTypeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnProgressTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            ProgressBubbleButtonControl _progressBubbleButtonControl = sender as ProgressBubbleButtonControl;

            //判断值
            switch ((ProgressType)e.NewValue) 
            {
                //如果是空
                case ProgressType.None:
                    _progressBubbleButtonControl.TextBorder.Background = null;
                    break;

                //如果是未完成
                case ProgressType.Undone:
                    _progressBubbleButtonControl.TextBorder.Background = _progressBubbleButtonControl.UndoneImage;
                    break;

                //如果是已完成
                case ProgressType.Solved:
                    _progressBubbleButtonControl.TextBorder.Background = _progressBubbleButtonControl.SolvedImage;
                    break;

                //如果是已废弃
                case ProgressType.Deprecat:
                    _progressBubbleButtonControl.TextBorder.Background = _progressBubbleButtonControl.DeprecatImage;
                    break;
            }
        }
        #endregion

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


        #region 依赖项属性：UndoneImage
        /// <summary>
        /// 依赖项属性：未完成的图标
        /// </summary>
        public static DependencyProperty UndoneImageProperty;

        /// <summary>
        /// 公开属性：未完成的图标
        /// </summary>
        public ImageBrush UndoneImage
        {
            get { return (ImageBrush)GetValue(UndoneImageProperty); }
            set { SetValue(UndoneImageProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当UndoneImageProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnUndoneImageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：SolvedImage
        /// <summary>
        /// 依赖项属性：已解决的图标
        /// </summary>
        public static DependencyProperty SolvedImageProperty;

        /// <summary>
        /// 公开属性：已解决的图标
        /// </summary>
        public ImageBrush SolvedImage
        {
            get { return (ImageBrush)GetValue(SolvedImageProperty); }
            set { SetValue(SolvedImageProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当SolvedImageProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnSolvedImageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：DeprecatImage
        /// <summary>
        /// 依赖项属性：已废弃的图标
        /// </summary>
        public static DependencyProperty DeprecatImageProperty;

        /// <summary>
        /// 公开属性：已废弃的图标
        /// </summary>
        public ImageBrush DeprecatImage
        {
            get { return (ImageBrush)GetValue(DeprecatImageProperty); }
            set { SetValue(DeprecatImageProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当DeprecatImageProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnDeprecatImageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
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
            args.RoutedEvent = ProgressBubbleButtonControl.ClickEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion




        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static ProgressBubbleButtonControl()
        {
            /*注册依赖项属性*/
            //注册MouseLeaveImageProperty
            MouseLeaveImageProperty = DependencyProperty.Register(
                "MouseLeaveImage", //属性的名字
                typeof(ImageBrush), //属性的类型
                typeof(ProgressBubbleButtonControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (ImageBrush)null,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnMouseLeaveImageChanged))
            );

            //注册MouseEnterImageProperty
            MouseEnterImageProperty = DependencyProperty.Register(
                "MouseEnterImage", typeof(ImageBrush), typeof(ProgressBubbleButtonControl),
                new FrameworkPropertyMetadata((ImageBrush)null,
                    new PropertyChangedCallback(OnMouseEnterImageChanged)));



            //注册UndoneImageProperty
            UndoneImageProperty = DependencyProperty.Register(
                "UndoneImage", typeof(ImageBrush), typeof(ProgressBubbleButtonControl),
                new FrameworkPropertyMetadata((ImageBrush)null,
                    new PropertyChangedCallback(OnUndoneImageChanged)));

            //注册SolvedImageProperty
            SolvedImageProperty = DependencyProperty.Register(
                "SolvedImage", typeof(ImageBrush), typeof(ProgressBubbleButtonControl),
                new FrameworkPropertyMetadata((ImageBrush)null,
                    new PropertyChangedCallback(OnSolvedImageChanged)));

            //注册DeprecatImageProperty
            DeprecatImageProperty = DependencyProperty.Register(
                "DeprecatImage", typeof(ImageBrush), typeof(ProgressBubbleButtonControl),
                new FrameworkPropertyMetadata((ImageBrush)null,
                    new PropertyChangedCallback(OnDeprecatImageChanged)));




            //注册ProgressTypeProperty
            ProgressTypeProperty = DependencyProperty.Register(
                "ProgressType", typeof(ProgressType), typeof(ProgressBubbleButtonControl),
                new FrameworkPropertyMetadata((ProgressType)ProgressType.None, new PropertyChangedCallback(OnProgressTypeChanged)));

            //注册PressAnimationSizeProperty
            PressAnimationSizeProperty = DependencyProperty.Register(
                "PressAnimationSize", typeof(Point), typeof(ProgressBubbleButtonControl),
                new FrameworkPropertyMetadata((Point)new Point(0.75, 0.75), new PropertyChangedCallback(OnPressAnimationSizeChanged)));





            /*注册路由事件*/
            //注册ClickEvent
            ClickEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "Click", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(ProgressBubbleButtonControl) //这个路由事件属于哪个控件？
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
                ElementName = "ProgressBubbleButtonUserControl",//要绑定的控件名称
                Path = new PropertyPath("MouseEnterImage"),// 需绑定的属性名
            };
            //重新绑定
            this.BubbleBorder.SetBinding(BackgroundProperty, _binding);
        }

        //当鼠标离开[按钮]的时候，触发此方法
        private void BaseButton_OnMouseLeave(object sender, MouseEventArgs e)
        {
            /* 更改按钮的背景 */
            //创建绑定数据
            Binding _binding = new Binding()
            {
                ElementName = "ProgressBubbleButtonUserControl",//要绑定的控件名称
                Path = new PropertyPath("MouseLeaveImage"),// 需绑定的属性名
            };
            //重新绑定
            this.BubbleBorder.SetBinding(BackgroundProperty, _binding);
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
            AnimationTool.PlayButtonAnimation(false,this.PressAnimationSize,this.BaseButtonScaleTransform);
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
