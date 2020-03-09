/* By: 絮大王（sukiup@163.com）
   Time：2019年10月12日07:40:11*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /*所有的枚举类型*/

    /// <summary>
    /// 排序类型
    /// </summary>
    public enum SortType : byte
    {
        None,//不排序
        LowToHigh,//由低到高排序（Up）
        HighToLow//由高到低排序（Down）
    }

    /// <summary>
    /// 进度类型
    /// </summary>
    public enum ProgressType : byte
    {
        None,
        Undone,//未完成
        Solved,//已解决
        Deprecat//已废弃
    }

    /// <summary>
    /// 优先级类型
    /// </summary>
    public enum PriorityType : byte
    {
        None,
        Low,//低
        Mid,//中
        High//高
    }

    /// <summary>
    /// 记录类型
    /// </summary>
    public enum RecordType : byte
    {
        None,
        Bug,//虫子
        Bear//熊
    }





    /// <summary>
    /// 语言类型
    /// </summary>
    public enum LanguageType : byte
    {
        None,
        English,//英语
        Chinese//中文
    }

    /// <summary>
    /// 皮肤类型
    /// </summary>
    public enum ThemeType : byte
    {
        None,
        White,//白色
        Dark,//黑色
        Cat_White,//猫咪(白色)
        Cat_Dark//猫咪(黑色)
    }

    /// <summary>
    /// DLC类型
    /// </summary>
    public enum DlcType : byte
    {
        None,
        CatTheme,//猫咪皮肤的DLC
    }

    /// <summary>
    /// 模式类型
    /// </summary>
    public enum ModeType : byte
    {
        None,
        Default,//[默认]模式
        Collaboration,//[协同合作]模式（Teamwork）
    }

    /// <summary>
    /// 比较类型
    /// </summary>
    public enum CompareType : byte
    {
        None,
        All,//比较所有的属性（所有的属性都要一样，才算一样）
        Id//只比较编号（只要编号一样，就算一样）
    }

    /// <summary>
    /// 改变类型
    /// </summary>
    public enum ChangeType : byte
    {
        None,
        Add,//添加
        Delete,//删除
        Change,//修改
    }





    /// <summary>
    /// 时间格式类型
    /// </summary>
    public enum TimeFormatType : byte
    {
        None,
        YearMonthDay,//年.月.日
        YearMonthDayHourMinute,//年.月.日 时:分
        YearMonthDayHourMinuteSecond,//年.月.日 时:分:秒
        YearMonthDayHourMinuteSecondMillisecond,//年 月 日 时 分 秒 毫秒
        HourMinuteSecond,//时:分:秒
    }

    /// <summary>
    /// [显示Bug记录]的类型
    /// </summary>
    public enum ShowBugRecordType : byte
    {
        None,
        All,//记录列表里：显示[所有]的Bug说的话
        One,//记录列表里：显示[1条]的Bug说的话
        Zero//记录列表里：显示[0条]的Bug说的话（不显示Bug说的话）
    }

    /// <summary>
    /// [同步状态]的类型
    /// </summary>
    public enum SyncStateType : byte
    {
        None,
        NoSync,//没有同步
        WaitSync,//等待同步
        Syncing,//同步中
        Synced,//同步完成
    }

    /// <summary>
    /// [动画状态]的类型
    /// </summary>
    public enum AnimationStateType : byte
    {
        None,
        Start,//开始
        End,//结束
    }







    /// <summary>
    /// 枚举类（和枚举相关的属性和方法）
    /// </summary>
    public static class Enums
    {
        #region [公开方法 - 对比]

        /// <summary>
        /// 对比2个枚举值的大小
        /// </summary>
        /// <param name="_value1">第1个枚举值</param>
        /// <param name="_value2">第2个枚举值</param>
        /// <returns>1代表第1个比第2个Bug大(大的排后面)；0代表第1个比第2个Bug相等，-1代表第1个比第2个Bug小</returns>
        public static int EnumValueCompare(int _value1, int _value2)
        {
            if (_value1 > _value2)
            {
                return 1;
            }
            else if (_value1 < _value2)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}
