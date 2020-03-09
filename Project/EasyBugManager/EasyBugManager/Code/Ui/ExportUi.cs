/* By: 絮大王（sukiup@163.com）
   Time：2019年11月14日13:00:03*/

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
    /// [导出Excel界面]的逻辑
    /// </summary>
    public class ExportUi
    {
        #region [公开属性]
        /// <summary>
        /// [导出Excel界面]的控件
        /// </summary>
        public ExportUiControl UiControl
        {
            get { return AppManager.MainWindow.ExportUiControl; }
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
            DialogResult _dialogResult = _folderBrowserDialog.ShowDialog();

            /* 获取用户打开的文件 的路径 */
            if (_dialogResult == DialogResult.OK)
            {
                //把用户选择的文件夹，赋值给ExportLocation属性
                string _folderPath = _folderBrowserDialog.SelectedPath + "/"
                                                                       + AppManager.Systems.ProjectSystem.ProjectData.FileName
                                                                       + " - "
                                                                       + DateTimeTool.DateTimeToString(DateTime.Now, TimeFormatType.YearMonthDayHourMinuteSecondMillisecond)
                                                                       + ".xlsx";
                UiControl.ExportLocation = _folderPath;
            }

        }

        /// <summary>
        /// 当点击[确认]按钮时
        /// </summary>
        public void ClickYesButton()
        {
            /* 如果路径为null */
            if (UiControl.ExportLocation == null || UiControl.ExportLocation == "")
            {
                //显示提示
                UiControl.TipString = AppManager.Systems.LanguageSystem.NoSaveLocationTip;
                return;
            }


            /* 如果填写了路径，就导出为Excel文件 */
            bool _isOk = AppManager.Systems.ExportSystem.ExportExcel(UiControl.ExportLocation);

            //如果导出不成功
            if (_isOk == false)
            {
                //显示提示
                UiControl.TipString = AppManager.Systems.LanguageSystem.UnknownErrorTip;
            }
            //如果导出成功
            else
            {
                //打开[Tip界面]:显示导出成功的提示
                AppManager.Uis.BaseTipUi.UiControl.TipTitle = AppManager.Systems.LanguageSystem.ExportSucceededTipTitle;
                AppManager.Uis.BaseTipUi.UiControl.TipContent = UiControl.ExportLocation;
                this.OpenOrClose(false);
                AppManager.Uis.BaseTipUi.OpenOrClose(true);
            }
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        public void ClickNoButton()
        {
            this.OpenOrClose(false);//关闭界面
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
            switch (_isOpen)
            {
                //如果是打开
                case true:
                    this.UiControl.Visibility = Visibility.Visible;//打开界面
                    AppManager.Uis.OpenOrCloseForeground(true);//打开前景(灰色)
                    break;

                //如果是关闭
                case false:
                    this.UiControl.Visibility = Visibility.Collapsed;//关闭界面
                    AppManager.Uis.OpenOrCloseForeground(false);//关闭前景(灰色)
                    break;
            }

            /* 清空数据 */
            UiControl.ExportLocation = "";
            UiControl.TipString = "";
        }
        #endregion [公开方法 - 打开or关闭]

        #endregion
    }
}
