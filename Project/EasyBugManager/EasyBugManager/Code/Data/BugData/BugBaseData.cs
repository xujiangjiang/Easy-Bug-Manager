/* By: 絮大王（xudawang@vip.163.com）
   Time：2019年11月23日03:02:43*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// Bug的基础数据
    /// (用于保存和读取)
    /// </summary>
    public class BugBaseData
    {
        #region [属性]
        /// <summary>
        /// 编号（如果Id为-1，就代表这个Bug不存在）
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 完成度
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 创建时间（年、月、日、时、分、秒）
        /// </summary>
        public List<int> CreateTime { get; set; }

        /// <summary>
        /// 完成时间（年、月、日、时、分、秒）
        /// </summary>
        public List<int> SolveTime { get; set; }

        /// <summary>
        /// 更新时间（年、月、日、时、分、秒）
        /// </summary>
        public List<int> UpdateTime { get; set; }

        /// <summary>
        /// 更新次数
        /// </summary>
        public int UpdateNumber { get; set; }



        /// <summary>
        /// 性格的编号（虫子的性格）
        /// </summary>
        public int TemperamentId { get; set; }

        /// <summary>
        /// 是否删除？（true代表已删除，false代表未删除）
        /// </summary>
        public bool IsDelete { get; set; }
        #endregion


        #region [构造方法]

        public BugBaseData()
        {
            Id = -1;
            Name = "";
            Progress = 0;
            Priority = 0;
            CreateTime = new List<int>();
            SolveTime = new List<int>();
            UpdateTime = new List<int>();
            UpdateNumber = 0;
            TemperamentId = -1;
            IsDelete = false;
        }

        #endregion


        #region [BaseData转Data]
        /// <summary>
        /// 把[BaseData对象]转换为[Data对象]
        /// </summary>
        /// <param name="_baseData">要转换的BaseData对象</param>
        /// <returns>转换后的Data对象</returns>
        public static BugData BaseDataToData(BugBaseData _baseData)
        {
            
            if (_baseData!=null)
            {
                BugData _data = new BugData();

                _data.Id = _baseData.Id;
                _data.Name = new HighlightText(){Text = _baseData.Name , Highlight = ""};
                _data.Progress = (ProgressType)_baseData.Progress;
                _data.Priority = (PriorityType)_baseData.Priority;
                _data.CreateTime = new DateTime(_baseData.CreateTime[0],_baseData.CreateTime[1],_baseData.CreateTime[2],_baseData.CreateTime[3],_baseData.CreateTime[4],_baseData.CreateTime[5]);
                _data.SolveTime = new DateTime(_baseData.SolveTime[0], _baseData.SolveTime[1], _baseData.SolveTime[2], _baseData.SolveTime[3], _baseData.SolveTime[4], _baseData.SolveTime[5]);
                _data.UpdateTime = new DateTime(_baseData.UpdateTime[0], _baseData.UpdateTime[1], _baseData.UpdateTime[2], _baseData.UpdateTime[3], _baseData.UpdateTime[4], _baseData.UpdateTime[5]);
                _data.UpdateNumber = _baseData.UpdateNumber;
                _data.TemperamentId = _baseData.TemperamentId;
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
        public static BugBaseData DataToBaseData(BugData _data)
        {

            if (_data!=null)
            {
                BugBaseData _baseData = new BugBaseData();

                _baseData.Id = _data.Id;
                _baseData.Name = _data.Name.Text;
                _baseData.Progress = (int)_data.Progress;
                _baseData.Priority = (int)_data.Priority;
                _baseData.CreateTime = new List<int>(){_data.CreateTime.Year,_data.CreateTime.Month,_data.CreateTime.Day,_data.CreateTime.Hour,_data.CreateTime.Minute,_data.CreateTime.Second};
                _baseData.SolveTime = new List<int>() { _data.SolveTime.Year, _data.SolveTime.Month, _data.SolveTime.Day, _data.SolveTime.Hour, _data.SolveTime.Minute, _data.SolveTime.Second };
                _baseData.UpdateTime = new List<int>() { _data.UpdateTime.Year, _data.UpdateTime.Month, _data.UpdateTime.Day, _data.UpdateTime.Hour, _data.UpdateTime.Minute, _data.UpdateTime.Second };
                _baseData.UpdateNumber = _data.UpdateNumber;
                _baseData.TemperamentId = _data.TemperamentId;
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
