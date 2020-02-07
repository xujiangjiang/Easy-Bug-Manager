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
    /// BugListItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class BugListItemControl : UserControl
    {
        /* 属性: IsChecked(是否选中？)
                 Progress(进度)
                 Priority(优先级)
                 Title(标题)
                 CreateTime(创建时间)
                 UpdateTime(更新时间) 
                 GoToPageNumber(跳转的页数)(把Bug跳转到哪一页？如果为-1，就不显示跳转提示；如果不为-1，就显示跳转提示)
                 IsDelete(是否删除)


           事件: ClickMoreButton(当点击"更多"按钮的时候)
                 ClickProgressButton(当点击"进度"按钮的时候)
                 Checked(当选中的时候)
                 MouseDoubleClickCheck(当鼠标双击[Check]的时候)
                 ClickRefreshButton(当点击"刷新"按钮的时候)
                 ClickGoToPageButton(当点击"跳转页面"按钮的时候)
                 ClickDeletedButton(当点击"已删除"按钮的时候)
                 


           右键菜单的事件：ClickDeleteButtonInContextMenu(当点击右键菜单中的[删除]按钮)
                          ClickMoreButtonInContextMenu(当点击右键菜单中的[更多]按钮)
                          ClickProgressTypeButtonInContextMenu(当点击右键菜单中的[进度Type]的 某一个Check时)(参数ProgressType：当前的进度类型)
                          ClickPriorityTypeButtonInContextMenu(当点击右键菜单中的[优先级Type]的 某一个Check时)(参数PriorityType：当前的优先级类型)*/


        /* 数据绑定：BugItemData类（在此控件的Tag属性中） */




        public BugListItemControl()
        {
            InitializeComponent();
        }





        #region 依赖项属性：IsChecked
        /// <summary>
        /// 依赖项属性：是否选中？
        /// </summary>
        public static DependencyProperty IsCheckedProperty;

        /// <summary>
        /// 公开属性：是否选中？
        /// </summary>
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsCheckedProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsCheckedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：Progress
        /// <summary>
        /// 依赖项属性：进度
        /// </summary>
        public static DependencyProperty ProgressProperty;

        /// <summary>
        /// 公开属性：进度
        /// </summary>
        public ProgressType Progress
        {
            get { return (ProgressType)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当PaddingProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnProgressChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //取到控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //判断是什么类型
            switch ((ProgressType)e.NewValue)
            {
                case ProgressType.Undone:
                    //更改图片
                    _bugListItemControl.UndoneProgressBorder.Visibility = Visibility.Visible;
                    _bugListItemControl.SolvedProgressBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.DeprecatProgressBorder.Visibility = Visibility.Collapsed;

                    //修改透明度
                    _bugListItemControl.CreateTimeTextBlock.Opacity = 1;
                    _bugListItemControl.UpdateTimeTextBlock.Opacity = 1;
                    _bugListItemControl.TitleTextBlock.Opacity = 1;
                    _bugListItemControl.PriorityGrid.Opacity = 1;

                    //修改进度按钮
                    _bugListItemControl.RedProgressButton.Visibility = Visibility.Collapsed;
                    _bugListItemControl.GreyProgressButton.Visibility = Visibility.Visible;
                    break;

                case ProgressType.Solved:
                    //更改图片
                    _bugListItemControl.UndoneProgressBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.SolvedProgressBorder.Visibility = Visibility.Visible;
                    _bugListItemControl.DeprecatProgressBorder.Visibility = Visibility.Collapsed;

                    //修改透明度
                    _bugListItemControl.CreateTimeTextBlock.Opacity = 0.5f;
                    _bugListItemControl.UpdateTimeTextBlock.Opacity = 0.5f;
                    _bugListItemControl.TitleTextBlock.Opacity = 0.5f;
                    _bugListItemControl.PriorityGrid.Opacity = 0.5f;

                    //修改进度按钮
                    _bugListItemControl.RedProgressButton.Visibility = Visibility.Visible;
                    _bugListItemControl.GreyProgressButton.Visibility = Visibility.Collapsed;
                    break;

                case ProgressType.Deprecat:
                    //更改图片
                    _bugListItemControl.UndoneProgressBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.SolvedProgressBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.DeprecatProgressBorder.Visibility = Visibility.Visible;

                    //修改透明度
                    _bugListItemControl.CreateTimeTextBlock.Opacity = 0.5f;
                    _bugListItemControl.UpdateTimeTextBlock.Opacity = 0.5f;
                    _bugListItemControl.TitleTextBlock.Opacity = 0.5f;
                    _bugListItemControl.PriorityGrid.Opacity = 0.5f;

                    //修改进度按钮
                    _bugListItemControl.RedProgressButton.Visibility = Visibility.Collapsed;
                    _bugListItemControl.GreyProgressButton.Visibility = Visibility.Visible;
                    break;

                case ProgressType.None:
                    //更改图片
                    _bugListItemControl.UndoneProgressBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.SolvedProgressBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.DeprecatProgressBorder.Visibility = Visibility.Collapsed;

                    //修改透明度
                    _bugListItemControl.CreateTimeTextBlock.Opacity = 1;
                    _bugListItemControl.UpdateTimeTextBlock.Opacity = 1;
                    _bugListItemControl.TitleTextBlock.Opacity = 1;
                    _bugListItemControl.PriorityGrid.Opacity = 1;

                    //修改进度按钮
                    _bugListItemControl.RedProgressButton.Visibility = Visibility.Collapsed;
                    _bugListItemControl.GreyProgressButton.Visibility = Visibility.Visible;
                    break;
            }

            //修改右键菜单的ProgressType
            _bugListItemControl.ContextMenuControl.Progress = (ProgressType)e.NewValue;
        }
        #endregion

        #region 依赖项属性：Priority
        /// <summary>
        /// 依赖项属性：优先级
        /// </summary>
        public static DependencyProperty PriorityProperty;

        /// <summary>
        /// 公开属性：优先级
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
            //取到控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //判断是什么类型
            switch ((PriorityType)e.NewValue)
            {
                case PriorityType.Low:
                    _bugListItemControl.LowPriorityBorder.Visibility = Visibility.Visible;
                    _bugListItemControl.MidPriorityBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.HighPriorityBorder.Visibility = Visibility.Collapsed;
                    break;

                case PriorityType.Mid:
                    _bugListItemControl.LowPriorityBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.MidPriorityBorder.Visibility = Visibility.Visible;
                    _bugListItemControl.HighPriorityBorder.Visibility = Visibility.Collapsed;
                    break;

                case PriorityType.High:
                    _bugListItemControl.LowPriorityBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.MidPriorityBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.HighPriorityBorder.Visibility = Visibility.Visible;
                    break;

                case PriorityType.None:
                    _bugListItemControl.LowPriorityBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.MidPriorityBorder.Visibility = Visibility.Collapsed;
                    _bugListItemControl.HighPriorityBorder.Visibility = Visibility.Collapsed;
                    break;
            }

            //修改右键菜单的PriorityType
            _bugListItemControl.ContextMenuControl.Priority = (PriorityType)e.NewValue;
        }
        #endregion

        #region 依赖项属性：Title
        /// <summary>
        /// 依赖项属性：标题
        /// </summary>
        public static DependencyProperty TitleProperty;

        /// <summary>
        /// 公开属性：标题
        /// </summary>
        public HighlightText Title
        {
            get { return (HighlightText)GetValue(TitleProperty); }
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

        #region 依赖项属性：CreateTime
        /// <summary>
        /// 依赖项属性：创建时间
        /// </summary>
        public static DependencyProperty CreateTimeProperty;

        /// <summary>
        /// 公开属性：创建时间
        /// </summary>
        public string CreateTime
        {
            get { return (string)GetValue(CreateTimeProperty); }
            set { SetValue(CreateTimeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当CreateTimeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnCreateTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：UpdateTime
        /// <summary>
        /// 依赖项属性：更新时间
        /// </summary>
        public static DependencyProperty UpdateTimeProperty;

        /// <summary>
        /// 公开属性：更新时间
        /// </summary>
        public string UpdateTime
        {
            get { return (string)GetValue(UpdateTimeProperty); }
            set { SetValue(UpdateTimeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当UpdateTimeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnUpdateTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：GoToPageNumber
        /// <summary>
        /// 依赖项属性：跳转的页数(把Bug跳转到哪一页？如果为-1，就不显示跳转提示；如果不为-1，就显示跳转提示)
        /// </summary>
        public static DependencyProperty GoToPageNumberProperty;

        /// <summary>
        /// 公开属性：跳转的页数(把Bug跳转到哪一页？如果为-1，就不显示跳转提示；如果不为-1，就显示跳转提示)
        /// </summary>
        public int GoToPageNumber
        {
            get { return (int)GetValue(GoToPageNumberProperty); }
            set { SetValue(GoToPageNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当GoToPageNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnGoToPageNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //判断：如果为-1，或者小于等于0，就代表不跳转（不显示跳转提示）
            if ((int)e.NewValue <= 0) 
            {
                //关闭提示面板
                _bugListItemControl.TipStackPanel.Visibility = Visibility.Collapsed;

                //修改按钮 长度
                _bugListItemControl.BaseCheckControl.SetValue(Grid.ColumnSpanProperty, 2);

                //显示[时间]和[更多按钮]
                _bugListItemControl.CreateTimeTextBlock.Visibility = Visibility.Visible;
                _bugListItemControl.UpdateTimeTextBlock.Visibility = Visibility.Visible;
            }

            //判断：如果大于等于1，就代表要显示跳转提示
            else
            {
                //修改按钮 长度
                _bugListItemControl.BaseCheckControl.SetValue(Grid.ColumnSpanProperty, 1);

                //显示提示面板
                _bugListItemControl.TipStackPanel.Visibility = Visibility.Visible;

                //关闭[时间]和[更多按钮]
                _bugListItemControl.CreateTimeTextBlock.Visibility = Visibility.Hidden;
                _bugListItemControl.UpdateTimeTextBlock.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region 依赖项属性：IsDelete
        /// <summary>
        /// 依赖项属性：是否删除
        /// </summary>
        public static DependencyProperty IsDeleteProperty;

        /// <summary>
        /// 公开属性：是否删除
        /// </summary>
        public bool IsDelete
        {
            get { return (bool)GetValue(IsDeleteProperty); }
            set { SetValue(IsDeleteProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IsDeleteProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnIsDeleteChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //判断值
            switch ((bool)e.NewValue)
            {
                //如果[已删除]
                case true:
                    _bugListItemControl.IsChecked = false;
                    _bugListItemControl.DeletedGrid.Visibility = Visibility.Visible;
                    break;

                //如果[未删除]
                case false:
                    _bugListItemControl.DeletedGrid.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        #endregion





        #region 路由事件：ClickMoreButton

        /// <summary>
        /// 路由事件：ClickMoreButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickMoreButtonEvent;


        /// <summary>
        /// 路由事件的属性：Click
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
            args.RoutedEvent = BugListItemControl.ClickMoreButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickProgressButton
        /// <summary>
        /// 路由事件：ClickProgressButtonEvent
        /// （当点击按钮时，触发此事件）
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
            args.RoutedEvent = BugListItemControl.ClickProgressButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickRefreshButton

        /// <summary>
        /// 路由事件：ClickRefreshButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickRefreshButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickRefreshButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickRefreshButton
        {
            //添加一条事件
            add { AddHandler(ClickRefreshButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickRefreshButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickRefreshButton 路由事件
        /// </summary>
        private void OnClickRefreshButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemControl.ClickRefreshButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickGoToPageButton

        /// <summary>
        /// 路由事件：ClickGoToPageButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickGoToPageButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickGoToPageButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickGoToPageButton
        {
            //添加一条事件
            add { AddHandler(ClickGoToPageButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickGoToPageButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickGoToPageButton 路由事件
        /// </summary>
        private void OnClickGoToPageButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemControl.ClickGoToPageButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickDeletedButton

        /// <summary>
        /// 路由事件：ClickDeletedButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickDeletedButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickDeletedButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickDeletedButton
        {
            //添加一条事件
            add { AddHandler(ClickDeletedButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickDeletedButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickDeletedButton 路由事件
        /// </summary>
        private void OnClickDeletedButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemControl.ClickDeletedButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：Checked

        /// <summary>
        /// 路由事件：CheckedEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent CheckedEvent;


        /// <summary>
        /// 路由事件的属性：Checked
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> Checked
        {
            //添加一条事件
            add { AddHandler(CheckedEvent, value); }

            //移除一条事件
            remove { RemoveHandler(CheckedEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 Checked 路由事件
        /// </summary>
        private void OnChecked()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemControl.CheckedEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：MouseDoubleClickCheck

        /// <summary>
        /// 路由事件：MouseDoubleClickCheckEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent MouseDoubleClickCheckEvent;


        /// <summary>
        /// 路由事件的属性：MouseDoubleClickCheck
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> MouseDoubleClickCheck
        {
            //添加一条事件
            add { AddHandler(MouseDoubleClickCheckEvent, value); }

            //移除一条事件
            remove { RemoveHandler(MouseDoubleClickCheckEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 MouseDoubleClickCheck 路由事件
        /// </summary>
        private void OnMouseDoubleClickCheck()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemControl.MouseDoubleClickCheckEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion



        #region 路由事件：ClickMoreButtonInContextMenu

        /// <summary>
        /// 路由事件：ClickMoreButtonInContextMenuEvent
        /// （当点击右键菜单的[更多]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickMoreButtonInContextMenuEvent;


        /// <summary>
        /// 路由事件的属性：ClickMoreButtonInContextMenu
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickMoreButtonInContextMenu
        {
            //添加一条事件
            add { AddHandler(ClickMoreButtonInContextMenuEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickMoreButtonInContextMenuEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickMoreButtonInContextMenu 路由事件
        /// </summary>
        private void OnClickMoreButtonInContextMenu()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemControl.ClickMoreButtonInContextMenuEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickDeleteButtonInContextMenu

        /// <summary>
        /// 路由事件：ClickDeleteButtonInContextMenuEvent
        /// （当点击右键菜单的[删除]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickDeleteButtonInContextMenuEvent;


        /// <summary>
        /// 路由事件的属性：ClickDeleteButtonInContextMenu
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickDeleteButtonInContextMenu
        {
            //添加一条事件
            add { AddHandler(ClickDeleteButtonInContextMenuEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickDeleteButtonInContextMenuEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickDeleteButtonInContextMenu 路由事件
        /// </summary>
        private void OnClickDeleteButtonInContextMenu()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemControl.ClickDeleteButtonInContextMenuEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickProgressTypeButtonInContextMenu

        /// <summary>
        /// 路由事件：ClickProgressTypeButtonInContextMenuEvent
        /// （当点击右键菜单中的 [ProgressCheckGroup]中的按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickProgressTypeButtonInContextMenuEvent;


        /// <summary>
        /// 路由事件的属性：ClickProgressTypeButtonInContextMenu
        /// </summary>
        public event RoutedPropertyChangedEventHandler<ProgressType> ClickProgressTypeButtonInContextMenu
        {
            //添加一条事件
            add { AddHandler(ClickProgressTypeButtonInContextMenuEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickProgressTypeButtonInContextMenuEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickProgressTypeButton 路由事件
        /// </summary>
        /// <param name="_oldValue">之前被选中的Check的索引</param>
        /// <param name="_newValue">新的被选中的Check的索引</param>
        private void OnClickProgressTypeButtonInContextMenu(ProgressType _oldValue, ProgressType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<ProgressType> args = new RoutedPropertyChangedEventArgs<ProgressType>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemControl.ClickProgressTypeButtonInContextMenuEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickPriorityTypeButtonInContextMenu

        /// <summary>
        /// 路由事件：ClickPriorityTypeButtonInContextMenuEvent
        /// （当点击右键菜单中的 [PriorityCheckGroup]中的按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickPriorityTypeButtonInContextMenuEvent;


        /// <summary>
        /// 路由事件的属性：ClickProgressTypeButtonInContextMenu
        /// </summary>
        public event RoutedPropertyChangedEventHandler<PriorityType> ClickPriorityTypeButtonInContextMenu
        {
            //添加一条事件
            add { AddHandler(ClickPriorityTypeButtonInContextMenuEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickPriorityTypeButtonInContextMenuEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickPriorityTypeButtonInContextMenu 路由事件
        /// </summary>
        /// <param name="_oldValue">之前被选中的Check的索引</param>
        /// <param name="_newValue">新的被选中的Check的索引</param>
        private void OnClickPriorityTypeButtonInContextMenu(PriorityType _oldValue, PriorityType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<PriorityType> args = new RoutedPropertyChangedEventArgs<PriorityType>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = BugListItemControl.ClickPriorityTypeButtonInContextMenuEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion






        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static BugListItemControl()
        {
            /*注册依赖项属性*/
            //注册ProgressProperty
            ProgressProperty = DependencyProperty.Register(
                "Progress", //属性的名字
                typeof(ProgressType), //属性的类型
                typeof(BugListItemControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (ProgressType)ProgressType.None,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnProgressChanged))
            );

            //注册PriorityProperty
            PriorityProperty = DependencyProperty.Register(
                "Priority", typeof(PriorityType), typeof(BugListItemControl),
                new FrameworkPropertyMetadata((PriorityType)PriorityType.None, new PropertyChangedCallback(OnPriorityChanged)));

            //注册TitleProperty
            TitleProperty = DependencyProperty.Register(
                "Title", typeof(HighlightText), typeof(BugListItemControl),
                new FrameworkPropertyMetadata((HighlightText)new HighlightText(), new PropertyChangedCallback(OnTitleChanged)));

            //注册CreateTimeProperty
            CreateTimeProperty = DependencyProperty.Register(
                "CreateTime", typeof(string), typeof(BugListItemControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnCreateTimeChanged)));

            //注册UpdateTimeProperty
            UpdateTimeProperty = DependencyProperty.Register(
                "UpdateTime", typeof(string), typeof(BugListItemControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnUpdateTimeChanged)));

            //注册GoToPageNumberProperty
            GoToPageNumberProperty = DependencyProperty.Register(
                "GoToPageNumber", typeof(int), typeof(BugListItemControl),
                new FrameworkPropertyMetadata((int)-2, new PropertyChangedCallback(OnGoToPageNumberChanged)));

            //注册IsCheckedProperty
            IsCheckedProperty = DependencyProperty.Register(
                "IsChecked", typeof(bool), typeof(BugListItemControl),
                new FrameworkPropertyMetadata((bool)false, new PropertyChangedCallback(OnIsCheckedChanged)));

            //注册IsDeleteProperty
            IsDeleteProperty = DependencyProperty.Register(
                "IsDelete", typeof(bool), typeof(BugListItemControl),
                new FrameworkPropertyMetadata((bool)false, new PropertyChangedCallback(OnIsDeleteChanged)));






            /*注册路由事件*/
            //注册ClickMoreButtonEvent
            ClickMoreButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickMoreButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(BugListItemControl) //这个路由事件属于哪个控件？
            );

            //注册ClickProgressButtonEvent
            ClickProgressButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickProgressButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugListItemControl));

            //注册ClickRefreshButtonEvent
            ClickRefreshButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickRefreshButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugListItemControl));

            //注册ClickGoToPageButtonEvent
            ClickGoToPageButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickGoToPageButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugListItemControl));

            //注册ClickDeletedButtonEvent
            ClickDeletedButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickDeletedButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugListItemControl));

            //注册CheckedEvent
            CheckedEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "Checked", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugListItemControl));

            //注册MouseDoubleClickCheckEvent
            MouseDoubleClickCheckEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "MouseDoubleClickCheck", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugListItemControl));





            //注册ClickDeleteButtonInContextMenuEvent
            ClickDeleteButtonInContextMenuEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickDeleteButtonInContextMenu", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(BugListItemControl) //这个路由事件属于哪个控件？
            );

            //注册ClickMoreButtonInContextMenuEvent
            ClickMoreButtonInContextMenuEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickMoreButtonInContextMenu", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(BugListItemControl));

            //注册ClickProgressTypeButtonInContextMenuEvent
            ClickProgressTypeButtonInContextMenuEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickProgressTypeButtonInContextMenu", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<ProgressType>), typeof(BugListItemControl));

            //注册ClickPriorityTypeButtonInContextMenuEvent
            ClickPriorityTypeButtonInContextMenuEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickPriorityTypeButtonInContextMenu", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<PriorityType>), typeof(BugListItemControl));

        }
        #endregion




        #region [事件]
        //如果被选中时
        private void BaseCheckControl_Checked(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnChecked();//触发Checked事件
        }

        //如果鼠标右键点击
        private void BaseCheckControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //显示右键菜单
            ContextMenuPopup.IsOpen = false;
            ContextMenuPopup.IsOpen = true;
        }

        //如果鼠标双击
        private void BaseCheckControl_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //获取Check的空姐
            ColorCheckControl _colorCheckControl = sender as ColorCheckControl;

            //让这个Check的按钮动画，恢复到正常的大小
            AnimationTool.PlayButtonAnimation(false, _colorCheckControl.PressAnimationSize, _colorCheckControl.BaseButtonScaleTransform);//获取"抬起动画"，并播放动画

            //触发事件
            this.OnMouseDoubleClickCheck();
        }






        //如果点击[更多]按钮
        private void MoreButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickMoreButton();//触发ClickMoreButton事件
        }

        //如果点击[进度]按钮
        private void ProgressButton_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickProgressButton();//触发ClickProgressButton事件
        }

        //当点击[刷新]按钮
        private void RefreshButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickRefreshButton();//触发事件
        }

        //当点击[跳转页面]按钮
        private void GoToPageButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickGoToPageButton();//触发事件
        }





        //当Item丢失焦点时
        private void BugListItemControl_OnLostFocus(object sender, RoutedEventArgs e)
        {
            ContextMenuPopup.IsOpen = false;
        }
        #endregion

        #region [事件 - 已删除]
        //当点击[已删除]按钮时
        private void DeletedButtonControl_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickDeletedButton();//触发事件
        }



        //当鼠标进入[已删除按钮]时
        private void DeletedButtonControl_OnMouseEnter(object sender, MouseEventArgs e)
        {
            //显示[点击刷新]
            this.DeletedBorder.Visibility = Visibility.Hidden;
            this.ClickRefreshBorder.Visibility = Visibility.Visible;
        }

        //当鼠标移出[已删除按钮]时
        private void DeletedButtonControl_OnMouseLeave(object sender, MouseEventArgs e)
        {
            //显示[已删除]
            this.DeletedBorder.Visibility = Visibility.Visible;
            this.ClickRefreshBorder.Visibility = Visibility.Hidden;
        }
        #endregion

        #region [事件 - 右键菜单]
        //当点击[右键菜单]中的[删除按钮]时
        private void ContextMenuControl_ClickDeleteButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //关闭右键菜单
            this.ContextMenuPopup.IsOpen = false;

            //触发事件
            this.OnClickDeleteButtonInContextMenu();
        }

        //当点击[右键菜单]中的[更多按钮]时
        private void ContextMenuControl_ClickMoreButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //关闭右键菜单
            this.ContextMenuPopup.IsOpen = false;

            //触发事件
            this.OnClickMoreButtonInContextMenu();
        }




        //当点击[右键菜单]中的[某一个进度Check]时
        private void ContextMenuControl_ClickProgressTypeButton(object sender, RoutedPropertyChangedEventArgs<ProgressType> e)
        {
            //关闭右键菜单
            this.ContextMenuPopup.IsOpen = false;

            //触发事件
            this.OnClickProgressTypeButtonInContextMenu((ProgressType)e.OldValue, (ProgressType)e.NewValue);
        }

        //当点击[右键菜单]中的[某一个优先级Check]时
        private void ContextMenuControl_ClickPriorityTypeButton(object sender, RoutedPropertyChangedEventArgs<PriorityType> e)
        {
            //关闭右键菜单
            this.ContextMenuPopup.IsOpen = false;

            //触发事件
            this.OnClickPriorityTypeButtonInContextMenu((PriorityType)e.OldValue, (PriorityType)e.NewValue);
        }




        //当[右键菜单]打开时
        private void ContextMenuPopup_Opened(object sender, EventArgs e)
        {
            //选中这个BugItem
            this.BaseCheckControl.IsChecked = true;

            //播放音效
            AppManager.Systems.AudioSystem.PlayAudio(AudioType.ButtonDown);
        }

        //当[右键菜单]关闭时
        private void ContextMenuPopup_Closed(object sender, EventArgs e)
        {
            //播放音效
            AppManager.Systems.AudioSystem.PlayAudio(AudioType.ButtonDown);
        }
        #endregion

        #region [事件 - 数据源]
        /// <summary>
        /// 当[数据源]改变时
        /// </summary>
        private void BugListItemControl_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((this.DataContext as BugItemData) ==null)
            {
                //关闭此控件
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                //显示此控件
                this.Visibility = Visibility.Visible;
            }

            //不选中此控件
            this.IsChecked = false;
        }


        #endregion



    }
}
