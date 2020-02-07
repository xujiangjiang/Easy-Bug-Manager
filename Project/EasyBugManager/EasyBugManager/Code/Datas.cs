/* By: 絮大王（sukiup@163.com）
   Time：2019年11月14日22:52:22*/

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
    /// 用于存放所有的数据
    /// （里面存放着所有要绑定的数据）
    /// </summary>
    public class Datas : INotifyPropertyChanged
    {
        /*数据(项目)：ProjectData(项目数据)
                     SortData(排序的数据)*/

        /*数据(其他)：SettingsData(设置的数据)
                     OtherData(其他的数据)*/





        /*数据：项目*/
        private ProjectData projectData;//项目的数据
        private SortData sortData;//排序的数据

        /*数据：其他*/
        private SettingsData settingsData;//设置的数据
        private OtherData otherData;//其他的数据(一些需要绑定，但不需要储存的数据)




        #region [属性 - 项目]
        /// <summary>
        /// 项目的数据
        /// </summary>
        public ProjectData ProjectData
        {
            get { return projectData; }
            set
            {
                projectData = value;
                PropertyChange("ProjectData");
            }
        }

        /// <summary>
        /// 排序的数据
        /// </summary>
        public SortData SortData
        {
            get { return sortData; }
            set
            {
                sortData = value;
                PropertyChange("SortData");
            }
        }
        #endregion

        #region [属性 - 其他]
        /// <summary>
        /// 设置的数据
        /// </summary>
        public SettingsData SettingsData
        {
            get { return settingsData; }
            set
            {
                settingsData = value;
                PropertyChange("SettingsData");
            }
        }


        /// <summary>
        /// 其他的数据
        /// (一些需要绑定，但不需要储存的数据)
        /// </summary>
        public OtherData OtherData
        {
            get { return otherData; }
            set
            {
                otherData = value;
                PropertyChange("OtherData");
            }
        }
        #endregion




        #region [构造函数]
        public Datas()
        {
            projectData = new ProjectData();
            sortData = new SortData();

            settingsData = new SettingsData();
            otherData = new OtherData();
        }
        #endregion




        #region [方法 - 清空]
        /// <summary>
        /// 清空[项目相关]的数据
        /// </summary>
        public void ClearProjectData()
        {
            ProjectData = new ProjectData();
            SortData = new SortData();
            OtherData = new OtherData();
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
