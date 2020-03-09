/* By: 絮大王（xudawang@vip.163.com）
   Time：2019年10月16日11:00:32*/

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
    /// Bug的数据
    /// </summary>
    public class BugData : INotifyPropertyChanged
    {
        /* 属性：Id(编号)
                 Name(名称)
                 Progress(完成度)
                 Priority(优先级)
                 CreateTime(创建时间)
                 SolveTime(完成时间)
                 UpdateTime(更新时间)
                 UpdateNumber(更新次数)

                 TemperamentId(性格数据的编号)（虫子的性格）
                 IsDelete(是否删除？)（true代表已删除，false代表未删除）
                 
            属性（不保存）：ItemData（BugItemData数据，用于ItemControl的数据绑定）*/



        private HighlightText name;//名称
        private ProgressType progress;//完成度
        private PriorityType priority;//优先级
        private DateTime createTime;//创建时间
        private DateTime solveTime;//完成时间
        private DateTime updateTime;//更新时间
        private int updateNumber;//更新次数

        private bool isDelete;//是否删除？（true代表已删除，false代表未删除）

        /* 不保存的数据 */
        private BugItemData itemData;//BugItemData数据，用于ItemControl的数据绑定





        #region [属性 - 保存]
        /// <summary>
        /// 编号（唯一）
        /// （如果Id为-1，就代表这个Bug不存在）
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public HighlightText Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChange("Name");
            }
        }

        /// <summary>
        /// 完成度
        /// </summary>
        public ProgressType Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                PropertyChange("Progress");
            }
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public PriorityType Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                PropertyChange("Priority");
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set
            {
                createTime = value;
                itemData.CreateTimeInYMD = DateTimeTool.DateTimeToString(value.ToLocalTime(), TimeFormatType.YearMonthDay);
                itemData.CreateTimeInYMDHM = DateTimeTool.DateTimeToString(value.ToLocalTime(), TimeFormatType.YearMonthDayHourMinute);
                PropertyChange("CreateTime");
            }
        }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime SolveTime
        {
            get { return solveTime; }
            set
            {
                solveTime = value;
                itemData.SolveTimeInYMDHM = DateTimeTool.DateTimeToString(value.ToLocalTime(), TimeFormatType.YearMonthDayHourMinute);
                PropertyChange("SolveTime");
            }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set
            {
                updateTime = value;
                itemData.UpdateTimeInYMD = DateTimeTool.DateTimeToString(value.ToLocalTime(), TimeFormatType.YearMonthDay);
                PropertyChange("UpdateTime");
            }
        }

        /// <summary>
        /// 更新次数
        /// </summary>
        public int UpdateNumber
        {
            get { return updateNumber; }
            set
            {
                updateNumber = value;
                PropertyChange("UpdateNumber");
            }
        }



        /// <summary>
        /// 性格数据的编号（虫子的性格）
        /// </summary>
        public int TemperamentId { get; set; }

        /// <summary>
        /// 是否删除？（true代表已删除，false代表未删除）
        /// </summary>
        public bool IsDelete
        {
            get { return isDelete; }
            set
            {
                isDelete = value;
                PropertyChange("IsDelete");
            }
        }
        #endregion

        #region [属性 - 不保存]
        /// <summary>
        /// BugItemData数据，用于ItemControl的数据绑定
        /// </summary>
        public BugItemData ItemData
        {
            get { return itemData; }
            set
            {
                itemData = value;
                PropertyChange("ItemData");
            }
        }
        #endregion



        #region 构造函数
        public BugData()
        {
            Id = -1;
            itemData = new BugItemData() { Data = this };

            name = new HighlightText();
            progress = ProgressType.None;
            priority = PriorityType.None;

            TemperamentId = -1;
            isDelete = false;
        }
        #endregion



        #region [静态方法]
        /// <summary>
        /// 复制一个BugData对象
        /// </summary>
        /// <param name="_bugData">要复制哪个？</param>
        /// <returns>复制出来的BugData对象</returns>
        public static BugData Copy(BugData _bugData)
        {
            if (_bugData == null)
            {
                return null;
            }

            else
            {
                BugData _copyBugData = new BugData();

                _copyBugData.Id = _bugData.Id;
                _copyBugData.Name = _bugData.Name;
                _copyBugData.Progress = _bugData.Progress;
                _copyBugData.Priority = _bugData.Priority;
                _copyBugData.CreateTime = _bugData.CreateTime;
                _copyBugData.SolveTime = _bugData.SolveTime;
                _copyBugData.UpdateTime = _bugData.UpdateTime;
                _copyBugData.UpdateNumber = _bugData.UpdateNumber;
                _copyBugData.TemperamentId = _bugData.TemperamentId;
                _copyBugData.IsDelete = _bugData.IsDelete;

                return _copyBugData;
            }

        }


        /// <summary>
        /// 验证完整性
        /// （验证1个Bug的完整性。
        /// 如果Bug是完整的，代表这个Bug是有效的；
        /// 如果Bug是不完整的，代表Bug文件还没有同步完，或者是Bug文件已损坏）
        /// </summary>
        /// <param name="_bugData">要验证的Bug</param>
        /// <returns>Bug是否是完整的</returns>
        public static bool VerifyIntegrity(BugData _bugData)
        {
            if (_bugData == null ||
                _bugData.Id < 0 ||
                _bugData.Priority == PriorityType.None ||
                _bugData.Progress == ProgressType.None ||
                _bugData.TemperamentId < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 比较2个Bug数据
        /// </summary>
        /// <param name="_compareType">是比较所有的属性，还是只比较编号？</param>
        /// <param name="_bugData1">第1个Bug数据</param>
        /// <param name="_bugData2">第2个Bug数据</param>
        /// <returns>2个Bug数据是否相同？</returns>
        public static bool Compare(CompareType _compareType, BugData _bugData1, BugData _bugData2)
        {
            if (_bugData1 == null || _bugData2 == null) return false;



            //是否相同？
            bool _isSame = false;

            //比较
            switch (_compareType)
            {
                case CompareType.Id:
                    if (_bugData1.Id == _bugData2.Id)
                    {
                        _isSame = true;
                    }
                    else
                    {
                        _isSame = false;
                    }
                    break;

                case CompareType.All:
                    if (_bugData1.Id == _bugData2.Id && _bugData1.Name.Text == _bugData2.Name.Text &&
                        _bugData1.Progress == _bugData2.Progress && _bugData1.Priority == _bugData2.Priority &&
                        _bugData1.CreateTime == _bugData2.CreateTime && _bugData1.SolveTime == _bugData2.SolveTime &&
                        _bugData1.UpdateTime == _bugData2.UpdateTime && _bugData1.UpdateNumber == _bugData2.UpdateNumber &&
                        _bugData1.IsDelete == _bugData2.IsDelete)
                    {
                        _isSame = true;
                    }
                    else
                    {
                        _isSame = false;
                    }
                    break;
            }


            //返回值
            return _isSame;
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
