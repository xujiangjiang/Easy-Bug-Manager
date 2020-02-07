/* By: 絮大王（sukiup@163.com）
   Time：2019年11月14日22:30:29*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 记录的基础数据
    /// (用于保存和读取)
    /// </summary>
    public class RecordBaseData
    {
        /* 属性：Id(编号)
                 BugId(Bug的编号)（这个Record属于哪个Bug？）
                 ReplyId(回复的编号)（用Bug中的TemperamentId和ReplyId，可以获取到Bug回复的内容）

                 Content(内容)
                 Time(时间)
                 Images(图片)(路径)
                 IsDelete(是否删除？)（true代表已删除，false代表未删除）*/


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
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public List<int> Time { get; set; }

        /// <summary>
        /// 图片（路径）
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// 是否删除？（true代表已删除，false代表未删除）
        /// </summary>
        public bool IsDelete { get; set; }
        #endregion


        #region [构造方法]

        public RecordBaseData()
        {
            Id = -1;
            BugId = -1;
            ReplyId = -1;

            Content = "";
            Time = new List<int>();
            Images = new List<string>();
            IsDelete = false;
        }

        #endregion


        #region [BaseData转Data]
        /// <summary>
        /// 把[BaseData对象]转换为[Data对象]
        /// </summary>
        /// <param name="_baseData">要转换的BaseData对象</param>
        /// <returns>转换后的Data对象</returns>
        public static RecordData BaseDataToData(RecordBaseData _baseData)
        {
            if (_baseData !=null)
            {
                RecordData _data = new RecordData();


                _data.Id = _baseData.Id;
                _data.BugId = _baseData.BugId;
                _data.ReplyId = _baseData.ReplyId;
                _data.Content = _baseData.Content;
                _data.Time = new DateTime(_baseData.Time[0], _baseData.Time[1], _baseData.Time[2], _baseData.Time[3], _baseData.Time[4], _baseData.Time[5]);
                _data.Images = ObservableCollectionTool.ListToObservableCollection(_baseData.Images);
                _data.IsDelete = _baseData.IsDelete;


                return _data;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 把[Data对象]转换为[BaseData对象]
        /// </summary>
        /// <param name="_data">要转换的Data对象</param>
        /// <returns>转换后的BaseData对象</returns>
        public static RecordBaseData DataToBaseData(RecordData _data)
        {
            if (_data!=null)
            {
                RecordBaseData _baseData = new RecordBaseData();


                _baseData.Id = _data.Id;
                _baseData.BugId = _data.BugId;
                _baseData.ReplyId = _data.ReplyId;
                _baseData.Content = _data.Content;
                _baseData.Time = new List<int>() { _data.Time.Year, _data.Time.Month, _data.Time.Day, _data.Time.Hour, _data.Time.Minute, _data.Time.Second };
                _baseData.Images = ObservableCollectionTool.ObservableCollectionToList(_data.Images);
                _baseData.IsDelete = _data.IsDelete;


                return _baseData;
            }
            else
            {
                return null;
            }
        }
        #endregion

    }
}
