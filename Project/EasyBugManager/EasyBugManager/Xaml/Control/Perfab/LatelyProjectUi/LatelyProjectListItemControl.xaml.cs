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
    /// LatelyProjectListItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class LatelyProjectListItemControl : UserControl
    {
        /*属性：Title(项目名)
                Path(项目路径)
                Time(时间)
                Mode(项目模式)

          事件：ClickBaseButton(点击[Base]按钮时)
                ClickOpenFolderButton(点击[打开文件夹]按钮时)
                ClickRemoveButton(点击[从列表中移除]按钮时)*/



        public LatelyProjectListItemControl()
        {
            InitializeComponent();
        }




        #region 依赖项属性：Title

        /// <summary>
        /// 依赖项属性：项目名
        /// </summary>
        public static DependencyProperty TitleProperty;

        /// <summary>
        /// 公开属性：项目名
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当TitleProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnTitleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：Path

        /// <summary>
        /// 依赖项属性：项目路径
        /// </summary>
        public static DependencyProperty PathProperty;

        /// <summary>
        /// 公开属性：项目路径
        /// </summary>
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当PathProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnPathChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
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

        #region 依赖项属性：Mode

        /// <summary>
        /// 依赖项属性：项目模式
        /// </summary>
        public static DependencyProperty ModeProperty;

        /// <summary>
        /// 公开属性：项目模式
        /// </summary>
        public string Mode
        {
            get { return (string)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ModeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnModeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取到控件
            LatelyProjectListItemControl _latelyProjectListItemControl = sender as LatelyProjectListItemControl;

            //如果没有任何的内容
            if (e.NewValue == null || (string)e.NewValue == "")
            {
                _latelyProjectListItemControl.LineBorder.Visibility = Visibility.Collapsed;//隐藏分割线
            }
            //如果有内容
            else
            {
                _latelyProjectListItemControl.LineBorder.Visibility = Visibility.Visible;//显示分割线
            }
        }
        #endregion



        #region 路由事件：ClickBaseButton
        /// <summary>
        /// 路由事件：ClickBaseButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickBaseButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickBaseButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickBaseButton
        {
            //添加一条事件
            add { AddHandler(ClickBaseButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickBaseButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickBaseButton 路由事件
        /// </summary>
        private void OnClickBaseButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = LatelyProjectListItemControl.ClickBaseButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickOpenFolderButton
        /// <summary>
        /// 路由事件：ClickOpenFolderButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickOpenFolderButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickOpenFolderButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickOpenFolderButton
        {
            //添加一条事件
            add { AddHandler(ClickOpenFolderButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickOpenFolderButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickOpenFolderButton 路由事件
        /// </summary>
        private void OnClickOpenFolderButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = LatelyProjectListItemControl.ClickOpenFolderButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickRemoveButton
        /// <summary>
        /// 路由事件：ClickRemoveButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickRemoveButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickRemoveButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickRemoveButton
        {
            //添加一条事件
            add { AddHandler(ClickRemoveButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickRemoveButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickRemoveButton 路由事件
        /// </summary>
        private void OnClickRemoveButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = LatelyProjectListItemControl.ClickRemoveButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion



        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static LatelyProjectListItemControl()
        {
            /*注册依赖项属性*/
            //注册TitleProperty
            TitleProperty = DependencyProperty.Register(
                "Title", //属性的名字
                typeof(string), //属性的类型
                typeof(LatelyProjectListItemControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (string)"",
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnTitleChanged))
            );

            //注册PathProperty
            PathProperty = DependencyProperty.Register(
                "Path", typeof(string), typeof(LatelyProjectListItemControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnPathChanged)));

            //注册TimeProperty
            TimeProperty = DependencyProperty.Register(
                "Time", typeof(string), typeof(LatelyProjectListItemControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnTimeChanged)));

            //注册ModeProperty
            ModeProperty = DependencyProperty.Register(
                "Mode", typeof(string), typeof(LatelyProjectListItemControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnModeChanged)));





            /*注册路由事件*/
            //注册ClickBaseButtonEvent
            ClickBaseButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickBaseButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(LatelyProjectListItemControl) //这个路由事件属于哪个控件？
            );

            //注册ClickOpenFolderButtonEvent
            ClickOpenFolderButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickOpenFolderButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(LatelyProjectListItemControl));

            //注册ClickRemoveButtonEvent
            ClickRemoveButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickRemoveButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(LatelyProjectListItemControl));

        }
        #endregion







        #region [事件 - 按钮]

        //如果鼠标右键点击
        private void BaseButtonControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //显示右键菜单
            ContextMenuPopup.IsOpen = false;
            ContextMenuPopup.IsOpen = true;
        }





        //如果点击了[更多]按钮时
        private void MoreButtonControl_OnClick(object sender, RoutedEventArgs e)
        {
            //显示右键菜单
            ContextMenuPopup.IsOpen = false;
            ContextMenuPopup.IsOpen = true;
        }

        //如果点击了[Base]按钮时
        private void BaseButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickBaseButton();//触发事件
        }

        //如果点击了[打开文件夹]按钮时
        private void OpenFolderButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickOpenFolderButton();

            //关闭[右键菜单]
            ContextMenuPopup.IsOpen = false;
        }

        //如果点击了[从列表中移除]按钮时
        private void RemoveButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickRemoveButton();

            //关闭[右键菜单]
            ContextMenuPopup.IsOpen = false;
        }

        #endregion

        #region [事件 - 移入、移出]
        //当鼠标移入[更多]按钮时
        private void MoreButtonControl_OnMouseEnter(object sender, MouseEventArgs e)
        {
            //取到控件
            Button _button = sender as Button;

            //改变控件的颜色
            _button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF595959"));
        }


        //当鼠标移出[更多]按钮时
        private void MoreButtonControl_OnMouseLeave(object sender, MouseEventArgs e)
        {
            //取到控件
            Button _button = sender as Button;

            //改变控件的颜色
            _button.BorderBrush = _button.Background;
        }
        #endregion

        #region [事件 - ItemControl]
        /// <summary>
        /// 当[数据源]改变时
        /// </summary>
        private void LatelyProjectListItemControl_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((this.DataContext as LatelyProjectData) == null)
            {
                //关闭此控件
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                //显示此控件
                this.Visibility = Visibility.Visible;
            }
        }


        //当Item丢失焦点时
        private void LatelyProjectListItemControl_OnLostFocus(object sender, RoutedEventArgs e)
        {
            //关闭[右键菜单]
            ContextMenuPopup.IsOpen = false;
        }



        /// <summary>
        /// 当鼠标进入[Item]时
        /// </summary>
        private void LatelyProjectListItemControl_OnMouseEnter(object sender, MouseEventArgs e)
        {
            //显示[是否显示Bug回复]控件（把Opacity属性设置为1）
            AnimationTool.PlayGridOpacityAnimation(this.MoreGrid, null, 1, 0.25f);
        }

        /// <summary>
        /// 当鼠标移出[Item]时
        /// </summary>
        private void LatelyProjectListItemControl_OnMouseLeave(object sender, MouseEventArgs e)
        {
            //隐藏[是否显示Bug回复]控件（把Opacity属性设置为0）
            AnimationTool.PlayGridOpacityAnimation(this.MoreGrid, null, 0, 0.25f);
        }
        #endregion

        #region [事件 - 右键菜单]
        //当[右键菜单]打开时
        private void ContextMenuPopup_Opened(object sender, EventArgs e)
        {
            //播放音效
            AppManager.Systems.AudioSystem.PlayAudio(AudioType.ButtonDown);
        }
        #endregion


    }
}
