using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace EasyBugManager
{
    /// <summary>
    /// RecordInputBoxControl.xaml 的交互逻辑
    /// </summary>
    public partial class RecordInputBoxControl : UserControl
    {
        /* 属性: Text*(文字)
                 ImagePaths(图片的路径)
                 IsShowSubmitButtonAnimation(是否显示提交按钮的动画？)
                 EllipsisNumber(省略号的个数)
                 ImageCurrentNumber(图片的当前个数)

           事件: ClickSubmitButton(当点击[提交]按钮的时候)
                 ClickChooseImageButton(当点击[选择图片]按钮的时候)
                 ClickImageButton(当点击[图片]按钮的时候)（参数string：被点击的图片路径）
                 ClickDeleteImageButton(当点击[删除图片]按钮的时候)（参数string：被点击的图片路径）*/



        public RecordInputBoxControl()
        {
            InitializeComponent();
        }



        #region 依赖项属性：IsShowSubmitButtonAnimation
        /// <summary>
        /// 依赖项属性：记录的类型
        /// </summary>
        public static DependencyProperty IsShowSubmitButtonAnimationProperty;

        /// <summary>
        /// 公开属性：记录的类型
        /// </summary>
        public bool IsShowSubmitButtonAnimation
        {
            get { return (bool)GetValue(IsShowSubmitButtonAnimationProperty); }
            set { SetValue(IsShowSubmitButtonAnimationProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsShowSubmitButtonAnimationProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsShowSubmitButtonAnimationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            RecordInputBoxControl _recordInputBoxControl = sender as RecordInputBoxControl;

            //判断
            switch ((bool)e.NewValue)
            {
                case true:
                    _recordInputBoxControl.PlayEllipsisAnimation();//播放动画
                    _recordInputBoxControl.EllipsisStackPanel.Visibility = Visibility.Visible;//显示省略号
                    _recordInputBoxControl.SendButtonBorder.Visibility = Visibility.Visible;//打开SendButtonBorder
                    break;

                case false:
                    _recordInputBoxControl.PlayEllipsisAnimation();//停止动画
                    _recordInputBoxControl.EllipsisStackPanel.Visibility = Visibility.Collapsed;//隐藏省略号
                    _recordInputBoxControl.SendButtonBorder.Visibility = Visibility.Collapsed;//关闭SendButtonBorder
                    break;
            }
        }
        #endregion

        #region 依赖项属性：Text
        /// <summary>
        /// 公开属性：文字
        /// </summary>
        public string Text
        {
            get { return this.InputTextBox.Text; }
            set { this.InputTextBox.Text = value; }
        }
        #endregion

        #region 依赖项属性：ImagePaths
        /// <summary>
        /// 依赖项属性：图片的路径
        /// </summary>
        public static DependencyProperty ImagePathsProperty;

        /// <summary>
        /// 公开属性：图片的路径
        /// </summary>
        public ObservableCollection<string> ImagePaths
        {
            get { return (ObservableCollection<string>)GetValue(ImagePathsProperty); }
            set { SetValue(ImagePathsProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ImagePathsProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnImagePathsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：EllipsisNumber
        /// <summary>
        /// 依赖项属性：省略号的个数
        /// </summary>
        public static DependencyProperty EllipsisNumberProperty;

        /// <summary>
        /// 公开属性：省略号的个数
        /// </summary>
        public int EllipsisNumber
        {
            get { return (int)GetValue(EllipsisNumberProperty); }
            set { SetValue(EllipsisNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当EllipsisNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnEllipsisNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            RecordInputBoxControl _recordInputBoxControl = sender as RecordInputBoxControl;

            //判断
            switch ((int)e.NewValue)
            {
                //如果有0个省略号
                case 0:
                    _recordInputBoxControl.Ellipsis1Border.Visibility = Visibility.Hidden;
                    _recordInputBoxControl.Ellipsis2Border.Visibility = Visibility.Hidden;
                    _recordInputBoxControl.Ellipsis3Border.Visibility = Visibility.Hidden;
                    break;

                //如果有1个省略号
                case 1:
                    _recordInputBoxControl.Ellipsis1Border.Visibility = Visibility.Visible;
                    _recordInputBoxControl.Ellipsis2Border.Visibility = Visibility.Hidden;
                    _recordInputBoxControl.Ellipsis3Border.Visibility = Visibility.Hidden;
                    break;

                //如果有2个省略号
                case 2:
                    _recordInputBoxControl.Ellipsis1Border.Visibility = Visibility.Visible;
                    _recordInputBoxControl.Ellipsis2Border.Visibility = Visibility.Visible;
                    _recordInputBoxControl.Ellipsis3Border.Visibility = Visibility.Hidden;
                    break;

                //如果有3个省略号
                case 3:
                    _recordInputBoxControl.Ellipsis1Border.Visibility = Visibility.Visible;
                    _recordInputBoxControl.Ellipsis2Border.Visibility = Visibility.Visible;
                    _recordInputBoxControl.Ellipsis3Border.Visibility = Visibility.Visible;
                    break;

            }
        }
        #endregion

        #region 依赖项属性：ImageCurrentNumber
        /// <summary>
        /// 依赖项属性：图片的当前个数
        /// </summary>
        public static DependencyProperty ImageCurrentNumberProperty;

        /// <summary>
        /// 公开属性：图片的当前个数
        /// </summary>
        public int ImageCurrentNumber
        {
            get { return (int)GetValue(ImageCurrentNumberProperty); }
            set { SetValue(ImageCurrentNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ImageCurrentNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnImageCurrentNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            RecordInputBoxControl _recordInputBoxControl = sender as RecordInputBoxControl;

            //判断：如果当前的图片个数为0的话
            if ((int)e.NewValue <= 0)
            {
                //隐藏[图片个数]的Grid
                _recordInputBoxControl.ImageMaxNumberStackPanel.Visibility = Visibility.Collapsed;
            }

            //如果当前的图片个数大于等于0的话
            else
            {
                //显示[图片个数]的Grid
                _recordInputBoxControl.ImageMaxNumberStackPanel.Visibility = Visibility.Visible;
            }
        }
        #endregion





        #region 路由事件：ClickSubmitButtonButton
        /// <summary>
        /// 路由事件：ClickSubmitButtonEvent
        /// （当点击[提交]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickSubmitButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickSubmitButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickSubmitButton
        {
            //添加一条事件
            add { AddHandler(ClickSubmitButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickSubmitButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickDeleteButton 路由事件
        /// </summary>
        private void OnClickSubmitButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = RecordInputBoxControl.ClickSubmitButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickChooseImageButton
        /// <summary>
        /// 路由事件：ClickChooseImageButtonEvent
        /// （当点击[选择图片]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickChooseImageButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickChooseImageButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickChooseImageButton
        {
            //添加一条事件
            add { AddHandler(ClickChooseImageButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickChooseImageButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickChooseImageButton 路由事件
        /// </summary>
        private void OnClickChooseImageButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = RecordInputBoxControl.ClickChooseImageButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickImageButton
        /// <summary>
        /// 路由事件：ClickImageButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickImageButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickImageButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<string> ClickImageButton
        {
            //添加一条事件
            add { AddHandler(ClickImageButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickImageButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickImageButton 路由事件
        /// </summary>
        /// <param name="_imagePath">点击的 图片的路径</param>
        private void OnClickImageButton(string _imagePath)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<string> args = new RoutedPropertyChangedEventArgs<string>(_imagePath, _imagePath);

            //设置这是哪个路由事件？
            args.RoutedEvent = RecordInputBoxControl.ClickImageButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickDeleteImageButton
        /// <summary>
        /// 路由事件：ClickDeleteImageButtonEvent
        /// （当点击[删除图片]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickDeleteImageButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickDeleteImageButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<string> ClickDeleteImageButton
        {
            //添加一条事件
            add { AddHandler(ClickDeleteImageButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickDeleteImageButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickDeleteImageButton 路由事件
        /// </summary>
        /// <param name="_imagePath">点击的 图片的路径</param>
        private void OnClickDeleteImageButton(string _imagePath)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<string> args = new RoutedPropertyChangedEventArgs<string>(_imagePath, _imagePath);

            //设置这是哪个路由事件？
            args.RoutedEvent = RecordInputBoxControl.ClickDeleteImageButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion




        #region 静态构造方法：注册依赖项属性 和 路由事件

        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static RecordInputBoxControl()
        {
            /*注册依赖项属性*/
            //注册IsShowSubmitButtonAnimationProperty
            IsShowSubmitButtonAnimationProperty = DependencyProperty.Register(
                "IsShowSubmitButtonAnimation", //属性的名字
                typeof(bool), //属性的类型
                typeof(RecordInputBoxControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (bool)false,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnIsShowSubmitButtonAnimationChanged))
            );

            //注册ImagePathsProperty
            ImagePathsProperty = DependencyProperty.Register(
                "ImagePaths", typeof(ObservableCollection<string>), typeof(RecordInputBoxControl),
                new FrameworkPropertyMetadata((ObservableCollection<string>)new ObservableCollection<string>(), new PropertyChangedCallback(OnImagePathsChanged)));

            //注册EllipsisNumberProperty
            EllipsisNumberProperty = DependencyProperty.Register(
                "EllipsisNumber", typeof(int), typeof(RecordInputBoxControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnEllipsisNumberChanged)));

            //注册ImageCurrentNumberProperty
            ImageCurrentNumberProperty = DependencyProperty.Register(
                "ImageCurrentNumber", typeof(int), typeof(RecordInputBoxControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnImageCurrentNumberChanged)));







            /*注册路由事件*/
            //注册ClickSubmitButtonEvent
            ClickSubmitButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickSubmitButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(RecordInputBoxControl) //这个路由事件属于哪个控件？
            );

            //注册ClickChooseImageButtonEvent
            ClickChooseImageButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickChooseImageButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(RecordInputBoxControl));

            //注册ClickImageButtonEvent
            ClickImageButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickImageButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<string>), typeof(RecordInputBoxControl));

            //注册ClickDeleteImageButtonEvent
            ClickDeleteImageButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickDeleteImageButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<string>), typeof(RecordInputBoxControl));

        }

        #endregion




        #region [事件]
        //当点击[删除图片]按钮的时候
        private void DeleteImageButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            ImageButtonControl _imageButtonControl = sender as ImageButtonControl;

            //触发事件
            this.OnClickDeleteImageButton((string)_imageButtonControl.Tag);
        }

        //当点击[查看图片]按钮的时候
        private void ImageButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            ColorButtonControl _colorButtonControl = sender as ColorButtonControl;

            //触发事件
            this.OnClickImageButton((string)_colorButtonControl.Tag);
        }



        //当点击[选择图片]按钮的时候
        private void ChooseImageButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickChooseImageButton();
        }

        //当点击[提交]按钮的时候
        private void SubmitButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickSubmitButton();
        }
        #endregion

        #region [事件 - 文本框]
        //当[文本输入框]里的文字改变时
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //取到TextBox控件
            TextBox _textBox = sender as TextBox;

            //如果TextBox的内容为空，就把TextBox的背景设置为透明
            //如果TextBox的内容不为空，把TextBox的背景设置为白色
            AnimationTool.PlayTextChangedAnimation(_textBox);
        }
        #endregion




        #region [动画 - 显示省略号]
        //省略号的动画
        private Int32Animation ellipsisAnimation = new Int32Animation();

        /// <summary>
        /// 播放省略号的动画
        /// </summary>
        private void PlayEllipsisAnimation()
        {
            //先停止动画
            StopEllipsisAnimation();


            /* 动画 */
            ellipsisAnimation.From = 0;
            ellipsisAnimation.To = 3;
            ellipsisAnimation.Duration = TimeSpan.FromSeconds(2f);//2秒
            ellipsisAnimation.RepeatBehavior = RepeatBehavior.Forever;//永远循环


            //播放动画 (让按钮的尺寸(Scale) 变小/变大)
            this.BeginAnimation(RecordInputBoxControl.EllipsisNumberProperty, ellipsisAnimation);
        }

        /// <summary>
        /// 停止省略号的动画
        /// </summary>
        private void StopEllipsisAnimation()
        {
            //停止动画
            this.BeginAnimation(RecordInputBoxControl.EllipsisNumberProperty, null);
        }
        #endregion




        #region [公开方法 - 显示“选择图片”的按钮]
        /// <summary>
        /// 显示“选择图片”的按钮
        /// </summary>
        public void ShowChooseImageButton()
        {
            //显示按钮
            AnimationTool.PlayGridOpacityAnimation(this.ChooseImageGrid, null, 1, 0.25f);
        }

        /// <summary>
        /// 隐藏“选择图片”的按钮
        /// </summary>
        public void HideChooseImageButton()
        {
            //如果没有图片在显示，那么就可以关闭掉选择图片的按钮
            if (this.ImagePaths == null || this.ImagePaths.Count == 0)
            {
                //隐藏按钮
                AnimationTool.PlayGridOpacityAnimation(this.ChooseImageGrid, null, 0, 0.25f);
            }
        }
        #endregion

        #region [公开方法 - 更新[当前的图片个数]]

        /// <summary>
        /// 更新[当前的图片个数]
        /// </summary>
        public void UpdateImageCurrentNumber()
        {
            //获取[图片的个数]
            ObservableCollection<string> _images = ImageListBox.ItemsSource as ObservableCollection<string>;

            //更新属性
            ImageCurrentNumber = _images.Count;
        }

        #endregion
    }
}
