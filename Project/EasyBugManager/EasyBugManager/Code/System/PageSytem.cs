/* By: 絮大王（sukiup@163.com）
   Time：2019年11月24日23:43:09*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace EasyBugManager
{
    /// <summary>
    /// 页面的系统（用于为BugList翻页）
    /// </summary>
    public class PageSytem
    {
        /* 属性：ShowBugDatas(显示的Bugs)(要显示在ListUi中的Bugs)
                 BugItems(显示的Bugs控件)(20个)
                
                 CurrentPageNumber(当前的页码)(从1开始)
                 TotalPageNumber(总页数)
                 ShowBugNumber(页面中显示的[Bug个数])

                  
           方法：Turn(跳转到[某一页])
                 Refresh(刷新页面) 
                 Insert(插入)(插入一个BugData。并把当前页面中的最后BugData，从页面中删掉)*/



        #region [公开属性 - 数据]

        /// <summary>
        /// 显示的Bugs
        /// (要显示在ListUi中的Bugs)
        /// </summary>
        public ObservableCollection<BugItemData> ShowBugItemDatas
        {
            get { return AppManager.Datas.OtherData.ShowBugItemDatas; }
            set { AppManager.Datas.OtherData.ShowBugItemDatas = value; }
        }

        #endregion

        #region [公开属性 - 控件]
        /// <summary>
        /// 显示的Bugs控件(20个)
        /// </summary>
        public List<BugListItemControl> BugItems
        {
            get { return AppManager.Uis.ListUi.UiControl.BugItems; }
        }

        #endregion

        #region [公开属性 - 页码]
        /// <summary>
        /// 当前的页码
        /// </summary>
        public int CurrentPageNumber
        {
            get { return AppManager.Uis.ListUi.UiControl.CurrentPageNumber; }
            set { AppManager.Uis.ListUi.UiControl.CurrentPageNumber = value; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageNumber
        {
            get { return AppManager.Uis.ListUi.UiControl.TotalPageNumber; }
            set { AppManager.Uis.ListUi.UiControl.TotalPageNumber = value; }
        }
        #endregion

        #region [公开属性 - 其他]
        /// <summary>
        /// 页面中显示的[Bug个数]
        /// </summary>
        public int ShowBugNumber
        {
            get { return AppManager.Datas.SortData.ShowBugNumber; }
        }

        #endregion



        #region [公开方法 - 翻页]
        /// <summary>
        /// 翻到[某一页]
        /// </summary>
        /// <param name="_pageNumber">要翻到哪一页？</param>
        public void Turn(int _pageNumber)
        {
            //如果这个要翻到的页数，大于了总页数，就让要翻到的页数为最大页数；
            //如果这个要翻到的页数，小于等于了0，就让要翻到的页数为1
            if (_pageNumber > TotalPageNumber)
            {
                _pageNumber = TotalPageNumber;
            }
            else if (_pageNumber <= 0)
            {
                _pageNumber = 1;
            }


            //获取到所有的[过滤好的Bugs]
            ObservableCollection<BugData> _allBugDatas = AppManager.Systems.SearchSystem.FilterBugDatas;

            //取到这一页开始的Bug索引
            int _startBugIndex = (_pageNumber - 1) * ShowBugNumber;

            //然后取到所有的Bug
            ObservableCollection<BugData> _currentPageBugDatas = new ObservableCollection<BugData>();
            if (_allBugDatas != null)
            {
                for (int i = _startBugIndex; i < _startBugIndex + ShowBugNumber; i++)
                {
                    if (i < _allBugDatas.Count)
                    {
                        _currentPageBugDatas.Add(_allBugDatas[i]);
                    }
                }
            }

            //让所有的Bug的跳转都为-1
            for (int i = 0; i < _currentPageBugDatas.Count; i++)
            {
                _currentPageBugDatas[i].ItemData.GoToPageNumber = -1;
            }

            //把BugData，转换为BugItemData
            ObservableCollection<BugItemData> _currentPageBugItemDatas = new ObservableCollection<BugItemData>();
            for (int i = 0; i < _currentPageBugDatas.Count; i++)
            {
                _currentPageBugItemDatas.Add(_currentPageBugDatas[i].ItemData);
            }


            //然后，赋值属性，更新Ui
            ShowBugItemDatas = _currentPageBugItemDatas;

            //修改页数
            CurrentPageNumber = _pageNumber;


        }


        /// <summary>
        /// 刷新页面
        /// </summary>
        public void Refresh()
        {
            //翻到当前页
            Turn(CurrentPageNumber);
        }


        /// <summary>
        /// 翻到[某一页]
        /// (根据BugData，跳转页面)
        /// </summary>
        /// <param name="_bugData">跳转到哪个Bug所在的页面？</param>
        public void Turn(BugData _bugData)
        {
            //清空[搜索的文字]
            AppManager.Systems.SearchSystem.ClearSearchText();

            //取到Bug所在的页码
            int _pageNumber = GetPageNumber(_bugData);

            //跳转到Bug所在页
            Turn(_pageNumber);

            //然后选中Bug
            AppManager.Uis.ListUi.UiControl.SelectedBugIndex = GetBugIndexInCurrentPage(_bugData);

        }
        #endregion

        #region [公开方法 - 插入]

        /// <summary>
        /// 插入
        /// (插入一个BugData，把Bug插入到它应该在的位置。并把当前页面中的最后BugData，从页面中删掉)
        /// (不影响BugSystem.BugDatas属性，只影响PageSystem.ShowBugDatas属性)
        /// </summary>
        /// <param name="_bugData">要插入的Bug</param>
        public void Insert(BugData _bugData)
        {
            /* 思路：1. 如果BugData在当前页
                        把Bug插入到当前页面的指定位置。
                        如果此时PageSystem.ShowBugDatas属性的数量，大于了ShowBugNumber属性，
                        那么就把当前页面中的最后一个BugData，从页面中删掉
                        
                    2. 如果BugData不在当前页，而是在当前页之前的页面里
                       把当前页的前一个Bug，插入到当前页的第0个位置上。
                       如果此时PageSystem.ShowBugDatas属性的数量，大于了ShowBugNumber属性，
                       那么就把当前页面中的最后一个BugData，从页面中删掉*/



            //首先，计算这个BugData，应该在哪一页？（从1开始）
            int _bugPageNumber = GetPageNumber(_bugData);

            //计算这个BugData，应该在页面的哪个位置？（从0开始）
            int _bugIndexInCurrentPage = GetBugIndexInCurrentPage(_bugData, true);



            //判断：如果这个Bug，在当前页面中
            if (_bugIndexInCurrentPage >= 0)
            {
                /* 插入：新的Bug */
                //如果这个Bug，是当前页面中的最后1个Bug -> 那么先移除当前页面中的当前的最后1个Bug，然后把这个Bug插入到最后1个Bug的位置上（因为List.Insert()方法不能超出索引）
                if (ShowBugItemDatas.Count - 1 < _bugIndexInCurrentPage)
                {
                    //判断：如果添加了1个Bug后，会超出显示的Bug数量
                    if (ShowBugItemDatas.Count + 1 > ShowBugNumber)
                    {
                        //移除最后一个Bug
                        ShowBugItemDatas.RemoveAt(ShowBugItemDatas.Count - 1);
                    }

                    //插入
                    ShowBugItemDatas.Add(_bugData.ItemData);
                }

                //如果这个Bug，不是当前页面中的，最后1个Bug -> 先插入到对应的位置上，然后移除当前页面中的最后1个Bug
                else
                {
                    //插入
                    ShowBugItemDatas.Insert(_bugIndexInCurrentPage, _bugData.ItemData);

                    //判断：如果添加了1个Bug后，会超出显示的Bug数量
                    if (ShowBugItemDatas.Count > ShowBugNumber)
                    {
                        //移除最后一个Bug
                        ShowBugItemDatas.RemoveAt(ShowBugItemDatas.Count - 1);
                    }

                }
            }

            //判断：如果这个Bug，不在当前页面中。并且这个Bug在当前页面之前。
            else if (_bugPageNumber >= 0 && _bugPageNumber < CurrentPageNumber)
            {
                /* 真实：表示的是FilterBugDatas中的数据
                   显示：表示的是ShowBugDatas中的数据*/

                //获取当前页(真实)的所有Bug（比如当前页面是第5页，就获取第5页的所有Bug）
                List<BugData> _bugDatasInCurrentPage = GetBugDatasInPage(CurrentPageNumber);

                //遍历当前页(真实)中，所有的Bug （从最后一个元素开始遍历）
                for (int i = _bugDatasInCurrentPage.Count - 1; i >= 0; i--)
                {
                    //获取到这个Bug
                    BugData _b = _bugDatasInCurrentPage[i];

                    //如果当前页(显示)中，没有这个Bug
                    if (ShowBugItemDatas.Contains(_b.ItemData) == false)
                    {
                        //插入到 当前页面的第0个位置上
                        if (ShowBugItemDatas.Count > 0)
                        {
                            ShowBugItemDatas.Insert(0, _b.ItemData);
                        }
                        else
                        {
                            ShowBugItemDatas.Add(_b.ItemData);
                        }

                        //判断：如果添加了1个Bug后，会超出显示的Bug数量
                        if (ShowBugItemDatas.Count > ShowBugNumber)
                        {
                            //移除最后一个Bug
                            ShowBugItemDatas.RemoveAt(ShowBugItemDatas.Count - 1);
                        }
                    }

                }
            }
        }

        #endregion

        #region [公开方法 - 个数]
        /// <summary>
        /// 统计页面个数
        /// </summary>
        public void CalculatedPagesNumber()
        {
            int _allPageNumber = 1;//页面的总数

            //取到Bug的集合
            ObservableCollection<BugData> _bugDatas = AppManager.Systems.SearchSystem.FilterBugDatas;

            //计算总数
            if (_bugDatas != null)
            {
                _allPageNumber = _bugDatas.Count / ShowBugNumber;
                if (_bugDatas.Count % ShowBugNumber > 0)
                {
                    _allPageNumber += 1;
                }
            }

            //赋值
            TotalPageNumber = _allPageNumber;
        }

        #endregion

        #region [公开方法 - 获取]
        /// <summary>
        /// 获取[页码]
        /// (根据BugData，获取Bug所在的页面)
        /// </summary>
        /// <param name="_bugData">跳转到哪个Bug所在的页面？</param>
        /// <returns>Bug所在的页面（从1开始）（如果这个Bug不存在，就返回-1）</returns>
        public int GetPageNumber(BugData _bugData)
        {

            int _pageNumber = -1;//bug所在的页数(从1开始)


            if (_bugData != null)
            {
                //获取到过滤后的文字
                ObservableCollection<BugData> _filterBugDatas = AppManager.Systems.SearchSystem.FilterBugDatas;

                //然后，查找到Bug所在的索引
                int _bugIndex = -1;//bug的索引(从0开始)
                if (_filterBugDatas != null)
                {
                    for (int i = 0; i < _filterBugDatas.Count; i++)
                    {
                        if (_filterBugDatas[i].Id == _bugData.Id)
                        {
                            _bugIndex = i;
                            break;
                        }
                    }
                }

                //查找到Bug所在的页数
                if (_bugIndex > -1)
                {
                    _pageNumber = (_bugIndex / ShowBugNumber) + 1;
                }
            }


            //返回值
            return _pageNumber;
        }


        /// <summary>
        /// 获取[Bug]在当前页面中的索引
        /// (根据BugData，获取Bug当前页面中的索引)
        /// </summary>
        /// <param name="_bugData">哪个Bug？</param>
        /// <param name="_isReal">是否查找的是真实的索引？ 如果为true，从FilterBugDatas中进行查找；如果为false，从ShowBugDatas中进行查找</param>
        /// <returns>>Bug在此页面中的索引（如果这个Bug不存在，就返回-1）(从0开始) </returns>
        public int GetBugIndexInCurrentPage(BugData _bugData, bool _isReal = false)
        {
            /* _isReal参数：是否查找的是真实的索引？
                            如果为true，从FilterBugDatas中进行查找，那么找到的就是这个Bug在当前页面的真实索引；
                            如果为false，从ShowBugDatas中进行查找，那么找到的就是这个Bug当前在当前页面中的索引 */

            int _index = -1;


            if (_bugData != null)
            {
                //判断
                switch (_isReal)
                {
                    //从ShowBugDatas中进行查找，那么找到的就是这个Bug当前在当前页面中的索引
                    case false:
                        if (ShowBugItemDatas != null)
                        {
                            //遍历所有正在显示的Bug
                            for (int i = 0; i < ShowBugItemDatas.Count; i++)
                            {
                                if (BugData.Compare(CompareType.Id, ShowBugItemDatas[i].Data, _bugData))
                                {
                                    _index = i;
                                    break;
                                }
                            }
                        }
                        break;


                    //从FilterBugDatas中进行查找，那么找到的就是这个Bug在当前页面的真实索引
                    case true:
                        //获取到过滤后的文字
                        ObservableCollection<BugData> _filterBugDatas = AppManager.Systems.SearchSystem.FilterBugDatas;

                        //找出Bug在当前页的索引
                        if (_filterBugDatas != null)
                        {
                            //然后，查找到Bug所在的索引
                            for (int i = 0; i < _filterBugDatas.Count; i++)
                            {
                                //如果找到了Bug
                                if (BugData.Compare(CompareType.Id, _filterBugDatas[i], _bugData) == true)
                                {
                                    //就判断Bug所在的页码是否是当前页码
                                    int _bugPageNumber = (i / ShowBugNumber) + 1;

                                    //如果是当前页码
                                    if (_bugPageNumber == CurrentPageNumber)
                                    {
                                        //就获取Bug所在的索引
                                        _index = i % ShowBugNumber;
                                    }
                                }
                            }
                        }

                        break;
                }
            }


            return _index;
        }



        /// <summary>
        /// 获取某个页面中的所有Bug
        /// </summary>
        /// <param name="_pageNumber">要获取哪个页面？（页码）（从1开始）</param>
        /// <returns>这个页面中所有的Bug</returns>
        public List<BugData> GetBugDatasInPage(int _pageNumber)
        {
            //这个页面中，所有的Bug
            List<BugData> _pageBugDatas = new List<BugData>();



            //判断：如果页码大于0
            if (_pageNumber > 0)
            {
                /* 从FilterBugDatas中进行查找，那么找到的就是这个Bug在当前页面的真实索引 */

                //获取到过滤后的文字
                ObservableCollection<BugData> _filterBugDatas = AppManager.Systems.SearchSystem.FilterBugDatas;

                //找出Bug在当前页的索引
                if (_filterBugDatas != null)
                {
                    //然后，查找到Bug所在的索引
                    for (int i = 0; i < _filterBugDatas.Count; i++)
                    {
                        //获取Bug所在的页码
                        int _bugPageNumber = (i / ShowBugNumber) + 1;

                        //如果Bug所在的页码 是我们要找的页码
                        if (_bugPageNumber == _pageNumber)
                        {
                            //就把Bug加入到列表中
                            _pageBugDatas.Add(_filterBugDatas[i]);
                        }
                    }
                }
            }



            //返回值
            return _pageBugDatas;
        }
        #endregion

    }
}
