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
    /// LatelyProjectUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class LatelyProjectUiControl : UserControl
    {
        /* 属性：Datas(所有的[最近的项目]数据)
                 Items*(所有的[Item]控件)（10个）
                 
           事件：ClickFoldButton(当点击[折叠]按钮时)

           事件(Item)：ClickListItemBaseButton(当点击[列表的Item]的[Base]按钮的时候)（e.Source参数：触发事件的LatelyProjectListItemControl控件 的LatelyProjectData数据）
                      ClickListItemOpenFolderButton(当点击[列表的Item]的[打开文件夹]按钮的时候)（e.Source参数：触发事件的LatelyProjectListItemControl控件 的LatelyProjectData数据）
                      ClickListItemRemoveButton(当点击[列表的Item]的[从列表中移除]按钮的时候)（e.Source参数：触发事件的LatelyProjectListItemControl控件 的LatelyProjectData数据）*/



        public LatelyProjectUiControl()
        {
            InitializeComponent();
        }



        #region 依赖项属性：Datas
        /// <summary>
        /// 依赖项属性：所有的[最近的项目]数据
        /// </summary>
        public static DependencyProperty DatasProperty;

        /// <summary>
        /// 公开属性：所有的[最近的项目]数据
        /// </summary>
        public ObservableCollection<LatelyProjectData> Datas
        {
            get { return (ObservableCollection<LatelyProjectData>)GetValue(DatasProperty); }
            set { SetValue(DatasProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当DatasProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnDatasChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：Items*
        //所有的[Item]控件（10个）
        private List<LatelyProjectListItemControl> items = new List<LatelyProjectListItemControl>();

        /// <summary>
        /// 公开属性：所有的[Item]控件（10个）
        /// </summary>
        public List<LatelyProjectListItemControl> Items
        {
            get { return items; }
            set { items = value; }
        }
        #endregion


        #region 路由事件：ClickFoldButton
        /// <summary>
        /// 路由事件：ClickFoldButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickFoldButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickFoldButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickFoldButton
        {
            //添加一条事件
            add { AddHandler(ClickFoldButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickFoldButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickFoldButton 路由事件
        /// </summary>
        private void OnClickFoldButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = LatelyProjectUiControl.ClickFoldButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion


        #region 路由事件：ClickListItemBaseButton
        /// <summary>
        /// 路由事件：ClickListItemBaseButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickListItemBaseButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickListItemBaseButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<LatelyProjectData> ClickListItemBaseButton
        {
            //添加一条事件
            add { AddHandler(ClickListItemBaseButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickListItemBaseButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickListItemBaseButton 路由事件
        /// </summary>
        /// <param name="_value">数据</param>
        private void OnClickListItemBaseButton(LatelyProjectData _value)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<LatelyProjectData> args = new RoutedPropertyChangedEventArgs<LatelyProjectData>(_value, _value);

            //设置这是哪个路由事件？
            args.RoutedEvent = LatelyProjectUiControl.ClickListItemBaseButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickOpenFolderButton
        /// <summary>
        /// 路由事件：ClickListItemOpenFolderButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickListItemOpenFolderButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickListItemOpenFolderButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<LatelyProjectData> ClickListItemOpenFolderButton
        {
            //添加一条事件
            add { AddHandler(ClickListItemOpenFolderButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickListItemOpenFolderButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickListItemOpenFolderButton 路由事件
        /// </summary>
        /// <param name="_value">数据</param>
        private void OnClickListItemOpenFolderButton(LatelyProjectData _value)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<LatelyProjectData> args = new RoutedPropertyChangedEventArgs<LatelyProjectData>(_value, _value);

            //设置这是哪个路由事件？
            args.RoutedEvent = LatelyProjectUiControl.ClickListItemOpenFolderButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickListItemRemoveButton
        /// <summary>
        /// 路由事件：ClickListItemRemoveButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickListItemRemoveButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickListItemRemoveButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<LatelyProjectData> ClickListItemRemoveButton
        {
            //添加一条事件
            add { AddHandler(ClickListItemRemoveButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickListItemRemoveButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickListItemRemoveButton 路由事件
        /// </summary>
        /// <param name="_value">数据</param>
        private void OnClickListItemRemoveButton(LatelyProjectData _value)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<LatelyProjectData> args = new RoutedPropertyChangedEventArgs<LatelyProjectData>(_value, _value);

            //设置这是哪个路由事件？
            args.RoutedEvent = LatelyProjectUiControl.ClickListItemRemoveButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion



        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static LatelyProjectUiControl()
        {
            /*注册依赖项属性*/
            //注册DatasProperty
            DatasProperty = DependencyProperty.Register(
                "Datas", //属性的名字
                typeof(ObservableCollection<LatelyProjectData>), //属性的类型
                typeof(LatelyProjectUiControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (ObservableCollection<LatelyProjectData>)new ObservableCollection<LatelyProjectData>(),
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnDatasChanged))
            );





            /*注册路由事件*/
            //注册ClickListItemBaseButtonEvent
            ClickListItemBaseButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickListItemBaseButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<LatelyProjectData>), //路由事件要处理的数据类型
                typeof(LatelyProjectUiControl) //这个路由事件属于哪个控件？
            );

            //注册ClickListItemOpenFolderButtonEvent
            ClickListItemOpenFolderButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickListItemOpenFolderButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<LatelyProjectData>), typeof(LatelyProjectUiControl));

            //注册ClickListItemRemoveButtonEvent
            ClickListItemRemoveButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickListItemRemoveButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<LatelyProjectData>), typeof(LatelyProjectUiControl));


            //注册ClickFoldButtonEvent
            ClickFoldButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickFoldButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(LatelyProjectUiControl));

        }
        #endregion



        #region [事件 - Item]
        //当点击[Item]中的[Base]按钮时
        private void LatelyProjectListItemControl_OnClickBaseButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            LatelyProjectListItemControl _latelyProjectListItemControl = sender as LatelyProjectListItemControl;

            //获取数据
            LatelyProjectData _latelyProjectData = _latelyProjectListItemControl.DataContext as LatelyProjectData;

            //触发事件
            this.OnClickListItemBaseButton(_latelyProjectData);
        }


        //当点击[Item]中的[打开文件夹]按钮时
        private void LatelyProjectListItemControl_OnClickOpenFolderButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            LatelyProjectListItemControl _latelyProjectListItemControl = sender as LatelyProjectListItemControl;

            //获取数据
            LatelyProjectData _latelyProjectData = _latelyProjectListItemControl.DataContext as LatelyProjectData;

            //触发事件
            this.OnClickListItemOpenFolderButton(_latelyProjectData);
        }


        //当点击[Item]中的[从列表中移除]按钮时
        private void LatelyProjectListItemControl_OnClickRemoveButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            LatelyProjectListItemControl _latelyProjectListItemControl = sender as LatelyProjectListItemControl;

            //获取数据
            LatelyProjectData _latelyProjectData = _latelyProjectListItemControl.DataContext as LatelyProjectData;

            //触发事件
            this.OnClickListItemRemoveButton(_latelyProjectData);
        }












        //当第1个（索引为0）的[Item]的[Visible]发生变化时（只用判断第1个Item，就可以啦！）
        private void LatelyProjectListItemControl1_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //判断列表是否为空，如果为null
            if (Datas==null || Datas.Count <= 0)
            {
                //就显示[暂无项目]的图片
                EmptyBorder.Visibility = Visibility.Visible;
            }

            //如果不为null
            else
            {
                //就隐藏[暂无项目]的图片
                EmptyBorder.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region [事件 - 控件]

        //当控件加载完毕时
        private void LatelyProjectUiControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            //获取所有的Item
            Items = new List<LatelyProjectListItemControl>();

            Items.Add(LatelyProjectListItemControl1);
            Items.Add(LatelyProjectListItemControl2);
            Items.Add(LatelyProjectListItemControl3);
            Items.Add(LatelyProjectListItemControl4);
            Items.Add(LatelyProjectListItemControl5);

            Items.Add(LatelyProjectListItemControl6);
            Items.Add(LatelyProjectListItemControl7);
            Items.Add(LatelyProjectListItemControl8);
            Items.Add(LatelyProjectListItemControl9);
            Items.Add(LatelyProjectListItemControl10);

        }



        //当鼠标进入控件时
        private void LatelyProjectUiControl_OnMouseEnter(object sender, MouseEventArgs e)
        {
            //显示[折叠]按钮（把Opacity属性设置为1）
            AnimationTool.PlayGridOpacityAnimation(this.FoldGrid, null, 1, 0.25f);
        }


        //当鼠标移出控件时
        private void LatelyProjectUiControl_OnMouseLeave(object sender, MouseEventArgs e)
        {
            //隐藏[折叠]按钮（把Opacity属性设置为1）
            AnimationTool.PlayGridOpacityAnimation(this.FoldGrid, null, 0, 0.25f);
        }
        #endregion

        #region [事件 - 按钮]
        //当点击[折叠]按钮时
        private void FoldButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickFoldButton();//触发事件
        }

        #endregion

        #region [事件 - 拖动窗口]

        /// <summary>
        /// 当在窗口顶部的矩形中，按下鼠标左键的时候：拖动窗口的时候
        /// </summary>
        private void WindowTitleBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /* 当点击拖拽区域的时候，让窗口跟着移动 */
            //DragMove();

            /*当点击拖拽区域的时候，让窗口跟着移动（并且阻止[窗口拖到屏幕边缘时 自动最大化]）*/
            AppManager.MainWindow.DragMoveWindow(e);
        }

        #endregion



    }
}
