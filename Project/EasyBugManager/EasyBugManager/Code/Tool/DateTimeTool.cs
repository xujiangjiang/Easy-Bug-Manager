/* By: 絮大王（sukiup@163.com）
   Time：2019年11月29日06:12:35*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// [DateTime]对象的工具
    /// </summary>
    public static class DateTimeTool
    {
        /// <summary>
        /// 把[DateTime对象]转换为[string]
        /// </summary>
        /// <param name="_dateTime">要转换的DateTime对象</param>
        /// <param name="_timeFormatType">转换成什么格式？</param>
        /// <returns>转换后的string</returns>
        public static string DateTimeToString(DateTime _dateTime, TimeFormatType _timeFormatType)
        {
            /* dateTime.ToString("yyyy/MM/dd HH:mm:ss fff");
               y代表年，M代表月，d代表日
               H代表时，m代表分，s代表秒
               f代表毫秒
               如果有4个y(比如yyyy)，就代表年的数字是4位*/


            //容器：转换后的string
            string _string = "";


            //判断格式的类型
            switch (_timeFormatType)
            {
                //如果是[年.月.日]格式
                case TimeFormatType.YearMonthDay:
                    _string = _dateTime.ToString("yyyy.MM.dd");
                    break;

                //如果是[年.月.日 时:分]格式
                case TimeFormatType.YearMonthDayHourMinute:
                    _string = _dateTime.ToString("yyyy.MM.dd  HH:mm");
                    break;

                //如果是[年/月/日 时:分:秒]格式
                case TimeFormatType.YearMonthDayHourMinuteSecond:
                    _string = _dateTime.ToString("yyyy/MM/dd  HH:mm:ss");
                    break;

                //如果是[年 月 日 时 分 秒 毫秒]格式
                case TimeFormatType.YearMonthDayHourMinuteSecondMillisecond:
                    _string = _dateTime.ToString("yyyyMMddHHmmss");
                    break;

                //如果是[时:分:秒]格式
                case TimeFormatType.HourMinuteSecond:
                    _string = _dateTime.ToString("HH:mm:ss");
                    break;
            }



            return _string;
        }


        /// <summary>
        /// 把[DateTime对象]转换为[Long]
        /// </summary>
        /// <param name="_dateTime">要转换的DateTime对象</param>
        /// <param name="_timeFormatType">转换成什么格式？</param>
        /// <returns>转换后的Long（如果为-1，就表示string转long出错）</returns>
        public static long DateTimeToLong(DateTime _dateTime, TimeFormatType _timeFormatType)
        {
            try
            {
                //先把DateTime转化为String
                string _dateTimeString = DateTimeToString(_dateTime, _timeFormatType);

                //然后把String转化为Long
                return long.Parse(_dateTimeString);
            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }
}
