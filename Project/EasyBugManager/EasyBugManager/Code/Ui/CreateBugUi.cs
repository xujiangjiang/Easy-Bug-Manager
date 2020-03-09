/* By: 絮大王（sukiup@163.com）
   Time：2019年11月14日14:54:32*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// [创建Bug界面]的逻辑
    /// </summary>
    public class CreateBugUi
    {

        #region [公开属性]
        /// <summary>
        /// [创建Bug界面]的控件
        /// </summary>
        public CreateBugUiControl UiControl
        {
            get { return AppManager.MainWindow.CreateBugUiControl; }
        }
        #endregion


        #region [事件]

        #region [事件 - 按钮]
        /// <summary>
        /// 当点击[确认]按钮时
        /// </summary>
        public void ClickYesButton()
        {
            /* 如果Bug名为null */
            if (UiControl.BugName == null || UiControl.BugName == "")
            {
                //显示提示
                UiControl.TipString = AppManager.Systems.LanguageSystem.NoBugNameTip;
                return;
            }


            /* 如果填写了BugName，就创建Bug */
            AppManager.Systems.BugSystem.AddBug(UiControl.BugName, UiControl.PriorityType);


            /* 关闭此界面，关闭MainUi，打开ListUi */
            this.OpenOrClose(false);
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        public void ClickNoButton()
        {
            //关闭此界面
            this.OpenOrClose(false);
        }
        #endregion [事件 - 按钮]

        #region [事件 - 相关Bug]

        /// <summary>
        /// 当点击[相关Bug]按钮时
        /// </summary>
        /// <param name="_bugName">点击的Bug的名字</param>
        public void ClickRelatedBugNameButton(HighlightText _bugName)
        {
            UiControl.BugName = _bugName.Text;//修改Bug的名字
        }

        #endregion [事件 - 相关Bug]

        #region [事件 - 值改变]
        /// <summary>
        /// 当[优先级]改变时
        /// </summary>
        /// <param name="_oldValue">旧的优先级</param>
        /// <param name="_newValue">新的优先级</param>
        public void PriorityChange(PriorityType _oldValue, PriorityType _newValue) { }


        /// <summary>
        /// 当[Bug的名字]改变时
        /// </summary>
        /// <param name="_oldValue">旧的名字</param>
        /// <param name="_newValue">新的名字</param>
        public void BugNameChange(string _oldValue, string _newValue)
        {
            //去调用Related系统的Related方法，找到相关的Bug
            AppManager.Systems.RelatedSystem.Related(UiControl.BugName);
        }
        #endregion [事件 - 值改变]

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
            UiControl.BugName = "";
            UiControl.PriorityType = PriorityType.Low;
            UiControl.TipString = "";

        }
        #endregion [公开方法 - 打开or关闭]

        #endregion

    }
}
