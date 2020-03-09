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

namespace EasyBugManager
{
    /// <summary>
    /// BugUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class BugUiControl : UserControl
    {
        /* 属性: BugName(Bug的名字)
                 ProgressType(进度的类型)
                 PriorityType(优先级的类型)
                 StartTime(开始时间)
                 EndTime(结束时间)
                 UpdateNumber(更新次数)

                 RecordItemDatas(所有的记录)

                 InputBoxText*(输入框中的 文字)
                 InputBoxImagePaths(输入框中的 图片的路径)
                 IsShowSubmitButtonAnimation(是否显示提交按钮的动画？)

                 IsShowBugReply(是否显示Bug的回复？)

                 UpdateNumberFrontText([更新次数]前面的文字)
                 UpdateNumberBehindText([更新次数]后面的文字)


           事件: ClickBackButton(当点击[返回]按钮的时候)
                 ClickProgressButton(当点击[进度]按钮的时候)
                 ClickPriorityButton(当点击[优先级]按钮的时候)
                 ClickBugNameButton(当点击[Bug名字]按钮的时候)
                 
                 ClickSubmitButton(当点击[提交]按钮的时候)
                 ClickChooseImageButton(当点击[选择图片]按钮的时候)
                 ClickInputBoxImageButton(当点击[输入框]的[图片]按钮的时候)（参数string：被点击的图片路径）
                 ClickInputBoxDeleteImageButton(当点击[输入框]的[删除图片]按钮的时候)（参数string：被点击的图片路径）
                 
                 ClickRecordListItemDeleteButton(当点击[记录列表的Item]的[删除]按钮的时候)（e.Source参数：触发事件的RecordItem控件 的RecordData数据）
                 ClickRecordListItemImageButton(当点击[记录列表的Item]的[图片]按钮的时候)（参数string：点击的 图片的路径）（e.Source参数：触发事件的RecordItem控件 的RecordData数据）
                 
                 IsShowBugReplyChange(当[是否显示Bug的回复？]改变时)
                 IsShowSubmitButtonAnimationChange(当[是否显示提交按钮的动画？]改变时)*/



        public BugUiControl()
        {
            InitializeComponent();
        }




        #region 依赖项属性：BugName
        /// <summary>
        /// 依赖项属性：Bug的名字
        /// </summary>
        public static DependencyProperty BugNameProperty;

        /// <summary>
        /// 公开属性：Bug的名字
        /// </summary>
        public string BugName
        {
            get { return (string)GetValue(BugNameProperty); }
            set { SetValue(BugNameProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当BugNameProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnBugNameChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

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
        }
        #endregion

        #region 依赖项属性：StartTime
        /// <summary>
        /// 依赖项属性：开始时间
        /// </summary>
        public static DependencyProperty StartTimeProperty;

        /// <summary>
        /// 公开属性：开始时间
        /// </summary>
        public string StartTime
        {
            get { return (string)GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当StartTimeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnStartTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：EndTime
        /// <summary>
        /// 依赖项属性：结束时间
        /// </summary>
        public static DependencyProperty EndTimeProperty;

        /// <summary>
        /// 公开属性：结束时间
        /// </summary>
        public string EndTime
        {
            get { return (string)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当EndTimeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnEndTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //取到控件
            BugUiControl _bugUiControl = sender as BugUiControl;

            //如果EndTime为空的话，就隐藏[解决Bug]的Border控件
            string _newValue = e.NewValue as string;
            if (_newValue == null || _newValue == "")
            {
                _bugUiControl.SolveBugBorder.Visibility = Visibility.Hidden;
            }
            else
            {
                _bugUiControl.SolveBugBorder.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region 依赖项属性：UpdateNumber
        /// <summary>
        /// 依赖项属性：更新次数
        /// </summary>
        public static DependencyProperty UpdateNumberProperty;

        /// <summary>
        /// 公开属性：更新次数
        /// </summary>
        public int UpdateNumber
        {
            get { return (int)GetValue(UpdateNumberProperty); }
            set { SetValue(UpdateNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当UpdateNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnUpdateNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion


        #region 依赖项属性：RecordItemDatas
        /// <summary>
        /// 依赖项属性：所有的记录
        /// </summary>
        public static DependencyProperty RecordItemDatasProperty;

        /// <summary>
        /// 公开属性：所有的记录
        /// </summary>
        public ObservableCollection<RecordItemData> RecordItemDatas
        {
            get { return (ObservableCollection<RecordItemData>)GetValue(RecordItemDatasProperty); }
            set { SetValue(RecordItemDatasProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当RecordItemDatasProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnRecordItemDatasChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion


        #region 依赖项属性：InputBoxText*
        /// <summary>
        /// 公开属性：输入框中的 文字
        /// </summary>
        public string InputBoxText
        {
            get { return this.RecordInputBoxControl.Text; }
            set { this.RecordInputBoxControl.Text = value; }
        }
        #endregion

        #region 依赖项属性：InputBoxImagePaths
        /// <summary>
        /// 依赖项属性：输入框中的 图片的路径
        /// </summary>
        public static DependencyProperty InputBoxImagePathsProperty;

        /// <summary>
        /// 公开属性：输入框中的 图片的路径
        /// </summary>
        public ObservableCollection<string> InputBoxImagePaths
        {
            get { return (ObservableCollection<string>)GetValue(InputBoxImagePathsProperty); }
            set { SetValue(InputBoxImagePathsProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当InputBoxImagePathsProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnInputBoxImagePathsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：IsShowSubmitButtonAnimation
        /// <summary>
        /// 依赖项属性：是否显示提交按钮的动画？
        /// </summary>
        public static DependencyProperty IsShowSubmitButtonAnimationProperty;

        /// <summary>
        /// 公开属性：是否显示提交按钮的动画？
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
            BugUiControl _bugUiControl = sender as BugUiControl;

            //判断：动画
            switch ((bool)e.NewValue)
            {
                //当[省略号]显示时，等2秒后，关闭[省略号]
                case true:
                    AnimationTool.PlayGridOpacityAnimation(_bugUiControl.AnimationGrid, 0, 0, 2f,
                        (object _sender1, EventArgs _e1) =>
                        {
                            //当完成动画后，关闭[省略号]
                            _bugUiControl.IsShowSubmitButtonAnimation = false;
                        });
                    break;
            }

            //判断：返回按钮
            switch ((bool)e.NewValue)
            {
                //当[省略号]显示时，关闭[返回按钮]
                case true:
                    _bugUiControl.BackGrid.Visibility = Visibility.Collapsed;
                    break;

                //当[省略号]隐藏时，显示[返回按钮]
                case false:
                    _bugUiControl.BackGrid.Visibility = Visibility.Visible;
                    break;
            }

            //触发事件
            _bugUiControl.OnIsShowSubmitButtonAnimationChange((bool)e.OldValue, (bool)e.NewValue);
        }
        #endregion


        #region 依赖项属性：IsShowBugReply
        /// <summary>
        /// 依赖项属性：是否显示Bug的回复？
        /// </summary>
        public static DependencyProperty IsShowBugReplyProperty;

        /// <summary>
        /// 公开属性：是否显示Bug的回复？
        /// </summary>
        public bool IsShowBugReply
        {
            get { return (bool)GetValue(IsShowBugReplyProperty); }
            set { SetValue(IsShowBugReplyProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsShowBugReplyProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsShowBugReplyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion


        #region 依赖项属性：UpdateNumberFrontText
        /// <summary>
        /// 依赖项属性：[更新次数]前面的文字
        /// </summary>
        public static DependencyProperty UpdateNumberFrontTextProperty;

        /// <summary>
        /// 公开属性：[更新次数]前面的文字
        /// </summary>
        public string UpdateNumberFrontText
        {
            get { return (string)GetValue(UpdateNumberFrontTextProperty); }
            set { SetValue(UpdateNumberFrontTextProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当UpdateNumberFrontTextProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnUpdateNumberFrontTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：UpdateNumberBehindText
        /// <summary>
        /// 依赖项属性：[更新次数]后面的文字
        /// </summary>
        public static DependencyProperty UpdateNumberBehindTextProperty;

        /// <summary>
        /// 公开属性：[更新次数]后面的文字
        /// </summary>
        public string UpdateNumberBehindText
        {
            get { return (string)GetValue(UpdateNumberBehindTextProperty); }
            set { SetValue(UpdateNumberBehindTextProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当UpdateNumberBehindTextProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnUpdateNumberBehindTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion





        #region 路由事件：ClickBackButton
        /// <summary>
        /// 路由事件：ClickBackButtonEvent
        /// （当点击[返回]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickBackButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickBackButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickBackButton
        {
            //添加一条事件
            add { AddHandler(ClickBackButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickBackButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickBackButton 路由事件
        /// </summary>
        private void OnClickBackButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugUiControl.ClickBackButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickProgressButton
        /// <summary>
        /// 路由事件：ClickProgressButtonEvent
        /// （当点击[进度]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickProgressButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickProgressButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickProgressButton
        {
            //添加一条事件
            add { AddHandler(ClickProgressButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickProgressButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickProgressButton 路由事件
        /// </summary>
        private void OnClickProgressButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugUiControl.ClickProgressButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickPriorityButton
        /// <summary>
        /// 路由事件：ClickPriorityButtonEvent
        /// （当点击[优先级]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickPriorityButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickPriorityButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickPriorityButton
        {
            //添加一条事件
            add { AddHandler(ClickPriorityButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickPriorityButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickPriorityButton 路由事件
        /// </summary>
        private void OnClickPriorityButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugUiControl.ClickPriorityButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickBugNameButton
        /// <summary>
        /// 路由事件：ClickBugNameButtonEvent
        /// （当点击[Bug名字]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickBugNameButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickBugNameButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickBugNameButton
        {
            //添加一条事件
            add { AddHandler(ClickBugNameButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickBugNameButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickBugNameButton 路由事件
        /// </summary>
        private void OnClickBugNameButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugUiControl.ClickBugNameButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion


        #region 路由事件：ClickSubmitButton
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
            args.RoutedEvent = BugUiControl.ClickSubmitButtonEvent;

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
            args.RoutedEvent = BugUiControl.ClickChooseImageButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickInputBoxImageButton
        /// <summary>
        /// 路由事件：ClickInputBoxImageButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickInputBoxImageButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickInputBoxImageButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<string> ClickInputBoxImageButton
        {
            //添加一条事件
            add { AddHandler(ClickInputBoxImageButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickInputBoxImageButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickInputBoxImageButton 路由事件
        /// </summary>
        /// <param name="_imagePath">点击的 图片的路径</param>
        private void OnClickInputBoxImageButton(string _imagePath)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<string> args = new RoutedPropertyChangedEventArgs<string>(_imagePath, _imagePath);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugUiControl.ClickInputBoxImageButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickInputBoxDeleteImageButton
        /// <summary>
        /// 路由事件：ClickInputBoxDeleteImageButtonEvent
        /// （当点击[删除图片]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickInputBoxDeleteImageButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickInputBoxDeleteImageButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<string> ClickInputBoxDeleteImageButton
        {
            //添加一条事件
            add { AddHandler(ClickInputBoxDeleteImageButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickInputBoxDeleteImageButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickInputBoxDeleteImageButton 路由事件
        /// </summary>
        /// <param name="_imagePath">点击的 图片的路径</param>
        private void OnClickInputBoxDeleteImageButton(string _imagePath)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<string> args = new RoutedPropertyChangedEventArgs<string>(_imagePath, _imagePath);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugUiControl.ClickInputBoxDeleteImageButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion


        #region 路由事件：ClickRecordListItemDeleteButton
        /// <summary>
        /// 路由事件：ClickRecordListItemDeleteButtonEvent
        /// （当点击[删除]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickRecordListItemDeleteButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickRecordListItemDeleteButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<RecordData> ClickRecordListItemDeleteButton
        {
            //添加一条事件
            add { AddHandler(ClickRecordListItemDeleteButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickRecordListItemDeleteButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickRecordListItemDeleteButton 路由事件
        /// </summary>
        /// <param name="_source">触发事件的RecordData对象</param>
        private void OnClickRecordListItemDeleteButton(RecordData _source)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<RecordData> args = new RoutedPropertyChangedEventArgs<RecordData>(_source, _source);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugUiControl.ClickRecordListItemDeleteButtonEvent;
            args.Source = _source;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickRecordListItemImageButton
        /// <summary>
        /// 路由事件：ClickRecordListItemImageButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickRecordListItemImageButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickRecordListItemImageButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<string> ClickRecordListItemImageButton
        {
            //添加一条事件
            add { AddHandler(ClickRecordListItemImageButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickRecordListItemImageButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickRecordListItemImageButton 路由事件
        /// </summary>
        /// <param name="_imagePath">点击的 图片的路径</param>
        private void OnClickRecordListItemImageButton(string _imagePath)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<string> args = new RoutedPropertyChangedEventArgs<string>(_imagePath, _imagePath);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugUiControl.ClickRecordListItemImageButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion


        #region 路由事件：IsShowBugReplyChange
        /// <summary>
        /// 路由事件：IsShowBugReplyChangeEvent
        /// （当点击[返回]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent IsShowBugReplyChangeEvent;


        /// <summary>
        /// 路由事件的属性：IsShowBugReplyChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> IsShowBugReplyChange
        {
            //添加一条事件
            add { AddHandler(IsShowBugReplyChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(IsShowBugReplyChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 IsShowBugReplyChange 路由事件
        /// </summary>
        /// <param name="_oldValue">旧的值</param>
        /// <param name="_newValue">新的值</param>
        private void OnIsShowBugReplyChange(bool _oldValue, bool _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugUiControl.IsShowBugReplyChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：IsShowSubmitButtonAnimationChange
        /// <summary>
        /// 路由事件：IsShowSubmitButtonAnimationChangeEvent
        /// （当点击[返回]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent IsShowSubmitButtonAnimationChangeEvent;


        /// <summary>
        /// 路由事件的属性：IsShowSubmitButtonAnimationChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> IsShowSubmitButtonAnimationChange
        {
            //添加一条事件
            add { AddHandler(IsShowSubmitButtonAnimationChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(IsShowSubmitButtonAnimationChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 IsShowSubmitButtonAnimationChange 路由事件
        /// </summary>
        /// <param name="_oldValue">旧的值</param>
        /// <param name="_newValue">新的值</param>
        private void OnIsShowSubmitButtonAnimationChange(bool _oldValue, bool _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugUiControl.IsShowSubmitButtonAnimationChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion





        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static BugUiControl()
        {
            /*注册依赖项属性*/
            //注册BugNameProperty
            BugNameProperty = DependencyProperty.Register(
                "BugName", //属性的名字
                typeof(string), //属性的类型
                typeof(BugUiControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (string)"",
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnBugNameChanged))
            );

            //注册ProgressTypeProperty
            ProgressTypeProperty = DependencyProperty.Register(
                "ProgressType", typeof(ProgressType), typeof(BugUiControl),
                new FrameworkPropertyMetadata((ProgressType)ProgressType.None, new PropertyChangedCallback(OnProgressTypeChanged)));

            //注册PriorityTypeProperty
            PriorityTypeProperty = DependencyProperty.Register(
                "PriorityType", typeof(PriorityType), typeof(BugUiControl),
                new FrameworkPropertyMetadata((PriorityType)PriorityType.None, new PropertyChangedCallback(OnPriorityTypeChanged)));

            //注册StartTimeProperty
            StartTimeProperty = DependencyProperty.Register(
                "StartTime", typeof(string), typeof(BugUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnStartTimeChanged)));

            //注册EndTimeProperty
            EndTimeProperty = DependencyProperty.Register(
                "EndTime", typeof(string), typeof(BugUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnEndTimeChanged)));

            //注册UpdateNumberProperty
            UpdateNumberProperty = DependencyProperty.Register(
                "UpdateNumber", typeof(string), typeof(BugUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnUpdateNumberChanged)));



            //注册RecordItemDatasProperty
            RecordItemDatasProperty = DependencyProperty.Register(
                "RecordItemDatas", typeof(ObservableCollection<RecordItemData>), typeof(BugUiControl),
                new FrameworkPropertyMetadata((ObservableCollection<RecordItemData>)new ObservableCollection<RecordItemData>(), new PropertyChangedCallback(OnRecordItemDatasChanged)));

            //注册InputBoxImagePathsProperty
            InputBoxImagePathsProperty = DependencyProperty.Register(
                "InputBoxImagePaths", typeof(ObservableCollection<string>), typeof(BugUiControl),
                new FrameworkPropertyMetadata((ObservableCollection<string>)new ObservableCollection<string>(), new PropertyChangedCallback(OnInputBoxImagePathsChanged)));

            //注册IsShowSubmitButtonAnimationProperty
            IsShowSubmitButtonAnimationProperty = DependencyProperty.Register(
                "IsShowSubmitButtonAnimation", typeof(bool), typeof(BugUiControl),
                new FrameworkPropertyMetadata((bool)false, new PropertyChangedCallback(OnIsShowSubmitButtonAnimationChanged)));

            //注册IsShowBugReplyProperty
            IsShowBugReplyProperty = DependencyProperty.Register(
                "IsShowBugReply", typeof(bool), typeof(BugUiControl),
                new FrameworkPropertyMetadata((bool)false, new PropertyChangedCallback(OnIsShowBugReplyChanged)));



            //注册UpdateNumberFrontTextProperty
            UpdateNumberFrontTextProperty = DependencyProperty.Register(
                "UpdateNumberFrontText", typeof(string), typeof(BugUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnUpdateNumberFrontTextChanged)));

            //注册UpdateNumberBehindTextProperty
            UpdateNumberBehindTextProperty = DependencyProperty.Register(
                "UpdateNumberBehindText", typeof(string), typeof(BugUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnUpdateNumberBehindTextChanged)));







            /*注册路由事件*/
            //注册ClickBackButtonEvent
            ClickBackButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickBackButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(BugUiControl) //这个路由事件属于哪个控件？
            );

            //注册ClickProgressButtonEvent
            ClickProgressButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickProgressButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugUiControl));

            //注册ClickPriorityButtonEvent
            ClickPriorityButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickPriorityButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugUiControl));

            //注册ClickBugNameButtonEvent
            ClickBugNameButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickBugNameButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugUiControl));



            //注册ClickSubmitButtonEvent
            ClickSubmitButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickSubmitButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugUiControl));

            //注册ClickChooseImageButtonEvent
            ClickChooseImageButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickChooseImageButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugUiControl));

            //注册ClickInputBoxImageButtonEvent
            ClickInputBoxImageButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickInputBoxImageButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<string>), typeof(BugUiControl));

            //注册ClickInputBoxDeleteImageButtonEvent
            ClickInputBoxDeleteImageButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickInputBoxDeleteImageButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<string>), typeof(BugUiControl));



            //注册ClickRecordListItemDeleteButtonEvent
            ClickRecordListItemDeleteButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickRecordListItemDeleteButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<RecordData>), typeof(BugUiControl));

            //注册ClickRecordListItemImageButtonEvent
            ClickRecordListItemImageButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickRecordListItemImageButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<string>), typeof(BugUiControl));




            //注册IsShowBugReplyChangeEvent
            IsShowBugReplyChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "IsShowBugReplyChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugUiControl));

            //注册IsShowSubmitButtonAnimationChangeEvent
            IsShowSubmitButtonAnimationChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "IsShowSubmitButtonAnimationChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugUiControl));

        }

        #endregion




        #region [事件]
        //当点击[返回]按钮时
        private void BackButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickBackButton();
        }

        //当点击[进度]按钮时
        private void ProgressButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickProgressButton();
        }

        //当点击[优先级]按钮时
        private void PriorityButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickPriorityButton();
        }

        //当点击[Bug名字]按钮时
        private void BugNameButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickBugNameButton();
        }
        #endregion

        #region [事件 - 输入框]
        //当点击[提交]按钮时
        private void SubmitButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickSubmitButton();
        }

        //当点击[选择图片]按钮时
        private void ChooseImageButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickChooseImageButton();
        }

        //当点击[输入框]中的某一个[图片]按钮时
        private void InputBoxControl_ClickImageButton(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            //触发事件
            this.OnClickInputBoxImageButton(e.NewValue);
        }

        //当点击[输入框]中的某一个[删除图片]按钮时
        private void InputBoxControl_ClickDeleteImageButton(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            //触发事件
            this.OnClickInputBoxDeleteImageButton(e.NewValue);
        }
        #endregion

        #region [事件 - 记录Item]
        //当点击某一个[记录Item]中的[删除]按钮时
        private void RecordListItemControl_ClickDeleteButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //取到控件
            RecordListItemControl _recordListItemControl = sender as RecordListItemControl;

            //取到控件的数据源
            RecordItemData _data = (RecordItemData)_recordListItemControl.Tag;

            //触发事件
            this.OnClickRecordListItemDeleteButton(_data.Data);
        }

        //当点击某一个[记录Item]中的 某一个[图片]按钮时
        private void RecordListItemControl_ClickImageButton(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            //取到控件
            RecordListItemControl _recordListItemControl = sender as RecordListItemControl;

            //触发事件
            this.OnClickRecordListItemImageButton(e.NewValue);
        }

        #endregion

        #region [事件 - 值改变]
        /// <summary>
        /// 当[是否显示Bug回复]的值改变时
        /// </summary>
        private void IsShowBugReplyCheckControl_OnIsCheckedChange(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnIsShowBugReplyChange(e.OldValue, e.NewValue);//触发事件
        }

        #endregion

        #region [事件 - 鼠标进入/移出]
        /// <summary>
        /// 当鼠标进入[记录列表]时
        /// </summary>
        private void RightCanvas_OnMouseEnter(object sender, MouseEventArgs e)
        {
            //显示[是否显示Bug回复]控件（把Opacity属性设置为1）
            AnimationTool.PlayGridOpacityAnimation(this.IsShowBugReplyGrid, null, 1, 0.25f);

            //显示“选择图片”按钮
            RecordInputBoxControl.ShowChooseImageButton();
        }

        /// <summary>
        /// 当鼠标移出[记录列表]时
        /// </summary>
        private void RightCanvas_OnMouseLeave(object sender, MouseEventArgs e)
        {
            //隐藏[是否显示Bug回复]控件（把Opacity属性设置为0）
            AnimationTool.PlayGridOpacityAnimation(this.IsShowBugReplyGrid, null, 0, 0.25f);

            //隐藏“选择图片”按钮
            RecordInputBoxControl.HideChooseImageButton();
        }
        #endregion

        #region [事件 - 打开和关闭]
        /// <summary>
        /// 当[界面]打开或者关闭时
        /// </summary>
        private void BugUiControl_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //把[记录ListBox]的滚动条设置到最下面
            RecordListBoxScrollToEnd();
        }

        #endregion



        #region [公开方法]
        /// <summary>
        /// 把[记录ListBox]的滚动条设置到最下面
        /// </summary>
        public void RecordListBoxScrollToEnd()
        {
            //取到最后一个Item的索引
            int _itemIndex = RecordListBox.Items.Count - 1;

            //把ListBox的滚动条设置到最下面
            if (_itemIndex > 0)
            {
                RecordListBox.ScrollIntoView(RecordListBox.Items[_itemIndex]);
            }
        }
        #endregion


    }
}
