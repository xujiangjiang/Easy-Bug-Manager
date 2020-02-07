/* By: 絮大王（sukiup@163.com）
   Time：2019年11月14日19:19:00*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// 高亮文字
    /// </summary>
    public class HighlightText: INotifyPropertyChanged
    {
        /* 思路：可以让一个文字中的某个字高亮显示 */


        private string text;//文字
        private string highlight;//高亮（高亮的文字）

        private string frontText;//前面的文字（不高亮）
        private string middleText;//中间的文字（高亮）
        private string behindText;//后面的文字（不高亮）



        #region [属性 - 可修改]
        /// <summary>
        /// 文字
        /// </summary>
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnChange();//更改frontText、middleText、behindText


                //更新Ui
                PropertyChange("Text");
            }
        }

        /// <summary>
        /// 高亮（高亮的文字）
        /// </summary>
        public string Highlight
        {
            get { return highlight; }
            set
            {
                highlight = value; 
                OnChange();//更改frontText、middleText、behindText
            }
        }
        #endregion

        #region [属性 - 不可修改]
        /// <summary>
        /// 前面的文字（不高亮）
        /// </summary>
        public string FrontText
        {
            get { return frontText; }
            set
            {
                frontText = value;
                PropertyChange("FrontText");
            }
        }

        /// <summary>
        /// 中间的文字（高亮）
        /// </summary>
        public string MiddleText
        {
            get { return middleText; }
            set
            {
                middleText = value;
                PropertyChange("MiddleText");
            }
        }

        /// <summary>
        /// 后面的文字（不高亮）
        /// </summary>
        public string BehindText
        {
            get { return behindText; }
            set
            {
                behindText = value;
                PropertyChange("BehindText");
            }
        }
        #endregion


        #region [构造方法]

        public HighlightText()
        {
            text = "";
            highlight = "";

            frontText = "";
            middleText = "";
            behindText = "";
        }

        #endregion

        #region [私有方法]
        /// <summary>
        /// 当[文字]或者[高亮]的值 改变时
        /// （更改frontText、middleText、behindText）
        /// </summary>
        private void OnChange()
        {
            //如果高亮不存在
            if (highlight == null || highlight=="")
            {
                FrontText = "";
                MiddleText = text;
                BehindText = "";
            }

            else
            {
                //把Text和Highlight改为小写
                string _newText = text.ToLower();//把字符串str中的所有字符转换为小写
                string _newHighlight = highlight.ToLower();

                //如果这个高亮存在
                if (_newText.Contains(_newHighlight) == true)
                {
                    //高亮的文字的索引
                    int _highlightIndex = _newText.IndexOf(_newHighlight);

                    //按照高亮来拆分
                    FrontText = text.Substring(0, _highlightIndex);
                    MiddleText = text.Substring(_highlightIndex, highlight.Length);
                    if ((_highlightIndex + highlight.Length) < text.Length)
                    {
                        BehindText = text.Substring(_highlightIndex + highlight.Length);
                    }
                    else
                    {
                        BehindText = "";
                    }
                }
            }

        }
        #endregion

        #region [公开方法]

        /// <summary>
        /// 复制
        /// (把当前对象，复制一遍)
        /// </summary>
        /// <returns>复制出来的新对象</returns>
        public HighlightText Copy()
        {
            //新对象
            HighlightText _newHighlightText = new HighlightText();

            //赋值
            _newHighlightText.Text = this.Text;
            _newHighlightText.Highlight = this.highlight;

            return _newHighlightText;
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
