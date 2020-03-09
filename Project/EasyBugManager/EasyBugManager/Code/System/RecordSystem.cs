/* By: 絮大王（xudawang@vip.163.com）
   Time：2019年11月24日05:13:26*/

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
    /// 记录的系统（用于保存、读取记录）
    /// </summary>
    public class RecordSystem
    {
        /* 属性：RecordFolderPath([记录文件夹]的路径)（ProjectFolderPath/Record/）
                 LastRecordId(标识符：最后的Record编号)
                 IsShowBugReply(是否显示Bug的回复？)*/

        /* 方法：LoadRecords(读取所有的记录)
                 SaveRecords(保存所有的记录)
                 LoadRecord(读取记录)
                 SaveRecord(保存记录)
                 
                 GetRecordFilePath(根据BugId，来获取记录文件的路径)
                 
                 CreateRecord(创建记录)
                 AddRecord(添加记录)
                 DeleteRecord(删除记录)
                 ReplyRecord(回复记录)
                 
                 SetShowRecords(设置OtherData.ShowRecords属性)*/





        #region [公开属性 - 路径]
        /// <summary>
        /// [记录文件]所在的 文件夹的路径 ([记录文件夹]的路径)
        /// </summary>
        public string RecordFolderPath
        {
            get { return AppManager.Systems.ProjectSystem.RecordFolderPath; }
        }
        #endregion

        #region [公开属性 - 标识符]
        /// <summary>
        /// 是否显示Bug的回复？
        /// </summary>
        public bool IsShowBugReply
        {
            get { return AppManager.Datas.OtherData.IsShowBugReply; }
            set { AppManager.Datas.OtherData.IsShowBugReply = value; }
        }
        #endregion

        #region [公开属性 - 数据]
        /// <summary>
        /// 所有的[Record]数据
        /// </summary>
        public ObservableCollection<RecordData> RecordDatas
        {
            get { return AppManager.Datas.ProjectData.RecordDatas; }
            set { AppManager.Datas.ProjectData.RecordDatas = value; }
        }

        /// <summary>
        /// 所有要显示的[Record]数据 (属于BugUi)
        /// (showRecords属性和records属性区别是，showRecords集合的最后，多一个Type为none的RecordData对象。
        /// 这样做的原因是，showRecords属性在Bug界面显示的时候，最后一个元素和最下面之间，有个空间)
        /// </summary>
        public ObservableCollection<RecordItemData> ShowRecordItemDatas
        {
            get { return AppManager.Datas.OtherData.ShowRecordItemDatas; }
            set { AppManager.Datas.OtherData.ShowRecordItemDatas = value; }
        }
        #endregion



        #region [公开方法 - 保存]
        /// <summary>
        /// 读取所有记录
        /// </summary>
        /// <param name="_modeType">项目模式的类型</param>
        public void LoadRecords(ModeType _modeType)
        {
            try
            {
                //判断项目的模式
                switch (_modeType)
                {

                    //如果项目是[默认模式]：所有的Record，都在/Record/Records.json文件中
                    case ModeType.Default:
                        //Record文件的路径（文件夹+文件名+后缀）
                        string _recordsFilePath = RecordFolderPath + "/Records" + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                        //FileInfo类 用于读取文件信息
                        FileInfo _recordsFileInfo = new FileInfo(_recordsFilePath);

                        /* 判断文件是否存在 */
                        if (_recordsFileInfo.Exists == true)//如果存在
                        {
                            //读取[Record]的Json文本中的内容
                            string _recordsJsonText = File.ReadAllText(_recordsFilePath);

                            //然后把Json文本解析成RecordBaseData对象
                            List<RecordBaseData> _recordBaseDatas = JsonMapper.ToObject<List<RecordBaseData>>(_recordsJsonText);

                            //创建RecordData对象的集合
                            ObservableCollection<RecordData> _recordDatas = new ObservableCollection<RecordData>();

                            //把BugBaseData对象，转化为BugData对象
                            if (_recordBaseDatas != null)
                            {
                                for (int i = 0; i < _recordBaseDatas.Count; i++)
                                {
                                    RecordData _recordData = RecordBaseData.BaseDataToData(_recordBaseDatas[i]);
                                    if (_recordData != null)
                                    {
                                        _recordDatas.Add(_recordData);
                                    }
                                }
                            }

                            //把[RecordData对象]赋值到[列表]中
                            RecordDatas = _recordDatas;
                        }

                        break;



                    //如果项目是[协同合作模式]：每一个Record，分别在/Record/Record - 20200119080230555.json文件中
                    case ModeType.Collaboration:
                        //取到Record文件夹 的信息
                        DirectoryInfo _recordDirectoryInfo = new DirectoryInfo(RecordFolderPath);

                        //获取到Record文件夹内所有的文件 的信息
                        FileInfo[] _recordFileInfos = _recordDirectoryInfo.GetFiles();

                        //遍历所有的Record文件
                        for (int i = 0; i < _recordFileInfos.Length; i++)
                        {
                            //取到Record文件的名字
                            string _recordFileName = Path.GetFileNameWithoutExtension(_recordFileInfos[i].FullName);

                            //把[Record文件的名字]转换为[RecordId]
                            _recordFileName = _recordFileName.Replace("Record - ", "");
                            long _recordId = -1;
                            bool _isParseOk = long.TryParse(_recordFileName, out _recordId);//把string转换为long

                            //如果转换成功
                            if (_isParseOk == true)
                            {
                                //就读取这个Record
                                LoadRecord(ModeType.Collaboration, _recordId);
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
        /// 保存所有记录
        /// </summary>
        /// <param name="_modeType">项目模式的类型</param>
        public void SaveRecords(ModeType _modeType)
        {
            /* 保存 */
            try
            {
                //获取所有的Record
                ObservableCollection<RecordData> _recordDatas = RecordDatas;

                //判断项目的模式
                switch (_modeType)
                {

                    //如果项目是[默认模式]：把所有的Record，保存到/Record/Records.json文件中
                    case ModeType.Default:
                        //把RecordData转换为RecordBaseData
                        List<RecordBaseData> _recordBaseDatas = new List<RecordBaseData>();
                        for (int i = 0; i < _recordDatas.Count; i++)
                        {
                            RecordBaseData _recordBaseData = RecordBaseData.DataToBaseData(_recordDatas[i]);
                            if (_recordBaseData != null)
                            {
                                _recordBaseDatas.Add(_recordBaseData);
                            }

                        }

                        //把RecordBaseData转换为json
                        string _recordsJsonText = JsonMapper.ToJson(_recordBaseDatas);

                        //Record文件的路径（文件夹+文件名+后缀）
                        string _recordsFilePath = RecordFolderPath + "/Records" + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                        //把json文件保存到[Records.json]文件里
                        File.WriteAllText(_recordsFilePath, _recordsJsonText, Encoding.Default);
                        break;



                    //如果项目是[协同合作模式]：把每一个Record，分别保存到/Record/Record - 20200119080230555.json文件中
                    case ModeType.Collaboration:
                        for (int i = 0; i < _recordDatas.Count; i++)
                        {
                            SaveRecord(ModeType.Collaboration, _recordDatas[i].Id);
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
        /// 读取1个记录
        /// </summary>
        /// <param name="_modeType">项目模式的类型</param>
        /// <param name="_recordId">Record的编号（通过Record的编号，来读取记录）</param>
        public void LoadRecord(ModeType _modeType, long _recordId)
        {
            try
            {
                //判断项目的模式
                switch (_modeType)
                {

                    //如果项目是[默认模式]：所有的Record，都在/Record/Records.json文件中
                    case ModeType.Default:
                        LoadRecords(_modeType);
                        break;



                    //如果项目是[协同合作模式]：每一个Record，分别在/Record/Record - 20200119080230555.json文件中
                    case ModeType.Collaboration:
                        //Record文件的路径（文件夹+文件名+后缀）
                        string _recordFilePath = RecordFolderPath + "/Record - " + _recordId + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                        //FileInfo类 用于读取文件信息
                        FileInfo _recordFileInfo = new FileInfo(_recordFilePath);

                        /* 判断文件是否存在 */
                        if (_recordFileInfo.Exists == true)//如果存在
                        {
                            //读取[Record]的Json文本中的内容
                            string _recordJsonText = File.ReadAllText(_recordFilePath);

                            //然后把Json文本解析成RecordBaseData对象
                            RecordBaseData _recordBaseData = null;
                            try
                            {
                                _recordBaseData = JsonMapper.ToObject<RecordBaseData>(_recordJsonText);
                            }
                            catch (Exception e)
                            {
                            }

                            //把BugBaseData对象，转化为BugData对象
                            RecordData _recordData = RecordBaseData.BaseDataToData(_recordBaseData);


                            //如果RecordData的完整度为true
                            if (RecordData.VerifyIntegrity(_recordData) == true)
                            {
                                //把[RecordData对象]赋值到[列表]中
                                RecordData _oldRecordData = GetRecordData(_recordData.Id); //通过BugId获取到旧的Record对象
                                if (_oldRecordData != null) //如果有旧的Record对象
                                {
                                    //如果旧的Record和新的Record有不同的地方
                                    if (RecordData.Compare(CompareType.All, _recordData, _oldRecordData) == false)
                                    {
                                        //修改旧的Record对象的值
                                        _oldRecordData.BugId = _recordData.BugId;
                                        _oldRecordData.ReplyId = _recordData.ReplyId;
                                        _oldRecordData.Content = _recordData.Content;
                                        _oldRecordData.Time = _recordData.Time;
                                        _oldRecordData.IsDelete = _recordData.IsDelete;
                                        _oldRecordData.Images = _recordData.Images;
                                    }
                                }
                                else
                                {
                                    RecordDatas.Add(_recordData); //把读取到的Record对象，添加到列表中
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
        /// 保存1个记录
        /// </summary>
        /// <param name="_modeType">项目模式的类型</param>
        /// <param name="_recordId">要保存的Record的编号</param>
        public void SaveRecord(ModeType _modeType, long _recordId)
        {
            /* 保存 */
            try
            {
                //判断项目的模式
                switch (_modeType)
                {

                    //如果项目是[默认模式]：把所有的Record，保存到/Record/Records.json文件中
                    case ModeType.Default:
                        SaveRecords(_modeType);
                        break;



                    //如果项目是[协同合作模式]：把每一个Record，分别保存到/Record/Record - 20200119080230555.json文件中
                    case ModeType.Collaboration:
                        //通过Id取到Record
                        RecordData _recordData = GetRecordData(_recordId);

                        if (_recordData != null)
                        {
                            //把RecordData转换为RecordBaseData
                            RecordBaseData _recordBaseData = RecordBaseData.DataToBaseData(_recordData);

                            //把RecordBaseData转换为json
                            string _recordJsonText = JsonMapper.ToJson(_recordBaseData);

                            //Record文件的路径（文件夹+文件名+后缀）
                            string _recordFilePath = RecordFolderPath + "/Record - " + _recordBaseData.Id + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                            //把json文件保存到[Record - RecordId.json]文件里
                            File.WriteAllText(_recordFilePath, _recordJsonText, Encoding.Default);
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
        /// 添加记录
        /// (往Bug中，添加1条记录)
        /// </summary>
        /// <param name="_bugData">对哪个BugData进行操作？</param>
        /// <param name="_content">记录的内容</param>
        /// <param name="_images">图片的路径</param>
        public void AddRecord(BugData _bugData, string _content, ObservableCollection<string> _images = null)
        {
            /* 创建记录 */
            //创建记录数据
            RecordData _recordData = new RecordData();
            _recordData.Id = DateTimeTool.DateTimeToLong(DateTime.UtcNow, TimeFormatType.YearMonthDayHourMinuteSecondMillisecond);
            _recordData.BugId = _bugData.Id;
            _recordData.Content = _content;
            _recordData.Time = DateTime.UtcNow;

            //添加到Record数据中
            AppManager.Datas.ProjectData.RecordDatas.Add(_recordData);

            //保存图片
            if (_images != null)
            {
                //遍历所有的图片
                for (int i = 0; i < _images.Count; i++)
                {
                    //保存图片（赋值图片）
                    AppManager.Systems.ImageSystem.AddImageFile(_recordData.Id, _images[i]);
                }
            }

            //赋值Bug里的ShowRecords属性
            AppManager.Systems.RecordSystem.SetShowRecords(_bugData, AppManager.Datas.OtherData.IsShowBugReply);





            /* 回复记录 (当Bear说话后，Bug需要回复一句话) */
            _recordData.ReplyId = AppManager.Systems.TemperamentSystem.RandomReply(_bugData.TemperamentId);//随机1个回复





            /* 保存记录 */
            SaveRecord(AppManager.Systems.CollaborationSystem.ModeType, _recordData.Id);
        }






        /// <summary>
        /// 删除记录
        /// (删除Bug中的一条记录)
        /// </summary>
        public void RemoveRecord(RecordData _recordData)
        {
            //所有记录
            ObservableCollection<RecordData> _allRecords = RecordDatas;
            ObservableCollection<RecordItemData> _allShowRecords = ShowRecordItemDatas;

            //标记要删除的记录
            /* (把1个记录文件，标记为要删除的文件)
               (如果不标记，直接删除的话，会报错。所以先标记，然后在下次软件启动的时候，再由[DeleteSystem系统]真正的把文件删除)*/
            _recordData.IsDelete = true;

            //把记录从ShowRecordItemDatas字段中删除
            for (int i = 0; i < _allShowRecords.Count; i++)
            {
                _allShowRecords.Remove(_recordData.BearRecordItemData);
                _allShowRecords.Remove(_recordData.BugRecordItemData);
            }

            //保存记录
            SaveRecord(AppManager.Systems.CollaborationSystem.ModeType, _recordData.Id);
        }

        /// <summary>
        /// 删除记录
        /// (删除1个Bug中的所有记录)
        /// </summary>
        public void RemoveRecords(BugData _bugData)
        {
            //获取到Bug相关的所有记录数据
            ObservableCollection<RecordData> _recordDatas = GetRecordDatas(_bugData.Id);

            //然后删除这些记录数据
            for (int i = 0; i < _recordDatas.Count; i++)
            {
                RemoveRecord(_recordDatas[i]);
            }
        }

        #endregion

        #region [公开方法 - 获取]

        /// <summary>
        /// 获取记录的数据
        /// (通过记录的编号，获取记录的数据)
        /// </summary>
        /// <param name="_recordId">记录的编号</param>
        /// <returns></returns>
        public RecordData GetRecordData(long _recordId)
        {
            //获取到所有的Record
            ObservableCollection<RecordData> _allRecordDatas = RecordDatas;

            //遍历所有Record
            for (int i = 0; i < _allRecordDatas.Count; i++)
            {
                if (_allRecordDatas[i].Id == _recordId)
                {
                    return _allRecordDatas[i];
                }
            }


            return null;
        }

        /// <summary>
        /// 获取Bug中的记录数据
        /// (通过Bug的编号，获取所有的记录数据)
        /// </summary>
        /// <param name="_bugId">Bug的编号</param>
        /// <returns>Bug中的所有记录</returns>
        public ObservableCollection<RecordData> GetRecordDatas(long _bugId)
        {
            ObservableCollection<RecordData> _recordDatas = new ObservableCollection<RecordData>();

            for (int i = 0; i < RecordDatas.Count; i++)
            {
                //如果记录是属于这个Bug的
                if (RecordDatas[i].BugId == _bugId)
                {
                    _recordDatas.Add(RecordDatas[i]);
                }
            }

            return _recordDatas;
        }

        #endregion



        #region [公开方法 - ShowRecords]
        /// <summary>
        /// 设置ShowRecords属性
        /// （设置OtherData.ShowRecords属性）
        /// </summary>
        /// <param name="_bugData">要显示哪个Bug中的记录？</param>
        /// <param name="_isHaveBug">是否包含Bug？(如果为true，表示要显示Bug说的话，也要显示Bear说的话；如果为false，表示只显示Bear说的话)</param>
        public void SetShowRecords(BugData _bugData, bool _isHaveBug = true)
        {
            /*赋值ShowRecords属性*/
            //如果[Bug说的话]和[Bear说的话]都要显示
            if (_isHaveBug == true)
            {
                SetShowRecords(_bugData, ShowBugRecordType.All);
            }
            //如果只显示[Bear说的话]
            else
            {
                SetShowRecords(_bugData, ShowBugRecordType.One);
            }
        }



        /// <summary>
        /// 设置ShowRecords属性
        /// （设置OtherData.ShowRecords属性）
        /// </summary>
        /// <param name="_bugData">要显示哪个Bug中的记录？</param>
        /// <param name="_showBugRecordType">[显示Bug记录]的类型</param>
        private void SetShowRecords(BugData _bugData, ShowBugRecordType _showBugRecordType)
        {
            if (_bugData == null || RecordDatas == null) return;



            /*容器：所有要显示的Record数据*/
            ObservableCollection<RecordItemData> _showrRecordItemDatas = new ObservableCollection<RecordItemData>();

            /*取到这个Bug中的Record*/
            List<RecordData> _recordDatas = ObservableCollectionTool.ObservableCollectionToList<RecordData>(GetRecordDatas(_bugData.Id));

            /* 进行排序：自定义排序方法 */
            _recordDatas.Sort((record1, record2) =>
            {
                /* 这个Lamba表达式的返回值为int类型，意思是record1和record2比较的大小。(大的排后面)*/
                return DateTime.Compare(record1.Time, record2.Time);
            });



            /*赋值ShowRecords属性
             (-1是[Bug创建时说的话])*/
            for (int i = -1; i < _recordDatas.Count; i++)//遍历Records集合
            {
                //Bug创建时说的话
                if (i <= -1)
                {
                    switch (_showBugRecordType)
                    {
                        //如果显示[所有][Bug说的话]
                        case ShowBugRecordType.All:
                            _showrRecordItemDatas.Add(new RecordItemData()
                            {
                                Type = RecordType.Bug,
                                Content = AppManager.Systems.TemperamentSystem.GetCreateString(_bugData.TemperamentId, 0),
                            });
                            break;

                        //如果显示[1条][Bug说的话]
                        case ShowBugRecordType.One:
                            //Bug创建时说的话
                            if (_recordDatas.Count <= 0)
                            {
                                _showrRecordItemDatas.Add(new RecordItemData()
                                {
                                    Type = RecordType.Bug,
                                    Content = AppManager.Systems.TemperamentSystem.GetCreateString(_bugData.TemperamentId, 0),
                                });
                            }
                            break;

                        //如果显示[0条][Bug说的话]
                        case ShowBugRecordType.Zero:
                            break;
                    }
                }

                //Bear说的话，以及 Bug回复时说的话
                else if (_recordDatas[i].IsDelete != true)
                {
                    switch (_showBugRecordType)
                    {
                        //如果显示[所有][Bug说的话]
                        case ShowBugRecordType.All:
                            //Bear的话
                            _showrRecordItemDatas.Add(_recordDatas[i].BearRecordItemData);

                            //Bug的话
                            if (_recordDatas[i].ReplyId >= 0)
                            {
                                _recordDatas[i].BugRecordItemData.Content = AppManager.Systems.TemperamentSystem.GetReplyString(_bugData.TemperamentId, _recordDatas[i].ReplyId);
                                _showrRecordItemDatas.Add(_recordDatas[i].BugRecordItemData);
                            }
                            break;

                        //如果显示[1条][Bug说的话]
                        case ShowBugRecordType.One:
                            //把Bear说的话，直接加入到列表中
                            _showrRecordItemDatas.Add(_recordDatas[i].BearRecordItemData);

                            //如果是最后一条记录数据，才加入到列表中
                            if (i == (_recordDatas.Count - 1) && _recordDatas[i].ReplyId >= 0)
                            {
                                _recordDatas[i].BugRecordItemData.Content = AppManager.Systems.TemperamentSystem.GetReplyString(_bugData.TemperamentId, _recordDatas[i].ReplyId);
                                _showrRecordItemDatas.Add(_recordDatas[i].BugRecordItemData);
                            }
                            break;

                        //如果显示[0条][Bug说的话]
                        case ShowBugRecordType.Zero:
                            //把Bear说的话，直接加入到列表中
                            _showrRecordItemDatas.Add(_recordDatas[i].BearRecordItemData);
                            break;
                    }
                }
            }

            //在集合中，多添加一个元素。这个最后1个元素的Type属性为None
            _showrRecordItemDatas.Add(new RecordItemData() { Type = RecordType.None });

            //显示
            ShowRecordItemDatas = _showrRecordItemDatas;
        }

        #endregion




        #region [私有方法 - 路径]

        /// <summary>
        /// 获取记录文件的路径
        /// （根据BugId，来获取记录文件的路径）
        /// </summary>
        /// <param name="_bugId">Bug的编号(要获取哪个记录文件？)</param>
        /// <returns>记录文件的路径</returns>
        public string GetRecordFilePath(long _bugId)
        {
            return RecordFolderPath + "/" + _bugId + AppManager.Systems.ProjectSystem.OtherFileSuffix;
        }

        #endregion

    }
}
