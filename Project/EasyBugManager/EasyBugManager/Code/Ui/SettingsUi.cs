/* By: 絮大王（sukiup@163.com）
   Time：2019年11月14日11:21:18*/

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
    /// [设置界面]的逻辑
    /// </summary>
    public class SettingsUi
    {
        #region [公开属性]
        /// <summary>
        /// [设置界面]的控件
        /// </summary>
        public SettingsUiControl UiControl
        {
            get { return AppManager.MainWindow.SettingsUiControl; }
        }
        #endregion

        #region [私有属性 - 文本]
        /// <summary>
        /// [工作人员名单]的文本
        /// </summary>
        private string StaffText {
            get { return Properties.Resources.StaffText; }
        }

        /// <summary>
        /// [Epplus]的文本
        /// </summary>
        private string EpplusText
        {
            get { return Properties.Resources.EpplusText; }
        }

        /// <summary>
        /// [Litjson]的文本
        /// </summary>
        private string LitjsonText
        {
            get { return Properties.Resources.LitjsonText; }
        }

        /// <summary>
        /// [Email]的文本
        /// </summary>
        private string EmailText
        {
            get { return Properties.Resources.EmailText; }
        }
        #endregion


        #region [事件]

        #region [事件 - 值]
        /// <summary>
        /// 当[语言]改变时
        /// </summary>
        /// <param name="_currentLanguage">当前的Language属性的值</param>
        public void LanguageChange(LanguageType _currentLanguage)
        {
            //根据[语言] 重新设置 文字和图片
            AppManager.Systems.LanguageSystem.Handle(_currentLanguage);
        }

        /// <summary>
        /// 当[声音]改变时
        /// </summary>
        /// <param name="_currentSound">当前的Sound属性的值</param>
        public void SoundChange(bool _currentSound) { }

        /// <summary>
        /// 当[皮肤]改变时
        /// </summary>
        /// <param name="_currentTheme">当前的Theme属性的值</param>
        public void ThemeChange(ThemeType _currentTheme)
        {
            //根据[皮肤] 重新设置 文字和图片
            AppManager.Systems.ThemeSystem.Handle(_currentTheme);

            //根据[语言] 重新设置 文字和图片
            AppManager.Systems.LanguageSystem.Handle(AppManager.Datas.SettingsData.Language);
        }

        /// <summary>
        /// 当[透明度]改变时
        /// </summary>
        /// <param name="_currentTransparent">当前的Transparent属性的值</param>
        public void TransparentChange(int _currentTransparent) { }
        #endregion [事件 - 值]

        #region [事件 - 按钮]

        /// <summary>
        /// 点击[关闭]按钮时
        /// </summary>
        public void ClickCloseButton()
        {
            AppManager.Uis.SettingsUi.OpenOrClose(false);//关闭设置界面
        }

        /// <summary>
        /// 点击[Github]按钮时
        /// </summary>
        public void ClickGithubButton()
        {
            //调用系统默认的浏览器
            System.Diagnostics.Process.Start("https://github.com/xujiangjiang/Easy-Bug-Manager");
        }

        /// <summary>
        /// 点击[导出Excel]按钮时
        /// </summary>
        public void ClickExportButton()
        {
            /* 如果是在[主界面]，就提示用户不能导出 */
            if (AppManager.Uis.MainUi.UiControl.Visibility == Visibility.Visible)
            {
                //设置提示界面
                AppManager.Uis.BaseTipUi.UiControl.TipTitle = AppManager.Systems.LanguageSystem.ErrorTipTitle;
                AppManager.Uis.BaseTipUi.UiControl.TipContent = AppManager.Systems.LanguageSystem.UnableToExportTipContent;

                //关闭设置界面+显示提示界面
                AppManager.Uis.SettingsUi.OpenOrClose(false);
                AppManager.Uis.BaseTipUi.OpenOrClose(true);
            }

            /* 如果是不在[主界面]，就打开[导出界面] */
            else
            {
                //关闭设置界面+显示导出界面
                AppManager.Uis.SettingsUi.OpenOrClose(false);
                AppManager.Uis.ExportUi.OpenOrClose(true);
            }
        }

        /// <summary>
        /// 点击[More]按钮
        /// </summary>
        public void ClickMoreButton()
        {
            this.UiControl.StaffPanelText = StaffText;//设置文字
        }

        /// <summary>
        /// 点击[Epplus]按钮时
        /// </summary>
        public void ClickEpplusButton()
        {
            this.UiControl.StaffPanelText = EpplusText;//设置文字
        }

        /// <summary>
        /// 点击[Litjson]按钮时
        /// </summary>
        public void ClickLitjsonButton()
        {
            this.UiControl.StaffPanelText = LitjsonText;//设置文字
        }

        /// <summary>
        /// 点击[用户手册]按钮时
        /// </summary>
        public void ClickUserManualButton()
        {
            //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
            //返回值是：X:\xxx\xxx (.exe文件所在的目录)
            string _appPath = System.Environment.CurrentDirectory;

            //打开Document文件夹
            try
            {
                System.Diagnostics.Process.Start(_appPath + "/User Manual");//打开文件夹
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 点击[更新日志]按钮时
        /// </summary>
        public void ClickUpdateLogButton()
        {
            //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
            //返回值是：X:\xxx\xxx (.exe文件所在的目录)
            string _appPath = System.Environment.CurrentDirectory;

            //打开Log文件夹
            try
            {
                System.Diagnostics.Process.Start(_appPath + "/Update Log");//打开文件夹
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 点击[工具]按钮时
        /// </summary>
        public void ClickToolButton()
        {
            //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
            //返回值是：X:\xxx\xxx (.exe文件所在的目录)
            string _appPath = System.Environment.CurrentDirectory;

            //打开[工具]文件夹
            try
            {

                //获取.exe的路径
                string _exeFilePath = System.Environment.CurrentDirectory+ "/Tool/Easy Bug Manager Tool.exe";

                //打开文件
                if (_exeFilePath != null && _exeFilePath != "")
                {
                    FileInfo _fileInfo = new FileInfo(_exeFilePath);

                    if (_fileInfo.Exists == true)
                    {
                        Process _process = new Process();
                        _process.StartInfo.UseShellExecute = true;
                        _process.StartInfo.WorkingDirectory = _fileInfo.DirectoryName;
                        _process.StartInfo.FileName = _exeFilePath;

                        //打开文件夹并选中单个文件
                        _process.Start();
                    }

                }


            }
            catch (Exception e)
            {
                try
                {
                    Process.Start(_appPath + "/Tool");//打开文件夹
                }
                catch (Exception exception)
                {
                }
            }
        }

        /// <summary>
        /// 点击[电子邮件]按钮时
        /// </summary>
        public void ClickEmailButton()
        {
            this.UiControl.StaffPanelText = EmailText;//设置文字
        }
        #endregion [事件 - 按钮]

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
        }
        #endregion [公开方法 - 打开or关闭]

        #endregion
    }
}
