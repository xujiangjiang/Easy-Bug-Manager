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
    /// BugListItemContextMenuControl.xaml 的交互逻辑
    /// </summary>
    public partial class BugListItemContextMenuControl : UserControl
    {
        /* 属性: ProgressType*(进度的类型)(当前)
         *       PriorityType*(优先级的类型)(当前)


           事件: ClickDeleteButton(当点击"删除"按钮的时候)
                 ClickMoreButton(当点击"更多"按钮的时候)
                 ProgressTypeChange(当进度Type改变时)(参数ProgressType：当前的进度类型)
                 PriorityTypeChange(当优先级Type改变时)(参数PriorityType：当前的优先级类型)
                 ClickProgressTypeButton(当点击"进度Type"的按钮时)(参数int：被点击的是第几个Check？)
                 ClickPriorityTypeButton(当点击"优先级Type"的按钮时)(参数int：被点击的是第几个按钮？)*/


        /* TypeChange事件和ClickTypeButton事件的区别：
           （1）TypeChange事件：不管是玩家点击还是赋值，只要导致Type改变，就会触发
           （2）ClickTypeButton事件：只有当玩家点击后，导致Type改变，才会触发*/




        public BugListItemContextMenuControl()
        {
            InitializeComponent();
        }




        #region 依赖项属性：Progress
        /// <summary>
        /// 依赖项属性：进度的类型
        /// </summary>
        public static DependencyProperty ProgressProperty;

        /// <summary>
        /// 公开属性：进度的类型
        /// </summary>
        public ProgressType Progress
        {
            get { return (ProgressType)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ProgressProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnProgressChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ((ProgressType)e.OldValue != (ProgressType)e.NewValue)
            {
                //获取控件
                BugListItemContextMenuControl _bugListItemContextMenuControl = sender as BugListItemContextMenuControl;

                //判断：修改CheckGroup
                switch ((ProgressType)e.NewValue)
                {
                    case ProgressType.Undone:
                        _bugListItemContextMenuControl.ProgressCheckGroup.CheckedIndex = 0;
                        break;

                    case ProgressType.Solved:
                        _bugListItemContextMenuControl.ProgressCheckGroup.CheckedIndex = 1;
                        break;

                    case ProgressType.Deprecat:
                        _bugListItemContextMenuControl.ProgressCheckGroup.CheckedIndex = 2;
                        break;
                }

                //触发事件
                _bugListItemContextMenuControl.OnProgressTypeChange((ProgressType)e.OldValue, (ProgressType)e.NewValue);
            }
        }

        #endregion

        #region 依赖项属性：Priority
        /// <summary>
        /// 依赖项属性：优先级的类型
        /// </summary>
        public static DependencyProperty PriorityProperty;

        /// <summary>
        /// 公开属性：优先级的类型
        /// </summary>
        public PriorityType Priority
        {
            get { return (PriorityType)GetValue(PriorityProperty); }
            set { SetValue(PriorityProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当PriorityProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnPriorityChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ((PriorityType)e.OldValue != (PriorityType)e.NewValue)
            {
                //获取控件
                BugListItemContextMenuControl _bugListItemContextMenuControl = sender as BugListItemContextMenuControl;

                //判断：修改CheckGroup
                switch ((PriorityType)e.NewValue)
                {
                    case PriorityType.Low:
                        _bugListItemContextMenuControl.PriorityCheckGroup.CheckedIndex = 0;
                        break;

                    case PriorityType.Mid:
                        _bugListItemContextMenuControl.PriorityCheckGroup.CheckedIndex = 1;
                        break;

                    case PriorityType.High:
                        _bugListItemContextMenuControl.PriorityCheckGroup.CheckedIndex = 2;
                        break;
                }

                //触发事件
                _bugListItemContextMenuControl.OnPriorityTypeChange((PriorityType)e.OldValue, (PriorityType)e.NewValue);
            }
        }
        #endregion



        #region 路由事件：ClickMoreButton

        /// <summary>
        /// 路由事件：ClickMoreButtonEvent
        /// （当点击[更多]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickMoreButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickMoreButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickMoreButton
        {
            //添加一条事件
            add { AddHandler(ClickMoreButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickMoreButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickMoreButton 路由事件
        /// </summary>
        private void OnClickMoreButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemContextMenuControl.ClickMoreButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
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
            add { AddHandler(ClickMoreButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickMoreButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickDeleteButton 路由事件
        /// </summary>
        private void OnClickDeleteButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemContextMenuControl.ClickDeleteButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ProgressTypeChange
        /// <summary>
        /// 路由事件：ProgressTypeChangeEvent
        /// （当进度Type改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ProgressTypeChangeEvent;


        /// <summary>
        /// 路由事件的属性：ProgressTypeChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<ProgressType> ProgressTypeChange
        {
            //添加一条事件
            add { AddHandler(ProgressTypeChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ProgressTypeChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ProgressTypeChange 路由事件
        /// </summary>
        /// <param name="_oldValue"></param>
        /// <param name="_newValue">当前被选中的[进度Type]</param>
        private void OnProgressTypeChange(ProgressType _oldValue, ProgressType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<ProgressType> args = new RoutedPropertyChangedEventArgs<ProgressType>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemContextMenuControl.ProgressTypeChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：PriorityTypeChange
        /// <summary>
        /// 路由事件：PriorityTypeChangeEvent
        /// （当优先级Type改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent PriorityTypeChangeEvent;


        /// <summary>
        /// 路由事件的属性：PriorityTypeChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<PriorityType> PriorityTypeChange
        {
            //添加一条事件
            add { AddHandler(PriorityTypeChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(PriorityTypeChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 PriorityTypeChange 路由事件
        /// </summary>
        /// <param name="_oldValue"></param>
        /// <param name="_newValue">当前被选中的[优先级Type]</param>
        private void OnPriorityTypeChange(PriorityType _oldValue, PriorityType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<PriorityType> args = new RoutedPropertyChangedEventArgs<PriorityType>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemContextMenuControl.PriorityTypeChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickProgressTypeButton

        /// <summary>
        /// 路由事件：ClickProgressTypeButtonEvent
        /// （当点击[ProgressCheckGroup]中的按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickProgressTypeButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickProgressTypeButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<ProgressType> ClickProgressTypeButton
        {
            //添加一条事件
            add { AddHandler(ClickProgressTypeButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickProgressTypeButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickProgressTypeButton 路由事件
        /// </summary>
        /// <param name="_oldValue">之前被选中的Check的索引</param>
        /// <param name="_newValue">新的被选中的Check的索引</param>
        private void OnClickProgressTypeButton(ProgressType _oldValue, ProgressType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<ProgressType> args = new RoutedPropertyChangedEventArgs<ProgressType>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemContextMenuControl.ClickProgressTypeButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickPriorityTypeButton

        /// <summary>
        /// 路由事件：ClickPriorityTypeButtonEvent
        /// （当点击[PriorityCheckGroup]中的按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickPriorityTypeButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickProgressTypeButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<PriorityType> ClickPriorityTypeButton
        {
            //添加一条事件
            add { AddHandler(ClickPriorityTypeButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickPriorityTypeButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickPriorityTypeButton 路由事件
        /// </summary>
        /// <param name="_oldValue">之前被选中的Check的索引</param>
        /// <param name="_newValue">新的被选中的Check的索引</param>
        private void OnClickPriorityTypeButton(PriorityType _oldValue, PriorityType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<PriorityType> args = new RoutedPropertyChangedEventArgs<PriorityType>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemContextMenuControl.ClickPriorityTypeButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion



        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static BugListItemContextMenuControl()
        {
            /*注册依赖项属性*/
            //注册ProgressProperty
            ProgressProperty = DependencyProperty.Register(
                "Progress", //属性的名字
                typeof(ProgressType), //属性的类型
                typeof(BugListItemContextMenuControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (ProgressType)ProgressType.None,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnProgressChanged))
            );

            //注册PriorityProperty
            PriorityProperty = DependencyProperty.Register(
                "Priority", typeof(PriorityType), typeof(BugListItemContextMenuControl),
                new FrameworkPropertyMetadata((PriorityType)PriorityType.None, new PropertyChangedCallback(OnPriorityChanged)));





            /*注册路由事件*/
            //注册ClickDeleteButtonEvent
            ClickDeleteButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickDeleteButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(BugListItemContextMenuControl) //这个路由事件属于哪个控件？
            );

            //注册ClickMoreButtonEvent
            ClickMoreButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickMoreButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugListItemContextMenuControl));


            //注册ProgressTypeChangeEvent
            ProgressTypeChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ProgressTypeChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<ProgressType>), typeof(BugListItemContextMenuControl));

            //注册PriorityTypeChangeEvent
            PriorityTypeChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "PriorityTypeChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<PriorityType>), typeof(BugListItemContextMenuControl));


            //注册ClickProgressTypeButtonEvent
            ClickProgressTypeButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickProgressTypeButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<ProgressType>), typeof(BugListItemContextMenuControl));

            //注册ClickPriorityTypeButtonEvent
            ClickPriorityTypeButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickPriorityTypeButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<PriorityType>), typeof(BugListItemContextMenuControl));
        }





        #endregion




        #region 字段
        /*显示Panel*/
        private bool isMouseEnterProgressPanel = false;//鼠标是否进入了Progress面板
        private bool isMouseEnterProgressButton = false;//鼠标是否进入了Progress按钮
        private bool isMouseEnterPriorityPanel = false;//鼠标是否进入了Progress面板
        private bool isMouseEnterPriorityButton = false;//鼠标是否进入了Progress按钮
        #endregion

        #region 事件 -[显示Panel]
        //当鼠标进入[进度]按钮时
        private void ProgressButtonControl_MouseEnter(object sender, MouseEventArgs e)
        {
            //修改标识符
            this.isMouseEnterProgressButton = true;

            //显示Panel
            if (this.ProgressPanel.IsOpen != true)
            {
                this.ProgressPanel.IsOpen = true;
            }
        }

        //当鼠标移出[进度]按钮时
        private void ProgressButtonControl_MouseLeave(object sender, MouseEventArgs e)
        {
            //修改标识符
            this.isMouseEnterProgressButton = false;

            //隐藏Panel
            if (this.isMouseEnterProgressButton == false && this.isMouseEnterProgressPanel == false)
            {
                this.ProgressPanel.IsOpen = false;
            }
        }

        //当鼠标进入[进度]面板时
        private void ProgressPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            //修改标识符
            this.isMouseEnterProgressPanel = true;

            //显示Panel
            if (this.ProgressPanel.IsOpen != true)
            {
                this.ProgressPanel.IsOpen = true;
            }
        }

        //当鼠标离开[进度]面板时
        private void ProgressPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            //修改标识符
            this.isMouseEnterProgressPanel = false;

            //隐藏Panel
            if (this.isMouseEnterProgressButton == false && this.isMouseEnterProgressPanel == false)
            {
                this.ProgressPanel.IsOpen = false;
            }
        }




        //当鼠标进入[进度]按钮时
        private void PriorityButtonControl_MouseEnter(object sender, MouseEventArgs e)
        {
            //修改标识符
            this.isMouseEnterPriorityButton = true;

            //显示Panel
            if (this.PriorityPanel.IsOpen != true)
            {
                this.PriorityPanel.IsOpen = true;
            }
        }

        //当鼠标移出[进度]按钮时
        private void PriorityButtonControl_MouseLeave(object sender, MouseEventArgs e)
        {
            //修改标识符
            this.isMouseEnterPriorityButton = false;

            //隐藏Panel
            if (this.isMouseEnterPriorityButton == false && this.isMouseEnterPriorityPanel == false)
            {
                this.PriorityPanel.IsOpen = false;
            }
        }

        //当鼠标进入[进度]面板时
        private void PriorityPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            //修改标识符
            this.isMouseEnterPriorityPanel = true;

            //显示Panel
            if (this.PriorityPanel.IsOpen != true)
            {
                this.PriorityPanel.IsOpen = true;
            }
        }

        //当鼠标离开[进度]面板时
        private void PriorityPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            //修改标识符
            this.isMouseEnterPriorityPanel = false;

            //隐藏Panel
            if (this.isMouseEnterPriorityButton == false && this.isMouseEnterPriorityPanel == false)
            {
                this.PriorityPanel.IsOpen = false;
            }
        }
        #endregion

        #region 事件 -[点击]
        //当[进度]CheckGroup控件中的  选中项改变时
        private void ProgressCheckGroupControl_CheckedChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //获取控件
            ImageCheckGroupControl _imageCheckGroupControl = sender as ImageCheckGroupControl;

            //判断
            switch (_imageCheckGroupControl.CheckedIndex)
            {
                //如果为0
                case 0:
                    Progress = ProgressType.Undone;
                    break;

                //如果为1
                case 1:
                    Progress = ProgressType.Solved;
                    break;

                //如果为2
                case 2:
                    Progress = ProgressType.Deprecat;
                    break;
            }
        }

        //当[优先级]CheckGroup控件中的  选中项改变时
        private void PriorityCheckGroupControl_CheckedChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //获取控件
            ImageCheckGroupControl _imageCheckGroupControl = sender as ImageCheckGroupControl;

            //判断
            switch (_imageCheckGroupControl.CheckedIndex)
            {
                //如果为0
                case 0:
                    Priority = PriorityType.Low;
                    break;

                //如果为1
                case 1:
                    Priority = PriorityType.Mid;
                    break;

                //如果为2
                case 2:
                    Priority = PriorityType.High;
                    break;
            }
        }



        //当点击[进度]CheckGroup控件中的 Check时
        private void ProgressCheckGroupControl_ClickCheck(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //触发事件
            ProgressType _oldValue = (ProgressType)(((int)e.OldValue) + 1);
            ProgressType _newValue = (ProgressType)(((int)e.NewValue) + 1);
            this.OnClickProgressTypeButton(_oldValue, _newValue);
        }

        //当点击[优先级]CheckGroup控件中的 Check时
        private void PriorityCheckGroupControl_ClickCheck(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //触发事件
            PriorityType _oldValue = (PriorityType)(((int)e.OldValue) + 1);
            PriorityType _newValue = (PriorityType)(((int)e.NewValue) + 1);
            this.OnClickPriorityTypeButton(_oldValue, _newValue);
        }






        //当点击[删除]按钮时
        private void DeleteButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickDeleteButton();
        }

        //当点击[更多]按钮时
        private void MoreButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickMoreButton();
        }
        #endregion

    }
}
