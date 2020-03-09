/* By: 絮大王（sukiup@163.com）
   Time：2019年11月30日15:03:50*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 随机的工具（用于随机）
    /// </summary>
    public static class RandomTool
    {
        private static Random random = new Random();//随机的对象(用于随机)


        #region [公开方法]

        /// <summary>
        /// 随机
        /// (指定1个最大值，和1个最小值。返回1个小于最大值，并且大于等于最小值的数)
        /// </summary>
        /// <param name="_min">最小值</param>
        /// <param name="_max">最大值（随机出来的数字，不包括这个值）</param>
        /// <returns>随机出来的值</returns>
        public static int Random(int _min, int _max)
        {
            return random.Next(_min, _max);
        }

        #endregion

    }
}