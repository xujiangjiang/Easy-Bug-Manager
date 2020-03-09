/* By: 絮大王（sukiup@163.com）
   Time：2019年12月25日15:19:02*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 文字(字符串)的工具
    /// </summary>
    public static class StringTool
    {
        /// <summary>
        /// 限制一个字符串的长度
        /// </summary>
        /// <param name="_value">要处理的字符串</param>
        /// <param name="_length">字符串的长度</param>
        /// <returns>处理过的字符串</returns>
        public static string Clamp(string _value, int _length)
        {
            string _newValue = "";

            //字符串的新长度（长度不要超过原本字符串的长度）
            int _newLength = IntTool.Clamp(_length, 0, _value.Length);

            //进行切割
            _newValue = _value.Substring(0, _newLength);

            //如果[字符串本身的长度]比[要截取的长度]长
            if (_value.Length > _length)
            {
                //那么就加一个省略号
                _newValue += "...";
            }

            return _newValue;
        }



        /// <summary>
        /// 删除[字符串]中的[非法字符]
        /// （文件夹和文件的 名字中，不能包含：? * : " < > \ / |
        /// 并且，不能以空格开头）
        /// </summary>
        /// <param name="_value">要处理的字符串</param>
        /// <returns>去除非法字符后的新字符串</returns>
        public static string RemoveInvaildChat(string _value)
        {
            /* 去除字符串中的非法字符：
                文件夹和文件的 名字中，不能包含：? * : " < > \ / |
                并且，不能以空格开头*/

            if (_value != null)
            {
                //去除开头的空格
                _value = _value.Trim();

                //去除? * : " < > \ / |
                _value = _value.Replace("?", "");
                _value = _value.Replace("*", "");
                _value = _value.Replace(":", "");
                _value = _value.Replace("\"", "");
                _value = _value.Replace("<", "");
                _value = _value.Replace(">", "");
                _value = _value.Replace("\\", "");
                _value = _value.Replace("/", "");
                _value = _value.Replace("|", "");
            }


            return _value;
        }
    }
}
