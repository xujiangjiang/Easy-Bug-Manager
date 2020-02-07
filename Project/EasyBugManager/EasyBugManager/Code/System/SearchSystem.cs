/* By: 絮大王（sukiup@163.com）
   Time：2019年11月25日23:56:59*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 搜索的系统（用于搜索Bug）
    /// </summary>
    public class SearchSystem
    {
        /* 属性：SearchText(搜索的文字)
                 FilterBugDatas(过滤后的Bugs) */

        /* 方法：Filter(根据搜索文字，过滤Bug) */



        #region [公开属性 - 关键字]
        /// <summary>
        /// 搜索的文字
        /// </summary>
        public string SearchText
        {
            get { return AppManager.Uis.ListUi.UiControl.SearchString; }
            set { AppManager.Uis.ListUi.UiControl.SearchString = value; }
        }

        #endregion

        #region [公开属性 - 数据]
        /// <summary>
        /// 过滤后的Bugs
        /// </summary>
        public ObservableCollection<BugData> FilterBugDatas
        {
            get; set;
        }

        #endregion


        #region [构造方法]

        public SearchSystem()
        {
            FilterBugDatas = new ObservableCollection<BugData>();
        }

        #endregion


        #region [公开方法 - 过滤]
        /// <summary>
        /// 过滤
        /// (根据搜索文字，过滤Bug)
        /// </summary>
        /// <returns>过滤后的Bugs</returns>
        public ObservableCollection<BugData> Filter()
        {
            //进行过滤
            FilterBugDatas = this.Filter(SearchText);
            return FilterBugDatas;
        }
        #endregion

        #region [公开方法 - 清空]
        /// <summary>
        /// 清空[搜索的文字]
        /// </summary>
        public void ClearSearchText()
        {
            SearchText = "";
        }

        #endregion


        #region [私有方法 - 过滤]

        /// <summary>
        /// 过滤
        /// (根据搜索文字，过滤Bug)
        /// </summary>
        /// <param name="_searchText">搜索的文字</param>
        /// <returns>过滤后的Bugs</returns>
        private ObservableCollection<BugData> Filter(string _searchText)
        {
            //获取到排序过后的[Bug数据]
            ObservableCollection<BugData> _sortBugDatas = AppManager.Systems.SortSystem.SortBugDatas;


            //如果没有[搜索文字]
            if (_searchText == null || _searchText == "")
            {
                if (_sortBugDatas != null)
                {
                    for (int i = 0; i < _sortBugDatas.Count; i++)
                    {
                        if (_sortBugDatas[i]!=null)
                        {
                            //让文字不高亮
                            _sortBugDatas[i].Name.Highlight = "";
                        }
                    }

                    return _sortBugDatas;
                }

                else
                {
                    return new ObservableCollection<BugData>();
                }

            }

            //如果有[搜索文字]
            else
            {
                //把_searchText改为小写
                string _newSearchText = _searchText.ToLower();//把字符串str中的所有字符转换为小写

                //容器：所有搜索到的相关的Bug
                ObservableCollection<BugData> _searchBugDatas = new ObservableCollection<BugData>();

                //进行过滤
                if (_sortBugDatas != null)
                {
                    for (int i = 0; i < _sortBugDatas.Count; i++)
                    {
                        //取到Bug
                        BugData _bugData = _sortBugDatas[i];

                        //把BugName改为小写
                        string _newBugName = _bugData.Name.Text.ToLower();//把字符串str中的所有字符转换为小写

                        if (_newBugName.Contains(_newSearchText) == true)
                        {
                            _bugData.Name.Highlight = _searchText;//更改高亮
                            _searchBugDatas.Add(_bugData);//加入集合中
                        }
                    }
                }

                return _searchBugDatas;
            }

        }

        #endregion

    }
}
