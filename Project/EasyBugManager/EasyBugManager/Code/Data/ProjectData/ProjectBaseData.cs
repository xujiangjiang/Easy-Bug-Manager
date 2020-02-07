/* By: 絮大王（sukiup@163.com）
   Time：2019年11月23日00:33:21*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 项目的基础数据
    /// (用于保存和读取)
    /// </summary>
    public class ProjectBaseData
    {
        /* 属性：Id(编号)
                Name(项目的名字)
                ModeType(项目的模式：默认模式、协同合作模式)   */



        #region [属性]
        /// <summary>
        /// 编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 项目的名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目的模式（默认模式、协同合作模式）
        /// </summary>
        public int ModeType { get; set; }

        #endregion


        #region [构造方法]

        public ProjectBaseData()
        {
            Id = -1;
            Name = "";
            ModeType = 1;
        }

        #endregion


        #region [BaseData转Data]
        /// <summary>
        /// 把[BaseData对象]转换为[Data对象]
        /// </summary>
        /// <param name="_baseData">要转换的BaseData对象</param>
        /// <returns>转换后的Data对象</returns>
        public static ProjectData BaseDataToData(ProjectBaseData _baseData)
        {

            if (_baseData!=null)
            {
                ProjectData _data = new ProjectData();

                _data.Id = _baseData.Id;
                _data.Name = _baseData.Name;
                _data.ModeType = (ModeType)_baseData.ModeType;

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
        public static ProjectBaseData DataToBaseData(ProjectData _data)
        {
            
            if (_data!=null)
            {
                ProjectBaseData _baseData = new ProjectBaseData();

                _baseData.Id = _data.Id;
                _baseData.Name = _data.Name;
                _baseData.ModeType = (int)_data.ModeType;

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
