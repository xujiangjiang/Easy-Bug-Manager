/* By: 絮大王（xudawang@vip.163.com）
   Time：2020年2月19日05:31:00*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// [最近的项目]的基础数据
    /// (用于保存和读取)
    /// </summary>
    public class LatelyProjectBaseData
    {
        /* 属性：Id(编号)
                Name(项目的名字)
                Path(项目的路径)([.bugs文件]所在的文件夹 的路径)
                Mode(项目的模式：默认模式、协同合作模式)
                OpenTime(打开的时间)
        */


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
        /// 项目的路径([.bugs文件]所在的文件夹 的路径)
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 项目的模式（默认模式、协同合作模式）
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// 打开的时间（年、月、日、时、分、秒）
        /// </summary>
        public List<int> OpenTime { get; set; }

        #endregion


        #region [构造方法]

        public LatelyProjectBaseData()
        {
            Id = -1;
            Name = "";
            Path = "";
            Mode = 1;
            OpenTime = new List<int>();
        }

        #endregion


        #region [BaseData转Data]
        /// <summary>
        /// 把[BaseData对象]转换为[Data对象]
        /// </summary>
        /// <param name="_baseData">要转换的BaseData对象</param>
        /// <returns>转换后的Data对象</returns>
        public static LatelyProjectData BaseDataToData(LatelyProjectBaseData _baseData)
        {

            if (_baseData != null)
            {
                LatelyProjectData _data = new LatelyProjectData();

                _data.Id = _baseData.Id;
                _data.Name = _baseData.Name;
                _data.Path = _baseData.Path;
                _data.Mode = (ModeType)_baseData.Mode;
                _data.OpenTime = new DateTime(_baseData.OpenTime[0], _baseData.OpenTime[1], _baseData.OpenTime[2], _baseData.OpenTime[3], _baseData.OpenTime[4], _baseData.OpenTime[5]);

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
        public static LatelyProjectBaseData DataToBaseData(LatelyProjectData _data)
        {

            if (_data != null)
            {
                LatelyProjectBaseData _baseData = new LatelyProjectBaseData();

                _baseData.Id = _data.Id;
                _baseData.Name = _data.Name;
                _baseData.Path = _data.Path;
                _baseData.Mode = (int)_data.Mode;
                _baseData.OpenTime = new List<int>() { _data.OpenTime.Year, _data.OpenTime.Month, _data.OpenTime.Day, _data.OpenTime.Hour, _data.OpenTime.Minute, _data.OpenTime.Second };

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
