/* By: 絮大王（xudawang@vip.163.com）
   Time：2020年2月19日05:30:50*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// App的数据
    /// (一些需要绑定，但不需要储存的数据)
    /// </summary>
    public class AppData : INotifyPropertyChanged
    {
        /* 属性：LatelyProjectDatas(所有的[最近的项目]) */


        /* 最近的项目 界面 */
        private ObservableCollection<LatelyProjectData> latelyProjectDatas;//所有的[最近的项目]
        private int projectNameMaxLength;//项目名字 的最大长度(最大个数)
        private int projectPathMaxLength;//项目路径 的最大长度(最大个数)

        /* 删除的提示 界面 */
        private bool isNotAgainShowDeleteBugTip;//是否不再显示[删除Bug的提示]？（如果为true，代表不再提示；如果为false，代表再次提示）
        private bool isNotAgainShowDeleteRecordTip;//是否不再显示[删除记录的提示]？（如果为true，代表不再提示；如果为false，代表再次提示）

        /* 修改or创建界面 */
        private int bugNameMaxLength;//Bug名字 的最大长度(最大个数)



        #region [公开属性 - 最近的项目界面]
        /// <summary>
        /// 所有的[最近的项目]
        /// </summary>
        public ObservableCollection<LatelyProjectData> LatelyProjectDatas
        {
            get { return latelyProjectDatas; }
            set
            {
                latelyProjectDatas = value;
                PropertyChange("LatelyProjectDatas");
            }
        }

        /// <summary>
        /// 项目名字 的最大长度(最大个数)
        /// </summary>
        public int ProjectNameMaxLength
        {
            get { return projectNameMaxLength; }
            set
            {
                projectNameMaxLength = value;
                PropertyChange("ProjectNameMaxLength");//通知Ui更新
            }
        }

        /// <summary>
        /// 项目路径 的最大长度(最大个数)
        /// </summary>
        public int ProjectPathMaxLength
        {
            get { return projectPathMaxLength; }
            set
            {
                projectPathMaxLength = value;
                PropertyChange("ProjectPathMaxLength");//通知Ui更新
            }
        }
        #endregion

        #region [公开属性 - 删除的提示界面]
        /// <summary>
        /// 是否再次提示？ - 是否不再显示[删除Bug的提示]？（如果为true，代表不再提示；如果为false，代表再次提示）
        /// </summary>
        public bool IsNotAgainShowDeleteBugTip
        {
            get { return isNotAgainShowDeleteBugTip; }
            set
            {
                isNotAgainShowDeleteBugTip = value;
                PropertyChange("IsNotAgainShowDeleteBugTip");
            }
        }

        /// <summary>
        /// 是否再次提示？ - 是否不再显示[删除记录的提示]？（如果为true，代表不再提示；如果为false，代表再次提示）
        /// </summary>
        public bool IsNotAgainShowDeleteRecordTip
        {
            get { return isNotAgainShowDeleteRecordTip; }
            set
            {
                isNotAgainShowDeleteRecordTip = value;
                PropertyChange("IsNotAgainShowDeleteRecordTip");
            }
        }
        #endregion

        #region [公开属性 - 修改or创建界面]
        /// <summary>
        /// Bug名字 的最大长度(最大个数)
        /// </summary>
        public int BugNameMaxLength
        {
            get { return bugNameMaxLength; }
            set
            {
                bugNameMaxLength = value;
                PropertyChange("BugNameMaxLength");//通知Ui更新
            }
        }
        #endregion





        #region [构造方法]

        public AppData()
        {
            latelyProjectDatas = new ObservableCollection<LatelyProjectData>();
            projectNameMaxLength = 50;
            projectPathMaxLength = 50;

            isNotAgainShowDeleteBugTip = false;
            isNotAgainShowDeleteRecordTip = false;

            bugNameMaxLength = 100;
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

        #endregion
    }
}
