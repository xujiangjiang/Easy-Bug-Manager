using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// ListUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class ListUiControl : UserControl
    {
        /* 属性: BugItemDatas(Bug列表Item的数据 们)
                 BugItems*(Bug列表Item的控件 们)
         
                 AllBugTotalNumber（Bug的总数）
                 UndoneBugTotalNumber（未完成的Bug数量）
                 LowUndoneBugTotalNumber（低优先级并且未完成 的Bug数量）
                 MidUndoneBugTotalNumber（中优先级并且未完成 的Bug数量）
                 HighUndoneBugTotalNumber（高优先级并且未完成 的Bug数量）

                 TotalPageNumber*（总页数）
                 CurrentPageNumber*（当前的页码）

                 ShowBugNumber（显示的Bug个数）

                 ProjectNameString（项目的名字）
                 SearchString*（搜索的文字）

                 ProgressSortType（进度 的排序方式）
                 PrioritySortType（优先级 的排序方式）
                 CreateTimeSortType（创建时间 的排序方式）
                 UpdateTimeSortType（更新时间 的排序方式）

                 SelectedBugContorl*（选中的Bug的控件）（如果为null，则不选中任何Bug）
                 SelectedBug*（选中的Bug的BugData）（如果为null，则不选中任何Bug）


                              


           事件: PageChange（当翻页时（当[当前页码]改变时））

                 ShowNumberChange（当[显示个数]改变时）
                 SearchTextChange（当[搜索的文字]改变时）
                 SortTypeChange（当排序方式改变时）         
                 
                 ClickMinimizeButton（点击[最小化]按钮时）
                 ClickCloseButton（点击[关闭]按钮时）
                 ClickSettingButton（点击[设置]按钮时）

                 ClickAddBugButton（当点击[添加]按钮时）
                 ClickDeleteBugButton（当点击[删除]按钮时）（参数：当前选中的BugItem控件）

                 ClickClearSearchButton(当点击[清空搜索内容]按钮时)
                 
             
            Bug的事件：ClickMoreButtonInBug(当点击Bug的[更多]按钮时)（参数：触发事件的BugItem控件）
                      ClickProgressButtonInBug(当点击Bug的[进度]按钮时)（参数：触发事件的BugItem控件）
                      ClickRefreshButtonInBug(当点击Bug的[刷新]按钮时)（参数：触发事件的BugItem控件）
                      ClickGoToPageButtonInBug(当点击Bug的[跳转页面]按钮时)（参数：触发事件的BugItem控件）
                      ClickDeletedButtonInBug(当点击Bug的[已删除]按钮时)（参数：触发事件的BugItem控件）
                      MouseDoubleClickCheckInBug(当鼠标双击[BugItem]的时候)（参数：触发事件的BugItem控件）

                      ClickDeleteButtonInBugContextMenu(当点击右键菜单中的[删除]按钮)（参数：触发事件的BugItem控件）
                      ClickMoreButtonInBugContextMenu(当点击右键菜单中的[更多]按钮)（参数：触发事件的BugItem控件）
                      ClickProgressTypeButtonInBugContextMenu(当点击右键菜单中的[进度Type]的 某一个Check时)（参数：触发事件的BugItem控件）
                      ClickPriorityTypeButtonInBugContextMenu(当点击右键菜单中的[优先级Type]的 某一个Check时)（参数：触发事件的BugItem控件）
                      
             
             ListTip的事件：ClickCloseButtonInListTip(当点击ListTip中的[关闭]按钮)
                            ClickLookButtonInListTip(当点击ListTip中的[查看]按钮) */





        public ListUiControl()
        {
            InitializeComponent();
        }




        #region 依赖项属性：BugItemDatas

        /// <summary>
        /// 依赖项属性：Bug列表Item的数据 们
        /// </summary>
        public static DependencyProperty BugItemDatasProperty;

        /// <summary>
        /// 公开属性：Bug列表Item的数据 们
        /// </summary>
        public ObservableCollection<BugItemData> BugItemDatas
        {
            get { return (ObservableCollection<BugItemData>)GetValue(BugItemDatasProperty); }
            set { SetValue(BugItemDatasProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当BugItemDatasProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnBugItemDatasChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion

        #region 依赖项属性：BugItems*
        //Bug列表Item的数据 们
        private List<BugListItemControl> bugItems = new List<BugListItemControl>();

        /// <summary>
        /// 公开属性：Bug列表Item的数据 们
        /// </summary>
        public List<BugListItemControl> BugItems
        {
            get { return bugItems; }
            set { bugItems = value; }
        }
        #endregion


        #region 依赖项属性：AllBugTotalNumber

        /// <summary>
        /// 依赖项属性：Bug的总数
        /// </summary>
        public static DependencyProperty AllBugTotalNumberProperty;

        /// <summary>
        /// 公开属性：Bug的总数
        /// </summary>
        public int AllBugTotalNumber
        {
            get { return (int)GetValue(AllBugTotalNumberProperty); }
            set { SetValue(AllBugTotalNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当AllBugTotalNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnAllBugTotalNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：UndoneBugTotalNumber
        /// <summary>
        /// 依赖项属性：未完成的Bug数量
        /// </summary>
        public static DependencyProperty UndoneBugTotalNumberProperty;

        /// <summary>
        /// 公开属性：未完成的Bug数量
        /// </summary>
        public int UndoneBugTotalNumber
        {
            get { return (int)GetValue(UndoneBugTotalNumberProperty); }
            set { SetValue(UndoneBugTotalNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当UndoneBugTotalNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnUndoneBugTotalNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：LowUndoneBugTotalNumber
        /// <summary>
        /// 依赖项属性：低优先级并且未完成 的Bug数量
        /// </summary>
        public static DependencyProperty LowUndoneBugTotalNumberProperty;

        /// <summary>
        /// 公开属性：低优先级并且未完成 的Bug数量
        /// </summary>
        public int LowUndoneBugTotalNumber
        {
            get { return (int)GetValue(LowUndoneBugTotalNumberProperty); }
            set { SetValue(LowUndoneBugTotalNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当LowUndoneBugTotalNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnLowUndoneBugTotalNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：MidUndoneBugTotalNumber
        /// <summary>
        /// 依赖项属性：中优先级并且未完成 的Bug数量
        /// </summary>
        public static DependencyProperty MidUndoneBugTotalNumberProperty;

        /// <summary>
        /// 公开属性：中优先级并且未完成 的Bug数量
        /// </summary>
        public int MidUndoneBugTotalNumber
        {
            get { return (int)GetValue(MidUndoneBugTotalNumberProperty); }
            set { SetValue(MidUndoneBugTotalNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当MidUndoneBugTotalNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnMidUndoneBugTotalNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：HighUndoneBugTotalNumber
        /// <summary>
        /// 依赖项属性：高优先级并且未完成 的Bug数量
        /// </summary>
        public static DependencyProperty HighUndoneBugTotalNumberProperty;

        /// <summary>
        /// 公开属性：高优先级并且未完成 的Bug数量
        /// </summary>
        public int HighUndoneBugTotalNumber
        {
            get { return (int)GetValue(HighUndoneBugTotalNumberProperty); }
            set { SetValue(HighUndoneBugTotalNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当HighUndoneBugTotalNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnHighUndoneBugTotalNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion


        #region 依赖项属性：TotalPageNumber*
        /// <summary>
        /// 公开属性：总页数
        /// </summary>
        public int TotalPageNumber
        {
            get
            {
                int _value = 1;

                if (TotalPageNumberTextBlock != null)
                {
                    //把文字转换为数字
                    Int32.TryParse(this.TotalPageNumberTextBlock.Text, out _value);
                }

                return _value;
            }
            set
            {
                if (this.TotalPageNumberTextBlock == null)
                {

                }

                //如果页码小于等于0
                else if (value <= 0)
                {
                    this.TotalPageNumberTextBlock.Text = 1 + "";
                }

                //如果页码大于0
                else
                {
                    this.TotalPageNumberTextBlock.Text = value + "";
                }
            }
        }
        #endregion

        #region 依赖项属性：CurrentPageNumber*
        /// <summary>
        /// 公开属性：当前的页码
        /// </summary>
        public int CurrentPageNumber
        {
            get
            {
                //把文字转换为数字
                int _value = 1;
                bool _isParse = false;
                if (CurrentPageNumberTextBox != null)
                {
                    _isParse = Int32.TryParse(this.CurrentPageNumberTextBox.Text, out _value);
                }


                //如果文字不能转换为数字，或者数字为0的话
                if (_isParse == false || _value <= 0)
                {
                    CurrentPageNumber = 1;
                    _value = 1;
                }
                //如果数字大于总页数的话
                else if (_value > TotalPageNumber)
                {
                    CurrentPageNumber = TotalPageNumber;
                    _value = TotalPageNumber;
                }

                return _value;
            }

            set
            {
                //赋值
                if (this.CurrentPageNumberTextBox == null)
                {

                }

                //如果页码小于等于0
                else if (value <= 0)
                {
                    this.CurrentPageNumberTextBox.Text = 1 + "";
                }

                //如果页码，大于0
                else
                {
                    this.CurrentPageNumberTextBox.Text = value + "";
                }
            }
        }
        #endregion


        #region 依赖项属性：ShowBugNumber
        /// <summary>
        /// 依赖项属性：显示的Bug个数
        /// </summary>
        public static DependencyProperty ShowBugNumberProperty;

        /// <summary>
        /// 公开属性：显示的Bug个数
        /// </summary>
        public int ShowBugNumber
        {
            get { return (int)GetValue(ShowBugNumberProperty); }
            set { SetValue(ShowBugNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ShowBugNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnShowBugNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                //获取控件
                ListUiControl _listUiControl = sender as ListUiControl;

                //如果要显示10个Bug，就选中第0个Check; 如果要显示20个Bug，就选中第1个Check
                switch (e.NewValue)
                {
                    case 10:
                        _listUiControl.ShowBugNumberCheckGroupControl.CheckedIndex = 0;
                        break;

                    case 20:
                        _listUiControl.ShowBugNumberCheckGroupControl.CheckedIndex = 1;
                        break;
                }

                //触发[ShowNumberCheckedChange]事件
                _listUiControl.OnShowNumberChange((int)e.OldValue, (int)e.NewValue);
            }
        }

        #endregion


        #region 依赖项属性：ProjectNameString
        /// <summary>
        /// 依赖项属性：项目的名字
        /// </summary>
        public static DependencyProperty ProjectNameStringProperty;

        /// <summary>
        /// 公开属性：项目的名字
        /// </summary>
        public string ProjectNameString
        {
            get { return (string)GetValue(ProjectNameStringProperty); }
            set { SetValue(ProjectNameStringProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ProjectNameStringProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnProjectNameStringChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：SearchString*
        /// <summary>
        /// 公开属性：当排序方式改变时
        /// </summary>
        public string SearchString
        {
            get
            {
                if (this.SearchTextBox != null)
                {
                    return this.SearchTextBox.Text;
                }

                return "";
            }
            set
            {
                if (this.SearchTextBox != null)
                {
                    this.SearchTextBox.Text = value;
                }
            }
        }
        #endregion


        #region 依赖项属性：ProgressSortType
        /// <summary>
        /// 依赖项属性：进度 的排序方式
        /// </summary>
        public static DependencyProperty ProgressSortTypeProperty;

        /// <summary>
        /// 公开属性：进度 的排序方式
        /// </summary>
        public SortType ProgressSortType
        {
            get { return (SortType)GetValue(ProgressSortTypeProperty); }
            set { SetValue(ProgressSortTypeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ProgressSortTypeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnProgressSortTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            /* 取到控件 */
            ListUiControl _listUiControl = sender as ListUiControl;

            /* 触发SortTypeChange事件 */
            _listUiControl.OnSortTypeChange();
        }

        #endregion

        #region 依赖项属性：PrioritySortType
        /// <summary>
        /// 依赖项属性：优先级 的排序方式
        /// </summary>
        public static DependencyProperty PrioritySortTypeProperty;

        /// <summary>
        /// 公开属性：优先级 的排序方式
        /// </summary>
        public SortType PrioritySortType
        {
            get { return (SortType)GetValue(PrioritySortTypeProperty); }
            set { SetValue(PrioritySortTypeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当PrioritySortTypeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnPrioritySortTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            /* 取到控件 */
            ListUiControl _listUiControl = sender as ListUiControl;

            /* 触发SortTypeChange事件 */
            _listUiControl.OnSortTypeChange();
        }

        #endregion

        #region 依赖项属性：CreateTimeSortType
        /// <summary>
        /// 依赖项属性：创建时间 的排序方式
        /// </summary>
        public static DependencyProperty CreateTimeSortTypeProperty;

        /// <summary>
        /// 公开属性：创建时间 的排序方式
        /// </summary>
        public SortType CreateTimeSortType
        {
            get { return (SortType)GetValue(CreateTimeSortTypeProperty); }
            set { SetValue(CreateTimeSortTypeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当CreateTimeSortTypeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnCreateTimeSortTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            /* 取到控件 */
            ListUiControl _listUiControl = sender as ListUiControl;

            /* 触发SortTypeChange事件 */
            _listUiControl.OnSortTypeChange();
        }

        #endregion

        #region 依赖项属性：UpdateTimeSortType
        /// <summary>
        /// 依赖项属性：更新时间 的排序方式
        /// </summary>
        public static DependencyProperty UpdateTimeSortTypeProperty;

        /// <summary>
        /// 公开属性：更新时间 的排序方式
        /// </summary>
        public SortType UpdateTimeSortType
        {
            get { return (SortType)GetValue(UpdateTimeSortTypeProperty); }
            set { SetValue(UpdateTimeSortTypeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当UpdateTimeSortTypeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnUpdateTimeSortTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            /* 取到控件 */
            ListUiControl _listUiControl = sender as ListUiControl;

            /* 触发SortTypeChange事件 */
            _listUiControl.OnSortTypeChange();
        }

        #endregion


        #region 依赖项属性：SelectedBugContorl*
        /// <summary>
        /// 公开属性：选中的Bug控件（如果为null，则不选中任何Bug）
        /// </summary>
        public BugListItemControl SelectedBugContorl
        {
            get
            {
                if (this.BugListBox != null)
                {
                    for (int i = 0; i < BugItems.Count; i++)
                    {
                        if (BugItems[i].IsChecked == true)
                        {
                            return BugItems[i];
                        }
                    }
                }

                return null;
            }


            set
            {
                if (this.BugListBox != null)
                {
                    if (value == null)
                    {
                        //抬起其他所有的BugListItem
                        for (int i = 0; i < BugItems.Count; i++)
                        {
                            BugItems[i].IsChecked = false;
                        }
                    }

                    else
                    {
                        //让这个BugListItemControl被选中
                        value.IsChecked = true;
                    }
                }
            }
        }
        #endregion

        #region 依赖项属性：SelectedBugIndex*
        /// <summary>
        /// 公开属性：选中的Bug索引（在列表中的索引）（如果为-1，则不选中任何Bug）
        /// （从0开始）
        /// </summary>
        public int SelectedBugIndex
        {
            get
            {
                if (this.BugListBox != null && SelectedBugContorl != null)
                {
                    //遍历所有的BugListItem
                    for (int i = 0; i < BugItems.Count; i++)
                    {
                        if (BugItems[i] == SelectedBugContorl)
                        {
                            return i;
                        }
                    }
                }


                return -1;
            }


            set
            {
                if (this.BugListBox != null && value > -1)
                {
                    if (value < BugItems.Count)
                    {
                        //选中Bug
                        SelectedBugContorl = BugItems[value];
                    }
                    else
                    {
                        SelectedBugContorl = null;
                    }
                }

                else if (this.BugListBox != null && value <= -1)
                {
                    //不选中任何Bug
                    SelectedBugContorl = null;
                }
            }
        }
        #endregion









        #region 路由事件：PageChange
        /// <summary>
        /// 路由事件：PageChangeEvent
        /// （当翻页时（当[当前页码]改变时），触发此事件）
        /// </summary>
        public static readonly RoutedEvent PageChangeEvent;


        /// <summary>
        /// 路由事件的属性：PageChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> PageChange
        {
            //添加一条事件
            add { AddHandler(PageChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(PageChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 PageChange 路由事件
        /// </summary>
        /// <param name="_newValue">现在的[当前页码]</param>
        private void OnPageChange(int _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<int> args = new RoutedPropertyChangedEventArgs<int>(0, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.PageChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion


        #region 路由事件：ShowNumberChange

        /// <summary>
        /// 路由事件：ShowNumberChangeEvent
        /// （当[显示个数]改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ShowNumberChangeEvent;


        /// <summary>
        /// 路由事件的属性：ShowNumberChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> ShowNumberChange
        {
            //添加一条事件
            add { AddHandler(ShowNumberChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ShowNumberChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ShowNumberChange 路由事件
        /// </summary>
        /// <param name="_oldValue">之前的选中项的索引</param>
        /// <param name="_newValue">新的选中项的索引</param>
        private void OnShowNumberChange(int _oldValue, int _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<int> args = new RoutedPropertyChangedEventArgs<int>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ShowNumberChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：SearchTextChange

        /// <summary>
        /// 路由事件：SearchTextChangeEvent
        /// （当[搜索的文字]改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent SearchTextChangeEvent;


        /// <summary>
        /// 路由事件的属性：SearchTextChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<string> SearchTextChange
        {
            //添加一条事件
            add { AddHandler(SearchTextChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(SearchTextChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 SearchTextChange 路由事件
        /// </summary>
        /// <param name="_newValue">新的[搜索文字]</param>
        private void OnSearchTextChange(string _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<string> args = new RoutedPropertyChangedEventArgs<string>("", _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.SearchTextChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：SortTypeChange

        /// <summary>
        /// 路由事件：SortTypeChangeEvent
        /// （当排序方式改变时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent SortTypeChangeEvent;


        /// <summary>
        /// 路由事件的属性：SortTypeChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> SortTypeChange
        {
            //添加一条事件
            add { AddHandler(SortTypeChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(SortTypeChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 SortTypeChange 路由事件
        /// </summary>
        private void OnSortTypeChange()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.SortTypeChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion


        #region 路由事件：ClickMinimizeButton

        /// <summary>
        /// 路由事件：ClickMinimizeButtonEvent
        /// （点击[最小化]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickMinimizeButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickMinimizeButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickMinimizeButton
        {
            //添加一条事件
            add { AddHandler(ClickMinimizeButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickMinimizeButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickMinimizeButton 路由事件
        /// </summary>
        private void OnClickMinimizeButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickMinimizeButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickCloseButton

        /// <summary>
        /// 路由事件：ClickCloseButtonEvent
        /// （点击[关闭]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickCloseButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickCloseButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickCloseButton
        {
            //添加一条事件
            add { AddHandler(ClickCloseButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickCloseButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickCloseButton 路由事件
        /// </summary>
        private void OnClickCloseButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickCloseButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickSettingButton

        /// <summary>
        /// 路由事件：ClickSettingButtonEvent
        /// （点击[设置]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickSettingButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickSettingButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickSettingButton
        {
            //添加一条事件
            add { AddHandler(ClickSettingButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickSettingButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickSettingButton 路由事件
        /// </summary>
        private void OnClickSettingButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickSettingButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion


        #region 路由事件：ClickClearSearchButton

        /// <summary>
        /// 路由事件：ClickClearSearchButtonEvent
        /// （点击[清空搜索内容]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickClearSearchButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickClearSearchButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickClearSearchButton
        {
            //添加一条事件
            add { AddHandler(ClickClearSearchButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickClearSearchButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickClearSearchButton 路由事件
        /// </summary>
        private void OnClickClearSearchButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickClearSearchButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion


        #region 路由事件：ClickAddBugButton

        /// <summary>
        /// 路由事件：ClickAddBugButtonEvent
        /// （当点击[添加]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickAddBugButtonEvent;


        /// <summary>
        /// 路由事件的属性：Click
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickAddBugButton
        {
            //添加一条事件
            add { AddHandler(ClickAddBugButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickAddBugButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickAddBugButton 路由事件
        /// </summary>
        private void OnClickAddBugButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickAddBugButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickDeleteBugButton

        /// <summary>
        /// 路由事件：ClickDeleteBugButtonEvent
        /// （当点击[删除]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickDeleteBugButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickDeleteBugButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<BugListItemControl> ClickDeleteBugButton
        {
            //添加一条事件
            add { AddHandler(ClickDeleteBugButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickDeleteBugButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickDeleteBugButton 路由事件
        /// </summary>
        /// <param name="_newValue">触发这个事件的Bug控件</param>
        private void OnClickDeleteBugButton(BugListItemControl _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<BugListItemControl> args = new RoutedPropertyChangedEventArgs<BugListItemControl>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickDeleteBugButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion


        #region 路由事件：ClickMoreButtonInBug

        /// <summary>
        /// 路由事件：ClickMoreButtonInBugEvent
        /// （当点击Bug的[更多]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickMoreButtonInBugEvent;


        /// <summary>
        /// 路由事件的属性：ClickMoreButtonInBug
        /// </summary>
        public event RoutedPropertyChangedEventHandler<BugListItemControl> ClickMoreButtonInBug
        {
            //添加一条事件
            add { AddHandler(ClickMoreButtonInBugEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickMoreButtonInBugEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickMoreButtonInBug 路由事件
        /// </summary>
        /// <param name="_newValue">触发这个事件的Bug控件</param>
        private void OnClickMoreButtonInBug(BugListItemControl _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<BugListItemControl> args = new RoutedPropertyChangedEventArgs<BugListItemControl>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickMoreButtonInBugEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickProgressButtonInBug

        /// <summary>
        /// 路由事件：ClickProgressButtonInBugEvent
        /// （当点击Bug的[进度]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickProgressButtonInBugEvent;


        /// <summary>
        /// 路由事件的属性：ClickProgressButtonInBug
        /// </summary>
        public event RoutedPropertyChangedEventHandler<BugListItemControl> ClickProgressButtonInBug
        {
            //添加一条事件
            add { AddHandler(ClickProgressButtonInBugEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickProgressButtonInBugEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickProgressButtonInBug 路由事件
        /// </summary>
        /// <param name="_newValue">触发这个事件的Bug控件</param>
        private void OnClickProgressButtonInBug(BugListItemControl _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<BugListItemControl> args = new RoutedPropertyChangedEventArgs<BugListItemControl>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickProgressButtonInBugEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickRefreshButtonInBug

        /// <summary>
        /// 路由事件：ClickRefreshButtonInBugEvent
        /// （当点击Bug的[刷新]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickRefreshButtonInBugEvent;


        /// <summary>
        /// 路由事件的属性：ClickRefreshButtonInBug
        /// </summary>
        public event RoutedPropertyChangedEventHandler<BugListItemControl> ClickRefreshButtonInBug
        {
            //添加一条事件
            add { AddHandler(ClickRefreshButtonInBugEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickRefreshButtonInBugEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickRefreshButtonInBug 路由事件
        /// </summary>
        /// <param name="_newValue">触发这个事件的Bug控件</param>
        private void OnClickRefreshButtonInBug(BugListItemControl _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<BugListItemControl> args = new RoutedPropertyChangedEventArgs<BugListItemControl>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickRefreshButtonInBugEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickGoToPageButtonInBug

        /// <summary>
        /// 路由事件：ClickGoToPageButtonInBugEvent
        /// （当点击Bug的[跳转页面]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickGoToPageButtonInBugEvent;


        /// <summary>
        /// 路由事件的属性：ClickGoToPageButtonInBug
        /// </summary>
        public event RoutedPropertyChangedEventHandler<BugListItemControl> ClickGoToPageButtonInBug
        {
            //添加一条事件
            add { AddHandler(ClickGoToPageButtonInBugEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickGoToPageButtonInBugEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickGoToPageButtonInBug 路由事件
        /// </summary>
        /// <param name="_newValue">触发这个事件的Bug控件</param>
        private void OnClickGoToPageButtonInBug(BugListItemControl _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<BugListItemControl> args = new RoutedPropertyChangedEventArgs<BugListItemControl>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickGoToPageButtonInBugEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickDeletedButtonInBug

        /// <summary>
        /// 路由事件：ClickDeletedButtonInBugEvent
        /// （当点击Bug的[已删除]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickDeletedButtonInBugEvent;


        /// <summary>
        /// 路由事件的属性：ClickDeletedButtonInBug
        /// </summary>
        public event RoutedPropertyChangedEventHandler<BugListItemControl> ClickDeletedButtonInBug
        {
            //添加一条事件
            add { AddHandler(ClickDeletedButtonInBugEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickDeletedButtonInBugEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickDeletedButtonInBug 路由事件
        /// </summary>
        /// <param name="_newValue">触发这个事件的Bug控件</param>
        private void OnClickDeletedButtonInBug(BugListItemControl _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<BugListItemControl> args = new RoutedPropertyChangedEventArgs<BugListItemControl>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickDeletedButtonInBugEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：MouseDoubleClickCheckInBug

        /// <summary>
        /// 路由事件：MouseDoubleClickCheckInBugEvent
        /// （当双击Bug的[Check]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent MouseDoubleClickCheckInBugEvent;


        /// <summary>
        /// 路由事件的属性：MouseDoubleClickCheckInBug
        /// </summary>
        public event RoutedPropertyChangedEventHandler<BugListItemControl> MouseDoubleClickCheckInBug
        {
            //添加一条事件
            add { AddHandler(MouseDoubleClickCheckInBugEvent, value); }

            //移除一条事件
            remove { RemoveHandler(MouseDoubleClickCheckInBugEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 MouseDoubleClickCheckInBug 路由事件
        /// </summary>
        /// <param name="_newValue">触发这个事件的Bug控件</param>
        private void OnMouseDoubleClickCheckInBug(BugListItemControl _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<BugListItemControl> args = new RoutedPropertyChangedEventArgs<BugListItemControl>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.MouseDoubleClickCheckInBugEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion


        #region 路由事件：ClickMoreButtonInBugContextMenu

        /// <summary>
        /// 路由事件：ClickMoreButtonInBugContextMenuEvent
        /// （当点击右键菜单的[更多]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickMoreButtonInBugContextMenuEvent;


        /// <summary>
        /// 路由事件的属性：ClickMoreButtonInBugContextMenu
        /// </summary>
        public event RoutedPropertyChangedEventHandler<BugListItemControl> ClickMoreButtonInBugContextMenu
        {
            //添加一条事件
            add { AddHandler(ClickMoreButtonInBugContextMenuEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickMoreButtonInBugContextMenuEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickMoreButtonInBugContextMenu 路由事件
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件</param>
        private void OnClickMoreButtonInBugContextMenu(BugListItemControl _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<BugListItemControl> args = new RoutedPropertyChangedEventArgs<BugListItemControl>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickMoreButtonInBugContextMenuEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickDeleteButtonInBugContextMenu

        /// <summary>
        /// 路由事件：ClickDeleteButtonInBugContextMenuEvent
        /// （当点击右键菜单的[删除]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickDeleteButtonInBugContextMenuEvent;


        /// <summary>
        /// 路由事件的属性：ClickDeleteButtonInBugContextMenu
        /// </summary>
        public event RoutedPropertyChangedEventHandler<BugListItemControl> ClickDeleteButtonInBugContextMenu
        {
            //添加一条事件
            add { AddHandler(ClickDeleteButtonInBugContextMenuEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickDeleteButtonInBugContextMenuEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickDeleteButtonInBugContextMenu 路由事件
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件</param>
        private void OnClickDeleteButtonInBugContextMenu(BugListItemControl _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<BugListItemControl> args = new RoutedPropertyChangedEventArgs<BugListItemControl>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickDeleteButtonInBugContextMenuEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickProgressTypeButtonInBugContextMenu

        /// <summary>
        /// 路由事件：ClickProgressTypeButtonInBugContextMenuEvent
        /// （当点击右键菜单中的 [ProgressCheckGroup]中的按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickProgressTypeButtonInBugContextMenuEvent;


        /// <summary>
        /// 路由事件的属性：ClickProgressTypeButtonInBugContextMenu
        /// </summary>
        public event RoutedPropertyChangedEventHandler<BugListItemControl> ClickProgressTypeButtonInBugContextMenu
        {
            //添加一条事件
            add { AddHandler(ClickProgressTypeButtonInBugContextMenuEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickProgressTypeButtonInBugContextMenuEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickProgressTypeButtonInBugContextMenu 路由事件
        /// </summary>
        /// <param name="_oldValue">之前被选中的Check的索引</param>
        /// <param name="_newValue">新的被选中的Check的索引</param>
        /// <param name="_source">触发这个事件的Bug控件</param>
        private void OnClickProgressTypeButtonInBugContextMenu(BugListItemControl _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<BugListItemControl> args = new RoutedPropertyChangedEventArgs<BugListItemControl>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickProgressTypeButtonInBugContextMenuEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickPriorityTypeButtonInBugContextMenu

        /// <summary>
        /// 路由事件：ClickPriorityTypeButtonInBugContextMenuEvent
        /// （当点击右键菜单中的 [PriorityCheckGroup]中的按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickPriorityTypeButtonInBugContextMenuEvent;


        /// <summary>
        /// 路由事件的属性：ClickProgressTypeButtonInBugContextMenu
        /// </summary>
        public event RoutedPropertyChangedEventHandler<BugListItemControl> ClickPriorityTypeButtonInBugContextMenu
        {
            //添加一条事件
            add { AddHandler(ClickPriorityTypeButtonInBugContextMenuEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickPriorityTypeButtonInBugContextMenuEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickPriorityTypeButtonInBugContextMenu 路由事件
        /// </summary>
        /// <param name="_oldValue">之前被选中的Check的索引</param>
        /// <param name="_newValue">新的被选中的Check的索引</param>
        /// <param name="_source">触发这个事件的Bug控件</param>
        private void OnClickPriorityTypeButtonInBugContextMenu(BugListItemControl _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<BugListItemControl> args = new RoutedPropertyChangedEventArgs<BugListItemControl>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickPriorityTypeButtonInBugContextMenuEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion


        #region 路由事件：ClickCloseButtonInListTip

        /// <summary>
        /// 路由事件：ClickCloseButtonInListTipEvent
        /// （点击[关闭]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickCloseButtonInListTipEvent;


        /// <summary>
        /// 路由事件的属性：ClickCloseButtonInListTip
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickCloseButtonInListTip
        {
            //添加一条事件
            add { AddHandler(ClickCloseButtonInListTipEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickCloseButtonInListTipEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickCloseButtonInListTip 路由事件
        /// </summary>
        private void OnClickCloseButtonInListTip()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickCloseButtonInListTipEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickLookButtonInListTip

        /// <summary>
        /// 路由事件：ClickLookButtonInListTipEvent
        /// （点击[查看]按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickLookButtonInListTipEvent;


        /// <summary>
        /// 路由事件的属性：ClickLookButtonInListTip
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickLookButtonInListTip
        {
            //添加一条事件
            add { AddHandler(ClickLookButtonInListTipEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickLookButtonInListTipEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickLookButtonInListTip 路由事件
        /// </summary>
        private void OnClickLookButtonInListTip()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = ListUiControl.ClickLookButtonInListTipEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion





        #region 静态构造方法：注册依赖项属性 和 路由事件

        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static ListUiControl()
        {
            /*注册依赖项属性*/
            //注册BugDatasProperty
            BugItemDatasProperty = DependencyProperty.Register(
                "BugItemDatas", //属性的名字
                typeof(ObservableCollection<BugItemData>), //属性的类型
                typeof(ListUiControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (ObservableCollection<BugItemData>)new ObservableCollection<BugItemData>(),
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnBugItemDatasChanged))
            );



            //注册AllBugTotalNumberProperty
            AllBugTotalNumberProperty = DependencyProperty.Register(
                "AllBugTotalNumber", typeof(int), typeof(ListUiControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnAllBugTotalNumberChanged)));

            //注册UndoneBugTotalNumberProperty
            UndoneBugTotalNumberProperty = DependencyProperty.Register(
                "UndoneBugTotalNumber", typeof(int), typeof(ListUiControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnUndoneBugTotalNumberChanged)));

            //注册LowUndoneBugTotalNumberProperty
            LowUndoneBugTotalNumberProperty = DependencyProperty.Register(
                "LowUndoneBugTotalNumber", typeof(int), typeof(ListUiControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnLowUndoneBugTotalNumberChanged)));

            //注册MidUndoneBugTotalNumberProperty
            MidUndoneBugTotalNumberProperty = DependencyProperty.Register(
                "MidUndoneBugTotalNumber", typeof(int), typeof(ListUiControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnMidUndoneBugTotalNumberChanged)));

            //注册HighUndoneBugTotalNumberProperty
            HighUndoneBugTotalNumberProperty = DependencyProperty.Register(
                "HighUndoneBugTotalNumber", typeof(int), typeof(ListUiControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnHighUndoneBugTotalNumberChanged)));



            //注册ShowBugNumberProperty
            ShowBugNumberProperty = DependencyProperty.Register(
                "ShowBugNumber", typeof(int), typeof(ListUiControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnShowBugNumberChanged)));

            //注册ProjectNameStringProperty
            ProjectNameStringProperty = DependencyProperty.Register(
                "ProjectNameString", typeof(string), typeof(ListUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnProjectNameStringChanged)));



            //注册ProgressSortTypeProperty
            ProgressSortTypeProperty = DependencyProperty.Register(
                "ProgressSortType", typeof(SortType), typeof(ListUiControl),
                new FrameworkPropertyMetadata((SortType)SortType.None, new PropertyChangedCallback(OnProgressSortTypeChanged)));

            //注册PrioritySortTypeProperty
            PrioritySortTypeProperty = DependencyProperty.Register(
                "PrioritySortType", typeof(SortType), typeof(ListUiControl),
                new FrameworkPropertyMetadata((SortType)SortType.None, new PropertyChangedCallback(OnPrioritySortTypeChanged)));

            //注册CreateTimeSortTypeProperty
            CreateTimeSortTypeProperty = DependencyProperty.Register(
                "CreateTimeSortType", typeof(SortType), typeof(ListUiControl),
                new FrameworkPropertyMetadata((SortType)SortType.None, new PropertyChangedCallback(OnCreateTimeSortTypeChanged)));

            //注册UpdateTimeSortTypeProperty
            UpdateTimeSortTypeProperty = DependencyProperty.Register(
                "UpdateTimeSortType", typeof(SortType), typeof(ListUiControl),
                new FrameworkPropertyMetadata((SortType)SortType.None, new PropertyChangedCallback(OnUpdateTimeSortTypeChanged)));








            /*注册路由事件*/
            //注册PageChangeEvent
            PageChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "PageChange", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<int>), //路由事件要处理的数据类型
                typeof(ListUiControl) //这个路由事件属于哪个控件？
            );


            //注册ShowNumberChangeEvent
            ShowNumberChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ShowNumberChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<int>), typeof(ListUiControl));

            //注册SearchTextChangeEvent
            SearchTextChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "SearchTextChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<string>), typeof(ListUiControl));

            //注册SortTypeChangeEvent
            SortTypeChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "SortTypeChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ListUiControl));


            //注册ClickAddBugButtonEvent
            ClickAddBugButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickAddBugButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ListUiControl));

            //注册ClickDeleteBugButtonEvent
            ClickDeleteBugButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickDeleteBugButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BugListItemControl>), typeof(ListUiControl));

            //注册ClickClearSearchButtonEvent
            ClickClearSearchButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickClearSearchButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ListUiControl));







            //注册ClickMinimizeButtonEvent
            ClickMinimizeButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickMinimizeButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ListUiControl));

            //注册ClickCloseButtonEvent
            ClickCloseButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickCloseButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ListUiControl));

            //注册ClickSettingButtonEvent
            ClickSettingButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickSettingButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ListUiControl));



            //注册ClickMoreButtonInBugEvent
            ClickMoreButtonInBugEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickMoreButtonInBug", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BugListItemControl>), typeof(ListUiControl));

            //注册ClickProgressButtonInBugEvent
            ClickProgressButtonInBugEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickProgressButtonInBug", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BugListItemControl>), typeof(ListUiControl));

            //注册ClickRefreshButtonInBugEvent
            ClickRefreshButtonInBugEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickRefreshButtonInBug", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BugListItemControl>), typeof(ListUiControl));

            //注册ClickGoToPageButtonInBugEvent
            ClickGoToPageButtonInBugEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickGoToPageButtonInBug", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BugListItemControl>), typeof(ListUiControl));

            //注册ClickDeletedButtonInBugEvent
            ClickDeletedButtonInBugEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickDeletedButtonInBug", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BugListItemControl>), typeof(ListUiControl));

            //注册MouseDoubleClickCheckInBugEvent
            MouseDoubleClickCheckInBugEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "MouseDoubleClickCheckInBug", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BugListItemControl>), typeof(ListUiControl));



            //注册ClickDeleteButtonInBugContextMenuEvent
            ClickDeleteButtonInBugContextMenuEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickDeleteButtonInBugContextMenu", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<BugListItemControl>), //路由事件要处理的数据类型
                typeof(ListUiControl) //这个路由事件属于哪个控件？
            );

            //注册ClickMoreButtonInBugContextMenuEvent
            ClickMoreButtonInBugContextMenuEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickMoreButtonInBugContextMenu", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BugListItemControl>), typeof(ListUiControl));

            //注册ClickProgressTypeButtonInBugContextMenuEvent
            ClickProgressTypeButtonInBugContextMenuEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickProgressTypeButtonInBugContextMenu", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BugListItemControl>), typeof(ListUiControl));

            //注册ClickPriorityTypeButtonInBugContextMenuEvent
            ClickPriorityTypeButtonInBugContextMenuEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickPriorityTypeButtonInBugContextMenu", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BugListItemControl>), typeof(ListUiControl));



            //注册ClickCloseButtonInListTipEvent
            ClickCloseButtonInListTipEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickCloseButtonInListTip", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ListUiControl));

            //注册ClickLookButtonInListTipEvent
            ClickLookButtonInListTipEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickLookButtonInListTip", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ListUiControl));
        }

        #endregion









        #region [事件 - 排序]
        /// <summary>
        /// 当点击[表头的进度]按钮时
        /// </summary>
        private void ProgressListHeadButton_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            /* 判断当前的[进度]的排序类型 */
            switch (ProgressSortType)
            {
                //如果排序方式是None，就把排序方式改为[从低到高]
                case SortType.None:
                    ProgressSortType = SortType.LowToHigh;
                    break;

                //如果排序方式是[从低到高]，就把排序方式改为[从高到低]
                case SortType.LowToHigh:
                    ProgressSortType = SortType.HighToLow;
                    break;

                //如果排序方式是[从高到低]，就把排序方式改为None
                case SortType.HighToLow:
                    ProgressSortType = SortType.None;
                    break;
            }
        }

        /// <summary>
        /// 当点击[表头的优先级]按钮时
        /// </summary>
        private void PriorityListHeadButton_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            /* 判断当前的[进度]的排序类型 */
            switch (PrioritySortType)
            {
                //如果排序方式是None，就把排序方式改为[从低到高]
                case SortType.None:
                    PrioritySortType = SortType.LowToHigh;
                    break;

                //如果排序方式是[从低到高]，就把排序方式改为[从高到低]
                case SortType.LowToHigh:
                    PrioritySortType = SortType.HighToLow;
                    break;

                //如果排序方式是[从高到低]，就把排序方式改为None
                case SortType.HighToLow:
                    PrioritySortType = SortType.None;
                    break;
            }
        }

        /// <summary>
        /// 当点击[表头的创建时间]按钮时
        /// </summary>
        private void CreateTimeListHeadButton_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            /* 判断当前的[创建时间]的排序类型 */
            switch (CreateTimeSortType)
            {
                //如果排序方式是None，就把排序方式改为[从低到高]
                case SortType.None:
                    CreateTimeSortType = SortType.HighToLow;
                    break;

                //如果排序方式是[从低到高]，就把排序方式改为[从高到低]
                case SortType.LowToHigh:
                    CreateTimeSortType = SortType.HighToLow;
                    break;

                //如果排序方式是[从高到低]，就把排序方式改为[从低到高]
                case SortType.HighToLow:
                    CreateTimeSortType = SortType.LowToHigh;
                    break;
            }

            /* 并且把[更新时间]的排序方式变成None */
            UpdateTimeSortType = SortType.None;
        }

        /// <summary>
        /// 当点击[表头的更新时间]按钮时
        /// </summary>
        private void UpdateTimeListHeadButton_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            /* 判断当前的[更新时间]的排序类型 */
            switch (UpdateTimeSortType)
            {
                //如果排序方式是None，就把排序方式改为[从低到高]
                case SortType.None:
                    UpdateTimeSortType = SortType.HighToLow;
                    break;

                //如果排序方式是[从低到高]，就把排序方式改为[从高到低]
                case SortType.LowToHigh:
                    UpdateTimeSortType = SortType.HighToLow;
                    break;

                //如果排序方式是[从高到低]，就把排序方式改为[从低到高]
                case SortType.HighToLow:
                    UpdateTimeSortType = SortType.LowToHigh;
                    break;
            }

            /* 并且把[创建时间]的排序方式变成None */
            CreateTimeSortType = SortType.None;
        }
        #endregion

        #region [事件 - 点击]
        /// <summary>
        /// 当点击[添加BUG]按钮时
        /// </summary>
        private void AddBugButton_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发[ClickAddBugButton]事件
            this.OnClickAddBugButton();
        }

        /// <summary>
        /// 当点击[删除BUG]按钮时
        /// </summary>
        private void DeleteBugButton_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发[ClickDeleteBugButton]事件
            this.OnClickDeleteBugButton(SelectedBugContorl);
        }



        /// <summary>
        /// 点击[最小化]按钮时
        /// </summary>
        private void MinimizeButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发[ClickMinimizeButton]事件
            this.OnClickMinimizeButton();
        }

        /// <summary>
        /// 点击[关闭]按钮时
        /// </summary>
        private void CloseButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发[ClickCloseButton]事件
            this.OnClickCloseButton();
        }

        /// <summary>
        /// 点击[设置]按钮时
        /// </summary>
        private void SettingButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发[ClickSettingButton]事件
            this.OnClickSettingButton();
        }
        #endregion

        #region [事件 - 页码]
        /// <summary>
        /// 当点击[上一页]按钮时
        /// </summary>
        private void PreviousPageButton_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            /* 如果[当前的页码]为1的话，就return */
            if (this.CurrentPageNumber <= 1) return;

            /* 当前的页码-1 */
            this.CurrentPageNumber -= 1;

        }

        /// <summary>
        /// 当点击[下一页]按钮时
        /// </summary>
        private void NextPageButton_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            /* 如果[当前的页码]为[总页码]的话，就return */
            if (this.CurrentPageNumber >= this.TotalPageNumber) return;

            /* 当前的页码+1 */
            this.CurrentPageNumber += 1;
        }

        /// <summary>
        /// 当[当前的页码]改变时
        /// </summary>
        private void CurrentPageNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.CurrentPageNumber > this.TotalPageNumber)
            {
                this.CurrentPageNumber = this.TotalPageNumber;
            }

            //触发[PageChange]事件
            this.OnPageChange(CurrentPageNumber);
        }
        #endregion

        #region [事件 - 搜索]
        /// <summary>
        /// 当[搜索文本框]的内容发生改变时
        /// </summary>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //取到TextBox控件
            TextBox _textBox = sender as TextBox;


            //如果TextBox的内容为空，就把TextBox的背景设置为透明
            //如果TextBox的内容不为空，把TextBox的背景设置为白色
            AnimationTool.PlayTextChangedAnimation(_textBox);

            //如果搜索框里有内容，就把Clear按钮显示出来
            if (_textBox.Text == null || _textBox.Text == "")
            {
                this.ClearSearchButtonControl.Visibility = Visibility.Collapsed;//隐藏
            }
            else
            {
                this.ClearSearchButtonControl.Visibility = Visibility.Visible;//显示
            }


            //然后触发[SearchTextChange]事件
            this.OnSearchTextChange(_textBox.Text);
        }


        //当点击[清空搜索内容]的按钮时
        private void ClearSearchButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickClearSearchButton();
        }
        #endregion

        #region [事件 - 显示的Bug个数]
        /// <summary>
        /// 当[显示个数]的 CheckedIndex发生改变时
        /// </summary>
        private void ShowBugNumberCheckGroupControl_CheckedChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //判断：被选中的Check
            switch ((int)e.NewValue)
            {
                //如果被选中的是0，就把显示个数修改为10
                case 0:
                    ShowBugNumber = 10;
                    break;

                //如果被选中的是1，就把显示个数修改为20
                case 1:
                    ShowBugNumber = 20;
                    break;
            }
        }
        #endregion

        #region [事件 - BugItem]
        /// <summary>
        /// 当点击Bug的[更多]按钮时
        /// </summary>
        private void BugListItemControl_ClickMoreButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //触发[ClickBugMoreButton]事件
            this.OnClickMoreButtonInBug(_bugListItemControl);
        }

        /// <summary>
        /// 当点击Bug的[进度]按钮时
        /// </summary>
        private void BugListItemControl_OnClickProgressButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //触发[ClickBugMoreButton]事件
            this.OnClickProgressButtonInBug(_bugListItemControl);
        }

        /// <summary>
        /// 当点击Bug的[刷新]按钮时
        /// </summary>
        private void BugListItemControl_ClickRefreshButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //触发[ClickBugRefreshButton]事件
            this.OnClickRefreshButtonInBug(_bugListItemControl);
        }

        /// <summary>
        /// 当点击Bug的[跳转页面]按钮时
        /// </summary>
        private void BugListItemControl_ClickGoToPageButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //触发[ClickBugGoToPageButton]事件
            this.OnClickGoToPageButtonInBug(_bugListItemControl);
        }

        /// <summary>
        /// 当点击Bug的[已删除]按钮时
        /// </summary>
        private void BugListItemControl_OnClickDeletedButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //触发[ClickDeletedButtonInBug]事件
            this.OnClickDeletedButtonInBug(_bugListItemControl);
        }



        /// <summary>
        /// 当Bug被选中的时候
        /// </summary>
        private void BugListItemControl_OnChecked(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //抬起其他所有的BugListItem（以及关闭其他所有的BugListItem的右键菜单）
            for (int i = 0; i < BugItems.Count; i++)
            {
                if (BugItems[i] != _bugListItemControl)
                {
                    BugItems[i].IsChecked = false;//抬起BugListItem
                }
            }
        }

        /// <summary>
        /// 当鼠标双击[BugListItem]的时候
        /// </summary>
        private void BugListItemControl_OnMouseDoubleClickCheck(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //触发[MouseDoubleClickCheckInBug]事件
            this.OnMouseDoubleClickCheckInBug(_bugListItemControl);
        }
        #endregion

        #region [事件 - BugItem右键菜单]
        //当点击BugItem右键菜单中的 [删除]按钮时
        private void BugListItemControl_ClickDeleteButtonInContextMenu(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //触发事件
            this.OnClickDeleteButtonInBugContextMenu(_bugListItemControl);
        }

        //当点击BugItem右键菜单中的 [更多]按钮时
        private void BugListItemControl_ClickMoreButtonInContextMenu(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //触发事件
            this.OnClickMoreButtonInBugContextMenu(_bugListItemControl);
        }

        //当点击BugItem右键菜单中的 [ProgressCheckGroup]中的Check时
        private void BugListItemControl_ClickProgressTypeButtonInContextMenu(object sender, RoutedPropertyChangedEventArgs<ProgressType> e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //触发事件
            this.OnClickProgressTypeButtonInBugContextMenu(_bugListItemControl);
        }

        //当点击BugItem右键菜单中的 [PriorityCheckGroup]中的Check时
        private void BugListItemControl_ClickPriorityTypeButtonInContextMenu(object sender, RoutedPropertyChangedEventArgs<PriorityType> e)
        {
            //获取控件
            BugListItemControl _bugListItemControl = sender as BugListItemControl;

            //触发事件
            this.OnClickPriorityTypeButtonInBugContextMenu(_bugListItemControl);
        }
        #endregion

        #region [事件 - 界面]
        /// <summary>
        /// 当界面读取完毕时
        /// </summary>
        private void ListUiControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            /* 给BugItems属性赋值 */
            BugItems = new List<BugListItemControl>();

            BugItems.Add(BugListItemControl1);
            BugItems.Add(BugListItemControl2);
            BugItems.Add(BugListItemControl3);
            BugItems.Add(BugListItemControl4);
            BugItems.Add(BugListItemControl5);
            BugItems.Add(BugListItemControl6);
            BugItems.Add(BugListItemControl7);
            BugItems.Add(BugListItemControl8);
            BugItems.Add(BugListItemControl9);
            BugItems.Add(BugListItemControl10);

            BugItems.Add(BugListItemControl11);
            BugItems.Add(BugListItemControl12);
            BugItems.Add(BugListItemControl13);
            BugItems.Add(BugListItemControl14);
            BugItems.Add(BugListItemControl15);
            BugItems.Add(BugListItemControl16);
            BugItems.Add(BugListItemControl17);
            BugItems.Add(BugListItemControl18);
            BugItems.Add(BugListItemControl19);
            BugItems.Add(BugListItemControl20);


            /* 设置皮肤 */
            try
            {
                AppManager.Systems.ThemeSystem.Handle(AppManager.Datas.SettingsData.Theme);
            }
            catch (Exception exception)
            {
            }
        }

        #endregion

        #region [事件 - ListTip]
        /// <summary>
        /// 当点击[ListTip]中的[查看]按钮时
        /// </summary>
        private void ListTipControl_OnClickLookButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickLookButtonInListTip();
        }

        /// <summary>
        /// 当点击[ListTip]中的[关闭]按钮时
        /// </summary>
        private void ListTipControl_OnClickCloseButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            this.OnClickCloseButtonInListTip();
        }
        #endregion




        #region [私有方法 - 只能输入数字]
        /* 请参考：https://blog.csdn.net/u011695973/article/details/80984040 
           在XAML中，TextBox组件上必须这样设置：
           <TextBox 
           InputMethod.IsInputMethodEnabled="False"
           ContextMenu="{x:Null}"
           PreviewTextInput="CurrentPageNumberTextBox_PreviewTextInput"
           PreviewKeyDown="CurrentPageNumberTextBox_PreviewKeyDown"/>
        */

        /// <summary>
        /// 文本框文本输入事件
        /// </summary>
        private void CurrentPageNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");//采用正则表达式
            e.Handled = re.IsMatch(e.Text);
        }

        /// <summary>
        /// 键盘按键事件
        /// 禁用粘贴
        /// </summary>
        private void CurrentPageNumberTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyStates == Keyboard.GetKeyStates(Key.LeftCtrl) || e.KeyStates == Keyboard.GetKeyStates(Key.RightCtrl) || e.KeyStates == Keyboard.GetKeyStates(Key.V))
                e.Handled = true;
            else if (e.KeyStates == Keyboard.GetKeyStates(Key.Space))
                e.Handled = true;
            else
                e.Handled = false;
        }

        #endregion





        #region [公开方法 - ListTip]
        /// <summary>
        /// 打开or关闭ListTip
        /// </summary>
        /// <param name="_isOpen">是否打开ListTip？</param>
        /// <param name="_isAddBugCompleteTip">是否是[添加Bug成功]的提示？（如果为true，就显示[添加Bug成功]的提示；如果为false，就显示[删除Bug成功]的提示；如果为null，则不改变提示）</param>
        /// <param name="_bugData">要操作的Bug。如果是[删除Bug成功]的话，可以为null</param>
        public void OpenOrCloseListTip(bool _isOpen, bool? _isAddBugCompleteTip, BugData _bugData = null)
        {
            /* 设置数据 */
            if (_bugData!=null)
            {
                this.ListTipControl.BugData = _bugData;
            }

            /* 设置Tip的类型 */
            if (_isAddBugCompleteTip != null)
            {
                this.ListTipControl.IsAddBugCompleteTip = (bool)_isAddBugCompleteTip;
            }

            /* 打开or关闭Tip */
            this.ListTipControl.IsOpen = _isOpen;
        }
        #endregion


    }
}
