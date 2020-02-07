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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyBugManager
{
    /// <summary>
    /// RecordListItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class RecordListItemControl : UserControl
    {
        /* 属性: RecordType(记录的类型：Bug/Bear)
                 Time(时间)
                 Text(文字)
                 ImagePaths(图片的路径)

           事件: ClickDeleteButton(当点击[删除]按钮的时候)
                 ClickImageButton(当点击[图片]按钮的时候)（参数string：点击的 图片的路径）*/



        public RecordListItemControl()
        {
            InitializeComponent();

            //根据皮肤的类型，设置一些属性
            AppManager.Systems.ThemeSystem.SetRecordItem(this);
        }




        #region 依赖项属性：RecordType
        /// <summary>
        /// 依赖项属性：记录的类型
        /// </summary>
        public static DependencyProperty RecordTypeProperty;

        /// <summary>
        /// 公开属性：记录的类型
        /// </summary>
        public RecordType RecordType
        {
            get { return (RecordType)GetValue(RecordTypeProperty); }
            set { SetValue(RecordTypeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当RecordTypeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnRecordTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //取到控件
            RecordListItemControl _recordListItemControl = sender as RecordListItemControl;

            //判断类型
            switch ((RecordType)e.NewValue)
            {
                //如果为[空]
                case RecordType.None:
                    _recordListItemControl.BugGrid.Visibility = Visibility.Collapsed;
                    _recordListItemControl.BearGrid.Visibility = Visibility.Collapsed;
                    break;

                //如果为[虫子]
                case RecordType.Bug:
                    _recordListItemControl.BugGrid.Visibility = Visibility.Visible;
                    _recordListItemControl.BearGrid.Visibility = Visibility.Collapsed;
                    break;

                //如果为[熊]
                case RecordType.Bear:
                    _recordListItemControl.BugGrid.Visibility = Visibility.Collapsed;
                    _recordListItemControl.BearGrid.Visibility = Visibility.Visible;
                    break;
            }

        }
        #endregion

        #region 依赖项属性：Time
        /// <summary>
        /// 依赖项属性：时间
        /// </summary>
        public static DependencyProperty TimeProperty;

        /// <summary>
        /// 公开属性：时间
        /// </summary>
        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当TimeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：Text
        /// <summary>
        /// 依赖项属性：文字
        /// </summary>
        public static DependencyProperty TextProperty;

        /// <summary>
        /// 公开属性：文字
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
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



        #region 路由事件：ClickDeleteButton
        /// <summary>
        /// 路由事件：ClickDeleteButtonEvent
        /// （当点击[删除]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickDeleteButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickDeleteButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickDeleteButton
        {
            //添加一条事件
            add { AddHandler(ClickDeleteButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickDeleteButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickDeleteButton 路由事件
        /// </summary>
        private void OnClickDeleteButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = RecordListItemControl.ClickDeleteButtonEvent;

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
            args.RoutedEvent = RecordListItemControl.ClickImageButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion




        #region 静态构造方法：注册依赖项属性 和 路由事件

        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static RecordListItemControl()
        {
            /*注册依赖项属性*/
            //注册RecordTypeProperty
            RecordTypeProperty = DependencyProperty.Register(
                "RecordType", //属性的名字
                typeof(RecordType), //属性的类型
                typeof(RecordListItemControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (RecordType)RecordType.None,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnRecordTypeChanged))
            );

            //注册TimeProperty
            TimeProperty = DependencyProperty.Register(
                "Time", typeof(string), typeof(RecordListItemControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnTimeChanged)));


            //注册TextProperty
            TextProperty = DependencyProperty.Register(
                "Text", typeof(string), typeof(RecordListItemControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnTextChanged)));

            //注册ImagePathsProperty
            ImagePathsProperty = DependencyProperty.Register(
                "ImagePaths", typeof(ObservableCollection<string>), typeof(RecordListItemControl),
                new FrameworkPropertyMetadata((ObservableCollection<string>)new ObservableCollection<string>(), new PropertyChangedCallback(OnImagePathsChanged)));







            /*注册路由事件*/
            //注册ClickDeleteButtonEvent
            ClickDeleteButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickDeleteButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(RecordListItemControl) //这个路由事件属于哪个控件？
            );

            //注册ClickImageButtonEvent
            ClickImageButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickImageButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<string>), typeof(RecordListItemControl));

        }

        #endregion



        #region 事件
        //当点击[图片]按钮时
        private void ImageButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //拿到控件
            ColorButtonControl _colorButtonControl = sender as ColorButtonControl;

            //触发事件
            this.OnClickImageButton((string)_colorButtonControl.Tag);
        }

        //当点击[删除]按钮时
        private void DeleteButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickDeleteButton();
        }




        //当鼠标离开[熊的气泡]时
        private void BearBubbleBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            //显示[删除按钮]
            this.DeleteButtonControl.Visibility = Visibility.Collapsed;
        }

        //当鼠标进入[熊的气泡]时
        private void BearBubbleBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            //隐藏[删除按钮]
            this.DeleteButtonControl.Visibility = Visibility.Visible;
        }
        #endregion

    }
}
