/* By: 絮大王（xudawang@vip.163.com）
   Time：2020年2月19日07:01:31*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace EasyBugManager
{
    /// <summary>
    /// [最近的项目界面]的逻辑
    /// </summary>
    public class LatelyProjectUi
    {
        #region [公开属性]
        /// <summary>
        /// [最近的项目界面]的控件
        /// </summary>
        public LatelyProjectUiControl UiControl
        {
            get { return AppManager.MainWindow.LatelyProjectUiControl; }
        }
        #endregion


        #region [事件 - Item]

        /// <summary>
        /// 当点击[列表的Item]的[Base]按钮的时候
        /// </summary>
        /// <param name="_source">触发事件的LatelyProjectData对象</param>
        public void ClickListItemBaseButton(LatelyProjectData _source)
        {
            //如果文件存在
            if (File.Exists(_source.Path) == true)
            {
                //读取项目
                AppManager.Uis.MainUi.LoadProjectAll(_source.Path);
            }
            
            //如果文件不存在
            else
            {
                //提示：是否把这个数据从文件中移除？
                AppManager.Uis.LatelyProjectTipUi.UiControl.TipTitle = AppManager.Systems.LanguageSystem.LatelyProjectTipTitle;
                AppManager.Uis.LatelyProjectTipUi.UiControl.TipContent = AppManager.Systems.LanguageSystem.LatelyProjectTipContent + "\n" +
                                                                         "\" "+StringTool.Clamp(_source.Name,20)+ " \"";

                //把数据传递给提示界面
                AppManager.Uis.LatelyProjectTipUi.UiControl.Tag = _source;

                //打开提示界面
                AppManager.Uis.LatelyProjectTipUi.OpenOrClose(true);
            }
        }


        /// <summary>
        /// 当点击[列表的Item]的[打开文件夹]按钮的时候
        /// </summary>
        /// <param name="_source">触发事件的LatelyProjectData对象</param>
        public void ClickListItemOpenFolderButton(LatelyProjectData _source)
        {
            //取到文件的信息
            FileInfo _fileInfo = new FileInfo(_source.Path);

            //如果文件存在
            if (_fileInfo.Exists == true)
            {
                //打开文件夹
                try
                {
                    System.Diagnostics.Process.Start(_fileInfo.Directory.FullName);//打开文件夹
                }
                catch (Exception e)
                {
                }
            }

            //如果文件不存在
            else
            {
                //提示：是否把这个数据从文件中移除？
                AppManager.Uis.LatelyProjectTipUi.UiControl.TipTitle = AppManager.Systems.LanguageSystem.LatelyProjectTipTitle;
                AppManager.Uis.LatelyProjectTipUi.UiControl.TipContent = AppManager.Systems.LanguageSystem.LatelyProjectTipContent + "\n" +
                                                                         "\" " + StringTool.Clamp(_source.Name, 20) + " \"";

                //把数据传递给提示界面
                AppManager.Uis.LatelyProjectTipUi.UiControl.Tag = _source;

                //打开提示界面
                AppManager.Uis.LatelyProjectTipUi.OpenOrClose(true);
            }
        }


        /// <summary>
        /// 当点击[列表的Item]的[从列表中移除]按钮的时候
        /// </summary>
        /// <param name="_source">触发事件的LatelyProjectData对象</param>
        public void ClickListItemRemoveButton(LatelyProjectData _source)
        {
            //从列表中删除这个项目
            if (_source != null)
            {
                AppManager.Systems.LatelySystem.Remove(_source);//把这个数据从列表中删除
            }
        }

        #endregion

        #region [事件 - 按钮]

        /// <summary>
        /// 当点击[折叠]按钮的时候
        /// </summary>
        public void ClickFoldButton()
        {
            //显示[最近]按钮
            AppManager.Datas.SettingsData.IsShowLatelyUi = false;

            //隐藏[最近]面板
            AppManager.Uis.LatelyProjectUi.UiControl.Visibility = Visibility.Collapsed;
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
            /* 打开/关闭界面 */
            switch (_isOpen)
            {
                //如果是打开
                case true:
                    //如果有数据
                    if (AppManager.Datas.SettingsData.IsShowLatelyUi == false)
                    {
                        this.UiControl.Visibility = Visibility.Collapsed;//关闭界面
                    }
                    
                    //如果没有数据
                    else
                    {
                        this.UiControl.Visibility = Visibility.Visible;//打开界面
                    }
                    break;

                //如果是关闭
                case false:
                    this.UiControl.Visibility = Visibility.Collapsed;//关闭界面
                    break;
            }
        }
        #endregion [公开方法 - 打开or关闭]

        #endregion
    }
}
