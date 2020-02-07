/* By: 絮大王（xudawang@vip.163.com）
   Time：2019年11月1日11:27:32*/

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
    /// Record（记录）的数据
    /// </summary>
    public class RecordData : INotifyPropertyChanged
    {
        /* 属性：Id(编号)
                 BugId(Bug的编号)（这个Record属于哪个Bug？）
                 ReplyId(回复的编号)（用Bug中的TemperamentId和ReplyId，可以获取到Bug回复的内容）

                 Content(内容)
                 Time(时间)
                 Images(图片)(路径：ImageId.jpg)

                 IsDelete(是否删除？)（true代表已删除，false代表未删除）
                 */



        private string content;//内容
        private DateTime time;//时间
        private ObservableCollection<string> images;// 图片 (路径：ImageId.jpg)
        private bool isDelete;//是否删除？（true代表已删除，false代表未删除）

        /* 不保存的字段 */
        private RecordItemData bearRecordItemData;//[Bear说的话]的ItemData
        private RecordItemData bugRecordItemData;//[Bug回复的话]的ItemData
        



        #region [属性]
        /// <summary>
        /// 编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Bug的编号（这个Record属于哪个Bug？）
        /// </summary>
        public long BugId { get; set; }

        /// <summary>
        /// 回复的编号（用Bug中的TemperamentId和ReplyId，可以获取到Bug回复的内容）
        /// </summary>
        public int ReplyId { get; set; }




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

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                BearRecordItemData.Content = value;
                PropertyChange("Content");
            }
        }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
                bearRecordItemData.TimeInYMDHMS = DateTimeTool.DateTimeToString(value.ToLocalTime(), TimeFormatType.YearMonthDayHourMinuteSecond);
                PropertyChange("Time");
            }
        }

        /// <summary>
        /// 图片 (路径：ImageId.jpg)
        /// </summary>
        public ObservableCollection<string> Images
        {
            get { return images; }
            set
            {
                images = value;
                BearRecordItemData.ImagePaths = AppManager.Systems.ImageSystem.GetImagePaths(images); //设置Bear的ImagePaths属性
            }
        }
        #endregion

        #region [属性 - 不保存]
        /// <summary>
        /// [Bear说的话]的ItemData
        /// </summary>
        public RecordItemData BearRecordItemData
        {
            get { return bearRecordItemData; }
            set
            {
                bearRecordItemData = value;
                PropertyChange("BearRecordItemData");
            }
        }

        /// <summary>
        /// [Bug回复的话]的ItemData
        /// </summary>
        public RecordItemData BugRecordItemData
        {
            get { return bugRecordItemData; }
            set
            {
                bugRecordItemData = value;
                PropertyChange("BugRecordItemData");
            }
        }
        #endregion

        #region [构造方法]
        public RecordData()
        {
            Id = -1;
            BugId = -1;
            ReplyId = -1;

            BearRecordItemData = new RecordItemData() { Type = RecordType.Bear, Data = this };
            BugRecordItemData = new RecordItemData() { Type = RecordType.Bug, Data = this };

            IsDelete = false;
            Images = new ObservableCollection<string>();
        }
        #endregion




        #region [静态方法]
        /// <summary>
        /// 复制一个RecordData对象
        /// </summary>
        /// <param name="_recordData">要复制哪个？</param>
        /// <returns>复制出来的RecordData对象</returns>
        public static RecordData Copy(RecordData _recordData)
        {
            if (_recordData == null)
            {
                return null;
            }

            else
            {
                RecordData _copyRecordData = new RecordData();

                _copyRecordData.Id = _recordData.Id;
                _copyRecordData.BugId = _recordData.BugId;
                _copyRecordData.ReplyId = _recordData.ReplyId;
                _copyRecordData.Content = _recordData.Content;
                _copyRecordData.Time = _recordData.Time;
                _copyRecordData.IsDelete = _recordData.IsDelete;

                _copyRecordData.Images = new ObservableCollection<string>();
                for (int i = 0; i < _recordData.Images.Count; i++)
                {
                    _copyRecordData.Images.Add(_recordData.Images[i]);
                }


                return _copyRecordData;
            }

        }



        /// <summary>
        /// 比较2个Record数据
        /// </summary>
        /// <param name="_compareType">是比较所有的属性，还是只比较编号？</param>
        /// <param name="_recordData1">第1个Record数据</param>
        /// <param name="_recordData2">第2个Record数据</param>
        /// <returns>2个Record数据是否相同？</returns>
        public static bool Compare(CompareType _compareType, RecordData _recordData1, RecordData _recordData2)
        {
            if (_recordData1 == null || _recordData2 == null) return false;



            //是否相同？
            bool _isSame = false;


            //比较
            switch (_compareType)
            {
                case CompareType.Id:
                    if (_recordData1.Id == _recordData2.Id)
                    {
                        _isSame = true;
                    }
                    else
                    {
                        _isSame = false;
                    }
                    break;

                case CompareType.All:
                    if (_recordData1.Id == _recordData2.Id && _recordData1.BugId == _recordData2.BugId &&
                        _recordData1.ReplyId == _recordData2.ReplyId && _recordData1.Content == _recordData2.Content &&
                        _recordData1.Time == _recordData2.Time && _recordData1.IsDelete == _recordData2.IsDelete)
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




        /// <summary>
        /// 验证完整性
        /// （验证1个Record的完整性。
        /// 如果Record是完整的，代表这个Record是有效的；
        /// 如果Record是不完整的，代表Record文件还没有同步完，或者是Record文件已损坏）
        /// </summary>
        /// <param name="_recordData">要验证的Record</param>
        /// <returns>Record是否是完整的</returns>
        public static bool VerifyIntegrity(RecordData _recordData)
        {
            if (_recordData == null||
                _recordData.Id < 0 || _recordData.BugId <0 || _recordData.ReplyId <0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
