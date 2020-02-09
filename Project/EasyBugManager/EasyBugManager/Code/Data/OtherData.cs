/* By: 絮大王（xudawang@vip.163.com）
   Time：2019年11月24日05:52:16*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 其他的数据
    /// (一些需要绑定，但不需要储存的数据)
    /// </summary>
    public class OtherData: INotifyPropertyChanged
    {

        /* Bug相关 */
        private ObservableCollection<BugItemData> showBugItemDatas;//显示的Bug列表（属于ListUi）
        private BugItemData showBugItemData;//显示的Bug数据（属于BugUi）

        /* Record相关 */
        private ObservableCollection<RecordItemData> showRecordItemDatas;//显示的Record列表（属于BugUi）

        /* 列表界面 */
        private int allBugTotalNumber;//Bug的总数
        private int undoneBugTotalNumber;//未完成的Bug数量
        private int lowUndoneBugTotalNumber;//低优先级并且未完成 的Bug数量
        private int midUndoneBugTotalNumber;//中优先级并且未完成 的Bug数量
        private int highUndoneBugTotalNumber;//高优先级并且未完成 的Bug数量

        /* Bug界面 */
        private bool isShowBugReply;//是否显示Bug的回复？
        private bool isShowSubmitButtonAnimation;//是否显示提交按钮的动画？

        /* 修改or创建界面 */
        private ObservableCollection<HighlightText> showRelatedBugNames;//要显示的 相关的Bug的名字（属于创建BugUi、修改BugUi）
        private int bugNameMaxLength;//Bug名字 的最大长度(最大个数)

        /* 删除的提示界面 */
        private bool isNotAgainShowDeleteBugTip;//是否不再显示[删除Bug的提示]？（如果为true，代表不再提示；如果为false，代表再次提示）
        private bool isNotAgainShowDeleteRecordTip;//是否不再显示[删除记录的提示]？（如果为true，代表不再提示；如果为false，代表再次提示）

        /* 同步界面 */
        private string syncLogText;//同步的日志文字（所有同步的日志）
        private int syncNumber;//同步的次数
        private DateTime lastSyncDateTime;//最后一次同步的时间
        private string lastSyncTime;//最后一次同步的时间（年月日-时分秒）
        private string syncedTime;//同步完成的时间（时分秒）
        private SyncStateType syncStateType;//同步状态的类型





        #region [公开属性 - Bug相关]
        /// <summary>
        /// 显示的Bug列表（属于ListUi）
        /// </summary>
        public ObservableCollection<BugItemData> ShowBugItemDatas
        {
            get { return showBugItemDatas; }
            set
            {
                showBugItemDatas = value;
                PropertyChange("ShowBugItemDatas");
            }
        }

        /// <summary>
        /// 显示的Bug数据（属于BugUi）
        /// </summary>
        public BugItemData ShowBugItemData
        {
            get { return showBugItemData; }
            set
            {
                showBugItemData = value;
                PropertyChange("ShowBugItemData");
            }
        }
        #endregion

        #region [公开属性 - Record相关]
        /// <summary>
        /// 显示的Record列表（属于BugUi）
        /// (showRecords属性和records属性区别是，showRecords集合的最后，多一个Type为none的RecordData对象。
        /// 这样做的原因是，showRecords属性在Bug界面显示的时候，最后一个元素和最下面之间，有个空间)
        /// </summary>
        public ObservableCollection<RecordItemData> ShowRecordItemDatas
        {
            get { return showRecordItemDatas; }
            set
            {
                showRecordItemDatas = value;
                PropertyChange("ShowRecordItemDatas");
            }
        }
        #endregion

        #region [公开属性 - 列表界面]
        /// <summary>
        /// Bug的总数
        /// </summary>
        public int AllBugTotalNumber
        {
            get { return allBugTotalNumber; }
            set
            {
                allBugTotalNumber = value;
                PropertyChange("AllBugTotalNumber");
            }
        }

        /// <summary>
        /// 未完成的Bug数量
        /// </summary>
        public int UndoneBugTotalNumber
        {
            get { return undoneBugTotalNumber; }
            set
            {
                undoneBugTotalNumber = value;
                PropertyChange("UndoneBugTotalNumber");
            }
        }

        /// <summary>
        /// 低优先级并且未完成 的Bug数量
        /// </summary>
        public int LowUndoneBugTotalNumber
        {
            get { return lowUndoneBugTotalNumber; }
            set
            {
                lowUndoneBugTotalNumber = value;
                PropertyChange("LowUndoneBugTotalNumber");
            }
        }

        /// <summary>
        /// 中优先级并且未完成 的Bug数量
        /// </summary>
        public int MidUndoneBugTotalNumber
        {
            get { return midUndoneBugTotalNumber; }
            set
            {
                midUndoneBugTotalNumber = value;
                PropertyChange("MidUndoneBugTotalNumber");
            }
        }

        /// <summary>
        /// 高优先级并且未完成 的Bug数量
        /// </summary>
        public int HighUndoneBugTotalNumber
        {
            get { return highUndoneBugTotalNumber; }
            set
            {
                highUndoneBugTotalNumber = value;
                PropertyChange("HighUndoneBugTotalNumber");
            }
        }

        #endregion

        #region [公开属性 - Bug界面]

        /// <summary>
        /// 是否显示Bug的回复？
        /// </summary>
        public bool IsShowBugReply
        {
            get { return isShowBugReply; }
            set
            {
                isShowBugReply = value;
                PropertyChange("IsShowBugReply");
            }
        }

        /// <summary>
        /// 是否显示提交按钮的动画？
        /// </summary>
        public bool IsShowSubmitButtonAnimation
        {
            get { return isShowSubmitButtonAnimation; }
            set
            {
                isShowSubmitButtonAnimation = value;
                PropertyChange("IsShowSubmitButtonAnimation");
            }
        }
        #endregion

        #region [公开属性 - 修改or创建界面]

        /// <summary>
        /// 要显示的 相关的Bug的名字
        /// （属于创建BugUi、修改BugUi）
        /// </summary>
        public ObservableCollection<HighlightText> ShowRelatedBugNames
        {
            get { return showRelatedBugNames; }
            set
            {
                showRelatedBugNames = value;
                PropertyChange("ShowRelatedBugNames");//通知Ui更新
            }
        }

        /// <summary>
        /// Bug名字 的最大长度(最大个数)
        /// </summary>
        public int BugNameMaxLength
        {
            get { return bugNameMaxLength; }
            set
            {
                bugNameMaxLength = value;
                PropertyChange("BugNameMaxLength");//通知Ui更新
            }
        }
        #endregion

        #region [公开属性 - 删除的提示界面]
        /// <summary>
        /// 是否再次提示？ - 是否不再显示[删除Bug的提示]？（如果为true，代表不再提示；如果为false，代表再次提示）
        /// </summary>
        public bool IsNotAgainShowDeleteBugTip
        {
            get { return isNotAgainShowDeleteBugTip; }
            set
            {
                isNotAgainShowDeleteBugTip = value;
                PropertyChange("IsNotAgainShowDeleteBugTip");
            }
        }

        /// <summary>
        /// 是否再次提示？ - 是否不再显示[删除记录的提示]？（如果为true，代表不再提示；如果为false，代表再次提示）
        /// </summary>
        public bool IsNotAgainShowDeleteRecordTip
        {
            get { return isNotAgainShowDeleteRecordTip; }
            set
            {
                isNotAgainShowDeleteRecordTip = value;
                PropertyChange("IsNotAgainShowDeleteRecordTip");
            }
        }
        #endregion

        #region [公开属性 - 同步界面]

        /// <summary>
        /// 同步的日志（所有同步的日志）
        /// </summary>
        public string SyncLogText
        {
            get { return syncLogText; }
            set
            {
                syncLogText = value;
                PropertyChange("SyncLogText");
            }
        }

        /// <summary>
        /// 同步的次数
        /// </summary>
        public int SyncNumber
        {
            get { return syncNumber; }
            set
            {
                syncNumber = value;
                PropertyChange("SyncNumber");
            }
        }

        /// <summary>
        /// 最后一次同步的时间
        /// </summary>
        public DateTime LastSyncDateTime
        {
            get { return lastSyncDateTime; }
            set
            {
                lastSyncDateTime = value;
                LastSyncTime = DateTimeTool.DateTimeToString(value.ToLocalTime(), TimeFormatType.YearMonthDayHourMinuteSecond);
                SyncedTime = DateTimeTool.DateTimeToString(value.ToLocalTime(), TimeFormatType.HourMinuteSecond);
                PropertyChange("LastSyncDateTime");
            }
        }

        /// <summary>
        /// 最后一次同步的时间（年月日-时分秒）
        /// </summary>
        public string LastSyncTime
        {
            get { return lastSyncTime; }
            set
            {
                lastSyncTime = value;
                PropertyChange("LastSyncTime");
            }
        }

        /// <summary>
        /// 同步完成的时间（时分秒）
        /// </summary>
        public string SyncedTime
        {
            get { return syncedTime; }
            set
            {
                syncedTime = value;
                PropertyChange("SyncedTime");
            }
        }

        /// <summary>
        /// 同步状态的类型
        /// </summary>
        public SyncStateType SyncStateType
        {
            get { return syncStateType; }
            set
            {
                syncStateType = value;
                PropertyChange("SyncStateType");
            }
        }

        #endregion






        #region [构造函数]

        public OtherData()
        {
            ShowBugItemDatas = new ObservableCollection<BugItemData>();
            ShowBugItemData = null;

            ShowRecordItemDatas = new ObservableCollection<RecordItemData>();

            AllBugTotalNumber = 0;
            UndoneBugTotalNumber = 0;
            LowUndoneBugTotalNumber = 0;
            MidUndoneBugTotalNumber = 0;
            HighUndoneBugTotalNumber = 0;

            IsShowBugReply = true;
            IsShowSubmitButtonAnimation = false;

            IsNotAgainShowDeleteBugTip = false;
            IsNotAgainShowDeleteRecordTip = false;

            BugNameMaxLength = 100;

            SyncLogText = "";
            SyncStateType = SyncStateType.NoSync;
        }

        #endregion



        #region 数据的双向绑定-更新方法

        /// <summary>
        /// 当属性改变的时候，就触发此方法
        /// </summary>
        /// <param name="propertyName">发生改变的属性的名字</param>
        private void PropertyChange(string propertyName)
        {
            if (PropertyChanged != null)//如果此事件被监听
            {
                //就发送通知
                //参数1：是哪个数据类的对象发生了改变？
                //参数2：发生改变的属性名
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 系统会自动监听此事件
        /// 如果此事件触发了，系统就会去通知相应的控件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion 数据的双向绑定-更新方法
    }
}
