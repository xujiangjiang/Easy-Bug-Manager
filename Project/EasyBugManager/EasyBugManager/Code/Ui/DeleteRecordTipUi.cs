/* By: 絮大王（sukiup@163.com）
   Time：2019年12月25日15:03:31*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// [删除记录的提示界面]的逻辑
    /// </summary>
    public class DeleteRecordTipUi
    {
        #region [公开属性]
        /// <summary>
        /// [删除记录的提示界面]的控件
        /// </summary>
        public DeleteTipUiControl UiControl
        {
            get { return AppManager.MainWindow.DeleteRecordTipUiControl; }
        }
        #endregion


        #region [事件]
        /// <summary>
        /// 当点击[确认]按钮时
        /// </summary>
        public void ClickYesButton()
        {
            //获取Bug和记录
            RecordData _recordData = UiControl.Data as RecordData;

            //删除记录
            AppManager.Systems.RecordSystem.RemoveRecord(_recordData);

            //关闭界面
            this.OpenOrClose(false);

            //清空Bug
            UiControl.Data = null;
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        public void ClickNoButton()
        {
            //关闭界面
            this.OpenOrClose(false);

            //清空Bug
            UiControl.Data = null;
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
            //打开界面
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


            //删除数据
            switch (_isOpen)
            {
                case false:
                    this.UiControl.Data = null;
                    this.UiControl.Text = "";
                    break;
            }
        }
        #endregion [公开方法 - 打开or关闭]

        #endregion
    }
}
