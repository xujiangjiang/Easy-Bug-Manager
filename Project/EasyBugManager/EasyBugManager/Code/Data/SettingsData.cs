/* By: 絮大王（sukiup@163.com）
   Time：2019年11月14日22:19:17*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 设置的数据
    /// </summary>
    public class SettingsData: INotifyPropertyChanged
    {
        private LanguageType language;//语言
        private bool sound;//是否有声音？
        private ThemeType theme;//皮肤
        private int transparent;//透明度

        private int windowWidth;//窗口的宽度
        private int windowHeight;//窗口的高度




        #region [属性 - 设置]
        /// <summary>
        /// 语言
        /// </summary>
        public LanguageType Language
        {
            get { return language; }
            set
            {
                language = value;
                PropertyChange("Language");
            }
        }

        /// <summary>
        /// 是否有声音？
        /// </summary>
        public bool Sound
        {
            get { return sound; }
            set
            {
                sound = value;
                PropertyChange("Sound");
            }
        }

        /// <summary>
        /// 皮肤
        /// </summary>
        public ThemeType Theme
        {
            get { return theme; }
            set
            {
                theme = value;
                PropertyChange("Theme");
            }
        }

        /// <summary>
        /// 透明度
        /// </summary>
        public int Transparent
        {
            get { return transparent; }
            set
            {
                transparent = value;
                PropertyChange("Transparent");
                PropertyChange("Transparent01");
            }
        }

        #endregion

        #region [属性 - 窗口]

        /// <summary>
        /// 窗口的宽度
        /// </summary>
        public int WindowWidth
        {
            get { return windowWidth; }
            set { windowWidth = value; }
        }

        /// <summary>
        /// 窗口的高度
        /// </summary>
        public int WindowHeight
        {
            get { return windowHeight; }
            set { windowHeight = value; }
        }

        #endregion

        #region [属性 - 其他]
        /// <summary>
        /// 透明度(0到1)
        /// </summary>
        public float Transparent01
        {
            get { return transparent/100.0f; }
            set { transparent = Convert.ToInt32(value * 100); }
        }
        #endregion

        #region [构造方法]

        public SettingsData()
        {
            Language = LanguageType.Chinese;
            Sound = true;
            Theme = ThemeType.White;
            Transparent = 100;

            windowWidth = 1480;
            windowHeight = 1030;
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
