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
    /// ChangeBugUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeBugUiControl : UserControl
    {
        /* 属性: BugName*(Bug名字)
                 PriorityType(优先级类型)
                 ProgressType(完成度类型)
                 RelatedBugNames(相关的Bug名字)
                 TipString(提示文字)

                 BugData(Bug的数据)
                 BugNameMaxLength(Bug名字 的最大长度(最大个数))

           事件: ClickYesButton(当点击[确定]按钮时)
                 ClickNoButton(当点击[取消]按钮时)
                 ClickRelatedBugNameButton(当点击[相关Bug名字]按钮时)
                 PriorityChange(当[优先级]改变时)
                 ProgressChange(当[完成度]改变时)
                 BugNameChange(当[Bug的名字]改变时)*/



        public ChangeBugUiControl()
        {
            InitializeComponent();
        }




        #region 依赖项属性：BugName*
        /// <summary>
        /// 公开属性：Bug的名字
        /// </summary>
        public string BugName
        {
            get { return this.BugNameTextBox.Text; }
            set { this.BugNameTextBox.Text = value; }
        }
        #endregion

        #region 依赖项属性：PriorityType
        /// <summary>
        /// 依赖项属性：优先级的类型
        /// </summary>
        public static DependencyProperty PriorityTypeProperty;

        /// <summary>
        /// 公开属性：优先级的类型
        /// </summary>
        public PriorityType PriorityType
        {
            get { return (PriorityType)GetValue(PriorityTypeProperty); }
            set { SetValue(PriorityTypeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当PriorityTypeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnPriorityTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ((PriorityType)e.OldValue != (PriorityType)e.NewValue)
            {
                //获取控件
                ChangeBugUiControl _changeBugUiControl = sender as ChangeBugUiControl;

                //判断：修改CheckGroup
                switch ((PriorityType)e.NewValue)
                {
                    case PriorityType.Low:
                        _changeBugUiControl.PriorityCheckGroup.CheckedIndex = 0;
                        break;

                    case PriorityType.Mid:
                        _changeBugUiControl.PriorityCheckGroup.CheckedIndex = 1;
                        break;

                    case PriorityType.High:
                        _changeBugUiControl.PriorityCheckGroup.CheckedIndex = 2;
                        break;
                }

                //触发事件
                _changeBugUiControl.OnPriorityChange((PriorityType)e.OldValue, (PriorityType)e.NewValue);
            }
        }
        #endregion

        #region 依赖项属性：ProgressType
        /// <summary>
        /// 依赖项属性：完成度类型
        /// </summary>
        public static DependencyProperty ProgressTypeProperty;

        /// <summary>
        /// 公开属性：完成度类型
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
            if ((ProgressType)e.OldValue != (ProgressType)e.NewValue)
            {
                //获取控件
                ChangeBugUiControl _changeBugUiControl = sender as ChangeBugUiControl;

                //判断：修改CheckGroup
                switch ((ProgressType)e.NewValue)
                {
                    case ProgressType.Undone:
                        _changeBugUiControl.ProgressCheckGroup.CheckedIndex = 0;
                        break;

                    case ProgressType.Solved:
                        _changeBugUiControl.ProgressCheckGroup.CheckedIndex = 1;
                        break;

                    case ProgressType.Deprecat:
                        _changeBugUiControl.ProgressCheckGroup.CheckedIndex = 2;
                        break;
                }

                //触发事件
                _changeBugUiControl.OnProgressChange((ProgressType)e.OldValue, (ProgressType)e.NewValue);
            }
        }
        #endregion

        #region 依赖项属性：RelatedBugNames
        /// <summary>
        /// 依赖项属性：相关的Bug名字
        /// </summary>
        public static DependencyProperty RelatedBugNamesProperty;

        /// <summary>
        /// 公开属性：相关的Bug名字
        /// </summary>
        public ObservableCollection<HighlightText> RelatedBugNames
        {
            get { return (ObservableCollection<HighlightText>)GetValue(RelatedBugNamesProperty); }
            set { SetValue(RelatedBugNamesProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当RelatedBugNamesProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnRelatedBugNamesChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：TipString
        /// <summary>
        /// 依赖项属性：提示文字
        /// </summary>
        public static DependencyProperty TipStringProperty;

        /// <summary>
        /// 公开属性：提示文字
        /// </summary>
        public string TipString
        {
            get { return (string)GetValue(TipStringProperty); }
            set { SetValue(TipStringProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当TipStringProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnTipStringChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：BugData
        /// <summary>
        /// 依赖项属性：Bug的数据
        /// </summary>
        public static DependencyProperty BugDataProperty;

        /// <summary>
        /// 公开属性：Bug的数据
        /// </summary>
        public BugData BugData
        {
            get { return (BugData)GetValue(BugDataProperty); }
            set { SetValue(BugDataProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当BugDataProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnBugDataChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：BugNameMaxLength
        /// <summary>
        /// 依赖项属性：Bug名字 的最大长度(最大个数)
        /// </summary>
        public static DependencyProperty BugNameMaxLengthProperty;

        /// <summary>
        /// 公开属性：Bug名字 的最大长度(最大个数)
        /// </summary>
        public int BugNameMaxLength
        {
            get { return (int)GetValue(BugNameMaxLengthProperty); }
            set { SetValue(BugNameMaxLengthProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当BugNameMaxLengthProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnBugNameMaxLengthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion



        #region 路由事件：ClickYesButton
        /// <summary>
        /// 路由事件：ClickYesButtonEvent
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
            args.RoutedEvent = ChangeBugUiControl.ClickYesButtonEvent;

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
            args.RoutedEvent = ChangeBugUiControl.ClickNoButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickRelatedBugNameButton
        /// <summary>
        /// 路由事件：ClickRelatedBugNameButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickRelatedBugNameButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickRelatedBugNameButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<HighlightText> ClickRelatedBugNameButton
        {
            //添加一条事件
            add { AddHandler(ClickRelatedBugNameButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickRelatedBugNameButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickRelatedBugNameButton 路由事件
        /// </summary>
        /// <param name="_newValue">被点击的Bug的名字</param>
        private void OnClickRelatedBugNameButton(HighlightText _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<HighlightText> args = new RoutedPropertyChangedEventArgs<HighlightText>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ChangeBugUiControl.ClickRelatedBugNameButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：PriorityChange
        /// <summary>
        /// 路由事件：PriorityChangeEvent
        /// （当值改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent PriorityChangeEvent;


        /// <summary>
        /// 路由事件的属性：PriorityChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<PriorityType> PriorityChange
        {
            //添加一条事件
            add { AddHandler(PriorityChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(PriorityChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 PriorityChange 路由事件
        /// </summary>
        private void OnPriorityChange(PriorityType _oldValue, PriorityType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<PriorityType> args = new RoutedPropertyChangedEventArgs<PriorityType>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ChangeBugUiControl.PriorityChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ProgressChange
        /// <summary>
        /// 路由事件：ProgressChangeEvent
        /// （当值改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ProgressChangeEvent;


        /// <summary>
        /// 路由事件的属性：ProgressChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<ProgressType> ProgressChange
        {
            //添加一条事件
            add { AddHandler(ProgressChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ProgressChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ProgressChange 路由事件
        /// </summary>
        private void OnProgressChange(ProgressType _oldValue, ProgressType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<ProgressType> args = new RoutedPropertyChangedEventArgs<ProgressType>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ChangeBugUiControl.ProgressChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：BugNameChange
        /// <summary>
        /// 路由事件：BugNameChangeEvent
        /// （当值改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent BugNameChangeEvent;


        /// <summary>
        /// 路由事件的属性：BugNameChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<string> BugNameChange
        {
            //添加一条事件
            add { AddHandler(BugNameChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(BugNameChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 BugNameChange 路由事件
        /// </summary>
        private void OnBugNameChange(string _oldValue, string _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<string> args = new RoutedPropertyChangedEventArgs<string>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ChangeBugUiControl.BugNameChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion




        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static ChangeBugUiControl()
        {
            /*注册依赖项属性*/
            //注册PriorityTypeProperty
            PriorityTypeProperty = DependencyProperty.Register(
                "PriorityType", //属性的名字
                typeof(PriorityType), //属性的类型
                typeof(ChangeBugUiControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (PriorityType)PriorityType.None,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnPriorityTypeChanged))
            );

            //注册ProgressTypeProperty
            ProgressTypeProperty = DependencyProperty.Register(
                "ProgressType", typeof(ProgressType), typeof(ChangeBugUiControl),
                new FrameworkPropertyMetadata((ProgressType)ProgressType.None, new PropertyChangedCallback(OnProgressTypeChanged)));

            //注册RelatedBugNamesProperty
            RelatedBugNamesProperty = DependencyProperty.Register(
                "RelatedBugNames", typeof(ObservableCollection<HighlightText>), typeof(ChangeBugUiControl),
                new FrameworkPropertyMetadata((ObservableCollection<HighlightText>)new ObservableCollection<HighlightText>(), new PropertyChangedCallback(OnRelatedBugNamesChanged)));

            //注册TipStringProperty
            TipStringProperty = DependencyProperty.Register(
                "TipString", typeof(string), typeof(ChangeBugUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnTipStringChanged)));

            //注册BugDataProperty
            BugDataProperty = DependencyProperty.Register(
                "BugData", typeof(BugData), typeof(ChangeBugUiControl),
                new FrameworkPropertyMetadata((BugData)null, new PropertyChangedCallback(OnBugDataChanged)));

            //注册BugNameMaxLengthProperty
            BugNameMaxLengthProperty = DependencyProperty.Register(
                "BugNameMaxLength", typeof(int), typeof(ChangeBugUiControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnBugNameMaxLengthChanged)));






            /*注册路由事件*/
            //注册ClickYesButtonEvent
            ClickYesButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickYesButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(ChangeBugUiControl) //这个路由事件属于哪个控件？
            );

            //注册ClickNoButtonEvent
            ClickNoButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickNoButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ChangeBugUiControl));

            //注册ClickRelatedBugNameButtonEvent
            ClickRelatedBugNameButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickRelatedBugNameButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<HighlightText>), typeof(ChangeBugUiControl));

            //注册PriorityChangeEvent
            PriorityChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "PriorityChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<PriorityType>), typeof(ChangeBugUiControl));

            //注册ProgressChangeEvent
            ProgressChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ProgressChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<ProgressType>), typeof(ChangeBugUiControl));

            //注册BugNameChangeEvent
            BugNameChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "BugNameChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<string>), typeof(ChangeBugUiControl));


        }
        #endregion





        #region [事件 - 文本框+相关Bug面板]
        //当Name文本框里的文字改变时
        private void BugNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //取到TextBox控件
            TextBox _textBox = sender as TextBox;

            //如果TextBox的内容为空，就把TextBox的背景设置为透明
            //如果TextBox的内容不为空，把TextBox的背景设置为白色
            AnimationTool.PlayTextChangedAnimation(_textBox);

            //触发事件
            this.OnBugNameChange(_textBox.Text, _textBox.Text);

            //显示[相关Bug]的面板
            OpenOrCloseRelatedBugsGrid(true);
        }

        //当BugName文本框 获取焦点时
        private void BugNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //显示[相关Bug]的面板
            OpenOrCloseRelatedBugsGrid(true);
        }

        //当BugName文本框 失去焦点时
        private void BugNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //隐藏[相关Bug]的面板
            //OpenOrCloseRelatedBugsGrid(false);
        }

        //当点击[关闭相关Bug面板]时
        private void CloseRelatedBugsGridButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //隐藏[相关Bug]的面板
            OpenOrCloseRelatedBugsGrid(false);
        }
        #endregion

        #region [事件 - 按钮]
        //当点击[浏览]按钮时
        private void RelatedBugButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取到控件
            ColorButtonControl _buttonControl = sender as ColorButtonControl;

            //触发事件
            this.OnClickRelatedBugNameButton(_buttonControl.Tag as HighlightText);
        }

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

        #region [事件 - CheckGroup]
        //当[优先级]的CheckGroup改变时
        private void PriorityCheckGroup_CheckedChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //获取控件
            ImageCheckGroupControl _imageCheckGroupControl = sender as ImageCheckGroupControl;

            //判断
            switch (_imageCheckGroupControl.CheckedIndex)
            {
                //如果为0
                case 0:
                    PriorityType = PriorityType.Low;
                    break;

                //如果为1
                case 1:
                    PriorityType = PriorityType.Mid;
                    break;

                //如果为2
                case 2:
                    PriorityType = PriorityType.High;
                    break;
            }
        }

        //当[完成度]的CheckGroup改变时
        private void ProgressCheckGroup_CheckedChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //获取控件
            ImageCheckGroupControl _imageCheckGroupControl = sender as ImageCheckGroupControl;

            //判断
            switch (_imageCheckGroupControl.CheckedIndex)
            {
                //如果为0
                case 0:
                    ProgressType = ProgressType.Undone;
                    break;

                //如果为1
                case 1:
                    ProgressType = ProgressType.Solved;
                    break;

                //如果为2
                case 2:
                    ProgressType = ProgressType.Deprecat;
                    break;
            }
        }
        #endregion




        #region [私有方法 - 显示/隐藏 相关Bug面板]
        /// <summary>
        /// 打开或者关闭 [相关Bug]的面板
        /// </summary>
        /// <param name="_isOpen">是否打开？</param>
        public void OpenOrCloseRelatedBugsGrid(bool _isOpen)
        {
            //如果是打开
            if (_isOpen == true)
            {
                ObservableCollection<HighlightText> _relatedBugs = this.RelatedBugsListBox.ItemsSource as ObservableCollection<HighlightText>;

                //如果没有[相关的Bug]，就关闭面板
                if (_relatedBugs == null || _relatedBugs.Count == 0)
                {
                    //隐藏[相关Bug]的面板
                    this.RelatedBugsGrid.Visibility = Visibility.Collapsed;
                }

                //如果有[相关的Bug]，就打开面板
                else
                {
                    //显示[相关Bug]的面板
                    this.RelatedBugsGrid.Visibility = Visibility.Visible;
                }
            }

            //如果是关闭
            else
            {
                //隐藏[相关Bug]的面板
                this.RelatedBugsGrid.Visibility = Visibility.Collapsed;
            }
        }

        #endregion
    }
}
