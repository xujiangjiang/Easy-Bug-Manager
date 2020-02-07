/* By: 絮大王（sukiup@163.com）
   Time：2019年11月27日09:21:18*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// 相关的系统（相关Bug的管理）
    /// </summary>
    public class RelatedSystem
    {
        /* 属性：ShowRelatedBugNames(要显示的相关BugName)
         
           方法：Related(获取相关的Bug名字)(根据string，查找关联的Bug的名字)*/


        #region [公开属性]
        /// <summary>
        /// 要显示的 相关BugName
        /// </summary>
        public ObservableCollection<HighlightText> ShowRelatedBugNames
        {
            get { return AppManager.Datas.OtherData.ShowRelatedBugNames; }
            set { AppManager.Datas.OtherData.ShowRelatedBugNames = value; }
        }
        #endregion

        #region [公开方法]
        /// <summary>
        /// 获取相关的Bug名字
        /// (根据string，查找关联的Bug的名字)
        /// </summary>
        /// <param name="_highlight">要查找的关键字</param>
        /// <returns>符合关键字的Bug的名字</returns>
        public ObservableCollection<HighlightText> Related(string _highlight)
        {

            //容器：相关联的BugName
            ObservableCollection<HighlightText> _relatedBugNames = new ObservableCollection<HighlightText>();

            if (_highlight != null && _highlight != "")
            {
                //把_highlight改为小写
                string _newHighlight = _highlight.ToLower();//把字符串str中的所有字符转换为小写

                //把_highlight去掉首尾的空格
                _newHighlight = _newHighlight.Trim();

                //获取到所有的Bug
                ObservableCollection<BugData> _bugDatas = AppManager.Systems.BugSystem.BugDatas;

                //重新排序（创建时间，从后到前排序）
                List<BugData> _sortBugDatas = new List<BugData>();
                if (_bugDatas!=null)
                {
                    for (int i = 0; i < _bugDatas.Count; i++)
                    {
                        //取到Bug
                        BugData _bugData = _bugDatas[i];

                        //如果Bug没有被删除
                        if (_bugData!=null && _bugData.IsDelete!=true)
                        {
                            _sortBugDatas.Add(_bugDatas[i]);
                        }
                    }
                }

                //对[创建时间]排序
                _sortBugDatas.Sort((bug1, bug2) =>
                {
                    /* 这个Lamba表达式的返回值为int类型，意思是bug1和bug2比较的大小。(大的排后面)
                       如果不能理解这段代码，可以搜索"C# List 多权重排序" */

                    int _index = 0;

                    //对[创建时间]进行排序（从高到低）
                    _index -= DateTime.Compare(bug1.CreateTime, bug2.CreateTime);

                    return _index;
                });

                //进行过滤
                if (_sortBugDatas != null)
                {
                    //遍历所有的Bug
                    for (int i = 0; i < _sortBugDatas.Count; i++)
                    {
                        //把BugName改为小写
                        string _newBugName = _sortBugDatas[i].Name.Text.ToLower();//把字符串str中的所有字符转换为小写

                        //如果BugName中，存在这个关键字
                        if (_newBugName.Contains(_newHighlight) == true && _newHighlight!="")
                        {
                            HighlightText _relatedBugName = _sortBugDatas[i].Name.Copy();//把这个Name复制一下
                            _relatedBugName.Highlight = _highlight;//更改高亮
                            _relatedBugNames.Add(_relatedBugName);//加入集合中
                        }
                    }
                }
            }

            //赋值
            ShowRelatedBugNames = _relatedBugNames;

            //返回值
            return _relatedBugNames;
        }
        #endregion

    }
}
