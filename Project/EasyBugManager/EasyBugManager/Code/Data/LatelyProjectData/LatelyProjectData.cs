/* By: 絮大王（xudawang@vip.163.com）
   Time：2020年2月19日05:31:10*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// [最近的项目]的数据
    /// </summary>
    public class LatelyProjectData : INotifyPropertyChanged
    {
        /* 属性(要保存)：Id(编号)
                        Name(项目的名字)
                        Path(项目的路径)([.bugs文件]的路径)
                        Mode(项目的模式：默认模式、协同合作模式)
                        OpenTime(打开的时间)

           属性(不保存)：NameString(文字：项目的名字)(限制字数)
                        PathString(文字：项目的路径)(限制字数)
                        ModeString(文字：项目的模式)
                        OpenTimeString(文字：打开的时间)
         */


        /* 要保存 */
        private long id;//编号
        private string name;//项目的名字
        private string path;//项目的路径([.bugs文件]的路径)
        private ModeType mode;//项目的模式：默认模式、协同合作模式
        private DateTime openTime;//打开的时间

        /* 不保存 */
        private string nameString;//文字：项目的名字(限制字数)
        private string pathString;//文字：项目的路径(限制字数)
        private string modeString;//文字：项目的模式
        private string openTimeString;//文字：打开的时间



        #region [属性 (要保存)]
        /// <summary>
        /// 编号
        /// </summary>
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 项目的名字
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
        /// 项目的路径 ([.bugs文件]的路径)
        /// </summary>
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                PropertyChange("Path");
            }
        }

        /// <summary>
        /// 项目的模式：默认模式、协同合作模式
        /// </summary>
        public ModeType Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        /// <summary>
        /// 打开的时间
        /// </summary>
        public DateTime OpenTime
        {
            get { return openTime; }
            set { openTime = value; }
        }
        #endregion

        #region [属性 (不保存)]
        /// <summary>
        /// 文字：项目的模式
        /// </summary>
        public string ModeString
        {
            get { return modeString; }
            set
            {
                modeString = value;
                PropertyChange("ModeString");
            }
        }

        /// <summary>
        /// 文字：打开的时间
        /// </summary>
        public string OpenTimeString
        {
            get { return openTimeString; }
            set
            {
                openTimeString = value;
                PropertyChange("OpenTimeString");
            }
        }
        #endregion



        #region [构造方法]

        public LatelyProjectData()
        {
            id = -1;
            name = "";
            path = "";
            mode = ModeType.None;
            openTime = DateTime.MinValue;

            nameString = "";
            pathString = "";
            modeString = "";
            openTimeString = "";
        }

        #endregion

        #region [静态方法]
        /// <summary>
        /// 复制一个LatelyProjectData对象
        /// </summary>
        /// <param name="_data">要复制哪个？</param>
        /// <returns>复制出来的LatelyProjectData对象</returns>
        public static LatelyProjectData Copy(LatelyProjectData _data)
        {
            if (_data == null)
            {
                return null;
            }

            else
            {
                LatelyProjectData _copyData = new LatelyProjectData();

                _copyData.Id = _data.Id;
                _copyData.Name = _data.Name;
                _copyData.Path = _data.Path;
                _copyData.Mode = _data.Mode;
                _copyData.OpenTime = _data.OpenTime;


                return _copyData;
            }

        }


        /// <summary>
        /// 验证完整性
        /// （验证1个Data的完整性。
        /// 如果Data是完整的，代表这个Data是有效的；
        /// 如果Data是不完整的，代表Data文件还没有同步完，或者是Data文件已损坏）
        /// </summary>
        /// <param name="_data">要验证的Data</param>
        /// <returns>Data是否是完整的</returns>
        public static bool VerifyIntegrity(LatelyProjectData _data)
        {
            if (_data == null ||
                _data.Id < 0 ||
                _data.Path == null || _data.path == "" ||
                _data.Name == null || _data.Name == "" ||
                _data.Mode == ModeType.None) 
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 比较2个数据是否相同
        /// </summary>
        /// <param name="_compareType">是比较所有的属性，还是只比较编号？</param>
        /// <param name="_data1">第1个数据</param>
        /// <param name="_data2">第2个数据</param>
        /// <returns>2个数据是否相同？</returns>
        public static bool Compare(CompareType _compareType, LatelyProjectData _data1, LatelyProjectData _data2)
        {
            if (_data1 == null || _data2 == null) return false;



            //是否相同？
            bool _isSame = false;

            //比较
            switch (_compareType)
            {
                case CompareType.Id:
                    if (_data1.Id == _data2.Id)
                    {
                        _isSame = true;
                    }
                    else
                    {
                        _isSame = false;
                    }
                    break;

                case CompareType.All:
                    if (_data1.Id == _data2.Id &&
                        _data1.Name == _data2.Name &&
                        _data1.Path == _data2.Path &&
                        _data1.Mode == _data2.Mode) 
                    {
                        _isSame = true;
                    }
                    else
                    {
                        _isSame = false;
                    }
                    break;
            }


            //返回值
            return _isSame;
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
