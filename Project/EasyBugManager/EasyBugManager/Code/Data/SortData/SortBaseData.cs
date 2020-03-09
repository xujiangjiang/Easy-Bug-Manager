/* By: 絮大王（xudawang@vip.163.com）
   Time：2020年2月3日04:53:13*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 排序的基础数据
    /// (用于保存和读取)
    /// </summary>
    public class SortBaseData
    {
        /* 属性：ProgressSortType([完成度]的排序类型)
                 PrioritySortType([优先级]的排序类型)
                 CreateTimeSortType([创建时间]的排序类型)
                 UpdateTimeSortType([更新时间]的排序类型)
                 
                 ShowBugNumber(页面中显示的[Bug个数]) */


        #region [属性]
        /// <summary>
        /// [完成度]的排序类型
        /// </summary>
        public int ProgressSortType { get; set; }

        /// <summary>
        /// [优先级]的排序类型
        /// </summary>
        public int PrioritySortType { get; set; }

        /// <summary>
        /// [创建时间]的排序类型
        /// </summary>
        public int CreateTimeSortType { get; set; }

        /// <summary>
        /// [更新时间]的排序类型
        /// </summary>
        public int UpdateTimeSortType { get; set; }



        /// <summary>
        /// 页面中显示的[Bug个数]
        /// </summary>
        public int ShowBugNumber { get; set; }
        #endregion


        #region [构造方法]

        public SortBaseData()
        {
            ProgressSortType = 1;
            PrioritySortType = 2;
            CreateTimeSortType = 2;
            UpdateTimeSortType = 0;

            ShowBugNumber = 10;
        }

        #endregion


        #region [BaseData转Data]
        /// <summary>
        /// 把[BaseData对象]转换为[Data对象]
        /// </summary>
        /// <param name="_baseData">要转换的BaseData对象</param>
        /// <returns>转换后的Data对象</returns>
        public static SortData BaseDataToData(SortBaseData _baseData)
        {
            if (_baseData != null)
            {
                SortData _data = new SortData();


                _data.ProgressSortType = (SortType)_baseData.ProgressSortType;
                _data.PrioritySortType = (SortType)_baseData.PrioritySortType;
                _data.CreateTimeSortType = (SortType)_baseData.CreateTimeSortType;
                _data.UpdateTimeSortType = (SortType)_baseData.UpdateTimeSortType;
                _data.ShowBugNumber = _baseData.ShowBugNumber;


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
        public static SortBaseData DataToBaseData(SortData _data)
        {
            if (_data != null)
            {
                SortBaseData _baseData = new SortBaseData();


                _baseData.ProgressSortType = (int)_data.ProgressSortType;
                _baseData.PrioritySortType = (int)_data.PrioritySortType;
                _baseData.CreateTimeSortType = (int)_data.CreateTimeSortType;
                _baseData.UpdateTimeSortType = (int)_data.UpdateTimeSortType;
                _baseData.ShowBugNumber = _data.ShowBugNumber;


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
