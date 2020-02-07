/* By: 絮大王（sukiup@163.com）
   Time：2019年11月24日22:47:20*/

using LitJson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// 排序的系统（为BugList排序）
    /// </summary>
    public class SortSystem
    {

        #region [公开属性 - 类型]

        /// <summary>
        /// 排序的数据
        /// </summary>
        public SortData SortData
        {
            get { return AppManager.Datas.SortData; }
            set { AppManager.Datas.SortData = value; }
        }

        #endregion

        #region [公开属性 - 数据]
        /// <summary>
        /// 排序后的Bugs
        /// </summary>
        public ObservableCollection<BugData> SortBugDatas { get; set; }

        #endregion

        #region [公开属性 - 路径]
        /// <summary>
        /// 排序文件的保存位置
        /// </summary>
        public string SortFolderPath
        {
            get
            {
                //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
                //返回值是：X:\xxx\xxx (.exe文件所在的目录)
                string _appPath = System.Environment.CurrentDirectory;
                
                //排序文件夹 [.exe文件所在的目录/Data/Sort]
                return _appPath + "/Data/Sort";
            }
        }

        #endregion



        #region [构造方法]

        public SortSystem()
        {
            SortBugDatas = new ObservableCollection<BugData>();
        }

        #endregion



        #region [公开方法 - 保存]
        /// <summary>
        /// 保存[排序方式]
        /// </summary>
        public void SaveSort()
        {
            try
            {
                if (SortData==null)return;



                /* 如果文件夹不存在，就创建文件夹 */
                CreateSortFolder();



                /* 然后保存当前的排序方式 */
                //把SortData转换为SortBaseData
                SortBaseData _sortBaseData = SortBaseData.DataToBaseData(SortData);

                //把SortBaseData转换为json
                string _sortJsonText = JsonMapper.ToJson(_sortBaseData);

                //Sort文件的路径（文件夹+文件名+后缀）
                string _sortFilePath = SortFolderPath +"/Sort - "+ AppManager.Systems.ProjectSystem.ProjectData.Id + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                //把json文件保存到[Sort - ProjectId.json]文件里
                File.WriteAllText(_sortFilePath, _sortJsonText, Encoding.Default);
            }
            catch (Exception e)
            {
            }
        }


        /// <summary>
        /// 读取[排序方式]
        /// </summary>
        public void LoadSort()
        {
            try
            {
                //Sort文件的路径（文件夹+文件名+后缀）
                string _sortFilePath = SortFolderPath + "/Sort - " + AppManager.Systems.ProjectSystem.ProjectData.Id +
                                       AppManager.Systems.ProjectSystem.OtherFileSuffix;

                //FileInfo类 用于读取文件信息
                FileInfo _sortFileInfo = new FileInfo(_sortFilePath);

                /* 判断文件是否存在 */
                if (_sortFileInfo.Exists == true) //如果存在
                {
                    //读取[Sort]的Json文本中的内容
                    string _sortJsonText = File.ReadAllText(_sortFilePath);

                    //然后把Json文本解析成SortBaseData对象
                    SortBaseData _sortBaseData = null;
                    try
                    {
                        _sortBaseData = JsonMapper.ToObject<SortBaseData>(_sortJsonText);
                    }
                    catch (Exception e)
                    {
                    }

                    //把SortBaseData对象，转化为SortData对象
                    SortData _sortData = SortBaseData.BaseDataToData(_sortBaseData);

                    //然后，赋值
                    if (_sortData!=null)
                    {
                        SortData = _sortData;
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        #endregion

        #region [公开方法 - 排序]
        /// <summary>
        /// 排序（对Bug集合进行排序）
        /// </summary>
        /// <returns>排序过后的Bug集合</returns>
        public ObservableCollection<BugData> Sort()
        {
            //排序（对Bug集合进行排序）
            SortBugDatas = this.Sort(AppManager.Datas.ProjectData.BugDatas,
                                     SortData.ProgressSortType,
                                     SortData.PrioritySortType,
                                     SortData.CreateTimeSortType,
                                     SortData.UpdateTimeSortType);

            return SortBugDatas;
        }

        #endregion

        
        
        #region [私有方法 - 排序]

        /// <summary>
        /// 排序（对Bug集合进行排序）
        /// </summary>
        /// <param name="_bugDatas">要排序的集合</param>
        /// <param name="_progressSortType">[进度]的排序类型</param>
        /// <param name="_prioritySortType">[优先级]的排序类型</param>
        /// <param name="_createTimeSortType">[创建时间]的排序类型</param>
        /// <param name="_updateTimeSortType">[更新时间]的排序类型</param>
        /// <returns>排序过后的Bug集合</returns>
        private ObservableCollection<BugData> Sort(ObservableCollection<BugData> _bugDatas,
                                                   SortType _progressSortType, SortType _prioritySortType,
                                                   SortType _createTimeSortType, SortType _updateTimeSortType)
        {

            /* 把ObservableCollection集合变为List集合 */
            List<BugData> _oldBugDatas = new List<BugData>();
            if (_bugDatas!=null)
            {
                for (int i = 0; i < _bugDatas.Count; i++)
                {
                    if (_bugDatas[i]!=null && _bugDatas[i].IsDelete != true)//判断下，如果这个Bug没有被删除，就添加到列表中
                    {
                        _oldBugDatas.Add(_bugDatas[i]);
                    }
                }
            }



            /* 进行排序：自定义排序方法 */
            //对[创建时间]排序
            _oldBugDatas.Sort((bug1, bug2) =>
            {
                /* 这个Lamba表达式的返回值为int类型，意思是bug1和bug2比较的大小。(大的排后面)
                   如果不能理解这段代码，可以搜索"C# List 多权重排序" */

                int _index = 0;

                //对[创建时间]进行排序
                switch (_createTimeSortType)
                {
                    case SortType.LowToHigh:
                        _index += DateTime.Compare(bug1.CreateTime, bug2.CreateTime);
                        break;
                    case SortType.HighToLow:
                        _index -= DateTime.Compare(bug1.CreateTime, bug2.CreateTime);
                        break;
                }

                //对[更新时间]进行排序
                switch (_updateTimeSortType)
                {
                    case SortType.LowToHigh:
                        _index += DateTime.Compare(bug1.UpdateTime, bug2.UpdateTime);
                        break;
                    case SortType.HighToLow:
                        _index -= DateTime.Compare(bug1.UpdateTime, bug2.UpdateTime);
                        break;
                }

                //对[优先级]进行排序 （权重为2）
                switch (_prioritySortType)
                {
                    case SortType.LowToHigh:
                        _index += ((Enums.EnumValueCompare((int)bug1.Priority, (int)bug2.Priority)) * 2);
                        break;
                    case SortType.HighToLow:
                        _index -= ((Enums.EnumValueCompare((int)bug1.Priority, (int)bug2.Priority)) * 2);
                        break;
                }

                //对[进度]进行排序 （权重为4）
                switch (_progressSortType)
                {
                    case SortType.LowToHigh:
                        _index += ((Enums.EnumValueCompare((int)bug1.Progress, (int)bug2.Progress)) * 4);
                        break;
                    case SortType.HighToLow:
                        _index -= ((Enums.EnumValueCompare((int)bug1.Progress, (int)bug2.Progress)) * 4);
                        break;
                }

                return _index;
            });



            /* 把排序后的Bug，装进_newBugDatas变量里 */
            ObservableCollection<BugData> _newBugDatas = new ObservableCollection<BugData>();//容器：排序后的Bug集合
            for (int i = 0; i < _oldBugDatas.Count; i++)
            {
                _newBugDatas.Add(_oldBugDatas[i]);
            }


            return _newBugDatas;

        }

        #endregion

        #region [私有方法 - 文件]
        /// <summary>
        /// 创建[排序]文件夹
        /// </summary>
        private void CreateSortFolder()
        {
            //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
            //返回值是：X:\xxx\xxx (.exe文件所在的目录)
            string _appPath = System.Environment.CurrentDirectory;

            //如果.exe/Data文件夹不存在
            DirectoryInfo _dataDirectoryInfo = new DirectoryInfo(_appPath+"/Data");
            if (_dataDirectoryInfo.Exists == false)
            {
                //就创建文件夹
                _dataDirectoryInfo.Create();
            }

            //如果.exe/Data/Sort文件夹不存在
            DirectoryInfo _sortDirectoryInfo = new DirectoryInfo(SortFolderPath);
            if (_sortDirectoryInfo.Exists == false)
            {
                //就创建文件夹
                _sortDirectoryInfo.Create();
            }
        }

        #endregion

    }
}
