/* By: 絮大王（sukiup@163.com）
   Time：2019年11月14日22:30:29*/

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
    /// 项目的数据
    /// </summary>
    public class ProjectData : INotifyPropertyChanged
    {
        /* 属性：Id(编号)
                 Name(项目的名字)
                 FileName(文件的名字)(.bugs文件的名字，不包括后缀)
               
                 ModeType(项目的模式：默认模式、协同合作模式)

                 BugDatas(所有的Bug数据)
                 RecordDatas(所有的Record数据)
                 */



        private string name;//项目的名字
        private ModeType modeType;//项目的模式（默认模式、协同合作模式）


        /* 不保存的数据 */
        private string fileName;//文件的名字(.bugs文件的名字，不包括后缀)
        private ObservableCollection<BugData> bugDatas;//所有的Bug数据
        private ObservableCollection<RecordData> recordDatas;//所有的Record数据






        #region [属性]
        /// <summary>
        /// 编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChange("Name");
            }
        }

        /// <summary>
        /// 项目的模式（默认模式、协同合作模式）
        /// </summary>
        public ModeType ModeType
        {
            get { return modeType; }
            set
            {
                modeType = value;
                PropertyChange("ModeType");
            }
        }

        #endregion

        #region [属性 - 不保存的数据]
        /// <summary>
        /// 文件的名字(.bugs文件的名字，不包括后缀)
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                PropertyChange("FileName");
            }
        }

        /// <summary>
        /// 所有的Bug数据
        /// </summary>
        public ObservableCollection<BugData> BugDatas
        {
            get { return bugDatas; }
            set
            {
                bugDatas = value;
                PropertyChange("BugDatas");
            }
        }

        /// <summary>
        /// 所有的Record数据
        /// </summary>
        public ObservableCollection<RecordData> RecordDatas
        {
            get { return recordDatas; }
            set
            {
                recordDatas = value;
                PropertyChange("RecordDatas");
            }
        }

        #endregion


        #region [构造方法]

        public ProjectData()
        {
            Id = -1;
            Name = "";
            FileName = "";

            ModeType = ModeType.Default;

            BugDatas = new ObservableCollection<BugData>();
            RecordDatas = new ObservableCollection<RecordData>();
        }

        #endregion

        #region [静态方法]
        /// <summary>
        /// 验证完整性
        /// （验证1个Project的完整性。
        /// 如果Project是完整的，代表这个Project是有效的；
        /// 如果Project是不完整的，代表Project文件还没有同步完，或者是Project文件已损坏）
        /// </summary>
        /// <param name="_data">要验证的Bug</param>
        /// <returns>Bug是否是完整的</returns>
        public static bool VerifyIntegrity(ProjectData _data)
        {
            if (_data == null ||
                _data.Id < 0 ||
                _data.Name == null ||_data.Name == "")
            {
                return false;
            }
            else
            {
                return true;
            }
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
