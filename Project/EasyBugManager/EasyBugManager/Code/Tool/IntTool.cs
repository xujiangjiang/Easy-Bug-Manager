/* By: 絮大王（sukiup@163.com）
   Time：2019年12月25日15:23:47*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// Int的工具
    /// </summary>
    public static class IntTool
    {
        /// <summary>
        /// 限制一个值的大小
        /// </summary>
        /// <param name="_value">要处理的值</param>
        /// <param name="_min">最小值</param>
        /// <param name="_max">最大值</param>
        /// <returns>处理过的值</returns>
        public static int Clamp(int _value, int _min, int _max)
        {
            //如果value大于max，就返回max
            //如果value小于max，就返回min
            //否则，就返回value

            if (_value > _max)
            {
                return _max;
            }
            else if (_value < _min)
            {
                return _min;
            }
            else
            {
                return _value;
            }
        }
    }
}