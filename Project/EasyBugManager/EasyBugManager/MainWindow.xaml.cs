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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyBugManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }




        #region [初始化]
        //当窗口初始化时，触发此方法
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //初始化
            AppManager.Start();
        }
        #endregion




        #region [拖动窗口 + 阻止窗口最大化]
        /// <summary>
        /// 当在窗口顶部的矩形中，按下鼠标左键的时候：拖动窗口的时候
        /// </summary>
        private void WindowTitleBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /* 当点击拖拽区域的时候，让窗口跟着移动 */
            //DragMove();

            /*当点击拖拽区域的时候，让窗口跟着移动（并且阻止[窗口拖到屏幕边缘时 自动最大化]）*/
            DragMoveWindow(e);
        }


        /// <summary>
        /// 当点击拖拽区域的时候，让窗口跟着移动
        /// （并且阻止[窗口拖到屏幕边缘时 自动最大化]）
        /// </summary>
        /// <param name="e">鼠标按钮事件</param>
        public void DragMoveWindow(MouseButtonEventArgs e)
        {
            /* 如何在Window.ResizeMode属性为CanResize的时候，阻止窗口拖动到屏幕边缘自动最大化。
               思路是当拖拽窗口时，把ResizeMode属性设置为NoResize；当拖拽结束后，把ResizeMode属性设置为CanResize

               参考文章： https://blog.csdn.net/wcc27857285/article/details/78223901
               文章作者：Bird鸟人
                (因为拖到屏幕边缘自动最大化，有个必要条件是鼠标按下去，然后拖，可以利用这个间隙将ResizeMode设置一下，然后鼠标放开之后再把ResizeMode设置回来就行了)*/
            if (e.ChangedButton == MouseButton.Left)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    var windowMode = this.ResizeMode;
                    if (this.ResizeMode != ResizeMode.NoResize)
                    {
                        this.ResizeMode = ResizeMode.NoResize;
                    }

                    this.UpdateLayout();



                    /* 当点击拖拽区域的时候，让窗口跟着移动 */
                    DragMove();



                    if (this.ResizeMode != windowMode)
                    {
                        this.ResizeMode = windowMode;
                    }

                    this.UpdateLayout();
                }
            }
        }
        #endregion

        #region [等比例缩放 + 解决拖动时窗口闪烁的问题]

        /* 参考文章：https://bbs.csdn.net/topics/390257164
           文章作者：a7066163、Hauk
           
           思路：
                【首先由Hauk提出】：
                拖拉时显示窗口内容  不勾选时：一次拖动只触发一次SizeChanged事件
                拖拉时显示窗口内容  勾选时：一次拖动只触发 N 次SizeChanged事件(n=newsize-oldsize)

                触发太频繁所以界面更新有点不对劲。
                可以使用timer来延迟更新,等用户拖好了来。
                
                
                【然后a7066163在Hauk想法的基础上做了更改】：
                虽然在在鼠标释放的慢的情况下，窗口会退回原来的大小，所以我只在处理鼠标释放的事件就行了。
                不过貌似自带边框的鼠标事件不属于窗口事件，最后我直接捕获WM_EXITSIZEMOVE消息，重载了窗口消息处理函数。
                通过定时器和WM_EXITSIZEMOVE消息处理的双重保证下，大致能实现窗口比例实时恒定的效果。
                
             
            代码如下 ↓ */


        private int lastWidth;//标识符：最后的宽度
        private int lastHeight;//标识符：最后的高度
        private float aspectRatio = (float)1480 / (float)1030;//这个属性是指 窗口的宽度和高度的比例（宽度/高度）(比如4:3)

        // 捕获窗口拖拉消息
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = HwndSource.FromVisual(this) as HwndSource;
            if (source != null)
            {
                source.AddHook(new HwndSourceHook(WinProc));
            }
        }

        public const Int32 WM_EXITSIZEMOVE = 0x0232;

        // 重载窗口消息处理函数
        private IntPtr WinProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref Boolean handled)
        {
            IntPtr result = IntPtr.Zero;
            switch (msg)
            {
                // 处理窗口消息
                case WM_EXITSIZEMOVE:
                    {
                        // 上下拖拉窗口
                        if (this.Height != lastHeight)
                        {
                            this.Width = this.Height * aspectRatio;
                        }
                        // 左右拖拉窗口
                        else if (this.Width != lastWidth)
                        {
                            this.Height = this.Width / aspectRatio;
                        }

                        //保存标识符
                        lastWidth = (int)this.Width;
                        lastHeight = (int)this.Height;



                        //赋值数据
                        if (AppManager.Datas!=null && AppManager.Datas.SettingsData!=null)
                        {
                            AppManager.Datas.SettingsData.WindowWidth = (int)this.Width;
                            AppManager.Datas.SettingsData.WindowHeight = (int) this.Height;
                        }
                        break;
                    }
            }

            return result;
        }

        #endregion




        #region [事件 - 主界面]
        /// <summary>
        /// 当点击[最小化]按钮时
        /// </summary>
        private void MainUiControl_ClickMinimizeButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.MainUi.ClickMinimizeButton();//调用逻辑
        }

        /// <summary>
        /// 当点击[关闭]按钮时
        /// </summary>
        private void MainUiControl_ClickCloseButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.MainUi.ClickCloseButton();//调用逻辑
        }

        /// <summary>
        /// 当点击[设置]按钮时
        /// </summary>
        private void MainUiControl_ClickSettingButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.MainUi.ClickSettingButton();//调用逻辑
        }

        /// <summary>
        /// 当点击[创建项目]按钮时
        /// </summary>
        private void MainUiControl_ClickCreateProjectButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.MainUi.ClickCreateProjectButton();//调用逻辑
        }

        /// <summary>
        /// 当点击[打开项目]按钮时
        /// </summary>
        private void MainUiControl_ClickOpenProjectButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.MainUi.ClickOpenProjectButton();//调用逻辑
        }
        #endregion

        #region [事件 - 列表界面]
        /// <summary>
        /// 当[页码]改变时
        /// </summary>
        private void ListUiControl_OnPageChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            AppManager.Uis.ListUi.PageChange(e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[显示个数]改变时
        /// </summary>
        private void ListUiControl_OnShowNumberChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            AppManager.Uis.ListUi.ShowNumberChange(e.OldValue,e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[搜索的文字]改变时
        /// </summary>
        private void ListUiControl_OnSearchTextChange(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            AppManager.Uis.ListUi.SearchTextChange(e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[排序方式]改变时
        /// </summary>
        private void ListUiControl_OnSortTypeChange(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ListUi.SortTypeChange();//触发事件
        }





        /// <summary>
        /// 当按下[最小化]按钮时
        /// </summary>
        private void ListUiControl_OnClickMinimizeButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ListUi.ClickMinimizeButton();//触发事件
        }

        /// <summary>
        /// 当按下[关闭]按钮时
        /// </summary>
        private void ListUiControl_OnClickCloseButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ListUi.ClickCloseButton();//触发事件
        }

        /// <summary>
        /// 当按下[设置]按钮时
        /// </summary>
        private void ListUiControl_OnClickSettingButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ListUi.ClickSettingButton();//触发事件
        }

        /// <summary>
        /// 当点击[添加Bug]按钮时
        /// </summary>
        private void ListUiControl_OnClickAddBugButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ListUi.ClickAddBugButton();//触发事件
        }

        /// <summary>
        /// 当点击[删除Bug]按钮时
        /// </summary>
        private void ListUiControl_OnClickDeleteBugButton(object sender, RoutedPropertyChangedEventArgs<BugListItemControl> e)
        {
            AppManager.Uis.ListUi.ClickDeleteBugButton(e.NewValue as BugListItemControl);//触发事件
        }

        /// <summary>
        /// 当点击[清空搜索内容]按钮时
        /// </summary>
        private void ListUiControl_OnClickClearSearchButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ListUi.ClickClearSearchButton();//触发事件
        }
        #endregion

        #region [事件 - 列表界面(BugItem)]
        /// <summary>
        /// 当点击Bug的[更多]按钮时
        /// </summary>
        private void ListUiControl_OnClickMoreButtonInBug(object sender, RoutedPropertyChangedEventArgs<BugListItemControl> e)
        {
            AppManager.Uis.ListUi.ClickMoreButtonInBug(e.NewValue as BugListItemControl);//触发事件
        }

        /// <summary>
        /// 当点击Bug的[进度]按钮时
        /// </summary>
        private void ListUiControl_OnClickProgressButtonInBug(object sender, RoutedPropertyChangedEventArgs<BugListItemControl> e)
        {
            AppManager.Uis.ListUi.ClickProgressButtonInBug(e.NewValue as BugListItemControl);//触发事件
        }

        /// <summary>
        /// 当点击Bug的[刷新]按钮时
        /// </summary>
        private void ListUiControl_OnClickRefreshButtonInBug(object sender, RoutedPropertyChangedEventArgs<BugListItemControl> e)
        {
            AppManager.Uis.ListUi.ClickRefreshButtonInBug(e.NewValue as BugListItemControl);//触发事件
        }

        /// <summary>
        /// 当点击Bug的[跳转页面]按钮时
        /// </summary>
        private void ListUiControl_OnClickGoToPageButtonInBug(object sender, RoutedPropertyChangedEventArgs<BugListItemControl> e)
        {
            AppManager.Uis.ListUi.ClickGoToPageButtonInBug(e.NewValue as BugListItemControl);//触发事件
        }

        /// <summary>
        /// 当点击Bug的[已删除]按钮时
        /// </summary>
        private void ListUiControl_OnClickDeletedButtonInBug(object sender, RoutedPropertyChangedEventArgs<BugListItemControl> e)
        {
            AppManager.Uis.ListUi.ClickDeletedButtonInBug(e.NewValue as BugListItemControl);//触发事件
        }

        /// <summary>
        /// 当双击Bug的按钮时
        /// </summary>
        private void ListUiControl_OnMouseDoubleClickCheckInBug(object sender, RoutedPropertyChangedEventArgs<BugListItemControl> e)
        {
            AppManager.Uis.ListUi.MouseDoubleClickCheckInBug(e.NewValue as BugListItemControl);//触发事件
        }





        /// <summary>
        /// 当点击右键菜单中的[删除]按钮
        /// </summary>
        private void ListUiControl_OnClickDeleteButtonInBugContextMenu(object sender, RoutedPropertyChangedEventArgs<BugListItemControl> e)
        {
            AppManager.Uis.ListUi.ClickDeleteButtonInBugContextMenu(e.NewValue as BugListItemControl);//触发事件
        }

        /// <summary>
        /// 当点击右键菜单中的[更多]按钮
        /// </summary>
        private void ListUiControl_OnClickMoreButtonInBugContextMenu(object sender, RoutedPropertyChangedEventArgs<BugListItemControl> e)
        {
            AppManager.Uis.ListUi.ClickMoreButtonInBugContextMenu(e.NewValue as BugListItemControl);//触发事件
        }

        /// <summary>
        /// 当点击右键菜单中的[进度Type]的 某一个Check时
        /// </summary>
        private void ListUiControl_OnClickProgressTypeButtonInBugContextMenu(object sender, RoutedPropertyChangedEventArgs<BugListItemControl> e)
        {
            AppManager.Uis.ListUi.ClickProgressTypeButtonInBugContextMenu(e.NewValue as BugListItemControl);//触发事件
        }

        /// <summary>
        /// 当点击右键菜单中的[优先级Type]的 某一个Check时
        /// </summary>
        private void ListUiControl_OnClickPriorityTypeButtonInBugContextMenu(object sender, RoutedPropertyChangedEventArgs<BugListItemControl> e)
        {
            AppManager.Uis.ListUi.ClickPriorityTypeButtonInBugContextMenu(e.NewValue as BugListItemControl);//触发事件
        }
        #endregion

        #region [事件 - 列表界面(ListTip)]
        /// <summary>
        /// 当点击[ListTip]中的[关闭]按钮时
        /// </summary>
        private void ListUiControl_OnClickCloseButtonInListTip(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            AppManager.Uis.ListUi.ClickCloseButtonInListTip();
        }

        /// <summary>
        /// 当点击[ListTip]中的[查看]按钮时
        /// </summary>
        private void ListUiControl_OnClickLookButtonInListTip(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //触发事件
            AppManager.Uis.ListUi.ClickLookButtonInListTip();
        }
        #endregion

        #region [事件 - Bug界面]
        /// <summary>
        /// 当点击[返回]按钮的时候
        /// </summary>
        private void BugUiControl_OnClickBackButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.BugUi.ClickBackButton();//触发事件
        }

        /// <summary>
        /// 当点击[进度]按钮的时候
        /// </summary>
        private void BugUiControl_OnClickProgressButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.BugUi.ClickProgressButton();//触发事件
        }

        /// <summary>
        /// 当点击[优先级]按钮的时候
        /// </summary>
        private void BugUiControl_OnClickPriorityButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.BugUi.ClickPriorityButton();//触发事件
        }

        /// <summary>
        /// 当点击[Bug名字]按钮的时候
        /// </summary>
        private void BugUiControl_OnClickBugNameButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.BugUi.ClickBugNameButton();//触发事件
        }




        /// <summary>
        /// 当点击[提交]按钮的时候
        /// </summary>
        private void BugUiControl_OnClickSubmitButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.BugUi.ClickSubmitButton();//触发事件
        }

        /// <summary>
        /// 当点击[选择图片]按钮的时候
        /// </summary>
        private void BugUiControl_OnClickChooseImageButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.BugUi.ClickChooseImageButton();//触发事件
        }

        /// <summary>
        /// 当点击[输入框]的[图片]按钮的时候
        /// </summary>
        private void BugUiControl_OnClickInputBoxImageButton(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            AppManager.Uis.BugUi.ClickInputBoxImageButton(e.NewValue);//触发事件
        }

        /// <summary>
        /// 当点击[输入框]的[删除图片]按钮的时候
        /// </summary>
        private void BugUiControl_OnClickInputBoxDeleteImageButton(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            AppManager.Uis.BugUi.ClickInputBoxDeleteImageButton(e.NewValue);//触发事件
        }


        /// <summary>
        /// 当[是否显示Bug的回复？]改变时
        /// </summary>
        private void BugUiControl_OnIsShowBugReplyChange(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.BugUi.IsShowBugReplyChange(e.OldValue,e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[是否显示提交按钮的动画？]改变时
        /// </summary>
        private void BugUiControl_OnIsShowSubmitButtonAnimationChange(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.BugUi.IsShowSubmitButtonAnimationChange(e.OldValue, e.NewValue);//触发事件
        }



        /// <summary>
        /// 当点击[记录列表的Item]的[删除]按钮的时候
        /// </summary>
        private void BugUiControl_OnClickRecordListItemDeleteButton(object sender, RoutedPropertyChangedEventArgs<RecordData> e)
        {
            AppManager.Uis.BugUi.ClickRecordListItemDeleteButton(e.NewValue);//触发事件
        }

        /// <summary>
        /// 当点击[记录列表的Item]的[图片]按钮的时候
        /// </summary>
        private void BugUiControl_OnClickRecordListItemImageButton(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            AppManager.Uis.BugUi.ClickRecordListItemImageButton(e.NewValue,e.Source as RecordData);//触发事件
        }
        #endregion

        #region [事件 - 设置界面]
        /// <summary>
        /// 当[语言]改变时
        /// </summary>
        private void SettingsUiControl_OnLanguageChange(object sender, RoutedPropertyChangedEventArgs<LanguageType> e)
        {
            AppManager.Uis.SettingsUi.LanguageChange(e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[声音]改变时
        /// </summary>
        private void SettingsUiControl_OnSoundChange(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SettingsUi.SoundChange(e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[皮肤]改变时
        /// </summary>
        private void SettingsUiControl_OnThemeChange(object sender, RoutedPropertyChangedEventArgs<ThemeType> e)
        {
            AppManager.Uis.SettingsUi.ThemeChange(e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[透明度]改变时
        /// </summary>
        private void SettingsUiControl_OnTransparentChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            AppManager.Uis.SettingsUi.TransparentChange(e.NewValue);//触发事件
        }






        /// <summary>
        /// 点击[关闭]按钮时
        /// </summary>
        private void SettingsUiControl_OnClickCloseButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SettingsUi.ClickCloseButton();//触发事件
        }

        /// <summary>
        /// 点击[Github]按钮时
        /// </summary>
        private void SettingsUiControl_OnClickGithubButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SettingsUi.ClickGithubButton();//触发事件
        }

        /// <summary>
        /// 点击[导出Excel]按钮时
        /// </summary>
        private void SettingsUiControl_OnClickExportButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SettingsUi.ClickExportButton();//触发事件
        }

        /// <summary>
        /// 点击[More]按钮时
        /// </summary>
        private void SettingsUiControl_OnClickMoreButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SettingsUi.ClickMoreButton();//触发事件
        }

        /// <summary>
        /// 点击[Epplus]按钮时
        /// </summary>
        private void SettingsUiControl_OnClickEpplusButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SettingsUi.ClickEpplusButton();//触发事件
        }

        /// <summary>
        /// 点击[Litjson]按钮时
        /// </summary>
        private void SettingsUiControl_OnClickLitjsonButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SettingsUi.ClickLitjsonButton();//触发事件
        }

        /// <summary>
        /// 点击[用户手册]按钮时
        /// </summary>
        private void SettingsUiControl_OnClickUserManualButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SettingsUi.ClickUserManualButton();//触发事件
        }

        /// <summary>
        /// 点击[更新日志]按钮时
        /// </summary>
        private void SettingsUiControl_OnClickUpdateLogButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SettingsUi.ClickUpdateLogButton();//触发事件
        }

        /// <summary>
        /// 点击[工具]按钮时
        /// </summary>
        private void SettingsUiControl_OnClickToolButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SettingsUi.ClickToolButton();//触发事件
        }

        /// <summary>
        /// 点击[电子邮件]按钮时
        /// </summary>
        private void SettingsUiControl_OnClickEmailButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SettingsUi.ClickEmailButton();//触发事件
        }
        #endregion

        #region [事件 - 创建项目界面]
        /// <summary>
        /// 当点击[浏览]按钮时
        /// </summary>
        private void CreateProjectUiControl_OnClickBrowseButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.CreateProjectUi.ClickBrowseButton();//触发事件
        }

        /// <summary>
        /// 当点击[确认]按钮时
        /// </summary>
        private void CreateProjectUiControl_OnClickYesButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.CreateProjectUi.ClickYesButton();//触发事件
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        private void CreateProjectUiControl_OnClickNoButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.CreateProjectUi.ClickNoButton();//触发事件
        }
        #endregion

        #region [事件 - 导出Excel界面]
        /// <summary>
        /// 当点击[浏览]按钮时
        /// </summary>
        private void ExportUiControl_OnClickBrowseButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ExportUi.ClickBrowseButton();//触发事件
        }

        /// <summary>
        /// 当点击[确认]按钮时
        /// </summary>
        private void ExportUiControl_OnClickYesButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ExportUi.ClickYesButton();//触发事件
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        private void ExportUiControl_OnClickNoButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ExportUi.ClickNoButton();//触发事件
        }
        #endregion

        #region [事件 - 创建Bug界面]
        /// <summary>
        /// 当点击[确定]按钮时
        /// </summary>
        private void CreateBugUiControl_OnClickYesButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.CreateBugUi.ClickYesButton();//触发事件
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        private void CreateBugUiControl_OnClickNoButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.CreateBugUi.ClickNoButton();//触发事件
        }

        /// <summary>
        /// 当点击[相关Bug]按钮时
        /// </summary>
        private void CreateBugUiControl_OnClickRelatedBugNameButton(object sender, RoutedPropertyChangedEventArgs<HighlightText> e)
        {
            AppManager.Uis.CreateBugUi.ClickRelatedBugNameButton(e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[优先级]改变时
        /// </summary>
        private void CreateBugUiControl_OnPriorityChange(object sender, RoutedPropertyChangedEventArgs<PriorityType> e)
        {
            AppManager.Uis.CreateBugUi.PriorityChange(e.OldValue,e.NewValue);//触发事件
        }

        /// <summary>
        /// (当[Bug的名字]改变时
        /// </summary>
        private void CreateBugUiControl_OnBugNameChange(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            AppManager.Uis.CreateBugUi.BugNameChange(e.OldValue, e.NewValue);//触发事件
        }
        #endregion

        #region [事件 - 修改Bug界面]
        /// <summary>
        /// 当点击[确定]按钮时
        /// </summary>
        private void ChangeBugUiControl_OnClickYesButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ChangeBugUi.ClickYesButton();//触发事件
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        private void ChangeBugUiControl_OnClickNoButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ChangeBugUi.ClickNoButton();//触发事件
        }

        /// <summary>
        /// 当点击[相关Bug]按钮时
        /// </summary>
        private void ChangeBugUiControl_OnClickRelatedBugNameButton(object sender, RoutedPropertyChangedEventArgs<HighlightText> e)
        {
            AppManager.Uis.ChangeBugUi.ClickRelatedBugNameButton(e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[优先级]改变时
        /// </summary>
        private void ChangeBugUiControl_OnPriorityChange(object sender, RoutedPropertyChangedEventArgs<PriorityType> e)
        {
            AppManager.Uis.ChangeBugUi.PriorityChange(e.OldValue, e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[进度]改变时
        /// </summary>
        private void ChangeBugUiControl_OnProgressChange(object sender, RoutedPropertyChangedEventArgs<ProgressType> e)
        {
            AppManager.Uis.ChangeBugUi.ProgressChange(e.OldValue, e.NewValue);//触发事件
        }

        /// <summary>
        /// (当[Bug的名字]改变时
        /// </summary>
        private void ChangeBugUiControl_OnBugNameChange(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            AppManager.Uis.ChangeBugUi.BugNameChange(e.OldValue, e.NewValue);//触发事件
        }
        #endregion

        #region [事件 - 删除Bug的Tip界面]
        /// <summary>
        /// 当点击[确定]按钮时
        /// </summary>
        private void DeleteBugTipUiControl_OnClickYesButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.DeleteBugTipUi.ClickYesButton();//触发事件
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        private void DeleteBugTipUiControl_OnClickNoButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.DeleteBugTipUi.ClickNoButton();//触发事件
        }
        #endregion

        #region [事件 - 删除记录的Tip界面]
        /// <summary>
        /// 当点击[确定]按钮时
        /// </summary>
        private void DeleteRecordTipUiControl_OnClickYesButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.DeleteRecordTipUi.ClickYesButton();//触发事件
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        private void DeleteRecordTipUiControl_OnClickNoButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.DeleteRecordTipUi.ClickNoButton();//触发事件
        }
        #endregion

        #region [事件 - Tip界面]
        /// <summary>
        /// 当点击[确定]按钮时
        /// </summary>
        private void BaseTipUiControl_OnClickYesButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.BaseTipUi.ClickYesButton();//触发事件
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        private void BaseTipUiControl_OnClickNoButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.BaseTipUi.ClickNoButton();//触发事件
        }
        #endregion

        #region [事件 - 图片界面]
        /// <summary>
        /// 当点击[关闭]按钮时
        /// </summary>
        private void ImageUiControl_OnClickCloseButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ImageUi.ClickCloseButton();//触发事件
        }

        /// <summary>
        /// 当点击[文件]按钮时
        /// </summary>
        private void ImageUiControl_OnClickFileButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ImageUi.ClickFileButton();//触发事件
        }
        #endregion

        #region [事件 - 错误界面]
        /// <summary>
        /// 当点击[确定]按钮时
        /// </summary>
        private void ErrorUiControl_OnClickYesButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ErrorUi.ClickYesButton();//触发事件
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        private void ErrorUiControl_OnClickNoButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.ErrorUi.ClickNoButton();//触发事件
        }
        #endregion

        #region [事件 - 同步界面]
        /// <summary>
        /// 当点击[确定]按钮时
        /// </summary>
        private void SyncUiControl_OnClickYesButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SyncUi.ClickYesButton();//触发事件
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        private void SyncUiControl_OnClickNoButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SyncUi.ClickNoButton();//触发事件
        }

        /// <summary>
        /// 当点击[同步]按钮时
        /// </summary>
        private void SyncUiControl_OnClickSyncButton(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SyncUi.ClickSyncButton();//触发事件
        }

        /// <summary>
        /// 当[同步状态]改变时
        /// </summary>
        private void SyncUiControl_OnSyncStateTypeChange(object sender, RoutedPropertyChangedEventArgs<SyncStateType> e)
        {
            AppManager.Uis.SyncUi.SyncStateTypeChange((SyncStateType)e.OldValue,(SyncStateType)e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[是否等待同步]改变时
        /// </summary>
        private void SyncUiControl_OnWaitSyncAnimationStateTypeChange(object sender, RoutedPropertyChangedEventArgs<AnimationStateType> e)
        {
            AppManager.Uis.SyncUi.WaitSyncAnimationStateTypeChange((AnimationStateType)e.OldValue, (AnimationStateType)e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[是否显示同步图标动画]改变时
        /// </summary>
        private void SyncUiControl_OnSyncIconAnimationStateTypeChange(object sender, RoutedPropertyChangedEventArgs<AnimationStateType> e)
        {
            AppManager.Uis.SyncUi.SyncIconAnimationStateTypeChange((AnimationStateType)e.OldValue,(AnimationStateType)e.NewValue);//触发事件
        }

        /// <summary>
        /// 当[是否显示同步日志]改变时
        /// </summary>
        private void SyncUiControl_OnIsShowSyncLogChange(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            AppManager.Uis.SyncUi.IsShowSyncLogChange((bool)e.OldValue, (bool)e.NewValue);//触发事件
        }
        #endregion



    }
}
