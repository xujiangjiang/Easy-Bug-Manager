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
            int _startBugIndex = (_pageNumber-1) * ShowBugNumber;

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
            //首先，计算这个BugData，应该在页面的哪个部分？
            int _bugIndexInCurrentPage = GetBugIndexInCurrentPage(_bugData, true);

            //进行插入
            Insert(_bugIndexInCurrentPage, _bugData);
        }


        /// <summary>
        /// 插入
        /// (插入一个BugData。并把当前页面中的最后BugData，从页面中删掉)
        /// (不影响BugSystem.BugDatas属性，只影响PageSystem.ShowBugDatas属性)
        /// </summary>
        /// <param name="_index">要把Bug插入到当前页面中的哪一个位置上？（从0开始）</param>
        /// <param name="_bugData">要插入的Bug</param>
        public void Insert(int _index, BugData _bugData)
        {
            /* 思路：把Bug插入到当前页面的指定位置。
                    如果此时PageSystem.ShowBugDatas属性的数量，大于了ShowBugNumber属性，
                    那么就当前页面中的最后BugData，从页面中删掉*/

            
            if (_index>=0)
            {
                //插入：新的Bug
                if (ShowBugItemDatas.Count-1 < _index)
                {
                    //判断：如果添加了1个Bug后，会超出显示的Bug数量
                    if (ShowBugItemDatas.Count+1 > ShowBugNumber)
                    {
                        //移除最后一个Bug
                        ShowBugItemDatas.RemoveAt(ShowBugItemDatas.Count - 1);
                    }

                    //插入
                    ShowBugItemDatas.Add(_bugData.ItemData);
                }
                else
                {
                    //插入
                    ShowBugItemDatas.Insert(_index, _bugData.ItemData);

                    //判断：如果添加了1个Bug后，会超出显示的Bug数量
                    if (ShowBugItemDatas.Count > ShowBugNumber)
                    {
                        //移除最后一个Bug
                        ShowBugItemDatas.RemoveAt(ShowBugItemDatas.Count - 1);
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
        /// <returns>Bug所在的页面（如果这个Bug不存在，就返回-1）</returns>
        public int GetPageNumber(BugData _bugData)
        {
            //获取到过滤后的文字
            ObservableCollection<BugData> _filterBugDatas = AppManager.Systems.SearchSystem.FilterBugDatas;

            //然后，查找到Bug所在的索引
            int _bugIndex = 0;//bug的索引(从0开始)
            for (int i = 0; i < _filterBugDatas.Count; i++)
            {
                if (_filterBugDatas[i].Id == _bugData.Id)
                {
                    _bugIndex = i;
                    break;
                }
            }

            //查找到Bug所在的页数
            int _pageNumber = (_bugIndex / ShowBugNumber) + 1;


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
        public int GetBugIndexInCurrentPage(BugData _bugData, bool _isReal=false)
        {
            /* _isReal参数：是否查找的是真实的索引？
                            如果为true，从FilterBugDatas中进行查找，那么找到的就是这个Bug在当前页面的真实索引；
                            如果为false，从ShowBugDatas中进行查找，那么找到的就是这个Bug当前在当前页面中的索引 */

            int _index = -1;


            if (_bugData!=null)
            {
                //判断
                switch (_isReal)
                {
                    //从ShowBugDatas中进行查找，那么找到的就是这个Bug当前在当前页面中的索引
                    case false:
                        if (ShowBugItemDatas!=null)
                        {
                            //遍历所有正在显示的Bug
                            for (int i = 0; i < ShowBugItemDatas.Count; i++)
                            {
                                if (BugData.Compare(CompareType.Id,ShowBugItemDatas[i].Data,_bugData))
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
                        if (_filterBugDatas!=null)
                        {
                            //然后，查找到Bug所在的索引
                            for (int i = 0; i < _filterBugDatas.Count; i++)
                            {
                                //如果找到了Bug
                                if (BugData.Compare(CompareType.Id,_filterBugDatas[i],_bugData) == true)
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
        #endregion

    }
}
