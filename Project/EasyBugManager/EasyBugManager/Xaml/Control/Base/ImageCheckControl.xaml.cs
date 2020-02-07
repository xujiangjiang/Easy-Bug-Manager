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
    /// ImageCheckControl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageCheckControl : UserControl
    {
        /* 属性: IsChecked(是否选中？)
         *       IsCanClick(是否可以点击？)
         *       IsCanUncheck(是否可以抬起？)(是否可以把选中的Check，再次点击时变为未选中？)
         *       PressAnimationSize(按下按钮时 动画缩小的尺寸)
         *       CurrentImage(当前的图片)
                 MouseEnterImage(鼠标进入时的图片) 
                 MouseLeaveImage(鼠标移出时的图片)
                 CheckedImage(选中时的图片)

           事件: Checked(当选中的时候)
                 Click(当点击按钮的时候)
                 IsCheckedChange(当[IsChecked]属性的值改变时)
           
           备注：一个Check有2个状态（UnCheck-未选中、Checked-选中）*/

        /* Click事件和Checked事件的区别：
            （1）Click事件：在点击时触发（并且如果点击了已选中的Check，并且IsCanUncheck为false时，则不能触发此事件）
            （2）Checked事件：只有在IsChecked属性发生变化时，并且IsChecked的当前值为true时，才会触发 */



        public ImageCheckControl()
        {
            InitializeComponent();
        }




        #region 依赖项属性：IsChecked
        /// <summary>
        /// 依赖项属性：是否选中？
        /// </summary>
        public static DependencyProperty IsCheckedProperty;

        /// <summary>
        /// 公开属性：是否选中？
        /// </summary>
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsCheckedProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsCheckedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            ImageCheckControl _imageCheckControl = sender as ImageCheckControl;

            //如果是选中
            if ((bool)e.NewValue == true)
            {
                /* 更改按钮的背景 */
                //创建绑定数据
                Binding _binding = new Binding()
                {
                    ElementName = "ImageCheckUserControl",//要绑定的控件名称
                    Path = new PropertyPath("CheckedImage"),// 需绑定的属性名
                };
                //重新绑定
                _imageCheckControl.ButtonImageBorder.SetBinding(BackgroundProperty, _binding);
            }
            //如果是未选中
            else 
            {
                /* 更改按钮的背景 */
                //创建绑定数据
                Binding _binding = new Binding()
                {
                    ElementName = "ImageCheckUserControl",//要绑定的控件名称
                    Path = new PropertyPath("MouseLeaveImage"),// 需绑定的属性名
                };
                //重新绑定
                _imageCheckControl.ButtonImageBorder.SetBinding(BackgroundProperty, _binding);
            }

            //触发事件
            if ((bool)e.NewValue != (bool)e.OldValue)
            {
                _imageCheckControl.OnIsCheckedChange((bool)e.OldValue,(bool)e.NewValue);//触发事件


                if ((bool)e.NewValue == true)
                {
                    _imageCheckControl.OnChecked();//触发事件
                }
            }

        }
        #endregion

        #region 依赖项属性：IsCanClick
        /// <summary>
        /// 依赖项属性：是否可以点击？
        /// </summary>
        public static DependencyProperty IsCanClickProperty;

        /// <summary>
        /// 公开属性：是否可以点击？
        /// </summary>
        public bool IsCanClick
        {
            get { return (bool)GetValue(IsCanClickProperty); }
            set { SetValue(IsCanClickProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsCanClickProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsCanClickChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            ImageCheckControl _imageCheckControl = sender as ImageCheckControl;

            //如果是可以点击
            if ((bool)e.NewValue == true)
            {
                _imageCheckControl.BaseButton.Visibility = Visibility.Visible;
                _imageCheckControl.ButtonImageBorder.Opacity = 1;
            }
            //如果是不可点击
            else
            {
                _imageCheckControl.BaseButton.Visibility = Visibility.Collapsed;
                _imageCheckControl.ButtonImageBorder.Opacity = 0.7f;
            }
        }
        #endregion

        #region 依赖项属性：IsCanUncheck
        /// <summary>
        /// 依赖项属性：是否可以抬起？(是否可以把选中的Check，再次点击时变为未选中？)
        /// </summary>
        public static DependencyProperty IsCanUncheckProperty;

        /// <summary>
        /// 公开属性：是否可以抬起？(是否可以把选中的Check，再次点击时变为未选中？)
        /// </summary>
        public bool IsCanUncheck
        {
            get { return (bool)GetValue(IsCanUncheckProperty); }
            set { SetValue(IsCanUncheckProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsCanUncheckProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsCanUncheckChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

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

        #region 依赖项属性：CheckedImage
        /// <summary>
        /// 依赖项属性：选中时的图片
        /// </summary>
        public static DependencyProperty CheckedImageProperty;

        /// <summary>
        /// 公开属性：选中时的图片
        /// </summary>
        public ImageBrush CheckedImage
        {
            get { return (ImageBrush)GetValue(CheckedImageProperty); }
            set { SetValue(CheckedImageProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当CheckedImageProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnCheckedImageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion




        #region 路由事件：Checked
        /// <summary>
        /// 路由事件：CheckedEvent
        /// （当选中时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent CheckedEvent;


        /// <summary>
        /// 路由事件的属性：OnChecked
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> Checked
        {
            //添加一条事件
            add { AddHandler(CheckedEvent, value); }

            //移除一条事件
            remove { RemoveHandler(CheckedEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 Click 路由事件
        /// </summary>
        private void OnChecked()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = ImageCheckControl.CheckedEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：Click
        /// <summary>
        /// 路由事件：ClickEvent
        /// （当被点击时，触发此事件）
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
        /// <param name="_oldValue">在点击之前，IsChecked字段的值</param>
        /// <param name="_newValue">在点击之后，IsChecked字段的值</param>
        private void OnClick(bool _oldValue, bool _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ImageCheckControl.ClickEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：IsCheckedChange
        /// <summary>
        /// 路由事件：IsCheckedChangeEvent
        /// （当[IsChecked]属性的值改变时）
        /// </summary>
        public static readonly RoutedEvent IsCheckedChangeEvent;


        /// <summary>
        /// 路由事件的属性：IsCheckedChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> IsCheckedChange
        {
            //添加一条事件
            add { AddHandler(IsCheckedChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(IsCheckedChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 IsCheckedChange 路由事件
        /// </summary>
        /// <param name="_oldValue">旧的值</param>
        /// <param name="_newValue">新的值</param>
        private void OnIsCheckedChange(bool _oldValue,bool _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ImageCheckControl.IsCheckedChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion



        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static ImageCheckControl()
        {
            /*注册依赖项属性*/
            //注册MouseLeaveImageProperty
            MouseLeaveImageProperty = DependencyProperty.Register(
                "MouseLeaveImage", //属性的名字
                typeof(ImageBrush), //属性的类型
                typeof(ImageCheckControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (ImageBrush)null,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnMouseLeaveImageChanged))
            );

            //注册MouseEnterImageProperty
            MouseEnterImageProperty = DependencyProperty.Register(
                "MouseEnterImage", typeof(ImageBrush), typeof(ImageCheckControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnMouseEnterImageChanged)));

            //注册CheckedImageProperty
            CheckedImageProperty = DependencyProperty.Register(
                "CheckedImage", typeof(ImageBrush), typeof(ImageCheckControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnCheckedImageChanged)));

            //注册PressAnimationSizeProperty
            PressAnimationSizeProperty = DependencyProperty.Register(
                "PressAnimationSize", typeof(Point), typeof(ImageCheckControl),
                new FrameworkPropertyMetadata((Point)new Point(0.75f,0.75f), new PropertyChangedCallback(OnPressAnimationSizeChanged)));

            //注册IsCheckedProperty
            IsCheckedProperty = DependencyProperty.Register(
                "IsChecked", typeof(bool), typeof(ImageCheckControl),
                new FrameworkPropertyMetadata((bool)false, new PropertyChangedCallback(OnIsCheckedChanged)));

            //注册IsCanClickProperty
            IsCanClickProperty = DependencyProperty.Register(
                "IsCanClick", typeof(bool), typeof(ImageCheckControl),
                new FrameworkPropertyMetadata((bool)true, new PropertyChangedCallback(OnIsCanClickChanged)));

            //注册IsCanUncheckProperty
            IsCanUncheckProperty = DependencyProperty.Register(
                "IsCanUncheck", typeof(bool), typeof(ImageCheckControl),
                new FrameworkPropertyMetadata((bool)true, new PropertyChangedCallback(OnIsCanUncheckChanged)));







            /*注册路由事件*/
            //注册ClickEvent
            CheckedEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "Checked", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(ImageCheckControl) //这个路由事件属于哪个控件？
            );

            //注册ClickEvent
            ClickEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "Click", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ImageCheckControl));

            //注册IsCheckedChangeEvent
            IsCheckedChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "IsCheckedChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ImageCheckControl));

        }
        #endregion



        #region 事件 -[点击按钮动画]
        //当鼠标进入[按钮]的时候，触发此方法
        private void BaseButton_OnMouseEnter(object sender, MouseEventArgs e)
        {
            /* 如果不能点击，
             * 如果选中了，
             * 就return*/
            if (IsCanClick == false) return;
            if (IsChecked == true) return;

            /* 更改按钮的背景 */
            //创建绑定数据
            Binding _binding = new Binding()
            {
                ElementName = "ImageCheckUserControl",//要绑定的控件名称
                Path = new PropertyPath("MouseEnterImage"),// 需绑定的属性名
            };
            //重新绑定
            this.ButtonImageBorder.SetBinding(BackgroundProperty, _binding);
        }

        //当鼠标离开[按钮]的时候，触发此方法
        private void BaseButton_OnMouseLeave(object sender, MouseEventArgs e)
        {
            /* 如果不能点击，
             * 如果选中了，
             * 就return*/
            if (IsCanClick == false) return;
            if (IsChecked == true) return;

            /* 更改按钮的背景 */
            //创建绑定数据
            Binding _binding = new Binding()
            {
                ElementName = "ImageCheckUserControl",//要绑定的控件名称
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
            /* 如果不能点击，
             * 如果选中了，并且不能被抬起，
             * 就return*/

            if (IsCanClick == false) return;
            if (IsChecked == true && IsCanUncheck == false) return;

            IsChecked = !IsChecked;//把未选中变为选中；把选中变为未选中

            /* 触发点击事件 */
            bool _oldIsChecked = !IsChecked;
            bool _newIsChecked = IsChecked;
            this.OnClick(_oldIsChecked, _newIsChecked);
        }
        #endregion
    }
}
