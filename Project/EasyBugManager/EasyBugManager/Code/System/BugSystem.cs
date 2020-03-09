/* By: 絮大王（sukiup@163.com）
   Time：2019年11月24日04:46:22*/

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
    /// Bug的系统（用于保存、读取Bug）
    /// </summary>
    public class BugSystem
    {
        /* 属性：BugFilePath([Bug文件]的路径)（ProjectFolderPath/Bug/bug.json）
                 BugFolderPath([Bug文件夹]的路径)（ProjectFolderPath/Bug/）
                 
                 LastBugId(标识符：最后的Bug编号)
                 
                 AllBugTotalNumber(Bug的总数)
                 UndoneBugTotalNumber(未完成的Bug数量)
                 LowUndoneBugTotalNumber(低优先级并且未完成 的Bug数量)
                 MidUndoneBugTotalNumber(中优先级并且未完成 的Bug数量)
                 HighUndoneBugTotalNumber(高优先级并且未完成 的Bug数量) */


        /* 数据：BugDatas(所有的[Bug]数据) */


        /* 方法：LoadBugs(读取所有Bug)
                 SaveBugs(保存所有Bug)
                 
                 AddBug(添加Bug)
                 DeleteBug(删除Bug)
                 ChangeBug(修改Bug)
                 
                 GetBugData(通过BugId，获取Bug数据)
                 
                 CalculatedBugsNumber(统计Bug个数)*/




        #region [公开属性 - 路径]
        /// <summary>
        /// [Bug文件夹]的路径(就是bug.json文件所在的文件夹路径)
        /// </summary>
        public string BugFolderPath
        {
            get { return AppManager.Systems.ProjectSystem.BugFolderPath; }
        }

        #endregion

        #region [公开属性 - 数据]
        /// <summary>
        /// 所有的[Bug]数据
        /// </summary>
        public ObservableCollection<BugData> BugDatas
        {
            get { return AppManager.Datas.ProjectData.BugDatas; }
            set { AppManager.Datas.ProjectData.BugDatas = value; }
        }

        /// <summary>
        /// 要显示的[Bug]数据
        /// </summary>
        public BugItemData ShowBugItemData
        {
            get { return AppManager.Datas.OtherData.ShowBugItemData; }
            set { AppManager.Datas.OtherData.ShowBugItemData = value; }
        }
        #endregion

        #region [公开属性 - 其他]

        /// <summary>
        /// Bug的总数
        /// </summary>
        public int AllBugTotalNumber
        {
            get { return AppManager.Datas.OtherData.AllBugTotalNumber; }
            set { AppManager.Datas.OtherData.AllBugTotalNumber = value; }
        }

        /// <summary>
        /// 未完成的Bug数量
        /// </summary>
        public int UndoneBugTotalNumber
        {
            get { return AppManager.Datas.OtherData.UndoneBugTotalNumber; }
            set { AppManager.Datas.OtherData.UndoneBugTotalNumber = value; }
        }

        /// <summary>
        /// 低优先级并且未完成 的Bug数量
        /// </summary>
        public int LowUndoneBugTotalNumber
        {
            get { return AppManager.Datas.OtherData.LowUndoneBugTotalNumber; }
            set { AppManager.Datas.OtherData.LowUndoneBugTotalNumber = value; }
        }

        /// <summary>
        /// 中优先级并且未完成 的Bug数量
        /// </summary>
        public int MidUndoneBugTotalNumber
        {
            get { return AppManager.Datas.OtherData.MidUndoneBugTotalNumber; }
            set { AppManager.Datas.OtherData.MidUndoneBugTotalNumber = value; }
        }

        /// <summary>
        /// 高优先级并且未完成 的Bug数量
        /// </summary>
        public int HighUndoneBugTotalNumber
        {
            get { return AppManager.Datas.OtherData.HighUndoneBugTotalNumber; }
            set { AppManager.Datas.OtherData.HighUndoneBugTotalNumber = value; }
        }

        #endregion




        #region [公开方法 - 保存]

        /// <summary>
        /// 读取所有的Bug
        /// </summary>
        /// <param name="_modeType">项目模式的类型</param>
        public void LoadBugs(ModeType _modeType)
        {
            /* 读取 */
            try
            {
                //判断项目的模式
                switch (_modeType)
                {

                    //如果项目是[默认模式]：所有的Bug，都在/Bug/Bugs.json文件中
                    case ModeType.Default:
                        //Bug文件的路径（文件夹+文件名+后缀）
                        string _bugsFilePath = BugFolderPath + "/Bugs" + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                        //FileInfo类 用于读取文件信息
                        FileInfo _fileInfo = new FileInfo(_bugsFilePath);

                        /* 判断文件是否存在 */
                        if (_fileInfo.Exists == true)//如果存在
                        {
                            //读取[Bug]的Json文本中的内容
                            string _bugsJsonText = File.ReadAllText(_bugsFilePath);

                            //然后把Json文本解析成BugBaseData对象
                            List<BugBaseData> _bugBaseDatas = JsonMapper.ToObject<List<BugBaseData>>(_bugsJsonText);

                            //创建BugData对象的集合
                            ObservableCollection<BugData> _bugs = new ObservableCollection<BugData>();

                            //把BugBaseData对象，转化为BugData对象
                            if (_bugBaseDatas != null)
                            {
                                for (int i = 0; i < _bugBaseDatas.Count; i++)
                                {
                                    BugData _bug = BugBaseData.BaseDataToData(_bugBaseDatas[i]);
                                    if (_bug != null)
                                    {
                                        _bugs.Add(_bug);
                                    }
                                }
                            }

                            //把BugData对象赋值
                            BugDatas = _bugs;
                        }

                        break;



                    //如果项目是[协同合作模式]：每一个Bug，分别在/Bug/Bug - 20200119080230555.json文件中
                    case ModeType.Collaboration:
                        //取到Bug文件夹 的信息
                        DirectoryInfo _bugDirectoryInfo = new DirectoryInfo(BugFolderPath);

                        //如果文件夹存在
                        if (_bugDirectoryInfo.Exists == true)
                        {
                            //获取到Bug文件夹内所有的文件 的信息
                            FileInfo[] _bugFileInfos = _bugDirectoryInfo.GetFiles();

                            //遍历所有的Bug文件
                            for (int i = 0; i < _bugFileInfos.Length; i++)
                            {
                                //取到Bug文件的名字
                                string _bugFileName = Path.GetFileNameWithoutExtension(_bugFileInfos[i].FullName);

                                //把[Bug文件的名字]转换为[BugId]
                                _bugFileName = _bugFileName.Replace("Bug - ", "");
                                long _bugId = -1;
                                bool _isParseOk = long.TryParse(_bugFileName, out _bugId);//把string转换为long

                                //如果转换成功
                                if (_isParseOk == true)
                                {
                                    //就读取这个Bug
                                    LoadBug(ModeType.Collaboration, _bugId);
                                }
                            }
                        }

                        break;
                }

            }
            catch (Exception e)
            {
                //输出错误
                AppManager.Uis.ErrorUi.UiControl.TipContent = e.ToString();
                AppManager.Uis.ErrorUi.OpenOrClose(true);
            }
        }

        /// <summary>
        /// 保存所有的Bug
        /// </summary>
        /// <param name="_modeType">项目模式的类型</param>
        public void SaveBugs(ModeType _modeType)
        {
            /* 保存 */
            try
            {
                //获取所有的Bug
                ObservableCollection<BugData> _bugDatas = BugDatas;

                //判断项目的模式
                switch (_modeType)
                {

                    //如果项目是[默认模式]：把所有的Bug，保存到/Bug/Bugs.json文件中
                    case ModeType.Default:
                        //把BugData转换为BugBaseData
                        List<BugBaseData> _bugBaseDatas = new List<BugBaseData>();
                        for (int i = 0; i < _bugDatas.Count; i++)
                        {
                            BugBaseData _bugBaseData = BugBaseData.DataToBaseData(_bugDatas[i]);
                            if (_bugBaseData != null)
                            {
                                _bugBaseDatas.Add(_bugBaseData);
                            }
                        }

                        //把BugBaseData转换为json
                        string _bugsJsonText = JsonMapper.ToJson(_bugBaseDatas);

                        //Bug文件的路径（文件夹+文件名+后缀）
                        string _bugsFilePath = BugFolderPath + "/Bugs" + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                        //把json文件保存到[Bugs.json]文件里
                        File.WriteAllText(_bugsFilePath, _bugsJsonText, Encoding.Default);
                        break;



                    //如果项目是[协同合作模式]：把每一个Bug，分别保存到/Bug/Bug - 20200119080230555.json文件中
                    case ModeType.Collaboration:
                        for (int i = 0; i < _bugDatas.Count; i++)
                        {
                            SaveBug(ModeType.Collaboration, _bugDatas[i].Id);
                        }
                        break;

                }
            }

            /* 如果有错误，就输出错误 */
            catch (Exception e)
            {
                //输出错误
                AppManager.Uis.ErrorUi.UiControl.TipContent = e.ToString();
                AppManager.Uis.ErrorUi.OpenOrClose(true);
            }
        }





        /// <summary>
        /// 读取一个Bug
        /// </summary>
        /// <param name="_modeType">项目模式的类型</param>
        /// <param name="_bugFilePath">要读取的Bug的编号</param>
        public void LoadBug(ModeType _modeType, long _bugId)
        {
            /* 读取 */
            try
            {
                //判断项目的模式
                switch (_modeType)
                {

                    //如果项目是[默认模式]：所有的Bug，都在/Bug/Bugs.json文件中
                    case ModeType.Default:
                        LoadBugs(ModeType.Default);
                        break;



                    //如果项目是[协同合作模式]：每一个Bug，分别在/Bug/Bug - 20200119080230555.json文件中
                    case ModeType.Collaboration:
                        //Bug文件的路径（文件夹+文件名+后缀）
                        string _bugFilePath = BugFolderPath + "/Bug - " + _bugId + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                        //FileInfo类 用于读取文件信息
                        FileInfo _bugFileInfo = new FileInfo(_bugFilePath);

                        /* 判断文件是否存在 */
                        if (_bugFileInfo.Exists == true)//如果存在
                        {
                            //读取[Bug]的Json文本中的内容
                            string _bugJsonText = File.ReadAllText(_bugFilePath);

                            //然后把Json文本解析成BugBaseData对象
                            BugBaseData _bugBaseData = null;
                            try
                            {
                                _bugBaseData = JsonMapper.ToObject<BugBaseData>(_bugJsonText);
                            }
                            catch (Exception e)
                            {
                            }

                            //把BugBaseData对象，转化为BugData对象
                            BugData _bugData = BugBaseData.BaseDataToData(_bugBaseData);

                            //如果BugData的完整度为true
                            if (BugData.VerifyIntegrity(_bugData) == true)
                            {
                                //把BugData对象赋值
                                BugData _oldBugData = GetBugData(_bugData.Id);//通过BugId获取到旧的Bug对象
                                if (_oldBugData != null)//如果有旧的Bug对象
                                {
                                    //如果旧的Bug和新的Bug有不同的地方
                                    if (BugData.Compare(CompareType.All, _bugData, _oldBugData) == false)
                                    {
                                        //修改旧的Bug对象的值
                                        _oldBugData.Name.Text = _bugData.Name.Text;
                                        _oldBugData.Progress = _bugData.Progress;
                                        _oldBugData.Priority = _bugData.Priority;
                                        _oldBugData.CreateTime = _bugData.CreateTime;
                                        _oldBugData.SolveTime = _bugData.SolveTime;
                                        _oldBugData.UpdateTime = _bugData.UpdateTime;
                                        _oldBugData.UpdateNumber = _bugData.UpdateNumber;
                                        _oldBugData.TemperamentId = _bugData.TemperamentId;
                                        _oldBugData.IsDelete = _bugData.IsDelete;
                                    }
                                }
                                else
                                {
                                    BugDatas.Add(_bugData);//把读取到的Bug对象，添加到列表中
                                }
                            }

                        }
                        break;
                }
            }

            /* 如果有错误，就输出错误 */
            catch (Exception e)
            {
                //输出错误
                AppManager.Uis.ErrorUi.UiControl.TipContent = e.ToString();
                AppManager.Uis.ErrorUi.OpenOrClose(true);
            }
        }

        /// <summary>
        /// 保存一个Bug
        /// </summary>
        /// <param name="_modeType">项目模式的类型</param>
        /// <param name="_bugId">要保存的Bug的编号</param>
        public void SaveBug(ModeType _modeType, long _bugId)
        {
            /* 保存 */
            try
            {
                //判断项目的模式
                switch (_modeType)
                {

                    //如果项目是[默认模式]：把所有的Bug，保存到/Bug/Bugs.json文件中
                    case ModeType.Default:
                        SaveBugs(ModeType.Default);
                        break;



                    //如果项目是[协同合作模式]：把每一个Bug，分别保存到/Bug/Bug - 20200119080230555.json文件中
                    case ModeType.Collaboration:
                        //通过Id取到Bug
                        BugData _bugData = GetBugData(_bugId);

                        if (_bugData != null)
                        {
                            //把BugData转换为BugBaseData
                            BugBaseData _bugBaseData = BugBaseData.DataToBaseData(_bugData);

                            //把BugBaseData转换为json
                            string _bugJsonText = JsonMapper.ToJson(_bugBaseData);

                            //Bug文件的路径（文件夹+文件名+后缀）
                            string _bugFilePath = BugFolderPath + "/Bug - " + _bugBaseData.Id + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                            //把json文件保存到[Bug - BugId.json]文件里
                            File.WriteAllText(_bugFilePath, _bugJsonText, Encoding.Default);
                        }
                        break;
                }
            }

            /* 如果有错误，就输出错误 */
            catch (Exception e)
            {
                //输出错误
                AppManager.Uis.ErrorUi.UiControl.TipContent = e.ToString();
                AppManager.Uis.ErrorUi.OpenOrClose(true);
            }

        }

        #endregion

        #region [公开方法 - 修改]

        /// <summary>
        /// 添加Bug
        /// </summary>
        /// <param name="_name">Bug的名字</param>
        /// <param name="_priority">Bug的优先级</param>
        public void AddBug(string _name, PriorityType _priority)
        {
            //创建一个Bug
            BugData _bugData = new BugData();
            _bugData.Id = DateTimeTool.DateTimeToLong(DateTime.UtcNow, TimeFormatType.YearMonthDayHourMinuteSecondMillisecond);
            _bugData.Name = new HighlightText() { Text = _name };
            _bugData.Progress = ProgressType.Undone;
            _bugData.Priority = _priority;
            _bugData.CreateTime = DateTime.UtcNow;
            _bugData.UpdateTime = DateTime.UtcNow;
            _bugData.TemperamentId = 0;

            //把Bug添加到BugDatas中
            BugDatas.Add(_bugData);

            //重新获取Bug的个数+页数
            AppManager.Systems.BugSystem.CalculatedBugsNumber();
            AppManager.Systems.PageSytem.CalculatedPagesNumber();

            //重新排序
            AppManager.Systems.SortSystem.Sort();

            //重新过滤
            AppManager.Systems.SearchSystem.Filter();

            //重新计算页数
            AppManager.Systems.PageSytem.CalculatedPagesNumber();

            //显示Bug到当前页面
            AppManager.Systems.PageSytem.Insert(_bugData);

            //显示ListBug的Tip
            AppManager.Uis.ListUi.UiControl.OpenOrCloseListTip(true, true, _bugData);

            //保存Bug文件
            this.SaveBug(AppManager.Datas.ProjectData.ModeType, _bugData.Id);
        }


        /// <summary>
        /// 删除Bug
        /// </summary>
        /// <param name="_bugData">要删除的Bug数据</param>
        public void DeleteBug(BugData _bugData)
        {
            if (_bugData != null)
            {
                //把这个Bug标记为删除
                _bugData.IsDelete = true;

                //重新获取Bug的个数+页数
                AppManager.Systems.BugSystem.CalculatedBugsNumber();
                AppManager.Systems.PageSytem.CalculatedPagesNumber();

                //重新排序
                AppManager.Systems.SortSystem.Sort();

                //重新过滤
                AppManager.Systems.SearchSystem.Filter();

                //重新计算页数
                AppManager.Systems.PageSytem.CalculatedPagesNumber();

                //刷新界面
                AppManager.Systems.PageSytem.Refresh();

                //显示ListBug
                AppManager.Uis.ListUi.UiControl.OpenOrCloseListTip(true, false);

                //保存Bug文件
                this.SaveBug(AppManager.Datas.ProjectData.ModeType, _bugData.Id);

                //删除Bug中的所有记录
                AppManager.Systems.RecordSystem.RemoveRecords(_bugData);

                //把这个Bug[从Datas中]删除
                BugDatas.Remove(_bugData);
            }
        }


        /// <summary>
        /// 修改Bug
        /// </summary>
        /// <param name="_bugData">要修改的Bug数据</param>
        public void ChangeBug(BugData _bugData)
        {

            if (_bugData != null)
            {
                //修改Bug
                SetUpdateNumber(_bugData);

                //重新获取Bug的个数
                AppManager.Systems.BugSystem.CalculatedBugsNumber();

                //重新排序
                AppManager.Systems.SortSystem.Sort();

                //重新过滤
                AppManager.Systems.SearchSystem.Filter();

                //获取到Bug的新页码
                int _pageNumber = AppManager.Systems.PageSytem.GetPageNumber(_bugData);

                //显示跳转的页码
                _bugData.ItemData.GoToPageNumber = _pageNumber;

                //保存Bug文件
                this.SaveBug(AppManager.Datas.ProjectData.ModeType, _bugData.Id);

            }

        }


        /// <summary>
        /// 修改Bug
        /// </summary>
        /// <param name="_bugData">要修改的Bug数据</param>
        /// <param name="_oldName">旧的名字</param>
        /// <param name="_newName">新的名字</param>
        /// <param name="_oldProgress">旧的进度</param>
        /// <param name="_newProgress">新的进度</param>
        /// <param name="_oldPriority">旧的优先级</param>
        /// <param name="_newPriority">新的优先级</param>
        public void ChangeBug(BugData _bugData,
                              string _oldName, string _newName,
                              ProgressType _oldProgress, ProgressType _newProgress,
                              PriorityType _oldPriority, PriorityType _newPriority)
        {

            /* 判断是否有改变 */
            if (_oldName == _newName && _oldProgress == _newProgress && _oldPriority == _newPriority) return;


            /* 如果有改变，就修改数据 */
            if (_bugData != null)
            {
                //修改数据
                _bugData.Name.Text = _newName;
                _bugData.Progress = _newProgress;
                _bugData.Priority = _newPriority;

                //触发[完成度改变]的方法
                if (_oldProgress != _newProgress)
                {
                    OnChangeBugProgress(_bugData);
                }

                //修改Bug
                ChangeBug(_bugData);
            }

        }

        #endregion

        #region [公开方法 - 完成]
        /// <summary>
        /// 当改变Bug的完成度时
        /// </summary>
        /// <param name="_bugData">Bug的数据</param>
        public void OnChangeBugProgress(BugData _bugData)
        {
            //如果是[完成]
            if (_bugData.Progress == ProgressType.Solved)
            {
                _bugData.SolveTime = DateTime.UtcNow;
            }
            else
            {
                _bugData.SolveTime = DateTime.MinValue;
            }
        }
        #endregion

        #region [公开方法 - 获取]
        /// <summary>
        /// 获取Bug数据
        /// （通过BugId，获取Bug数据）
        /// </summary>
        /// <param name="_bugId">Bug的编号</param>
        /// <returns>Bug的数据</returns>
        public BugData GetBugData(long _bugId)
        {
            //遍历所有的Bug数据
            for (int i = 0; i < BugDatas.Count; i++)
            {
                //如果Bug数据的Id 和参数的Id相同
                if (BugDatas[i].Id == _bugId)
                {
                    return BugDatas[i];
                }
            }

            return null;
        }



        /// <summary>
        /// 获取Bug的数据
        /// (通过记录，获取记录所在的Bug数据)
        /// </summary>
        /// <param name="_recordData">记录的数据</param>
        /// <returns>记录所在的Bug数据</returns>
        public BugData GetBugData(RecordData _recordData)
        {
            if (_recordData != null)
            {
                //根据记录，获取Bug
                BugData _bugData = GetBugData(_recordData.BugId);
                return _bugData;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region [公开方法 - 个数]
        /// <summary>
        /// 统计Bug个数
        /// </summary>
        public void CalculatedBugsNumber()
        {
            int _allBugTotalNumber = 0;//Bug的总数
            int _undoneBugTotalNumber = 0;//未完成的Bug数量
            int _lowUndoneBugTotalNumber = 0;//低优先级并且未完成 的Bug数量
            int _midUndoneBugTotalNumber = 0;//中优先级并且未完成 的Bug数量
            int _highUndoneBugTotalNumber = 0;//高优先级并且未完成 的Bug数量



            //遍历所有Bug数据，计算数量
            for (int i = 0; i < BugDatas.Count; i++)
            {
                if (BugDatas[i].IsDelete != true)
                {
                    //总数
                    _allBugTotalNumber += 1;

                    //如果是[未完成]
                    if (BugDatas[i].Progress == ProgressType.Undone)
                    {
                        //未完成的数量
                        _undoneBugTotalNumber += 1;

                        //优先级的数量
                        switch (BugDatas[i].Priority)
                        {
                            case PriorityType.Low:
                                _lowUndoneBugTotalNumber += 1;
                                break;
                            case PriorityType.Mid:
                                _midUndoneBugTotalNumber += 1;
                                break;
                            case PriorityType.High:
                                _highUndoneBugTotalNumber += 1;
                                break;
                        }
                    }
                }

            }



            //赋值
            AllBugTotalNumber = _allBugTotalNumber;
            UndoneBugTotalNumber = _undoneBugTotalNumber;
            LowUndoneBugTotalNumber = _lowUndoneBugTotalNumber;
            MidUndoneBugTotalNumber = _midUndoneBugTotalNumber;
            HighUndoneBugTotalNumber = _highUndoneBugTotalNumber;
        }

        #endregion

        #region [公开方法 - UpdateNumber]
        /// <summary>
        /// 设置Bug的[更新次数]
        /// </summary>
        /// <param name="_bugData">要操作的是哪个Bug？</param>
        /// <param name="_changeNumber">要修改的个数（如果是1，那么更新次数+1；如果是-1，那么更新次数-1）</param>
        public void SetUpdateNumber(BugData _bugData, int _changeNumber = 1)
        {
            _bugData.UpdateTime = DateTime.UtcNow;
            _bugData.UpdateNumber += _changeNumber;
        }

        #endregion


    }
}
