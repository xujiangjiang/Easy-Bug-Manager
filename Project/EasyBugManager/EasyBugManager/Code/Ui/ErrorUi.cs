/* By: 絮大王（xudawang@vip.163.com）
   Time：2019年12月26日19:13:06*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// [错误界面]的逻辑
    /// </summary>
    public class ErrorUi
    {
        #region [公开属性]
        /// <summary>
        /// [错误界面]的控件
        /// </summary>
        public ErrorUiControl UiControl
        {
            get { return AppManager.MainWindow.ErrorUiControl; }
        }
        #endregion


        #region [事件]

        /// <summary>
        /// 当点击[确认]按钮时
        /// </summary>
        public void ClickYesButton()
        {
            this.OpenOrClose(false);//关闭提示
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        public void ClickNoButton()
        {
            this.OpenOrClose(false);//关闭提示
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
            if (AppManager.MainWindow == null || this.UiControl == null) return;



            switch (_isOpen)
            {
                //如果是打开
                case true:
                    //打开界面
                    this.UiControl.Visibility = Visibility.Visible;

                    //打开前景(灰色)
                    AppManager.Uis.OpenOrCloseForeground(true);

                    //移动界面
                    if (AppManager.Uis.MainUi.UiControl.Visibility == Visibility.Visible)//如果主界面是打开的
                    {
                        AppManager.Uis.BaseTipUi.UiControl.Margin = new Thickness(-95, 15, 0, 0);
                    }
                    else
                    {
                        AppManager.Uis.BaseTipUi.UiControl.Margin = new Thickness(0, 0, 0, 0);
                    }
                    break;

                //如果是关闭
                case false:
                    //关闭界面
                    this.UiControl.Visibility = Visibility.Collapsed;

                    //关闭前景(灰色)
                    AppManager.Uis.OpenOrCloseForeground(false);

                    //移动界面
                    AppManager.Uis.BaseTipUi.UiControl.Margin = new Thickness(0, 0, 0, 0);
                    break;
            }


            //修改数据
            AppManager.Uis.ErrorUi.UiControl.TipTitle = AppManager.Systems.LanguageSystem.ErrorTipTitle;
        }
        #endregion [公开方法 - 打开or关闭]

        #endregion
    }
}
