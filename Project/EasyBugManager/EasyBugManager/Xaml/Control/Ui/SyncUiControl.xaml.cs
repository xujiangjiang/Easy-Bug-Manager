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
    /// SyncUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class SyncUiControl : UserControl
    {

        /* 属性: IsShowYesButton(是否显示[确认按钮]？)
                 IsShowNoButton(是否显示[取消按钮]？)
                 IsShowSyncLog(是否显示同步日志？)

                 WaitSyncAnimationStateType([等待同步]动画的状态)（如果为Start，会开启4秒的等待时间；如果为End，会立刻中止等待；如果为None，不会中止等待。）(只有当动画完成时才会设置为End)
                 SyncIconAnimationStateType([同步图标]动画的状态)（如果为Start，会开启3秒钟的旋转动画；如果为End，会立刻中止旋转动画；如果为None，不会中止旋转动画。）(只有当动画完成时才会设置为End)

                 SyncStateType(同步状态的类型)
                 SyncNumber(同步的次数)
                 LastSyncTime(最后一次同步的时间)
                 SyncedTime(同步完成的时间)
                 SyncLogText(同步的日志文字)（所有同步的日志）

                 WaitSyncTime([等待同步]的时间)(单位：秒)（4秒）(WaitSyncTime必须比SyncIconAnimationTime的时间更长，这是为了避免出现同步冲突)
                 SyncIconAnimationTime([同步的图标动画]的时间)(单位：秒)（3秒）
                 
                 

           事件: ClickYesButton(当点击[确认]按钮时)
                 ClickNoButton(当点击[取消]按钮时)
                 
                 ClickSyncButton(当点击[同步]按钮时)

                 SyncStateTypeChange(当[同步状态的类型]改变时)
                 SyncIconAnimationStateTypeChange(当[同步图标的动画状态]改变时)
                 WaitSyncAnimationStateTypeChange(当[等待同步的动画状态]改变时)
                 IsShowSyncLogChange(当[是否显示同步日志？]改变时)*/



        public SyncUiControl()
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

        #region 依赖项属性：IsShowSyncLog
        /// <summary>
        /// 依赖项属性：是否显示同步日志？
        /// </summary>
        public static DependencyProperty IsShowSyncLogProperty;

        /// <summary>
        /// 公开属性：是否显示同步日志？
        /// </summary>
        public bool IsShowSyncLog
        {
            get { return (bool)GetValue(IsShowSyncLogProperty); }
            set { SetValue(IsShowSyncLogProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsShowSyncLogProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsShowSyncLogChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            SyncUiControl _syncUiControl = sender as SyncUiControl;

            //触发事件
            _syncUiControl.OnIsShowSyncLogChange((bool)e.OldValue, (bool)e.NewValue);
        }
        #endregion

        #region 依赖项属性：WaitSyncAnimationStateType
        /// <summary>
        /// 依赖项属性：[等待同步]动画的状态（如果为Start，会开启4秒的等待时间；如果为End，会立刻中止等待；如果为None，不会中止等待。）(只有当动画完成时才会设置为End)
        /// </summary>
        public static DependencyProperty WaitSyncAnimationStateTypeProperty;

        /// <summary>
        /// 公开属性：[等待同步]动画的状态（如果为Start，会开启4秒的等待时间；如果为End，会立刻中止等待；如果为None，不会中止等待。）(只有当动画完成时才会设置为End)
        /// </summary>
        public AnimationStateType WaitSyncAnimationStateType
        {
            get { return (AnimationStateType)GetValue(WaitSyncAnimationStateTypeProperty); }
            set { SetValue(WaitSyncAnimationStateTypeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当WaitSyncAnimationStateTypeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnWaitSyncAnimationStateTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            SyncUiControl _syncUiControl = sender as SyncUiControl;


            //判断
            switch ((AnimationStateType)e.NewValue)
            {
                case AnimationStateType.None:
                    break;

                //如果是[开始]动画
                case AnimationStateType.Start:
                    _syncUiControl.PlayWaitSyncAnimation();//播放动画
                    break;

                //如果是[结束]动画
                case AnimationStateType.End:
                    _syncUiControl.StopWaitSyncAnimation();//停止动画
                    break;
            }


            //触发事件
            _syncUiControl.OnWaitSyncAnimationStateTypeChange((AnimationStateType)e.OldValue, (AnimationStateType)e.NewValue);
        }
        #endregion

        #region 依赖项属性：SyncIconAnimationStateType
        /// <summary>
        /// 依赖项属性：[同步图标]动画的状态（如果为Start，会开启3秒钟的旋转动画；如果为End，会立刻中止旋转动画；如果为None，不会中止旋转动画。）(只有当动画完成时才会设置为End)
        /// </summary>
        public static DependencyProperty SyncIconAnimationStateTypeProperty;

        /// <summary>
        /// 公开属性：[同步图标]动画的状态（如果为Start，会开启3秒钟的旋转动画；如果为End，会立刻中止旋转动画；如果为None，不会中止旋转动画。）(只有当动画完成时才会设置为End)
        /// </summary>
        public AnimationStateType SyncIconAnimationStateType
        {
            get { return (AnimationStateType)GetValue(SyncIconAnimationStateTypeProperty); }
            set { SetValue(SyncIconAnimationStateTypeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当SyncIconAnimationStateTypeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnSyncIconAnimationStateTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            SyncUiControl _syncUiControl = sender as SyncUiControl;


            //判断
            switch ((AnimationStateType)e.NewValue)
            {
                case AnimationStateType.None:
                    break;

                //如果是[开始]动画
                case AnimationStateType.Start:
                    _syncUiControl.PlaySyncIconAnimation();//播放动画
                    break;

                //如果是[结束]动画
                case AnimationStateType.End:
                    _syncUiControl.StopSyncIconAnimation();//停止动画
                    break;
            }

            //触发事件
            _syncUiControl.OnSyncIconAnimationStateTypeChange((AnimationStateType)e.OldValue, (AnimationStateType)e.NewValue);
        }
        #endregion


        #region 依赖项属性：SyncStateType
        /// <summary>
        /// 依赖项属性：同步状态的类型
        /// </summary>
        public static DependencyProperty SyncStateTypeProperty;

        /// <summary>
        /// 公开属性：同步状态的类型
        /// </summary>
        public SyncStateType SyncStateType
        {
            get { return (SyncStateType)GetValue(SyncStateTypeProperty); }
            set { SetValue(SyncStateTypeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当SyncStateTypeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnSyncStateTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            SyncUiControl _syncUiControl = sender as SyncUiControl;

            //判断值
            switch ((SyncStateType)e.NewValue)
            {
                //如果是[空]
                case SyncStateType.None:
                    //不做任何处理
                    break;

                //如果是[不同步]
                case SyncStateType.NoSync:
                    _syncUiControl.EnNoSyncBorder.Visibility = Visibility.Visible;
                    _syncUiControl.EnSyncingBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.EnSyncedBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.CnNoSyncBorder.Visibility = Visibility.Visible;
                    _syncUiControl.CnSyncingBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.CnSyncedBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.SyncIconBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.TimeTextBlock.Visibility = Visibility.Collapsed;
                    _syncUiControl.WaitSyncAnimationStateType = AnimationStateType.None;//停止等待
                    _syncUiControl.SyncIconAnimationStateType = AnimationStateType.None;//停止动画
                    break;

                //如果是[准备同步]
                case SyncStateType.WaitSync:
                    _syncUiControl.WaitSyncAnimationStateType = AnimationStateType.Start;//准备同步
                    break;

                //如果是[同步中]
                case SyncStateType.Syncing:
                    _syncUiControl.EnNoSyncBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.EnSyncingBorder.Visibility = Visibility.Visible;
                    _syncUiControl.EnSyncedBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.CnNoSyncBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.CnSyncingBorder.Visibility = Visibility.Visible;
                    _syncUiControl.CnSyncedBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.SyncIconBorder.Visibility = Visibility.Visible;
                    _syncUiControl.TimeTextBlock.Visibility = Visibility.Collapsed;
                    _syncUiControl.SyncIconAnimationStateType = AnimationStateType.Start;//播放动画
                    break;

                //如果是[同步完成]
                case SyncStateType.Synced:
                    _syncUiControl.EnNoSyncBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.EnSyncingBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.EnSyncedBorder.Visibility = Visibility.Visible;
                    _syncUiControl.CnNoSyncBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.CnSyncingBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.CnSyncedBorder.Visibility = Visibility.Visible;
                    _syncUiControl.SyncIconBorder.Visibility = Visibility.Collapsed;
                    _syncUiControl.TimeTextBlock.Visibility = Visibility.Visible;
                    _syncUiControl.WaitSyncAnimationStateType = AnimationStateType.None;//停止等待
                    _syncUiControl.SyncIconAnimationStateType = AnimationStateType.None;//停止动画
                    break;
            }

            //触发事件
            _syncUiControl.OnSyncStateTypeChange((SyncStateType)e.OldValue, (SyncStateType)e.NewValue);
        }
        #endregion

        #region 依赖项属性：SyncNumber
        /// <summary>
        /// 依赖项属性：同步的次数
        /// </summary>
        public static DependencyProperty SyncNumberProperty;

        /// <summary>
        /// 公开属性：同步的次数
        /// </summary>
        public int SyncNumber
        {
            get { return (int)GetValue(SyncNumberProperty); }
            set { SetValue(SyncNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当SyncNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnSyncNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：LastSyncTime
        /// <summary>
        /// 依赖项属性：最后一次同步的时间
        /// </summary>
        public static DependencyProperty LastSyncTimeProperty;

        /// <summary>
        /// 公开属性：最后一次同步的时间
        /// </summary>
        public string LastSyncTime
        {
            get { return (string)GetValue(LastSyncTimeProperty); }
            set { SetValue(LastSyncTimeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当LastSyncTimeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnLastSyncTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：SyncedTime
        /// <summary>
        /// 依赖项属性：同步完成的时间
        /// </summary>
        public static DependencyProperty SyncedTimeProperty;

        /// <summary>
        /// 公开属性：同步完成的时间
        /// </summary>
        public string SyncedTime
        {
            get { return (string)GetValue(SyncedTimeProperty); }
            set { SetValue(SyncedTimeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当SyncedTimeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnSyncedTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：SyncLogText
        /// <summary>
        /// 依赖项属性：同步的日志文字（所有同步的日志）
        /// </summary>
        public static DependencyProperty SyncLogTextProperty;

        /// <summary>
        /// 公开属性：同步的日志文字（所有同步的日志）
        /// </summary>
        public string SyncLogText
        {
            get { return (string)GetValue(SyncLogTextProperty); }
            set { SetValue(SyncLogTextProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当SyncLogTextProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnSyncLogTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion


        #region 依赖项属性：WaitSyncTime
        /// <summary>
        /// 依赖项属性：[等待同步]的时间(单位：秒)（4秒）(WaitSyncTime必须比SyncIconAnimationTime的时间更长，这是为了避免出现同步冲突)
        /// </summary>
        public static DependencyProperty WaitSyncTimeProperty;

        /// <summary>
        /// 公开属性：[等待同步]的时间(单位：秒)（4秒）(WaitSyncTime必须比SyncIconAnimationTime的时间更长，这是为了避免出现同步冲突)
        /// </summary>
        public int WaitSyncTime
        {
            get { return (int)GetValue(WaitSyncTimeProperty); }
            set { SetValue(WaitSyncTimeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当WaitSyncTimeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnWaitSyncTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：SyncIconAnimationTime
        /// <summary>
        /// 依赖项属性：[同步的图标动画]的时间(单位：秒)（3秒）
        /// </summary>
        public static DependencyProperty SyncIconAnimationTimeProperty;

        /// <summary>
        /// 公开属性：[同步的图标动画]的时间(单位：秒)（3秒）
        /// </summary>
        public int SyncIconAnimationTime
        {
            get { return (int)GetValue(SyncIconAnimationTimeProperty); }
            set { SetValue(SyncIconAnimationTimeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当SyncIconAnimationTimeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnSyncIconAnimationTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
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
            args.RoutedEvent = SyncUiControl.ClickYesButtonEvent;

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
            args.RoutedEvent = SyncUiControl.ClickNoButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickSyncButton
        /// <summary>
        /// 路由事件：ClickSyncButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickSyncButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickSyncButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickSyncButton
        {
            //添加一条事件
            add { AddHandler(ClickSyncButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickSyncButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickSyncButton 路由事件
        /// </summary>
        private void OnClickSyncButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = SyncUiControl.ClickSyncButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion


        #region 路由事件：SyncStateTypeChange
        /// <summary>
        /// 路由事件：SyncStateTypeChangeEvent
        /// （当值改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent SyncStateTypeChangeEvent;


        /// <summary>
        /// 路由事件的属性：SyncStateTypeChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<SyncStateType> SyncStateTypeChange
        {
            //添加一条事件
            add { AddHandler(SyncStateTypeChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(SyncStateTypeChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 SyncStateTypeChange 路由事件
        /// </summary>
        private void OnSyncStateTypeChange(SyncStateType _oldValue, SyncStateType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<SyncStateType> args = new RoutedPropertyChangedEventArgs<SyncStateType>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = SyncUiControl.SyncStateTypeChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：SyncIconAnimationStateTypeChange
        /// <summary>
        /// 路由事件：SyncIconAnimationStateTypeChangeEvent
        /// （当值改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent SyncIconAnimationStateTypeChangeEvent;


        /// <summary>
        /// 路由事件的属性：SyncIconAnimationStateTypeChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<AnimationStateType> SyncIconAnimationStateTypeChange
        {
            //添加一条事件
            add { AddHandler(SyncIconAnimationStateTypeChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(SyncIconAnimationStateTypeChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 SyncIconAnimationStateTypeChange 路由事件
        /// </summary>
        private void OnSyncIconAnimationStateTypeChange(AnimationStateType _oldValue, AnimationStateType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<AnimationStateType> args = new RoutedPropertyChangedEventArgs<AnimationStateType>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = SyncUiControl.SyncIconAnimationStateTypeChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：WaitSyncAnimationStateTypeChange
        /// <summary>
        /// 路由事件：WaitSyncAnimationStateTypeChangeEvent
        /// （当值改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent WaitSyncAnimationStateTypeChangeEvent;


        /// <summary>
        /// 路由事件的属性：WaitSyncAnimationStateTypeChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<AnimationStateType> WaitSyncAnimationStateTypeChange
        {
            //添加一条事件
            add { AddHandler(WaitSyncAnimationStateTypeChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(WaitSyncAnimationStateTypeChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 WaitSyncAnimationStateTypeChange 路由事件
        /// </summary>
        private void OnWaitSyncAnimationStateTypeChange(AnimationStateType _oldValue, AnimationStateType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<AnimationStateType> args = new RoutedPropertyChangedEventArgs<AnimationStateType>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = SyncUiControl.WaitSyncAnimationStateTypeChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：IsShowSyncLogChange
        /// <summary>
        /// 路由事件：IsShowSyncLogChangeEvent
        /// （当值改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent IsShowSyncLogChangeEvent;


        /// <summary>
        /// 路由事件的属性：IsShowSyncLogChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> IsShowSyncLogChange
        {
            //添加一条事件
            add { AddHandler(IsShowSyncLogChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(IsShowSyncLogChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 IsShowSyncLogChange 路由事件
        /// </summary>
        private void OnIsShowSyncLogChange(bool _oldValue, bool _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = SyncUiControl.IsShowSyncLogChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion




        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static SyncUiControl()
        {
            /*注册依赖项属性*/
            //注册IsShowSyncLogProperty
            IsShowSyncLogProperty = DependencyProperty.Register(
                "IsShowSyncLog", //属性的名字
                typeof(bool), //属性的类型
                typeof(SyncUiControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (bool)false,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnIsShowSyncLogChanged))
            );

            //注册SyncStateTypeProperty
            SyncStateTypeProperty = DependencyProperty.Register(
                "SyncStateType", typeof(SyncStateType), typeof(SyncUiControl),
                new FrameworkPropertyMetadata((SyncStateType)SyncStateType.NoSync, new PropertyChangedCallback(OnSyncStateTypeChanged)));

            //注册IsShowYesButtonProperty
            IsShowYesButtonProperty = DependencyProperty.Register(
                "IsShowYesButton", typeof(bool), typeof(SyncUiControl),
                new FrameworkPropertyMetadata((bool)true, new PropertyChangedCallback(OnIsShowYesButtonChanged)));

            //注册IsShowNoButtonProperty
            IsShowNoButtonProperty = DependencyProperty.Register(
                "IsShowNoButton", typeof(bool), typeof(SyncUiControl),
                new FrameworkPropertyMetadata((bool)true, new PropertyChangedCallback(OnIsShowNoButtonChanged)));

            //注册SyncIconAnimationStateTypeProperty
            SyncIconAnimationStateTypeProperty = DependencyProperty.Register(
                "SyncIconAnimationStateType", typeof(AnimationStateType), typeof(SyncUiControl),
                new FrameworkPropertyMetadata((AnimationStateType)AnimationStateType.None, new PropertyChangedCallback(OnSyncIconAnimationStateTypeChanged)));

            //注册WaitSyncAnimationStateTypeProperty
            WaitSyncAnimationStateTypeProperty = DependencyProperty.Register(
                "WaitSyncAnimationStateType", typeof(AnimationStateType), typeof(SyncUiControl),
                new FrameworkPropertyMetadata((AnimationStateType)AnimationStateType.None, new PropertyChangedCallback(OnWaitSyncAnimationStateTypeChanged)));


            //注册SyncNumberProperty
            SyncNumberProperty = DependencyProperty.Register(
                "SyncNumber", typeof(int), typeof(SyncUiControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnSyncNumberChanged)));

            //注册LastSyncTimeProperty
            LastSyncTimeProperty = DependencyProperty.Register(
                "LastSyncTime", typeof(string), typeof(SyncUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnLastSyncTimeChanged)));

            //注册SyncedTimeProperty
            SyncedTimeProperty = DependencyProperty.Register(
                "SyncedTime", typeof(string), typeof(SyncUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnSyncedTimeChanged)));

            //注册SyncLogTextProperty
            SyncLogTextProperty = DependencyProperty.Register(
                "SyncLogText", typeof(string), typeof(SyncUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnSyncLogTextChanged)));


            //注册WaitSyncTimeProperty
            WaitSyncTimeProperty = DependencyProperty.Register(
                "WaitSyncTime", typeof(int), typeof(SyncUiControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnWaitSyncTimeChanged)));

            //注册SyncIconAnimationTimeProperty
            SyncIconAnimationTimeProperty = DependencyProperty.Register(
                "SyncIconAnimationTime", typeof(int), typeof(SyncUiControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnSyncIconAnimationTimeChanged)));





            /*注册路由事件*/
            //注册ClickYesButtonEvent
            ClickYesButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickYesButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(SyncUiControl) //这个路由事件属于哪个控件？
            );

            //注册ClickNoButtonEvent
            ClickNoButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickNoButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SyncUiControl));

            //注册ClickSyncButtonEvent
            ClickSyncButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickSyncButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SyncUiControl));

            //注册SyncStateTypeChangeEvent
            SyncStateTypeChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "SyncStateTypeChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<SyncStateType>), typeof(SyncUiControl));

            //注册SyncIconAnimationStateTypeChangeEvent
            SyncIconAnimationStateTypeChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "SyncIconAnimationStateTypeChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<AnimationStateType>), typeof(SyncUiControl));

            //注册WaitSyncAnimationStateTypeChangeEvent
            WaitSyncAnimationStateTypeChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "WaitSyncAnimationStateTypeChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<AnimationStateType>), typeof(SyncUiControl));

            //注册IsShowSyncLogChangeEvent
            IsShowSyncLogChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "IsShowSyncLogChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SyncUiControl));

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

        //当点击[同步]按钮时
        private void SyncButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickSyncButton();//触发事件
        }
        #endregion




        #region [动画 - 同步图标]
        //[同步图标]的动画
        private DoubleAnimation syncIconAnimation = new DoubleAnimation();

        /// <summary>
        /// 播放[同步图标]的动画
        /// </summary>
        private void PlaySyncIconAnimation()
        {
            //先停止动画
            StopSyncIconAnimation();


            /* 动画 */
            syncIconAnimation = new DoubleAnimation();
            syncIconAnimation.From = 0;
            syncIconAnimation.To = 360;
            syncIconAnimation.Duration = TimeSpan.FromSeconds(1f);//1秒
            syncIconAnimation.RepeatBehavior = new RepeatBehavior(SyncIconAnimationTime);//循环3次（3秒）
            syncIconAnimation.Completed += (sender, args) => { SyncIconAnimationStateType = AnimationStateType.End; };//动画完成后，关闭动画 


            //播放动画 (让按钮的尺寸(Scale) 变小/变大)
            SyncIconBorderRotateTransform.BeginAnimation(RotateTransform.AngleProperty, syncIconAnimation, HandoffBehavior.SnapshotAndReplace);
        }

        /// <summary>
        /// 停止[同步图标]的动画
        /// </summary>
        private void StopSyncIconAnimation()
        {
            //停止动画
            SyncIconBorderRotateTransform.BeginAnimation(RotateTransform.AngleProperty, null, HandoffBehavior.SnapshotAndReplace);
        }
        #endregion

        #region [动画 - 等待同步]
        //[等待同步]的动画
        private DoubleAnimation waitSyncAnimation = new DoubleAnimation();

        /// <summary>
        /// 播放[等待同步]的动画
        /// </summary>
        private void PlayWaitSyncAnimation()
        {
            //先停止动画
            StopWaitSyncAnimation();


            /* 动画 */
            waitSyncAnimation = new DoubleAnimation();
            waitSyncAnimation.From = 0;
            waitSyncAnimation.To = 0;
            waitSyncAnimation.Duration = TimeSpan.FromSeconds(WaitSyncTime);//4秒
            waitSyncAnimation.Completed += (sender, args) =>
            {
                //动画完成后
                WaitSyncAnimationStateType = AnimationStateType.End;//关闭[WaitSyncAnimation]动画 
            };


            //播放动画 (让Border的透明度 变小/变大)
            WaitSyncBorder.BeginAnimation(Border.OpacityProperty, waitSyncAnimation, HandoffBehavior.SnapshotAndReplace);
        }

        /// <summary>
        /// 停止[等待同步]的动画
        /// </summary>
        private void StopWaitSyncAnimation()
        {
            //停止动画
            WaitSyncBorder.BeginAnimation(Border.OpacityProperty, null, HandoffBehavior.SnapshotAndReplace);
        }
        #endregion

    }
}
