/* By: 絮大王（sukiup@163.com）
   Time：2019年11月13日09:50:54*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// [列表界面]的逻辑
    /// </summary>
    public class ListUi
    {

        #region [公开属性]
        /// <summary>
        /// [列表界面]的控件
        /// </summary>
        public ListUiControl UiControl
        {
            get { return AppManager.MainWindow.ListUiControl; }
        }
        #endregion


        #region [事件]

        #region [事件 - 按钮]
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
            //关闭项目
            AppManager.Systems.ProjectSystem.CloseProject();

            //关闭此界面+打开主界面
            OpenOrClose(false);
            AppManager.Uis.MainUi.OpenOrClose(true);
        }

        /// <summary>
        /// 当点击[设置]按钮时
        /// </summary>
        public void ClickSettingButton()
        {
            AppManager.Uis.SettingsUi.OpenOrClose(true);//打开设置界面
        }

        /// <summary>
        /// 当点击[添加Bug]按钮时
        /// </summary>
        public void ClickAddBugButton()
        {
            //打开创建Bug界面
            AppManager.Uis.CreateBugUi.OpenOrClose(true);
        }

        /// <summary>
        /// 当点击[删除Bug]按钮时
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件（Tag属性里有BugData）</param>
        public void ClickDeleteBugButton(BugListItemControl _source)
        {
            //如果用户选择了Bug
            if (_source != null)
            {
                //取到BugData
                BugItemData _bugItemData = _source.Tag as BugItemData;
                BugData _bugData = _bugItemData.Data;

                //如果[还要提示]
                if (AppManager.Datas.AppData.IsNotAgainShowDeleteBugTip == false)
                {
                    //显示删除Bug界面
                    AppManager.Uis.DeleteBugTipUi.UiControl.Data = _bugData;
                    AppManager.Uis.DeleteBugTipUi.UiControl.Text = StringTool.Clamp(_bugData.Name.Text, 50);
                    AppManager.Uis.DeleteBugTipUi.OpenOrClose(true);
                }

                //如果[不再提示]
                else
                {
                    //直接删除Bug
                    AppManager.Systems.BugSystem.DeleteBug(_bugData);
                }
            }

        }

        /// <summary>
        /// 当点击[清空搜索内容]按钮时
        /// </summary>
        public void ClickClearSearchButton()
        {
            //清空[搜索文字]
            UiControl.SearchString = "";
        }
        #endregion [事件 - 按钮]

        #region [事件 - 值改变]
        /// <summary>
        /// 当[页码]改变时
        /// </summary>
        /// <param name="_currentPage">当前的页码</param>
        public void PageChange(int _currentPage)
        {
            AppManager.Systems.PageSytem.Turn(_currentPage);//显示新的页码
        }

        /// <summary>
        /// 当[显示个数]改变时
        /// </summary>
        /// <param name="_oldCheckedIndex">之前的选中项的索引</param>
        /// <param name="_newCheckedIndex">新的选中项的索引</param>
        public void ShowNumberChange(int _oldCheckedIndex, int _newCheckedIndex)
        {
            AppManager.Systems.PageSytem.CalculatedPagesNumber();//重新计算页数
            AppManager.Systems.PageSytem.Turn(1);//显示第1页
        }

        /// <summary>
        /// 当[搜索的文字]的选中项改变时
        /// </summary>
        /// <param name="_currentSearchText">当前的搜索文字</param>
        public void SearchTextChange(string _currentSearchText)
        {
            AppManager.Systems.SearchSystem.Filter();//进行过滤
            AppManager.Systems.PageSytem.CalculatedPagesNumber();//重新计算页数
            AppManager.Systems.PageSytem.Turn(1);//显示第1页
        }

        /// <summary>
        /// 当[排序方式]的选中项改变时
        /// </summary>
        public void SortTypeChange()
        {
            AppManager.Systems.SortSystem.Sort();//重新排序
            AppManager.Systems.SearchSystem.Filter();//过滤
            AppManager.Systems.PageSytem.Turn(1);//显示第1页
        }
        #endregion [事件 - 其他]

        #region [事件 - BugItem]
        /// <summary>
        /// 当点击Bug的[更多]按钮时
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件（Tag属性里有BugData）</param>
        public void ClickMoreButtonInBug(BugListItemControl _source)
        {
            //获取Bug数据
            BugItemData _bugItemData = _source.Tag as BugItemData;
            BugData _bugData = _bugItemData.Data;


            //更新Bug的[显示数据]
            AppManager.Datas.OtherData.ShowBugItemData = _bugItemData;
            //更新Bug的[显示记录]
            AppManager.Systems.RecordSystem.SetShowRecords(_bugData, AppManager.Datas.OtherData.IsShowBugReply);


            //打开Bug界面
            this.OpenOrClose(false);
            AppManager.Uis.BugUi.OpenOrClose(true);
        }

        /// <summary>
        /// 当点击Bug的[进度]按钮时
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件（Tag属性里有BugData）</param>
        public void ClickProgressButtonInBug(BugListItemControl _source)
        {
            //获取Bug数据
            BugItemData _bugItemData = _source.Tag as BugItemData;
            BugData _bugData = _bugItemData.Data;

            //打开Bug界面
            AppManager.Uis.ChangeBugUi.UiControl.BugData = _bugData;
            AppManager.Uis.ChangeBugUi.OpenOrClose(true);
        }

        /// <summary>
        /// 当点击Bug的[刷新]按钮时
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件（Tag属性里有BugData）</param>
        public void ClickRefreshButtonInBug(BugListItemControl _source)
        {
            //刷新当前页面
            AppManager.Systems.PageSytem.Refresh();
        }

        /// <summary>
        /// 当点击Bug的[跳转页面]按钮时
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件（Tag属性里有BugData）</param>
        public void ClickGoToPageButtonInBug(BugListItemControl _source)
        {
            //获取Bug数据
            BugItemData _bugItemData = _source.Tag as BugItemData;
            BugData _bugData = _bugItemData.Data;

            //跳转到Bug所在的页面，并选中Bug
            AppManager.Systems.PageSytem.Turn(_bugData);
        }

        /// <summary>
        /// 当点击Bug的[已删除]按钮时
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件（Tag属性里有BugData）</param>
        public void ClickDeletedButtonInBug(BugListItemControl _source)
        {
            //刷新当前页面
            AppManager.Systems.PageSytem.Refresh();
        }

        /// <summary>
        /// 当双击Bug的按钮时
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件（Tag属性里有BugData）</param>
        public void MouseDoubleClickCheckInBug(BugListItemControl _source)
        {
            //进入BugUi(和点击[More]按钮一样)
            ClickMoreButtonInBug(_source);
        }





        /// <summary>
        /// 当点击右键菜单中的[删除]按钮
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件（Tag属性里有BugData）</param>
        public void ClickDeleteButtonInBugContextMenu(BugListItemControl _source)
        {
            ClickDeleteBugButton(_source);
        }

        /// <summary>
        /// 当点击右键菜单中的[更多]按钮
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件（Tag属性里有BugData）</param>
        public void ClickMoreButtonInBugContextMenu(BugListItemControl _source)
        {
            ClickMoreButtonInBug(_source);
        }

        /// <summary>
        /// 当点击右键菜单中的[进度Type]的 某一个Check时
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件（Tag属性里有BugData）</param>
        public void ClickProgressTypeButtonInBugContextMenu(BugListItemControl _source)
        {
            //获取Bug数据
            BugItemData _bugItemData = _source.Tag as BugItemData;
            BugData _bugData = _bugItemData.Data;

            //先通知Bug修改了进度
            AppManager.Systems.BugSystem.OnChangeBugProgress(_bugData);

            //通知Bug已经修改
            AppManager.Systems.BugSystem.ChangeBug(_bugData);
        }

        /// <summary>
        /// 当点击右键菜单中的[优先级Type]的 某一个Check时
        /// </summary>
        /// <param name="_source">触发这个事件的Bug控件（Tag属性里有BugData）</param>
        public void ClickPriorityTypeButtonInBugContextMenu(BugListItemControl _source)
        {
            //获取Bug数据
            BugItemData _bugItemData = _source.Tag as BugItemData;
            BugData _bugData = _bugItemData.Data;

            //通知Bug已经修改
            AppManager.Systems.BugSystem.ChangeBug(_bugData);
        }

        #endregion [事件 - BugItem]

        #region [事件 - ListTip]
        /// <summary>
        /// 当点击ListTip中的[关闭]按钮
        /// </summary>
        public void ClickCloseButtonInListTip()
        {
            //关闭Tip
            UiControl.OpenOrCloseListTip(false, null);
        }

        /// <summary>
        /// 当点击ListTip中的[查看]按钮
        /// </summary>
        public void ClickLookButtonInListTip()
        {
            //取到Tip中的BugData
            BugData _bugData = UiControl.ListTipControl.BugData;

            //关闭Tip
            UiControl.OpenOrCloseListTip(false, null);

            //跳转到Bug所在的页面
            AppManager.Systems.PageSytem.Turn(_bugData);
        }
        #endregion

        #endregion


        #region [公开方法]

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
                    this.UiControl.Visibility = Visibility.Visible;//打开界面
                    AppManager.Uis.OpenOrCloseBackground(true);//打开背景
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
