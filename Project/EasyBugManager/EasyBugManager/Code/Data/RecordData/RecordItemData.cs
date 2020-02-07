/* By: 絮大王（xudawang@vip.163.com）
   Time：2020年1月20日02:42:06*/

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
    /// Record的Item数据
    /// （用于绑定在ItemControl上的数据）
    /// </summary>
    public class RecordItemData : INotifyPropertyChanged
    {
        /* 属性(不保存)：Data(Record数据)
                        Type(类型)(虫子、熊)
                        Content(内容)
                        ImagePaths(图片)(绝对路径：XXXX/项目名/Image/ImageId.png)
                        TimeInYMDHMS(时间)（格式：年/月/日 时:分:秒） */


        /* 不保存的字段 */
        private RecordData data;//Record数据
        private RecordType type;//类型（虫子、熊）
        private string content;//内容
        private ObservableCollection<string> imagePaths;//图片 (绝对路径：XXXX/项目名/Image/ImageId.png)
        private string timeInYMDHMS;//时间（格式：年/月/日 时:分:秒）



        #region [属性 - 不保存]
        /// <summary>
        /// Record数据
        /// </summary>
        public RecordData Data
        {
            get { return data; }
            set
            {
                data = value;
                PropertyChange("Data");
            }
        }

        /// <summary>
        /// 类型（虫子、熊）
        /// </summary>
        public RecordType Type
        {
            get { return type; }
            set
            {
                type = value;
                PropertyChange("Type");
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                PropertyChange("Content");
            }
        }

        /// <summary>
        /// 图片 (绝对路径：XXXX/项目名/Image/ImageId.png)
        /// </summary>
        public ObservableCollection<string> ImagePaths
        {
            get { return imagePaths; }
            set
            {
                imagePaths = value;
                PropertyChange("ImagePaths");
            }
        }

        /// <summary>
        /// 时间（格式：年/月/日 时:分:秒）
        /// </summary>
        public string TimeInYMDHMS
        {
            get { return timeInYMDHMS; }
            set
            {
                timeInYMDHMS = value;
                PropertyChange("TimeInYMDHMS");
            }
        }
        #endregion


        #region 构造方法
        public RecordItemData()
        {
            ImagePaths = new ObservableCollection<string>();
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
