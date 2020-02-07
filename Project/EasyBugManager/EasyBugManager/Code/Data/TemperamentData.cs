/* By: 絮大王（sukiup@163.com）
   Time：2019年11月14日19:47:30*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 性格的数据（虫子的性格）
    /// </summary>
    public class TemperamentData
    {
        /* 属性：Id(编号)
                 BearStringInCreate(当创建Bug时，Bear说的话)
                 BugStringInCreate(当创建Bug时，Bug说的话)
                 BugStringInReply(当回复时，Bug说的话) */


        #region [公开属性]
        /// <summary>
        /// 编号
        /// </summary>
        public int Id;

        /// <summary>
        /// 当创建Bug时，Bug说的话
        /// </summary>
        public List<string> BugStringInCreate;

        /// <summary>
        /// 当回复时，Bug说的话
        /// </summary>
        public List<string> BugStringInReply;

        #endregion


        #region [构造方法]
        public TemperamentData()
        {
            Id = 0;
            BugStringInCreate = new List<string>();
            BugStringInReply = new List<string>();
        }
        #endregion


    }
}
