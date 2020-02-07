/* By: 絮大王（sukiup@163.com）
   Time：2019年11月13日08:49:56*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// 用于存放所有的界面
    /// </summary>
    public class Uis
    {
        private MainUi mainUi;//[主]界面
        private ListUi listUi;//[列表]界面
        private BugUi bugUi;//[Bug]界面
        private SyncUi syncUi;//[同步]界面

        private SettingsUi settingsUi;//[设置]界面
        private CreateProjectUi createProjectUi;//[创建项目]界面
        private ExportUi exportUi;//[导出Excel]界面
        private CreateBugUi createBugUi;//[创建Bug]界面
        private ChangeBugUi changeBugUi;//[修改Bug]界面
        private DeleteBugTipUi deleteBugTipUi;//[删除Bug的提示]界面
        private DeleteRecordTipUi deleteRecordTipUi;//[删除记录的提示]界面
        private BaseTipUi baseTipUi;//[提示]界面
        private ImageUi imageUi;//[图片]界面
        private ErrorUi errorUi;//[错误]界面



        #region 属性
        /// <summary>
        /// [主]界面
        /// </summary>
        public MainUi MainUi 
        {
            get { return mainUi; }
        }

        /// <summary>
        /// [列表]界面
        /// </summary>
        public ListUi ListUi
        {
            get { return listUi; }
        }

        /// <summary>
        /// [Bug]界面
        /// </summary>
        public BugUi BugUi
        {
            get { return bugUi; }
        }

        /// <summary>
        /// [同步]界面
        /// </summary>
        public SyncUi SyncUi
        {
            get { return syncUi; }
        }



        /// <summary>
        /// [设置]界面
        /// </summary>
        public SettingsUi SettingsUi
        {
            get { return settingsUi; }
        }

        /// <summary>
        /// [创建项目]界面
        /// </summary>
        public CreateProjectUi CreateProjectUi
        {
            get { return createProjectUi; }
        }

        /// <summary>
        /// [导出Excel]界面
        /// </summary>
        public ExportUi ExportUi
        {
            get { return exportUi; }
        }

        /// <summary>
        /// [创建Bug]界面
        /// </summary>
        public CreateBugUi CreateBugUi
        {
            get { return createBugUi; }
        }

        /// <summary>
        /// [修改Bug]界面
        /// </summary>
        public ChangeBugUi ChangeBugUi
        {
            get { return changeBugUi; }
        }

        /// <summary>
        /// [删除Bug的提示]界面
        /// </summary>
        public DeleteBugTipUi DeleteBugTipUi
        {
            get { return deleteBugTipUi; }
        }

        /// <summary>
        /// [删除记录的提示]界面
        /// </summary>
        public DeleteRecordTipUi DeleteRecordTipUi
        {
            get { return deleteRecordTipUi; }
        }

        /// <summary>
        /// [提示]界面
        /// </summary>
        public BaseTipUi BaseTipUi
        {
            get { return baseTipUi; }
        }

        /// <summary>
        /// [图片]界面
        /// </summary>
        public ImageUi ImageUi
        {
            get { return imageUi; }
        }

        /// <summary>
        /// [错误]界面
        /// </summary>
        public ErrorUi ErrorUi
        {
            get { return errorUi; }
        }
        #endregion


        #region 构造方法
        public Uis() 
        {
            mainUi = new MainUi();
            listUi = new ListUi();
            bugUi = new BugUi();
            syncUi = new SyncUi();

            settingsUi = new SettingsUi();
            createProjectUi = new CreateProjectUi();
            exportUi = new ExportUi();
            createBugUi = new CreateBugUi();
            changeBugUi = new ChangeBugUi();
            deleteBugTipUi = new DeleteBugTipUi();
            deleteRecordTipUi = new DeleteRecordTipUi();
            baseTipUi = new BaseTipUi();
            imageUi = new ImageUi();
            errorUi = new ErrorUi();
        }
        #endregion


        #region [公开方法]

        /// <summary>
        /// 打开/关闭 前景
        /// (灰色的前景用于遮挡按钮)
        /// </summary>
        /// <param name="_isOpen">是否打开？</param>
        public void OpenOrCloseForeground(bool _isOpen)
        {
            switch (_isOpen)
            {
                //如果是打开
                case true:

                    /* 打开前景(灰色) */
                    //打开[前景]的拖动窗口
                    OpenOrCloseDragGrid(false);


                    //如果[主界面]是打开的
                    if (MainUi.UiControl.Visibility == Visibility.Visible)
                    {
                        MainUi.UiControl.ForegroundCanvas.Visibility = Visibility.Visible;
                        ListUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        BugUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        SyncUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        ImageUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                    }

                    //如果[图片界面]是打开的
                    else if (ImageUi.UiControl.Visibility == Visibility.Visible)
                    {
                        MainUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        ListUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        BugUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        SyncUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        ImageUi.UiControl.ForegroundCanvas.Visibility = Visibility.Visible;
                    }

                    //如果[同步界面]是打开的
                    else if (SyncUi.UiControl.Visibility == Visibility.Visible)
                    {
                        MainUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        ListUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        BugUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        SyncUi.UiControl.ForegroundCanvas.Visibility = Visibility.Visible;
                        ImageUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;

                        if (AppManager.Uis.ListUi.UiControl.Visibility == Visibility.Visible)
                        {
                            SyncUi.UiControl.TopButtonForegroundGroupStackPanel.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            SyncUi.UiControl.TopButtonForegroundGroupStackPanel.Visibility = Visibility.Collapsed;
                        }
                    }

                    //如果[列表界面]是打开的
                    else if (ListUi.UiControl.Visibility == Visibility.Visible)
                    {
                        MainUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        ListUi.UiControl.ForegroundCanvas.Visibility = Visibility.Visible;
                        BugUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        SyncUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        ImageUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                    }

                    //如果[Bug界面]是打开的
                    else if (BugUi.UiControl.Visibility == Visibility.Visible)
                    {
                        MainUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        ListUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        BugUi.UiControl.ForegroundCanvas.Visibility = Visibility.Visible;
                        SyncUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                        ImageUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                    }
                    break;



                //如果是关闭
                case false:
                    //打开[背景]的拖动窗口
                    OpenOrCloseDragGrid(true);

                    /* 关闭前景(灰色) */
                    MainUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                    ListUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                    BugUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                    SyncUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                    ImageUi.UiControl.ForegroundCanvas.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        /// <summary>
        /// 打开/关闭 背景
        /// (白色的背景)
        /// </summary>
        /// <param name="_isOpen">是否打开？</param>
        public void OpenOrCloseBackground(bool _isOpen)
        {
            /* 打开/关闭界面 */
            switch (_isOpen)
            {
                //如果是打开
                case true:
                    AppManager.MainWindow.BackgroudBorder.Visibility = Visibility.Visible;//打开背景
                    break;

                //如果是关闭
                case false:
                    AppManager.MainWindow.BackgroudBorder.Visibility = Visibility.Collapsed;//关闭背景
                    break;
            }
        }


        /// <summary>
        /// 打开or关闭 拖动网格
        /// </summary>
        /// <param name="_isOpenBackgroundDragGrid">是否打开[背景]的拖动网格</param>
        public void OpenOrCloseDragGrid(bool _isOpenBackgroundDragGrid)
        {
            switch (_isOpenBackgroundDragGrid)
            {
                case true:
                    //打开[背景]的拖动网格（关闭[前景]的拖动网格）
                    AppManager.MainWindow.DragBackgroudGrid.Visibility = Visibility.Visible;
                    AppManager.MainWindow.DragForegroundGrid.Visibility = Visibility.Collapsed;
                    break;

                case false:
                    //关闭[背景]的拖动网格（打开[前景]的拖动网格）
                    AppManager.MainWindow.DragBackgroudGrid.Visibility = Visibility.Collapsed;
                    AppManager.MainWindow.DragForegroundGrid.Visibility = Visibility.Visible;
                    break;
            }
        }

        #endregion
    }
}
