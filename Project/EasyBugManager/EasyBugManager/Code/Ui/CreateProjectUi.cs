/* By: 絮大王（sukiup@163.com）
   Time：2019年11月14日11:59:42*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;


namespace EasyBugManager
{
    /// <summary>
    /// [创建项目]的逻辑
    /// </summary>
    public class CreateProjectUi
    {
        #region [公开属性]
        /// <summary>
        /// [创建项目界面]的控件
        /// </summary>
        public CreateProjectUiControl UiControl
        {
            get { return AppManager.MainWindow.CreateProjectUiControl; }
        }
        #endregion


        #region [事件]
        /// <summary>
        /// 当点击[浏览]按钮时
        /// </summary>
        public void ClickBrowseButton()
        {
            /* FolderBrowserDialog类，用于打开文件夹对话框 */
            FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();

            /* 调用OpenFileDialog.ShowDialog()方法，显示[打开文件对话框]
               这个方法有一个bool?类型的返回值
               返回值为Ok，代表用户选择了文件；否则就代表用户没有选择文件 */
            DialogResult _dialogResult =  _folderBrowserDialog.ShowDialog();

            /* 获取用户打开的文件 的路径 */
            if (_dialogResult == DialogResult.OK)
            {
                //把用户选择的文件夹，赋值给SaveLocation属性
                string _folderPath = _folderBrowserDialog.SelectedPath;
                UiControl.SaveLocation = _folderPath;
            }
        }

        /// <summary>
        /// 当点击[确认]按钮时
        /// </summary>
        public void ClickYesButton()
        {
            /* 如果项目名为null */
            if (UiControl.ProjectName == null || UiControl.ProjectName == "")
            {
                //显示提示
                UiControl.TipString = AppManager.Systems.LanguageSystem.NoProjectNameTip;
                return;
            }

            /* 如果路径为null */
            if (UiControl.SaveLocation == null || UiControl.SaveLocation == "")
            {
                //显示提示
                UiControl.TipString = AppManager.Systems.LanguageSystem.NoSaveLocationTip;
                return;
            }





            /* 获取项目的模式 */
            ModeType _modeType = ModeType.Default;
            if (UiControl.IsCollaborationMode == true)
            {
                _modeType = ModeType.Collaboration;
            }

            /* 如果填写了项目名和路径，就创建项目 */
            bool _isCreate = AppManager.Systems.ProjectSystem.CreateProject(UiControl.SaveLocation,UiControl.ProjectName,_modeType);

            //如果创建不成功
            if (_isCreate == false)
            {
                //显示提示
                UiControl.TipString = AppManager.Systems.LanguageSystem.UnknownErrorTip;
            }
            //如果创建成功
            else
            {
                //关闭此界面，关闭MainUi，打开ListUi
                this.OpenOrClose(false);
                AppManager.Uis.MainUi.OpenOrClose(false);
                AppManager.Uis.ListUi.OpenOrClose(true);
            }

            //显示[协同合作功能测试版]提示
            if (_isCreate == true && _modeType == ModeType.Collaboration)
            {
                AppManager.Uis.BaseTipUi.UiControl.TipTitle = AppManager.Systems.LanguageSystem.CollaborativeModeTestTipTitle;
                AppManager.Uis.BaseTipUi.UiControl.TipContent = AppManager.Systems.LanguageSystem.CollaborativeModeTestTipContent;
                AppManager.Uis.BaseTipUi.OpenOrClose(true);
            }
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        public void ClickNoButton()
        {
            this.OpenOrClose(false);//关闭此界面
        }
        #endregion


        #region [公开方法]

        #region [公开方法 - 打开or关闭]
        /// <summary>
        /// 打开或者关闭 界面
        /// </summary>
        /// <param name="_isOpen">是否打开？</param>
        public void OpenOrClose(bool _isOpen)
        {
            /* 打开界面 */
            switch (_isOpen)
            {
                //如果是打开
                case true:
                    //打开界面
                    this.UiControl.Visibility = Visibility.Visible;

                    //打开前景(灰色)
                    AppManager.Uis.OpenOrCloseForeground(true);
                    break;

                //如果是关闭
                case false:
                    //关闭界面
                    this.UiControl.Visibility = Visibility.Collapsed;

                    //关闭前景(灰色)
                    AppManager.Uis.OpenOrCloseForeground(false);
                    break;
            }


            /* 关闭时，重置界面 */
            switch (_isOpen)
            {
                case false:
                    UiControl.SaveLocation = "";
                    UiControl.ProjectName = "";
                    UiControl.TipString = "";
                    UiControl.IsShowAdvancedOption = false;
                    UiControl.IsCollaborationMode = false;
                    break;
            }
        }
        #endregion [公开方法 - 打开or关闭]

        #endregion
    }
}
