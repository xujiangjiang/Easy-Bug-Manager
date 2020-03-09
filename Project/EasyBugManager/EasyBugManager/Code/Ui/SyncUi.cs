/* By: 絮大王（xudawang@vip.163.com）
   Time：2020年1月30日03:36:22*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EasyBugManager
{
    /// <summary>
    /// [同步界面]的逻辑
    /// </summary>
    public class SyncUi
    {

        #region [公开属性 - Ui]
        /// <summary>
        /// [同步界面]的控件
        /// </summary>
        public SyncUiControl UiControl
        {
            get { return AppManager.MainWindow.SyncUiControl; }
        }
        #endregion



        #region [事件 - 按钮]
        /// <summary>
        /// 当点击[确认]按钮时
        /// </summary>
        public void ClickYesButton()
        {
            this.UiControl.IsShowSyncLog = false;//关闭[日志]
        }

        /// <summary>
        /// 当点击[取消]按钮时
        /// </summary>
        public void ClickNoButton()
        {
            this.UiControl.IsShowSyncLog = false;//关闭[日志]
        }

        /// <summary>
        /// 当点击[同步]按钮时
        /// </summary>
        public void ClickSyncButton()
        {
            this.UiControl.IsShowSyncLog = true;//打开[日志]
        }
        #endregion

        #region [事件 - 值改变]
        /// <summary>
        /// 当[同步状态类型]改变时
        /// </summary>
        /// <param name="_oldValue">旧的值</param>
        /// <param name="_newValue">新的值</param>
        public void SyncStateTypeChange(SyncStateType _oldValue, SyncStateType _newValue)
        {
            //判断值
            switch (_newValue)
            {
                //如果是[没有同步]
                case SyncStateType.NoSync:
                    break;

                //如果是[同步中]
                case SyncStateType.Syncing:
                    break;

                //如果是[同步完成]
                case SyncStateType.Synced:
                    break;
            }
        }


        /// <summary>
        /// 当[是否等待同步]改变时
        /// </summary>
        /// <param name="_oldValue">旧的值</param>
        /// <param name="_newValue">新的值</param>
        public void WaitSyncAnimationStateTypeChange(AnimationStateType _oldValue, AnimationStateType _newValue)
        {
            //如果当前不是协同合作模式
            if (UiControl.Visibility == Visibility.Collapsed)
            {
                AppManager.Systems.CollaborationSystem.SyncStateType = SyncStateType.NoSync;
            }

            //如果当前是协同合作模式
            else
            {
                //比较值
                switch (_newValue)
                {
                    //如果是[等待同步]
                    case AnimationStateType.Start:
                        break;

                    //如果是[等待同步结束了]
                    case AnimationStateType.End:
                        //进行同步
                        bool _isHaveSync = AppManager.Systems.CollaborationSystem.Sync();

                        //如果同步成功
                        if (_isHaveSync == true)
                        {
                            //修改同步状态
                            AppManager.Systems.CollaborationSystem.SyncStateType = SyncStateType.Syncing;
                        }
                        else
                        {
                            //修改同步状态
                            AppManager.Systems.CollaborationSystem.SyncStateType = SyncStateType.None;
                        }
                        break;
                }
            }
        }


        /// <summary>
        /// 当[是否显示同步图标动画]改变时
        /// </summary>
        /// <param name="_oldValue">旧的值</param>
        /// <param name="_newValue">新的值</param>
        public void SyncIconAnimationStateTypeChange(AnimationStateType _oldValue, AnimationStateType _newValue)
        {
            //如果当前不是协同合作模式
            if (UiControl.Visibility == Visibility.Collapsed)
            {
                AppManager.Systems.CollaborationSystem.SyncStateType = SyncStateType.NoSync;
            }

            //如果当前是协同合作模式
            else
            {
                //比较值
                switch (_newValue)
                {
                    //如果是[动画开始了]
                    case AnimationStateType.Start:
                        break;

                    //如果是[动画结束了]
                    case AnimationStateType.End:
                        //修改同步状态
                        AppManager.Systems.CollaborationSystem.SyncStateType = SyncStateType.Synced;
                        break;
                }
            }
        }


        /// <summary>
        /// 当[是否显示同步日志]改变时
        /// </summary>
        /// <param name="_oldValue">旧的值</param>
        /// <param name="_newValue">新的值</param>
        public void IsShowSyncLogChange(bool _oldValue, bool _newValue)
        {
            switch (_newValue)
            {
                //如果是打开[同步日志]
                case true:
                    //如果是[ListUi]打开
                    if (AppManager.Uis.ListUi.UiControl.Visibility == Visibility.Visible)
                    {
                        UiControl.TopButtonBackgroundGroupStackPanel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        UiControl.TopButtonBackgroundGroupStackPanel.Visibility = Visibility.Collapsed;
                    }

                    //打开前景的拖动
                    AppManager.Uis.OpenOrCloseDragGrid(false);
                    break;

                //如果是关闭[同步日志]
                case false:
                    //关闭前景的拖动
                    AppManager.Uis.OpenOrCloseDragGrid(true);
                    break;
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
            if (AppManager.MainWindow == null || this.UiControl == null) return;


            //显示
            switch (_isOpen)
            {
                //如果是打开
                case true:
                    //打开界面
                    this.UiControl.Visibility = Visibility.Visible;

                    //修改[Bug界面]的[返回按钮]的位置
                    Canvas.SetTop(AppManager.Uis.BugUi.UiControl.BackGrid, 70);
                    break;



                //如果是关闭
                case false:
                    //关闭界面
                    this.UiControl.Visibility = Visibility.Collapsed;

                    //修改[Bug界面]的[返回按钮]的位置
                    Canvas.SetTop(AppManager.Uis.BugUi.UiControl.BackGrid, 57);
                    break;
            }


            //清空
            switch (_isOpen)
            {
                //关闭界面
                case false:
                    this.UiControl.IsShowSyncLog = false;
                    this.UiControl.SyncIconAnimationStateType = AnimationStateType.None;
                    this.UiControl.WaitSyncAnimationStateType = AnimationStateType.None;
                    break;
            }
        }
        #endregion [公开方法 - 打开or关闭]

        #endregion

    }
}
