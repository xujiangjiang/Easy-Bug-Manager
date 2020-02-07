/* By: 絮大王（sukiup@163.com）
   Time：2019年12月2日04:25:09*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// [图片界面]的逻辑
    /// </summary>
    public class ImageUi
    {

        #region [公开属性]
        /// <summary>
        /// [图片界面]的控件
        /// </summary>
        public ImageUiControl UiControl
        {
            get { return AppManager.MainWindow.ImageUiControl; }
        }
        #endregion


        #region [事件]

        /// <summary>
        /// 当点击[关闭]按钮时
        /// </summary>
        public void ClickCloseButton()
        {
            //关闭这个界面
            this.OpenOrClose(false);
        }

        /// <summary>
        /// 当点击[文件]按钮时
        /// </summary>
        public void ClickFileButton()
        {
            bool _isOpenFile = false;//是否打开了文件？

            //打开文件
            try
            {
                //获取图片的路径
                string _filePath = UiControl.ImagePath;

                //打开文件
                if (_filePath!=null && _filePath!="")
                {
                    FileInfo _fileInfo = new FileInfo(_filePath);

                    if (_fileInfo.Exists == true)
                    {
                        Process _process = new Process();
                        _process.StartInfo.UseShellExecute = true;
                        _process.StartInfo.WorkingDirectory = _fileInfo.DirectoryName;
                        _process.StartInfo.FileName = "rundll32.exe";
                        _process.StartInfo.Arguments = @"C:\WINDOWS\system32\shimgvw.dll,ImageView_Fullscreen " + _fileInfo.FullName;

                        //打开文件夹并选中单个文件
                        _process.Start();

                        //标记为[已打开了文件]
                        _isOpenFile = true;
                    }

                }
                
            }
            catch (Exception e)
            {
            }

            //输出错误
            if (_isOpenFile == false)
            {
                //输出错误（提示：无法打开文件）
                AppManager.Uis.BaseTipUi.UiControl.TipTitle = AppManager.Systems.LanguageSystem.ErrorTipTitle;
                AppManager.Uis.BaseTipUi.UiControl.TipContent = AppManager.Systems.LanguageSystem.NotOpenFileErrorTip;
                AppManager.Uis.BaseTipUi.OpenOrClose(true);
            }
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
            /* 打开界面or关闭界面 */
            switch (_isOpen)
            {
                //如果是打开
                case true:
                    //打开界面
                    this.UiControl.Visibility = Visibility.Visible;

                    //打开前景的拖动
                    AppManager.Uis.OpenOrCloseDragGrid(false);
                    break;

                //如果是关闭
                case false:
                    //关闭界面
                    this.UiControl.Visibility = Visibility.Collapsed;

                    //关闭前景的拖动
                    AppManager.Uis.OpenOrCloseDragGrid(true);
                    break;
            }

            /* 清空数据 */
            switch (_isOpen)
            {
                //如果是关闭
                case false:
                    UiControl.ImagePath = "";
                    break;
            }

        }
        #endregion [公开方法 - 打开or关闭]

        #endregion

    }
}
