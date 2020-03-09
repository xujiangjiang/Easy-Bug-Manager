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
    /// DeleteTipUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class DeleteTipUiControl : UserControl
    {
        /* 属性: IsShowYesButton(是否显示[确认按钮]？)
                IsShowNoButton(是否显示[取消按钮]？)
                IsNotRemindAgain(是否再次提示？)（如果为true，代表不再提示；如果为false，代表再次提示）

                Text(提示的文字)

                Data(要删除的数据)(Bug数据或者记录数据)


           事件: ClickYesButton(当点击[确认]按钮时)
                ClickNoButton(当点击[取消]按钮时)*/



        public DeleteTipUiControl()
        {
            InitializeComponent();
        }




        #region 依赖项属性：IsShowYesButton
        /// <summary>
        /// 依赖项属性：是否显示[确认按钮]？
        /// </summary>
        public static DependencyProperty IsShowYesButtonProperty;

        /// <summary>
        /// 公开属性：是否显示[确认按钮]？
        /// </summary>
        public bool IsShowYesButton
        {
            get { return (bool)GetValue(IsShowYesButtonProperty); }
            set { SetValue(IsShowYesButtonProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsShowYesButtonProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsShowYesButtonChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：IsShowNoButton
        /// <summary>
        /// 依赖项属性：是否显示[取消按钮]？
        /// </summary>
        public static DependencyProperty IsShowNoButtonProperty;

        /// <summary>
        /// 公开属性：是否显示[取消按钮]？
        /// </summary>
        public bool IsShowNoButton
        {
            get { return (bool)GetValue(IsShowNoButtonProperty); }
            set { SetValue(IsShowNoButtonProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsShowNoButtonProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsShowNoButtonChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：IsNotRemindAgain
        /// <summary>
        /// 依赖项属性：是否再次提示？（如果为true，代表不再提示；如果为false，代表再次提示）
        /// </summary>
        public static DependencyProperty IsNotRemindAgainProperty;

        /// <summary>
        /// 公开属性：是否再次提示？（如果为true，代表不再提示；如果为false，代表再次提示）
        /// </summary>
        public bool IsNotRemindAgain
        {
            get { return (bool)GetValue(IsNotRemindAgainProperty); }
            set { SetValue(IsNotRemindAgainProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsNotRemindAgainProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsNotRemindAgainChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：Data
        /// <summary>
        /// 依赖项属性：要删除的数据(Bug数据或者记录数据)
        /// </summary>
        public static DependencyProperty DataProperty;

        /// <summary>
        /// 公开属性：要删除的数据(Bug数据或者记录数据)
        /// </summary>
        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当DataProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnDataChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：Text
        /// <summary>
        /// 依赖项属性：提示的文字
        /// </summary>
        public static DependencyProperty TextProperty;

        /// <summary>
        /// 公开属性：提示的文字
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
            args.RoutedEvent = DeleteTipUiControl.ClickYesButtonEvent;

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
            args.RoutedEvent = DeleteTipUiControl.ClickNoButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion




        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static DeleteTipUiControl()
        {
            /*注册依赖项属性*/
            //注册DataProperty
            DataProperty = DependencyProperty.Register(
                "Data", //属性的名字
                typeof(object), //属性的类型
                typeof(DeleteTipUiControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                     (object)null,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnDataChanged))
            );

            //注册IsShowYesButtonProperty
            IsShowYesButtonProperty = DependencyProperty.Register(
                "IsShowYesButton", typeof(bool), typeof(DeleteTipUiControl),
                new FrameworkPropertyMetadata((bool)true, new PropertyChangedCallback(OnIsShowYesButtonChanged)));

            //注册IsShowNoButtonProperty
            IsShowNoButtonProperty = DependencyProperty.Register(
                "IsShowNoButton", typeof(bool), typeof(DeleteTipUiControl),
                new FrameworkPropertyMetadata((bool)true, new PropertyChangedCallback(OnIsShowNoButtonChanged)));

            //注册IsNotRemindAgainProperty
            IsNotRemindAgainProperty = DependencyProperty.Register(
                "IsNotRemindAgain", typeof(bool), typeof(DeleteTipUiControl),
                new FrameworkPropertyMetadata((bool)true, new PropertyChangedCallback(OnIsNotRemindAgainChanged)));

            //注册TextProperty
            TextProperty = DependencyProperty.Register(
                "Text", typeof(string), typeof(DeleteTipUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnTextChanged)));








            /*注册路由事件*/
            //注册ClickYesButtonEvent
            ClickYesButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickYesButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(DeleteTipUiControl) //这个路由事件属于哪个控件？
            );

            //注册ClickNoButtonEvent
            ClickNoButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickNoButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(DeleteTipUiControl));

        }
        #endregion




        #region [事件 - 按钮]
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
