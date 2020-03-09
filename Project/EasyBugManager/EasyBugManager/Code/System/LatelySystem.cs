using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;

namespace EasyBugManager
{
    /// <summary>
    /// 最近的系统（用于管理最近的项目）
    /// </summary>
    public class LatelySystem
    {

        #region [公开属性 - 数据]

        /// <summary>
        /// 所有的[最近的项目]
        /// </summary>
        public ObservableCollection<LatelyProjectData> LatelyProjectDatas
        {
            get { return AppManager.Datas.AppData.LatelyProjectDatas; }
            set { AppManager.Datas.AppData.LatelyProjectDatas = value; }
        }

        #endregion

        #region [公开属性 - 路径]
        /// <summary>
        /// [最近的项目]文件的保存位置
        /// </summary>
        public string LatelyFolderPath
        {
            get
            {
                //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
                //返回值是：X:\xxx\xxx (.exe文件所在的目录)
                string _appPath = System.Environment.CurrentDirectory;

                //排序文件夹 [.exe文件所在的目录/Data/Sort]
                return _appPath + "/Data/Lately";
            }
        }

        #endregion



        #region [公开方法 - 保存]

        /// <summary>
        /// 保存[最近的项目]
        /// </summary>
        public void Save()
        {
            try
            {
                if (LatelyProjectDatas == null)
                {
                    LatelyProjectDatas = new ObservableCollection<LatelyProjectData>();
                }



                /* 如果文件夹不存在，就创建文件夹 */
                CreateFolder();



                /* 然后保存当前的排序方式 */
                //把Data转换为BaseData
                List<LatelyProjectBaseData> _latelyProjectBaseDatas = new List<LatelyProjectBaseData>();
                for (int i = 0; i < LatelyProjectDatas.Count; i++)
                {
                    if (LatelyProjectDatas[i]!=null)
                    {
                        _latelyProjectBaseDatas.Add(LatelyProjectBaseData.DataToBaseData(LatelyProjectDatas[i]));
                    }
                }

                //把BaseData转换为json
                string _jsonText = JsonMapper.ToJson(_latelyProjectBaseDatas);

                //文件的路径（文件夹+文件名+后缀）
                string _filePath = LatelyFolderPath + "/Lately" + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                //把json文件保存到[Lately.json]文件里
                File.WriteAllText(_filePath, _jsonText, Encoding.Default);
            }
            catch (Exception e)
            {
            }
        }


        /// <summary>
        /// 读取[最近的项目]
        /// </summary>
        public void Load()
        {
            try
            {
                //文件的路径（文件夹+文件名+后缀）
                string _filePath = LatelyFolderPath + "/Lately" + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                //FileInfo类 用于读取文件信息
                FileInfo _fileInfo = new FileInfo(_filePath);

                /* 判断文件是否存在 */
                if (_fileInfo.Exists == true) //如果存在
                {
                    //读取[最近的项目]的Json文本中的内容
                    string _jsonText = File.ReadAllText(_filePath);

                    //然后把Json文本解析成BaseData对象
                    List<LatelyProjectBaseData> _latelyProjectBaseDatas = null;
                    try
                    {
                        _latelyProjectBaseDatas = JsonMapper.ToObject<List<LatelyProjectBaseData>>(_jsonText);
                    }
                    catch (Exception e)
                    {
                    }

                    //把BaseData对象，转化为Data对象
                    ObservableCollection<LatelyProjectData> _latelyProjectDatas = new ObservableCollection<LatelyProjectData>();
                    if (_latelyProjectBaseDatas != null)
                    {
                        for (int i = 0; i < _latelyProjectBaseDatas.Count; i++)
                        {
                            if (_latelyProjectBaseDatas[i]!=null)
                            {
                                _latelyProjectDatas.Add(LatelyProjectBaseData.BaseDataToData(_latelyProjectBaseDatas[i]));
                            }
                        }
                    }

                    //进行排序
                    _latelyProjectDatas = Sort(_latelyProjectDatas);

                    //然后，赋值
                    LatelyProjectDatas = _latelyProjectDatas;

                    //更新Ui
                    OnLanguageChange(AppManager.Systems.LanguageSystem.LanguageType);

                }
            }
            catch (Exception e)
            {
            }
        }

        #endregion

        #region [公开方法 - 修改]
        /// <summary>
        /// 添加1个数据
        /// </summary>
        /// <param name="_data">Project数据</param>
        /// <param name="_projectFolderPath">项目文件夹的路径(.bug文件所在的文件夹)</param>
        public void Add(ProjectData _data, string _projectFolderPath)
        {
            /* 把Project数据，变为LatelyProject数据 */
            LatelyProjectData _latelyProjectData = new LatelyProjectData();
            _latelyProjectData.Id = _data.Id;
            _latelyProjectData.Name = _data.Name;
            _latelyProjectData.Path = _projectFolderPath;
            _latelyProjectData.Mode = _data.ModeType;


            /* 把数据添加到列表中 */
            Change(_latelyProjectData);
        }


        /// <summary>
        /// 修改1个数据
        /// (修改打开时间)
        /// </summary>
        /// <param name="_data">数据</param>
        public void Change(LatelyProjectData _data)
        {
            //标识符：是否有相同的数据
            bool _isSame = false;

            //遍历所有的数据
            for (int i = 0; i < LatelyProjectDatas.Count; i++)
            {
                /* 如果有一样的数据 */
                if (LatelyProjectData.Compare(CompareType.All, _data, LatelyProjectDatas[i]) == true)
                {
                    //修改当前的打开时间
                    LatelyProjectDatas[i] = _data;
                    _isSame = true;
                    break;
                }
            }


            //如果没有一样的数据 ，就把这个数据添加到所有数据中
            if (_isSame == false)
            {
                LatelyProjectDatas.Add(_data);
            }

            //修改这个数据的打开时间
            _data.OpenTime = DateTime.UtcNow;

            //然后进行排序
            ObservableCollection<LatelyProjectData> _sortDatas = Sort(LatelyProjectDatas);

            //然后只保留10个数据
            List<LatelyProjectData> _deleteDatas = new List<LatelyProjectData>();//要删除的数据
            for (int i = 0; i < _sortDatas.Count; i++)//找出要删除的数据
            {
                if (i >= 10)
                {
                    _deleteDatas.Add(_sortDatas[i]);
                }
            }
            for (int i = 0; i < _deleteDatas.Count; i++)//删除要删除的数据
            {
                _sortDatas.Remove(_deleteDatas[i]);
            }

            //赋值
            LatelyProjectDatas = _sortDatas;

            //更新Ui
            OnLanguageChange(AppManager.Systems.LanguageSystem.LanguageType);

            //保存
            Save();
        }


        /// <summary>
        /// 删除1个数据
        /// </summary>
        /// <param name="_data">数据</param>
        public void Remove(LatelyProjectData _data)
        {
            //删除数据
            LatelyProjectDatas.Remove(_data);

            //保存
            Save();
        }

        #endregion

        #region [公开方法 - 语言]

        /// <summary>
        /// 当软件的[语言]改变时
        /// </summary>
        /// <param name="_languageType">是中文的性格？还是英文的性格？</param>
        public void OnLanguageChange(LanguageType _languageType)
        {
            //修改每一个数据
            for (int i = 0; i < LatelyProjectDatas.Count; i++)
            {
                LatelyProjectData _data = LatelyProjectDatas[i];

                if (_data != null)
                {
                    _data.OpenTimeString = DateTimeTool.DateTimeToString(_data.OpenTime.ToLocalTime(), TimeFormatType.YearMonthDayHourMinute);
                    if (_data.Mode == ModeType.Collaboration)
                    {
                        _data.ModeString = AppManager.Systems.LanguageSystem.CollaborativeModeString;
                    }
                    else
                    {
                        _data.ModeString = "";
                    }
                }
            }

            //修改[最近项目]的右键菜单
            if (AppManager.Uis.LatelyProjectUi.UiControl!=null && AppManager.Uis.LatelyProjectUi.UiControl.Items!=null)
            {
                //获取所有的Item
                List<LatelyProjectListItemControl> _items = AppManager.Uis.LatelyProjectUi.UiControl.Items;

                //遍历所有的Item
                for (int i = 0; i < _items.Count; i++)
                {
                    _items[i].OpenFolderTextBlock.Text = AppManager.Systems.LanguageSystem.LatelyProjectItemOpenFolderString;
                    _items[i].RemoveTextBlock.Text = AppManager.Systems.LanguageSystem.LatelyProjectItemRemoveString;
                }
            }
        }

        #endregion



        #region [私有方法 - 文件]
        /// <summary>
        /// 创建[最近的项目]文件夹
        /// </summary>
        private void CreateFolder()
        {
            //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
            //返回值是：X:\xxx\xxx (.exe文件所在的目录)
            string _appPath = System.Environment.CurrentDirectory;

            //如果.exe/Data文件夹不存在
            DirectoryInfo _dataDirectoryInfo = new DirectoryInfo(_appPath + "/Data");
            if (_dataDirectoryInfo.Exists == false)
            {
                //就创建文件夹
                _dataDirectoryInfo.Create();
            }

            //如果.exe/Data/Lately文件夹不存在
            DirectoryInfo _latelyDirectoryInfo = new DirectoryInfo(LatelyFolderPath);
            if (_latelyDirectoryInfo.Exists == false)
            {
                //就创建文件夹
                _latelyDirectoryInfo.Create();
            }
        }

        #endregion

        #region [私有方法 - 排序]

        /// <summary>
        /// 排序
        /// （对[最近的项目]进行排序）
        /// </summary>
        /// <param name="_datas">要进行排序的数据</param>
        /// <returns>排序后的数据</returns>
        private ObservableCollection<LatelyProjectData> Sort(ObservableCollection<LatelyProjectData> _datas)
        {
            //排序后的数据
            List<LatelyProjectData> _sortDataList = new List<LatelyProjectData>();

            //首先，只取10个数据（最多10个数据）
            for (int i = 0; i < _datas.Count; i++)
            {
                //如果这个数据不为空，并且还没有超过10个数据
                if (_datas[i]!=null && _sortDataList.Count<10)
                {
                    _sortDataList.Add(_datas[i]);
                }
            }

            //对10个数据进行排序（打开时间[从后到前]排序）
            _sortDataList.Sort((project1, project2) =>
            {
                /* 这个Lamba表达式的返回值为int类型，意思是bug1和bug2比较的大小。(大的排后面)
                   如果不能理解这段代码，可以搜索"C# List 多权重排序" */

                int _index = 0;

                //对[创建时间]进行排序（从高到低）
                _index -= DateTime.Compare(project1.OpenTime, project2.OpenTime);

                return _index;
            });

            //把排序后的数据，从List集合变为ObservableCollection集合
            ObservableCollection<LatelyProjectData> _sortDataObservableCollection = new ObservableCollection<LatelyProjectData>();
            for (int i = 0; i < _sortDataList.Count; i++)
            {
                _sortDataObservableCollection.Add(_sortDataList[i]);
            }

            //把ObservableCollection集合返回
            return _sortDataObservableCollection;
        }

        #endregion


    }
}
