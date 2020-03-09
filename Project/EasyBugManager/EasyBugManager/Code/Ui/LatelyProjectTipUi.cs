/* By: 絮大王（xudawang@vip.163.com）
   Time：2020年2月20日07:33:17*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// [最近项目的提示界面]的逻辑
    /// </summary>
    public class LatelyProjectTipUi
    {
        #region [公开属性]
        /// <summary>
        /// [最近项目的提示界面]的控件
        /// </summary>
        public TipUiControl UiControl
        {
            get { return AppManager.MainWindow.LatelyProjectTipUiControl; }
        }
        #endregion


        #region [事件]
        /// <summary>
        /// 当点击[确认]按钮时
        /// </summary>
        public void ClickYesButton()
        {
            //从列表中删除这个项目
            LatelyProjectData _data = this.UiControl.Tag as LatelyProjectData;//取到当前要操作的数据
            if (_data!=null)
            {
                AppManager.Systems.LatelySystem.Remove(_data);//把这个数据从列表中删除
            }

            //关闭提示
            this.OpenOrClose(false);
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        public void ClickNoButton()
        {
            this.OpenOrClose(false);//关闭提示
        }
        #endregion


        #region [公开方法 - 打开or关闭]
        /// <summary>
        /// 打开或者关闭 界面
        /// </summary>
        /// <param name="_isOpen">是否打开？</param>
        public void OpenOrClose(bool _isOpen)
        {
            /* 界面 */
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
                    this.UiControl.Visibility = Visibility.Collapsed;//关闭界面
                    AppManager.Uis.OpenOrCloseForeground(false);//关闭前景(灰色)
                    AppManager.Uis.BaseTipUi.UiControl.Margin = new Thickness(0, 0, 0, 0);//移动界面
                    break;
            }


            /* 数据 */
            switch (_isOpen)
            {
                //如果是关闭
                case false:
                    //清空数据
                    UiControl.Tag = null;
                    UiControl.TipTitle = "";
                    UiControl.TipContent = "";
                    break;
            }

        }
        #endregion [公开方法 - 打开or关闭]
    }
}
