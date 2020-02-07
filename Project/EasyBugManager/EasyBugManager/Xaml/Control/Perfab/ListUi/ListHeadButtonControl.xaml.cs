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
    /// ListHeadButtonControl.xaml 的交互逻辑
    /// </summary>
    public partial class ListHeadButtonControl : UserControl
    {
        /* 属性: SortType(排序的类型)
         *       IsCanClick(是否可以点击？)
         * 
         *       ArrowNoneImage(没有箭头的图片)
                 ArrowUpImage(箭头向上的图片) 
                 ArrowDownImage(箭头向下的图片)

                 ButtonPadding(按钮的间距)
                 ButtonWidth(按钮的宽度)
                 ButtonHeight(按钮的高度)

                Text(文字的图片) 
                TextPadding(文字的间距)
                TextWidth(文字的宽度)
                TextHeight(文字的高度)


            事件: OnClick(当点击按钮的时候)*/




        public ListHeadButtonControl()
        {
            InitializeComponent();
        }




        #region 依赖项属性：SortType
        /// <summary>
        /// 依赖项属性：排序的类型
        /// </summary>
        public static DependencyProperty SortTypeProperty;

        /// <summary>
        /// 公开属性：排序的类型
        /// </summary>
        public SortType SortType
        {
            get { return (SortType)GetValue(SortTypeProperty); }
            set { SetValue(SortTypeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当SortTypeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnSortTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件的对象
            ListHeadButtonControl _listHeadButtonControl = sender as ListHeadButtonControl;

            //判断排序的类型
            switch ((SortType)e.NewValue)
            {
                //如果不排序
                case SortType.None:
                    /* 更改图片*/
                    //创建绑定数据
                    Binding _binding_None = new Binding()
                    {
                        ElementName = "ListHeadButtonUserControl",//要绑定的控件名称
                        Path = new PropertyPath("ArrowNoneImage"),// 需绑定的属性名
                    };
                    //重新绑定
                    _listHeadButtonControl.ListHandArrowBorder.SetBinding(BackgroundProperty, _binding_None);
                    break;

                //如果从低到高排序
                case SortType.LowToHigh:
                    /* 更改图片*/
                    //创建绑定数据
                    Binding _binding_LowToHigh = new Binding()
                    {
                        ElementName = "ListHeadButtonUserControl",//要绑定的控件名称
                        Path = new PropertyPath("ArrowUpImage"),// 需绑定的属性名
                    };
                    //重新绑定
                    _listHeadButtonControl.ListHandArrowBorder.SetBinding(BackgroundProperty, _binding_LowToHigh);
                    break;

                //如果从高到低排序
                case SortType.HighToLow:
                    /* 更改图片*/
                    //创建绑定数据
                    Binding _binding_HighToLow = new Binding()
                    {
                        ElementName = "ListHeadButtonUserControl",//要绑定的控件名称
                        Path = new PropertyPath("ArrowDownImage"),// 需绑定的属性名
                    };
                    //重新绑定
                    _listHeadButtonControl.ListHandArrowBorder.SetBinding(BackgroundProperty, _binding_HighToLow);
                    break;
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
            //获取控件的对象
            ListHeadButtonControl _listHeadButtonControl = sender as ListHeadButtonControl;

            //如果isCanClick为true
            if ((bool)e.NewValue == true)
            {
                //那就把Button启用
                _listHeadButtonControl.ListHeadColorButtonControl.Visibility = Visibility.Visible;
            }
            else 
            {
                //那就把Button禁用
                _listHeadButtonControl.ListHeadColorButtonControl.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region 依赖项属性：ArrowNoneImage
        /// <summary>
        /// 依赖项属性：没有箭头的图片
        /// </summary>
        public static DependencyProperty ArrowNoneImageProperty;

        /// <summary>
        /// 公开属性：没有箭头的图片
        /// </summary>
        public ImageBrush ArrowNoneImage
        {
            get { return (ImageBrush)GetValue(ArrowNoneImageProperty); }
            set { SetValue(ArrowNoneImageProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ArrowNoneImageProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnArrowNoneImageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：ArrowUpImage
        /// <summary>
        /// 依赖项属性：箭头向上的图片
        /// </summary>
        public static DependencyProperty ArrowUpImageProperty;

        /// <summary>
        /// 公开属性：箭头向上的图片
        /// </summary>
        public ImageBrush ArrowUpImage
        {
            get { return (ImageBrush)GetValue(ArrowUpImageProperty); }
            set { SetValue(ArrowUpImageProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ArrowUpImageProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnArrowUpImageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：ArrowDownImage
        /// <summary>
        /// 依赖项属性：箭头向下的图片
        /// </summary>
        public static DependencyProperty ArrowDownImageProperty;

        /// <summary>
        /// 公开属性：箭头向下的图片
        /// </summary>
        public ImageBrush ArrowDownImage
        {
            get { return (ImageBrush)GetValue(ArrowDownImageProperty); }
            set { SetValue(ArrowDownImageProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ArrowDownImageProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnArrowDownImageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：ButtonPadding
        /// <summary>
        /// 依赖项属性：按钮的间距
        /// </summary>
        public static DependencyProperty ButtonPaddingProperty;

        /// <summary>
        /// 公开属性：按钮的间距
        /// </summary>
        public Thickness ButtonPadding
        {
            get { return (Thickness)GetValue(ButtonPaddingProperty); }
            set { SetValue(ButtonPaddingProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ButtonPaddingProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnButtonPaddingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：ButtonWidth
        /// <summary>
        /// 依赖项属性：按钮的宽度
        /// </summary>
        public static DependencyProperty ButtonWidthProperty;

        /// <summary>
        /// 公开属性：按钮的宽度
        /// </summary>
        public double ButtonWidth
        {
            get { return (double)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ButtonWidthProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnButtonWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：ButtonHeight
        /// <summary>
        /// 依赖项属性：按钮的高度
        /// </summary>
        public static DependencyProperty ButtonHeightProperty;

        /// <summary>
        /// 公开属性：按钮的高度
        /// </summary>
        public double ButtonHeight
        {
            get { return (double)GetValue(ButtonHeightProperty); }
            set { SetValue(ButtonHeightProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ButtonHeightProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnButtonHeightChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion


        #region 依赖项属性：Text
        /// <summary>
        /// 依赖项属性：文字的图片
        /// </summary>
        public static DependencyProperty TextProperty;

        /// <summary>
        /// 公开属性：文字的图片
        /// </summary>
        public ImageBrush Text
        {
            get { return (ImageBrush)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当TextProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：TextPadding
        /// <summary>
        /// 依赖项属性：文字的间距
        /// </summary>
        public static DependencyProperty TextPaddingProperty;

        /// <summary>
        /// 公开属性：文字的间距
        /// </summary>
        public Thickness TextPadding
        {
            get { return (Thickness)GetValue(TextPaddingProperty); }
            set { SetValue(TextPaddingProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当TextPaddingProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnTextPaddingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：TextWidth
        /// <summary>
        /// 依赖项属性：文字的宽度
        /// </summary>
        public static DependencyProperty TextWidthProperty;

        /// <summary>
        /// 公开属性：文字的宽度
        /// </summary>
        public double TextWidth
        {
            get { return (double)GetValue(TextWidthProperty); }
            set { SetValue(TextWidthProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当TextWidthProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnTextWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：TextHeight
        /// <summary>
        /// 依赖项属性：文字的高度
        /// </summary>
        public static DependencyProperty TextHeightProperty;

        /// <summary>
        /// 公开属性：文字的高度
        /// </summary>
        public double TextHeight
        {
            get { return (double)GetValue(TextHeightProperty); }
            set { SetValue(TextHeightProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当TextHeightProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnTextHeightChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
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
            args.RoutedEvent = ListHeadButtonControl.ClickEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion



        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static ListHeadButtonControl()
        {
            /*注册依赖项属性*/
            //注册SortTypeProperty
            SortTypeProperty = DependencyProperty.Register(
                "SortType", //属性的名字
                typeof(SortType), //属性的类型
                typeof(ListHeadButtonControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (SortType)SortType.None,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnSortTypeChanged))
            );

            //注册IsCanClickProperty
            IsCanClickProperty = DependencyProperty.Register(
                "IsCanClick", typeof(bool), typeof(ListHeadButtonControl),
                new FrameworkPropertyMetadata((bool)true, new PropertyChangedCallback(OnIsCanClickChanged)));

            //注册ArrowNoneImageProperty
            ArrowNoneImageProperty = DependencyProperty.Register(
                "ArrowNoneImage", typeof(ImageBrush), typeof(ListHeadButtonControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnArrowNoneImageChanged)));

            //注册ArrowUpImageProperty
            ArrowUpImageProperty = DependencyProperty.Register(
                "ArrowUpImage", typeof(ImageBrush), typeof(ListHeadButtonControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnArrowUpImageChanged)));

            //注册ArrowDownImageProperty
            ArrowDownImageProperty = DependencyProperty.Register(
                "ArrowDownImage", typeof(ImageBrush), typeof(ListHeadButtonControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnArrowDownImageChanged)));

            //注册ButtonPaddingProperty
            ButtonPaddingProperty = DependencyProperty.Register(
                "ButtonPadding", typeof(Thickness), typeof(ListHeadButtonControl),
                new FrameworkPropertyMetadata((Thickness)new Thickness(0), new PropertyChangedCallback(OnButtonPaddingChanged)));

            //注册ButtonWidthProperty
            ButtonWidthProperty = DependencyProperty.Register(
                "ButtonWidth", typeof(double), typeof(ListHeadButtonControl),
                new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnButtonWidthChanged)));

            //注册ButtonHeightProperty
            ButtonHeightProperty = DependencyProperty.Register(
                "ButtonHeight", typeof(double), typeof(ListHeadButtonControl),
                new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnButtonHeightChanged)));


            //注册TextProperty
            TextProperty = DependencyProperty.Register(
                "Text", typeof(ImageBrush), typeof(ListHeadButtonControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnTextChanged)));

            //注册TextPaddingProperty
            TextPaddingProperty = DependencyProperty.Register(
                "TextPadding", typeof(Thickness), typeof(ListHeadButtonControl),
                new FrameworkPropertyMetadata((Thickness)new Thickness(0), new PropertyChangedCallback(OnTextPaddingChanged)));

            //注册TextWidthProperty
            TextWidthProperty = DependencyProperty.Register(
                "TextWidth", typeof(double), typeof(ListHeadButtonControl),
                new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnTextWidthChanged)));

            //注册TextHeightProperty
            TextHeightProperty = DependencyProperty.Register(
                "TextHeight", typeof(double), typeof(ListHeadButtonControl),
                new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnTextHeightChanged)));





            /*注册路由事件*/
            //注册ClickEvent
            ClickEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "Click", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(ListHeadButtonControl) //这个路由事件属于哪个控件？
            );

        }

        #endregion




        #region [事件]
        /// <summary>
        /// 当点击[表头按钮]时
        /// </summary>
        private void ListHeadColorButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClick();//触发事件
        }
        #endregion


    }
}
