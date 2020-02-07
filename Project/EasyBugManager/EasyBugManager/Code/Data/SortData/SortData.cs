/* By: 絮大王（xudawang@vip.163.com）
   Time：2020年2月3日04:37:47*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 排序的数据
    /// </summary>
    public class SortData : INotifyPropertyChanged
    {
        /* 属性： ProgressSortType([完成度]的排序类型)
                  PrioritySortType([优先级]的排序类型)
                  CreateTimeSortType([创建时间]的排序类型)
                  UpdateTimeSortType([更新时间]的排序类型)
                  
                  ShowBugNumber(页面中显示的[Bug个数])*/


        private SortType progressSortType;//[完成度]的排序类型
        private SortType prioritySortType;//[优先级]的排序类型
        private SortType createTimeSortType;//[创建时间]的排序类型
        private SortType updateTimeSortType;//[更新时间]的排序类型

        private int showBugNumber;//页面中显示的[Bug个数]


        #region [公开属性 - 保存]

        /// <summary>
        /// [完成度]的排序类型
        /// </summary>
        public SortType ProgressSortType
        {
            get { return progressSortType; }
            set
            {
                progressSortType = value;
                PropertyChange("ProgressSortType");
            }
        }

        /// <summary>
        /// [优先级]的排序类型
        /// </summary>
        public SortType PrioritySortType
        {
            get { return prioritySortType; }
            set
            {
                prioritySortType = value;
                PropertyChange("PrioritySortType");
            }
        }

        /// <summary>
        /// [创建时间]的排序类型
        /// </summary>
        public SortType CreateTimeSortType
        {
            get { return createTimeSortType; }
            set
            {
                createTimeSortType = value;
                PropertyChange("CreateTimeSortType");
            }
        }

        /// <summary>
        /// [更新时间]的排序类型
        /// </summary>
        public SortType UpdateTimeSortType
        {
            get { return updateTimeSortType; }
            set
            {
                updateTimeSortType = value;
                PropertyChange("UpdateTimeSortType");
            }
        }



        /// <summary>
        /// 页面中显示的[Bug个数]
        /// </summary>
        public int ShowBugNumber
        {
            get { return showBugNumber; }
            set
            {
                showBugNumber = value;
                PropertyChange("ShowBugNumber");
            }
        }
        #endregion

        #region [构造函数]

        public SortData()
        {
            ProgressSortType = SortType.LowToHigh;
            PrioritySortType = SortType.HighToLow;
            CreateTimeSortType = SortType.HighToLow;
            UpdateTimeSortType = SortType.None;

            ShowBugNumber = 10;
        }

        #endregion





        #region 数据的双向绑定-更新方法

        /// <summary>
        /// 当属性改变的时候，就触发此方法
        /// </summary>
        /// <param name="propertyName">发生改变的属性的名字</param>
        private void PropertyChange(string propertyName)
        {
            if (PropertyChanged != null)//如果此事件被监听
            {
                //就发送通知
                //参数1：是哪个数据类的对象发生了改变？
                //参数2：发生改变的属性名
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 系统会自动监听此事件
        /// 如果此事件触发了，系统就会去通知相应的控件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion 数据的双向绑定-更新方法
    }
}
