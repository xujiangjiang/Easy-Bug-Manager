/* By: 絮大王（xudawang@vip.163.com）
   Time：2020年1月20日02:40:27*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// Bug的Item数据
    /// （用于绑定在ItemControl上的数据）
    /// </summary>
    public class BugItemData : INotifyPropertyChanged
    {
        /* 属性（不保存）：Data（BugData数据）
                        
                          GoToPageNumber(跳转的页数(把Bug跳转到哪一页？如果为-1，就不显示跳转提示；如果不为-1，就显示跳转提示))
                          CreateTimeInYMD(创建时间（格式：年.月.日）)
                          UpdateTimeInYMD(更新时间（格式：年.月.日）)
                          CreateTimeInYMDHM(创建时间（格式：年.月.日 时:分）)
                          SolveTimeInYMDHM(完成时间（格式：年.月.日 时:分）)
                          */

        private BugData data;//BugData数据

        private int goToPageNumber;//跳转的页数(把Bug跳转到哪一页？如果为-1，就不显示跳转提示；如果不为-1，就显示跳转提示)
        private string createTimeInYMD;//创建时间（格式：年.月.日）
        private string updateTimeInYMD;//更新时间（格式：年.月.日）
        private string createTimeInYMDHM;//创建时间（格式：年.月.日 时:分）
        private string solveTimeInYMDHM;//完成时间（格式：年.月.日 时:分）


        #region [属性 - 不保存]
        /// <summary>
        /// BugData数据
        /// </summary>
        public BugData Data
        {
            get { return data; }
            set
            {
                data = value;
                PropertyChange("Data");
            }
        }


        /// <summary>
        /// 跳转的页数
        /// (把Bug跳转到哪一页？如果为-1，就不显示跳转提示；如果不为-1，就显示跳转提示)
        /// </summary>
        public int GoToPageNumber
        {
            get { return goToPageNumber; }
            set
            {
                goToPageNumber = value;
                PropertyChange("GoToPageNumber");
            }
        }
        #endregion

        #region [属性 - 不保存(时间)]
        /// <summary>
        /// 创建时间（格式：年.月.日）
        /// </summary>
        public string CreateTimeInYMD
        {
            get { return createTimeInYMD; }
            set
            {
                createTimeInYMD = value;
                PropertyChange("CreateTimeInYMD");
            }
        }

        /// <summary>
        /// 更新时间（格式：年.月.日）
        /// </summary>
        public string UpdateTimeInYMD
        {
            get { return updateTimeInYMD; }
            set
            {
                updateTimeInYMD = value;
                PropertyChange("UpdateTimeInYMD");
            }
        }



        /// <summary>
        /// 创建时间（格式：年.月.日 时:分）
        /// </summary>
        public string CreateTimeInYMDHM
        {
            get { return createTimeInYMDHM; }
            set
            {
                createTimeInYMDHM = value;
                PropertyChange("CreateTimeInYMDHM");
            }
        }

        /// <summary>
        /// 完成时间（格式：年.月.日 时:分）
        /// </summary>
        public string SolveTimeInYMDHM
        {
            get
            {
                if (Data == null || Data.Progress != ProgressType.Solved)
                {
                    return "";
                }
                else
                {
                    return solveTimeInYMDHM;
                }
            }
            set
            {
                solveTimeInYMDHM = value;
                PropertyChange("SolveTimeInYMDHM");
            }
        }
        #endregion



        #region [构造函数]

        public BugItemData()
        {
            goToPageNumber = -1;
            createTimeInYMD = "";
            updateTimeInYMD = "";
            createTimeInYMDHM = "";
            solveTimeInYMDHM = "";
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
