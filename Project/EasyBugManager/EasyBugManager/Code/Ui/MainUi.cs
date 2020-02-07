/* By: 絮大王（sukiup@163.com）
   Time：2019年11月13日08:53:02*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace EasyBugManager
{
    /// <summary>
    /// [主界面]的逻辑
    /// </summary>
    public class MainUi
    {
        #region [公开属性]
        /// <summary>
        /// [主界面]的控件
        /// </summary>
        public MainUiControl UiControl
        {
            get { return AppManager.MainWindow.MainUiControl; }
        }
        #endregion


        #region [事件]
        /// <summary>
        /// 当点击[最小化]按钮时
        /// </summary>
        public void ClickMinimizeButton() 
        {
            AppManager.MainWindow.WindowState = WindowState.Minimized; //最小化此窗口
        }

        /// <summary>
        /// 当点击[关闭]按钮时
        /// </summary>
        public void ClickCloseButton()
        {
            AppManager.MainApp.Shutdown();//关闭应用程序
        }

        /// <summary>
        /// 当点击[设置]按钮时
        /// </summary>
        public void ClickSettingButton()
        {
            AppManager.Uis.SettingsUi.OpenOrClose(true);//打开设置界面
        }

        /// <summary>
        /// 当点击[创建项目]按钮时
        /// </summary>
        public void ClickCreateProjectButton()
        {
            AppManager.Uis.CreateProjectUi.OpenOrClose(true);//打开创建项目界面
        }

        /// <summary>
        /// 当点击[打开项目]按钮时
        /// </summary>
        public void ClickOpenProjectButton()
        {
            /* OpenFileDialog类，用于打开文件对话框 */
            OpenFileDialog _openFileDialog = new OpenFileDialog();

            /* 设置文件过滤 */
            _openFileDialog.Filter = "项目文件|*.bugs";

            /* 设置其他 */
            _openFileDialog.Title = "打开项目";

            /* 调用OpenFileDialog.ShowDialog()方法，显示[打开文件对话框]
               这个方法有一个bool?类型的返回值
               返回值为true，代表用户选择了文件；否则就代表用户没有选择文件 */
            bool? _isChooseFile = _openFileDialog.ShowDialog();

            /* 获取用户打开的文件 的路径 */
            if (_isChooseFile == true)
            {
                /* [读取文件] */
                //把路径赋值给ProjectSystem系统的SavePath属性
                string _filePath = _openFileDialog.FileName;

                //读取项目（_isLoadOk代表是否读取成功？）
                bool _isLoadProjectOk = AppManager.Systems.ProjectSystem.LoadProject(_filePath);//读取项目

                //读取Bug和记录
                AppManager.Systems.BugSystem.LoadBugs(AppManager.Systems.CollaborationSystem.ModeType);//读取所有Bug
                AppManager.Systems.RecordSystem.LoadRecords(AppManager.Systems.CollaborationSystem.ModeType);//读取所有Record

                /* [测试代码] */
                AppManager.Test();

                /* [更新Ui] */
                AppManager.Systems.BugSystem.CalculatedBugsNumber();//计算个数
                AppManager.Systems.SortSystem.Sort();//排序
                AppManager.Systems.SearchSystem.Filter();//过滤
                AppManager.Systems.PageSytem.CalculatedPagesNumber();//重新计算页码
                AppManager.Systems.PageSytem.Turn(1);//显示第一页

                /* 打开界面 （判断是否读取成功？） */
                if (_isLoadProjectOk == true)
                {
                    //关闭Main界面，打开List界面
                    AppManager.Uis.MainUi.OpenOrClose(false);
                    AppManager.Uis.ListUi.OpenOrClose(true);
                }
                else
                {
                    //打开Tip界面
                    AppManager.Uis.BaseTipUi.UiControl.TipTitle = AppManager.Systems.LanguageSystem.ErrorTipTitle;
                    AppManager.Uis.BaseTipUi.UiControl.TipContent = AppManager.Systems.LanguageSystem.OpenProjectErrorTipContent;
                    AppManager.Uis.BaseTipUi.OpenOrClose(true);
                }



                /* [进行备份] */
                if (_isLoadProjectOk == true)
                {
                    //备份工程
                    AppManager.Systems.BackupSystem.BackupProject();

                    //备份Bug
                    if (AppManager.Datas.ProjectData.BugDatas.Count > 0)
                    {
                        AppManager.Systems.BackupSystem.BackupBug();
                    }

                    //备份Record
                    if (AppManager.Datas.ProjectData.RecordDatas.Count > 0)
                    {
                        AppManager.Systems.BackupSystem.BackupRecord();
                    }
                }




                /* [删除的文件] */
                if (_isLoadProjectOk == true)
                {
                    //删除所有要删除的文件
                    AppManager.Systems.DeleteSystem.DeleteFile();
                }

            }
        }
        #endregion


        #region [公开方法 - 打开or关闭]
        /// <summary>
        /// 打开或者关闭 界面
        /// </summary>
        /// <param name="_isOpen">是否打开？</param>
        public void OpenOrClose(bool _isOpen) 
        {
            switch (_isOpen)
            {
                //如果是打开
                case true:
                    this.UiControl.Visibility = Visibility.Visible;//打开界面
                    AppManager.Uis.OpenOrCloseBackground(false);//关闭背景
                    break;

                //如果是关闭
                case false:
                    this.UiControl.Visibility = Visibility.Collapsed;//关闭界面
                    break;
            }
        }
        #endregion
    }
}
