/* By: 絮大王（xudawang@vip.163.com）
   Time：2020年1月19日07:41:57*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LitJson;

namespace EasyBugManager
{
    /// <summary>
    /// 协同合作的系统
    /// </summary>
    public class CollaborationSystem
    {

        #region [公开属性 - 数据]
        /// <summary>
        /// 项目的模式（默认模式、协同合作模式）
        /// </summary>
        public ModeType ModeType
        {
            get { return AppManager.Datas.ProjectData.ModeType; }
            set { AppManager.Datas.ProjectData.ModeType = value; }
        }
        #endregion

        #region [私有属性 - Watcher]

        /// <summary>
        /// Bug的FileSystemWatcher对象
        /// （用于监控项目的[Bug]文件夹）
        /// </summary>
        private FileSystemWatcher BugFileSystemWatcher { get; set; }

        /// <summary>
        /// Record的FileSystemWatcher对象
        /// （用于监控项目的[Record]文件夹）
        /// </summary>
        private FileSystemWatcher RecordFileSystemWatcher { get; set; }

        #endregion

        #region [公开属性 - 同步]
        /// <summary>
        /// 需要同步的Bug编号
        /// </summary>
        private List<long> SyncBugIds { get; set; }

        /// <summary>
        /// 需要同步的Record编号
        /// </summary>
        private List<long> SyncRecordIds { get; set; }




        /// <summary>
        /// 同步的日志文字（所有同步的日志）
        /// </summary>
        public string SyncLogText
        {
            get { return AppManager.Datas.OtherData.SyncLogText; }
            set { AppManager.Datas.OtherData.SyncLogText = value; }
        }

        /// <summary>
        /// 同步的次数
        /// </summary>
        public int SyncNumber
        {
            get { return AppManager.Datas.OtherData.SyncNumber; }
            set { AppManager.Datas.OtherData.SyncNumber = value; }
        }

        /// <summary>
        /// 最后一次同步的时间
        /// </summary>
        public DateTime LastSyncDateTime
        {
            get { return AppManager.Datas.OtherData.LastSyncDateTime; }
            set { AppManager.Datas.OtherData.LastSyncDateTime = value; }
        }

        /// <summary>
        /// 同步状态的类型
        /// </summary>
        public SyncStateType SyncStateType
        {
            get { return AppManager.Datas.OtherData.SyncStateType; }
            set { AppManager.Datas.OtherData.SyncStateType = value; }
        }
        #endregion

        #region [公开属性 - 文字]

        /// <summary>
        /// [删除]的文字
        /// </summary>
        private string DeleteString { get; set; }

        /// <summary>
        /// [修改]的文字
        /// </summary>
        private string ChangeString { get; set; }

        /// <summary>
        /// [创建]的文字
        /// </summary>
        private string AddString { get; set; }

        /// <summary>
        /// [Bug]的文字
        /// </summary>
        private string BugString { get; set; }

        /// <summary>
        /// [聊天记录]的文字
        /// </summary>
        private string RecordString { get; set; }

        #endregion



        #region [构造方法]

        public CollaborationSystem()
        {
            /*文字*/
            DeleteString = "删除";
            AddString = "添加";
            ChangeString = "修改";
            BugString = "Bug";
            RecordString = "聊天记录";

            /*Watcher*/
            BugFileSystemWatcher = null;
            RecordFileSystemWatcher = null;

            /*同步*/
            SyncBugIds = new List<long>();
            SyncRecordIds = new List<long>();
        }

        #endregion



        #region [公开方法 - 处理]
        /// <summary>
        /// 处理（开启或者关闭协同合作功能）
        /// </summary>
        /// <param name="_isEnableCollaboration">是否开启协同合作功能？</param>
        public void Handle(bool _isEnableCollaboration)
        {

            switch (_isEnableCollaboration)
            {
                /* 打开[协同合作] */
                case true:
                    if (ModeType == ModeType.Collaboration)
                    {
                        //如果[项目的模式]是[协同合作模式]，开启协同合作
                        EnableCollaboration();

                        //打开[协同合作]界面
                        AppManager.Uis.SyncUi.OpenOrClose(true);
                    }
                    else
                    {
                        //如果[项目的模式]不是[协同合作模式]，关闭协同合作
                        DisableCollaboration();

                        //关闭[协同合作]界面
                        AppManager.Uis.SyncUi.OpenOrClose(false);
                    }
                    break;


                /* 关闭[协同合作] */
                case false:
                    //关闭协同合作
                    DisableCollaboration();

                    //关闭[协同合作]界面
                    AppManager.Uis.SyncUi.OpenOrClose(false);
                    break;
            }
        }

        #endregion

        #region [公开方法 - 同步]
        /// <summary>
        /// 同步
        /// （当有文件更改时，3秒后，把已经修改的文件，进行同步）
        /// </summary>
        /// <returns>是否有新的数据被同步？（true代表，有数据被同步了；false代表，这次的修改没有数据被同步）</returns>
        public bool Sync()
        {
            if (ModeType != ModeType.Collaboration) return false;


            /* 获取数据 */
            List<long> _syncBugIds = SyncBugIds;
            List<long> _syncRecordIds = SyncRecordIds;

            /* 清除数据 */
            SyncBugIds = new List<long>();
            SyncRecordIds = new List<long>();

            /* 数据容器 */
            List<string> _bugLogs;//Bug的日志
            List<string> _recordLogs;//记录的日志
            Dictionary<long, ChangeType> _bugChangeTypes;//Bug的改变
            Dictionary<long, ChangeType> _recordChangeTypes;//记录的改变





            /*同步*/
            SyncBug(_syncBugIds, out _bugChangeTypes, out _bugLogs);
            SyncRecord(_syncRecordIds, out _recordChangeTypes, out _recordLogs);





            /*更新Ui：Bug*/
            if (_bugLogs.Count > 0)
            {
                /* 重新排列 */
                //重新获取Bug的个数+页数
                AppManager.Systems.BugSystem.CalculatedBugsNumber();
                AppManager.Systems.PageSytem.CalculatedPagesNumber();

                //重新排序
                AppManager.Systems.SortSystem.Sort();

                //重新过滤
                AppManager.Systems.SearchSystem.Filter();

                //重新计算页数
                AppManager.Systems.PageSytem.CalculatedPagesNumber();



                /*更新Ui*/
                foreach (long _bugId in _bugChangeTypes.Keys)//遍历所有的改变
                {
                    //获取Bug数据
                    BugData _bugData = AppManager.Systems.BugSystem.GetBugData(_bugId);

                    //获取改变
                    ChangeType _changeType = ChangeType.None;
                    _bugChangeTypes.TryGetValue(_bugId, out _changeType);

                    //判断Bug的改变
                    if (_bugData != null)
                    {
                        switch (_changeType)
                        {
                            //如果是[添加Bug]
                            case ChangeType.Add:
                                //把Bug插入到当前的页面中
                                AppManager.Systems.PageSytem.Insert(_bugData);
                                break;

                            //如果是[删除Bug]
                            case ChangeType.Delete:
                                //并且正在显示这个Bug的话
                                if (AppManager.Uis.BugUi.UiControl.Visibility == Visibility.Visible &&
                                    AppManager.Datas.OtherData.ShowBugItemData != null &&
                                    AppManager.Datas.OtherData.ShowBugItemData.Data.Id == _bugId &&
                                    _bugId > -1)
                                {
                                    //关闭修改Bug的界面
                                    if (AppManager.Uis.ChangeBugUi.UiControl.Visibility == Visibility.Visible)
                                    {
                                        AppManager.Uis.ChangeBugUi.OpenOrClose(false);
                                    }

                                    //关闭Bug的界面
                                    AppManager.Uis.BugUi.OpenOrClose(false);

                                    //如果MainUi没有打开
                                    if (AppManager.Uis.MainUi.UiControl.Visibility == Visibility.Collapsed)
                                    {
                                        //就打开ListUi
                                        AppManager.Uis.ListUi.OpenOrClose(true);
                                    }
                                }
                                break;

                            //如果是[修改Bug]
                            case ChangeType.Change:
                                //显示BugItem的GoToPage按钮
                                int _pageNumber = AppManager.Systems.PageSytem.GetPageNumber(_bugData);//获取到Bug的新页码
                                _bugData.ItemData.GoToPageNumber = _pageNumber;//显示跳转的页码
                                break;
                        }
                    }
                }
            }

            /*更新Ui：Record*/
            if (_recordLogs.Count > 0)
            {
                /*更新Ui*/
                foreach (long _recordId in _recordChangeTypes.Keys)
                {
                    //获取Record数据
                    RecordData _recordData = AppManager.Systems.RecordSystem.GetRecordData(_recordId);

                    //获取Bug数据
                    BugData _bugData = AppManager.Systems.BugSystem.GetBugData(_recordData);

                    //如果Bug是正在显示的Bug，那么就刷新BugUi的记录
                    if (AppManager.Datas.OtherData.ShowBugItemData != null &&
                        _bugData != null &&
                        AppManager.Datas.OtherData.ShowBugItemData.Data.Id == _bugData.Id)
                    {
                        //刷新记录
                        AppManager.Systems.RecordSystem.SetShowRecords(_bugData, AppManager.Datas.OtherData.IsShowBugReply);
                    }

                }
            }






            /*日志*/
            if (_bugLogs.Count > 0 || _recordLogs.Count > 0)
            {
                //这次的日志
                string _syncLogText = "";

                //时间
                _syncLogText += "【" + DateTimeTool.DateTimeToString(DateTime.Now, TimeFormatType.YearMonthDayHourMinuteSecond) + "】";

                //Bug的日志
                for (int i = 0; i < _bugLogs.Count; i++)
                {
                    _syncLogText += "\n" + "    " + "(" + (i + 1) + ") " + _bugLogs[i];
                }
                //Record的日志
                for (int i = 0; i < _recordLogs.Count; i++)
                {
                    _syncLogText += "\n" + "    " + "(" + (i + 1) + ") " + _recordLogs[i];
                }

                //提行
                _syncLogText += "\n\n\n";

                //刷新Ui
                SyncLogText = _syncLogText + SyncLogText;
            }


            /*时间和次数*/
            if (_bugLogs.Count > 0 || _recordLogs.Count > 0)
            {
                SyncNumber += 1;
                LastSyncDateTime = DateTime.UtcNow;
            }


            /* 返回值 */
            if (_bugLogs.Count > 0 || _recordLogs.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion



        #region [FileSystemWatcher - Bug文件的监控 事件]
        /// <summary>
        /// 当[创建]文件或文件夹时
        /// </summary>
        private void BugFileSystemWatcher_OnFileCreated(object sender, FileSystemEventArgs e)
        {
            /* FileSystemEventArgs类：
               Name属性：文件的名称
               ChangeType属性：事件类型（Changed、Created、Deleted、Renamed） 
               FullPath属性：文件的完整路径 */

            //MessageBox.Show("新创建1个文件：" + e.FullPath);
        }

        /// <summary>
        /// 当[删除]文件或文件夹时
        /// </summary>
        private void BugFileSystemWatcher_OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            /* FileSystemEventArgs类：
               Name属性：文件的名称
               ChangeType属性：事件类型（Changed、Created、Deleted、Renamed） 
               FullPath属性：文件的完整路径 */

            //MessageBox.Show("新删除1个文件：" + e.FullPath);

            //判断是否Bug被修改，如果这个Bug不在同步列表中，就把它加入同步列表中
            BugFileSystemWatcher_OnFileChanged(sender, e);
        }

        /// <summary>
        /// 当[改名]文件或文件夹时
        /// </summary>
        private void BugFileSystemWatcher_OnFileRenamed(object sender, FileSystemEventArgs e)
        {
            /* FileSystemEventArgs类：
               Name属性：文件的名称
               ChangeType属性：事件类型（Changed、Created、Deleted、Renamed） 
               FullPath属性：文件的完整路径 */

            //MessageBox.Show("新改名1个文件：" + e.FullPath);
        }


        /// <summary>
        /// 当[修改]文件或文件夹时
        /// </summary>
        private void BugFileSystemWatcher_OnFileChanged(object sender, FileSystemEventArgs e)
        {
            /* FileSystemEventArgs类：
               Name属性：文件的名称
               ChangeType属性：事件类型（Changed、Created、Deleted、Renamed） 
               FullPath属性：文件的完整路径 */

            //MessageBox.Show("修改1个文件：" + e.FullPath);

            //如果是[协同合作]模式
            if (ModeType == ModeType.Collaboration)
            {
                //取到Bug文件的名字
                string _bugFileName = Path.GetFileNameWithoutExtension(e.FullPath);

                //把[Bug文件的名字]转换为[BugId]
                _bugFileName = _bugFileName.Replace("Bug - ", "");
                long _bugId = -1;
                bool _isParseOk = long.TryParse(_bugFileName, out _bugId);//把string转换为long

                //如果转换成功
                if (_isParseOk == true)
                {
                    //这个Bug，是否已经需要同步了？
                    bool _isSyncHaveSameBug = false;

                    //遍历所有需要同步的Bug
                    for (int i = 0; i < SyncBugIds.Count; i++)
                    {
                        if (SyncBugIds[i] == _bugId)
                        {
                            _isSyncHaveSameBug = true;
                        }
                    }

                    //如果这个Bug不在同步列表中，就把它加入同步列表中
                    if (_isSyncHaveSameBug == false)
                    {
                        SyncBugIds.Add(_bugId);

                        //然后开始准备同步
                        SyncStateType = SyncStateType.WaitSync;
                    }
                }
            }
        }
        #endregion

        #region [FileSystemWatcher - Record文件的监控 事件]

        /// <summary>
        /// 当[创建]文件或文件夹时
        /// </summary>
        private void RecordFileSystemWatcher_OnFileCreated(object sender, FileSystemEventArgs e)
        {
            /* FileSystemEventArgs类：
               Name属性：文件的名称
               ChangeType属性：事件类型（Changed、Created、Deleted、Renamed） 
               FullPath属性：文件的完整路径 */

            //MessageBox.Show("新创建1个文件：" + e.FullPath);
        }

        /// <summary>
        /// 当[删除]文件或文件夹时
        /// </summary>
        private void RecordFileSystemWatcher_OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            /* FileSystemEventArgs类：
               Name属性：文件的名称
               ChangeType属性：事件类型（Changed、Created、Deleted、Renamed） 
               FullPath属性：文件的完整路径 */

            //MessageBox.Show("新删除1个文件：" + e.FullPath);

            //判断是否Record被修改，如果这个Record不在同步列表中，就把它加入同步列表中
            RecordFileSystemWatcher_OnFileChanged(sender, e);
        }

        /// <summary>
        /// 当[改名]文件或文件夹时
        /// </summary>
        private void RecordFileSystemWatcher_OnFileRenamed(object sender, FileSystemEventArgs e)
        {
            /* FileSystemEventArgs类：
               Name属性：文件的名称
               ChangeType属性：事件类型（Changed、Created、Deleted、Renamed） 
               FullPath属性：文件的完整路径 */

            //MessageBox.Show("新改名1个文件：" + e.FullPath);
        }

        /// <summary>
        /// 当[修改]文件或文件夹时
        /// </summary>
        private void RecordFileSystemWatcher_OnFileChanged(object sender, FileSystemEventArgs e)
        {
            /* FileSystemEventArgs类：
               Name属性：文件的名称
               ChangeType属性：事件类型（Changed、Created、Deleted、Renamed） 
               FullPath属性：文件的完整路径 */

            //MessageBox.Show("修改1个文件：" + e.FullPath);

            //如果是[协同合作]模式
            if (ModeType == ModeType.Collaboration)
            {
                //取到Record文件的名字
                string _recordFileName = Path.GetFileNameWithoutExtension(e.FullPath);

                //把[Record文件的名字]转换为[RecordId]
                _recordFileName = _recordFileName.Replace("Record - ", "");
                long _recordId = -1;
                bool _isParseOk = long.TryParse(_recordFileName, out _recordId);//把string转换为long

                //如果转换成功
                if (_isParseOk == true)
                {
                    //这个Record，是否已经需要同步了？
                    bool _isSyncHaveSameRecord = false;

                    //遍历所有需要同步的Record
                    for (int i = 0; i < SyncRecordIds.Count; i++)
                    {
                        if (SyncRecordIds[i] == _recordId)
                        {
                            _isSyncHaveSameRecord = true;
                        }
                    }

                    //如果这个Record不在同步列表中，就把它加入同步列表中
                    if (_isSyncHaveSameRecord == false)
                    {
                        SyncRecordIds.Add(_recordId);

                        //然后开始准备同步
                        SyncStateType = SyncStateType.WaitSync;
                    }
                }
            }
        }
        #endregion



        #region [私有方法 - 监控]
        /// <summary>
        /// 初始化[协同合作]功能
        /// </summary>
        private void AwakeCollaboration()
        {
            if (ModeType == ModeType.Collaboration)
            {
                /* Bug文件的Watcher */
                if (BugFileSystemWatcher == null)
                {
                    FileSystemWatcher _bugFileSystemWatcher = new FileSystemWatcher();

                    //设置是否启用监听?（true启用监听;false关闭监听）
                    _bugFileSystemWatcher.EnableRaisingEvents = false;

                    //要监控的文件夹路径
                    _bugFileSystemWatcher.Path = AppManager.Systems.ProjectSystem.BugFolderPath;

                    //要监控什么文件？
                    _bugFileSystemWatcher.Filter = "*.json";

                    //是否监控子文件夹？
                    _bugFileSystemWatcher.IncludeSubdirectories = false;

                    //缓冲区可以设置 4 KB 或更大，但不能超过 64 KB（默认值为8kb）
                    //增加缓冲区的大小可以防止丢失[文件更改的事件]。 但是，增加缓冲区大小的开销较大，因为它来自无法换出到磁盘的非分页内存，所以将缓冲区保持得越小越好。 
                    _bugFileSystemWatcher.InternalBufferSize = 16;

                    //要监听文件的哪些改变？（比如要监听文件的创建时间、文件的名字改变、文件的大小改变等）
                    /*  枚举值：
                        Attributes - 文件或文件夹的属性
                        CreationTime - 文件或文件夹的创建时间
                        DirectoryName - 目录的名称
                        FileName - 文件的名称
                        LastAccess - 文件或文件夹上一次打开的日期
                        LastWrite - 上一次向文件或文件夹写入内容的日期
                        Security - 文件或文件夹的安全设置
                        Size - 文件或文件夹的大小 */
                    _bugFileSystemWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime |
                                           NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
                                           | NotifyFilters.LastWrite | NotifyFilters.Security |
                                           NotifyFilters.Size;

                    //注册监听事件
                    _bugFileSystemWatcher.Created += new FileSystemEventHandler(BugFileSystemWatcher_OnFileCreated);//当创建文件或文件夹时
                    _bugFileSystemWatcher.Deleted += new FileSystemEventHandler(BugFileSystemWatcher_OnFileDeleted);//当删除文件或文件夹时
                    _bugFileSystemWatcher.Renamed += new RenamedEventHandler(BugFileSystemWatcher_OnFileRenamed);//当文件或文件夹的名字改变时（当1个文件的名字改变时，可能会触发Renamed事件和Changed事件）
                    _bugFileSystemWatcher.Changed += new FileSystemEventHandler(BugFileSystemWatcher_OnFileChanged);//当文件或文件夹发生改变时（当修改一个文件时，可能会多次触发Changed事件）

                    //赋值
                    BugFileSystemWatcher = _bugFileSystemWatcher;
                }


                /* Record文件的Watcher */
                if (RecordFileSystemWatcher == null)
                {
                    FileSystemWatcher _recordFileSystemWatcher = new FileSystemWatcher();

                    //设置是否启用监听?（true启用监听;false关闭监听）
                    _recordFileSystemWatcher.EnableRaisingEvents = false;

                    //要监控的文件夹路径
                    _recordFileSystemWatcher.Path = AppManager.Systems.ProjectSystem.RecordFolderPath;

                    //要监控什么文件？
                    _recordFileSystemWatcher.Filter = "*.json";

                    //是否监控子文件夹？
                    _recordFileSystemWatcher.IncludeSubdirectories = false;

                    //缓冲区可以设置 4 KB 或更大，但不能超过 64 KB（默认值为8kb）
                    //增加缓冲区的大小可以防止丢失[文件更改的事件]。 但是，增加缓冲区大小的开销较大，因为它来自无法换出到磁盘的非分页内存，所以将缓冲区保持得越小越好。 
                    _recordFileSystemWatcher.InternalBufferSize = 16;

                    //要监听文件的哪些改变？（比如要监听文件的创建时间、文件的名字改变、文件的大小改变等）
                    /*  枚举值：
                        Attributes - 文件或文件夹的属性
                        CreationTime - 文件或文件夹的创建时间
                        DirectoryName - 目录的名称
                        FileName - 文件的名称
                        LastAccess - 文件或文件夹上一次打开的日期
                        LastWrite - 上一次向文件或文件夹写入内容的日期
                        Security - 文件或文件夹的安全设置
                        Size - 文件或文件夹的大小 */
                    _recordFileSystemWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime |
                                           NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
                                           | NotifyFilters.LastWrite | NotifyFilters.Security |
                                           NotifyFilters.Size;

                    //注册监听事件
                    _recordFileSystemWatcher.Created += new FileSystemEventHandler(RecordFileSystemWatcher_OnFileCreated);//当创建文件或文件夹时
                    _recordFileSystemWatcher.Deleted += new FileSystemEventHandler(RecordFileSystemWatcher_OnFileDeleted);//当删除文件或文件夹时
                    _recordFileSystemWatcher.Renamed += new RenamedEventHandler(RecordFileSystemWatcher_OnFileRenamed);//当文件或文件夹的名字改变时（当1个文件的名字改变时，可能会触发Renamed事件和Changed事件）
                    _recordFileSystemWatcher.Changed += new FileSystemEventHandler(RecordFileSystemWatcher_OnFileChanged);//当文件或文件夹发生改变时（当修改一个文件时，可能会多次触发Changed事件）

                    //赋值
                    RecordFileSystemWatcher = _recordFileSystemWatcher;
                }
            }
        }


        /// <summary>
        /// 开启[协同合作]功能
        /// </summary>
        private void EnableCollaboration()
        {
            if (BugFileSystemWatcher == null || RecordFileSystemWatcher == null)
            {
                AwakeCollaboration();
            }


            if (BugFileSystemWatcher != null && RecordFileSystemWatcher != null)
            {
                //设置要监控的文件夹路径
                BugFileSystemWatcher.Path = AppManager.Systems.ProjectSystem.BugFolderPath;
                RecordFileSystemWatcher.Path = AppManager.Systems.ProjectSystem.RecordFolderPath;

                //设置是否启用监听?（true启用监听;false关闭监听）
                BugFileSystemWatcher.EnableRaisingEvents = true;
                RecordFileSystemWatcher.EnableRaisingEvents = true;
            }

        }


        /// <summary>
        /// 关闭[协同合作]功能
        /// </summary>
        private void DisableCollaboration()
        {
            if (BugFileSystemWatcher != null)
            {
                //设置是否启用监听?（true启用监听;false关闭监听）
                BugFileSystemWatcher.EnableRaisingEvents = false;
            }

            if (RecordFileSystemWatcher != null)
            {
                //设置是否启用监听?（true启用监听;false关闭监听）
                RecordFileSystemWatcher.EnableRaisingEvents = false;
            }

        }
        #endregion

        #region [私有方法 - 同步]
        /// <summary>
        /// 同步[Bug]
        /// （当有[Bug]文件更改时，2秒后，把已经修改的文件，进行同步）
        /// </summary>
        /// <param name="_syncBugIds">需要同步的所有Bug编号</param>
        /// <param name="_changeTypes">改变的类型（key：Bug的编号；value：改变的类型）</param>
        /// <param name="_logs">同步的日志</param>
        private void SyncBug(List<long> _syncBugIds, out Dictionary<long, ChangeType> _changeTypes, out List<string> _logs)
        {
            //out
            _logs = new List<string>();//所有的日志
            _changeTypes = new Dictionary<long, ChangeType>();//改变的类型



            //遍历所有需要同步的Bug
            for (int i = 0; i < _syncBugIds.Count; i++)
            {
                //Bug的编号
                long _bugId = _syncBugIds[i];



                /* 取到当前的数据 */
                //取到当前Bug的数据
                BugData _oldBugData = BugData.Copy(AppManager.Systems.BugSystem.GetBugData(_bugId));
                if (_oldBugData != null)
                {
                    _oldBugData.Name = _oldBugData.Name.Copy();
                }



                /* 读取Bug数据 */
                //Bug文件的路径（文件夹+文件名+后缀）
                string _bugFilePath = AppManager.Systems.ProjectSystem.BugFolderPath + "/Bug - " + _bugId + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                string _bugJsonText;
                BugBaseData _bugBaseData = null;
                BugData _newBugData = null;

                try
                {
                    //读取[Bug]的Json文本中的内容
                    _bugJsonText = File.ReadAllText(_bugFilePath);

                    //然后把Json文本解析成BugBaseData对象
                    _bugBaseData = JsonMapper.ToObject<BugBaseData>(_bugJsonText);

                    //把BugBaseData对象，转化为BugData对象（取到新的Bug数据）
                    _newBugData = BugBaseData.BaseDataToData(_bugBaseData);
                }
                catch (Exception e)
                {
                }


                /* 对比2个Bug的修改 */

                //如果是添加一个Bug，或者是修改一个Bug，或者是IsDelete为true的删除Bug
                if ((_oldBugData != null && _newBugData != null) ||
                    (_oldBugData == null && _newBugData != null))
                {

                    //如果新的Bug是完整的
                    if (BugData.VerifyIntegrity(_newBugData))
                    {
                        //如果旧的Bug和新的Bug有区别
                        if (BugData.Compare(CompareType.All, _oldBugData, _newBugData) == false)
                        {
                            //读取这个Bug文件
                            AppManager.Systems.BugSystem.LoadBug(ModeType, _bugId);



                            //获取Bug的更改
                            ChangeType _changeType = CompareOldBugAndNewBug(_oldBugData, _newBugData);

                            //获取Log
                            string _log = GenerateBugSyncLogText(_oldBugData, _newBugData, _changeType);



                            //把改变的类型加入到字典中
                            _changeTypes.Add(_bugId, _changeType);

                            //把_log加入到_logs中
                            if (_log != null && _log != "")
                            {
                                _logs.Add(_log);
                            }

                        }
                    }
                }


                //如果是直接删除文件的删除Bug
                else if (_oldBugData != null && _newBugData == null)
                {
                    //判断Bug文件是否存在
                    FileInfo _bugFileInfo = new FileInfo(_bugFilePath);

                    //如果Bug文件已被删除
                    if (_bugFileInfo.Exists == false)
                    {
                        //标记为已删除
                        AppManager.Systems.BugSystem.GetBugData(_bugId).IsDelete = true;



                        //获取Bug的更改
                        ChangeType _changeType = CompareOldBugAndNewBug(_oldBugData, _newBugData);

                        //获取Log
                        string _log = GenerateBugSyncLogText(_oldBugData, _newBugData, _changeType);



                        //把改变的类型加入到字典中
                        _changeTypes.Add(_bugId, _changeType);

                        //把_log加入到_logs中
                        if (_log != null && _log != "")
                        {
                            _logs.Add(_log);
                        }
                    }
                }

            }


        }


        /// <summary>
        /// 同步[Record]
        /// （当有[Record]文件更改时，2秒后，把已经修改的文件，进行同步）
        /// </summary>
        /// <param name="_syncRecordIds">需要同步的所有Record编号</param>
        /// <param name="_changeTypes">改变的类型（key：Record的编号；value：改变的类型）</param>
        /// <param name="_logs">同步的日志</param>
        private void SyncRecord(List<long> _syncRecordIds, out Dictionary<long, ChangeType> _changeTypes, out List<string> _logs)
        {
            //out
            _logs = new List<string>();//所有的日志
            _changeTypes = new Dictionary<long, ChangeType>();//改变的类型



            //遍历所有需要同步的Record
            for (int i = 0; i < _syncRecordIds.Count; i++)
            {
                //Record的编号
                long _recordId = _syncRecordIds[i];



                /* 取到当前的数据 */
                //取到当前Record的数据
                RecordData _oldRecordData = RecordData.Copy(AppManager.Systems.RecordSystem.GetRecordData(_recordId));



                /* 读取Record数据 */
                //Record文件的路径（文件夹+文件名+后缀）
                string _recordFilePath = AppManager.Systems.ProjectSystem.RecordFolderPath + "/Record - " + _recordId + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                string _recordJsonText;
                RecordBaseData _recordBaseData = null;
                RecordData _newRecordData = null;

                try
                {
                    //读取[Record]的Json文本中的内容
                    _recordJsonText = File.ReadAllText(_recordFilePath);

                    //然后把Json文本解析成RecordBaseData对象
                    _recordBaseData = JsonMapper.ToObject<RecordBaseData>(_recordJsonText);

                    //把RecordBaseData对象，转化为RecordData对象（取到新的Record数据）
                    _newRecordData = RecordBaseData.BaseDataToData(_recordBaseData);
                }
                catch (Exception e)
                {
                }





                /* 对比2个Record的修改 */
                //如果是添加一个Record，或者是修改一个Record，或者是IsDelete为true的删除Record
                if ((_oldRecordData != null && _newRecordData != null) ||
                    (_oldRecordData == null && _newRecordData != null))
                {
                    //如果新的Record是完整的
                    if (RecordData.VerifyIntegrity(_newRecordData))
                    {
                        //如果旧的Record和新的Record有区别
                        if (RecordData.Compare(CompareType.All, _oldRecordData, _newRecordData) == false)
                        {
                            //读取这个Record文件
                            AppManager.Systems.RecordSystem.LoadRecord(ModeType, _recordId);



                            //获取Record的更改
                            ChangeType _changeType = CompareOldRecordAndNewRecord(_oldRecordData, _newRecordData);

                            //获取Log
                            string _log = GenerateRecordSyncLogText(_oldRecordData, _newRecordData, _changeType);



                            //把改变的类型加入到字典中
                            _changeTypes.Add(_recordId, _changeType);

                            //把_log加入到_logs中
                            if (_log != null && _log != "")
                            {
                                _logs.Add(_log);
                            }
                        }
                    }
                }


                //如果是直接删除文件的删除Record
                else if (_oldRecordData != null && _newRecordData == null)
                {
                    //判断Record文件是否存在
                    FileInfo _recordFileInfo = new FileInfo(_recordFilePath);

                    //如果Record文件已被删除
                    if (_recordFileInfo.Exists == false)
                    {
                        //标记为已删除
                        AppManager.Systems.RecordSystem.GetRecordData(_recordId).IsDelete = true;



                        //获取Record的更改
                        ChangeType _changeType = CompareOldRecordAndNewRecord(_oldRecordData, _newRecordData);

                        //获取Log
                        string _log = GenerateRecordSyncLogText(_oldRecordData, _newRecordData, _changeType);



                        //把改变的类型加入到字典中
                        _changeTypes.Add(_recordId, _changeType);

                        //把_log加入到_logs中
                        if (_log != null && _log != "")
                        {
                            _logs.Add(_log);
                        }

                    }
                }
            }

        }
        #endregion

        #region [私有方法 - 日志]
        /// <summary>
        /// 获取[Bug]日志的文字
        /// （通过比较新Bug和旧Bug的区别，来生成日志的文字）
        /// </summary>
        /// <param name="_oldBugData">旧的Bug数据</param>
        /// <param name="_newBugData">新的Bug数据</param>
        /// <param name="_changeType">改变的类型</param>
        /// <returns>Bug的修改信息</returns>
        private string GenerateBugSyncLogText(BugData _oldBugData, BugData _newBugData, ChangeType _changeType)
        {
            //容器：Bug的日志
            string _log = "";


            //判断更改的类型
            switch (_changeType)
            {
                //如果是[新建Bug]
                case ChangeType.Add:
                    //文字：创建 Bug: [XXXXXXXXX]
                    if (_newBugData != null)
                    {
                        _log = AddString + " " + BugString + ": [" + StringTool.Clamp(_newBugData.Name.Text, 25) + "]";
                    }
                    else if (_oldBugData != null)
                    {
                        _log = AddString + " " + BugString + ": [" + StringTool.Clamp(_oldBugData.Name.Text, 25) + "]";
                    }
                    break;

                //如果是[删除Bug]
                case ChangeType.Delete:
                    //文字：删除 Bug: [XXXXXXXXX]
                    if (_newBugData != null)
                    {
                        _log = DeleteString + " " + BugString + ": [" + StringTool.Clamp(_newBugData.Name.Text, 25) + "]";
                    }
                    else if (_oldBugData != null)
                    {
                        _log = DeleteString + " " + BugString + ": [" + StringTool.Clamp(_oldBugData.Name.Text, 25) + "]";
                    }
                    break;

                //如果是[修改Bug]
                case ChangeType.Change:
                    //文字：修改 Bug: [XXXXXXXXX]
                    if (_newBugData != null)
                    {
                        _log = ChangeString + " " + BugString + ": [" + StringTool.Clamp(_newBugData.Name.Text, 25) + "]";
                    }
                    else if (_oldBugData != null)
                    {
                        _log = ChangeString + " " + BugString + ": [" + StringTool.Clamp(_oldBugData.Name.Text, 25) + "]";
                    }
                    break;
            }


            //返回值
            return _log;
        }



        /// <summary>
        /// 获取[Record]日志的文字
        /// （通过比较新Record和旧Record的区别，来生成日志的文字）
        /// </summary>
        /// <param name="_oldRecordData">旧的Record数据</param>
        /// <param name="_newRecordData">新的Record数据</param>
        /// <param name="_changeType">改变的类型</param>
        /// <returns>Record的修改信息</returns>
        private string GenerateRecordSyncLogText(RecordData _oldRecordData, RecordData _newRecordData, ChangeType _changeType)
        {
            //容器：Record的日志
            string _log = "";


            //取到Bug
            BugData _oldBugData = AppManager.Systems.BugSystem.GetBugData(_oldRecordData);
            BugData _newBugData = AppManager.Systems.BugSystem.GetBugData(_newRecordData);


            //判断是否有修改？
            switch (_changeType)
            {
                //如果是[新建Record]
                case ChangeType.Add:
                    //文字：添加 聊天记录: [Bug：XXXXXXXXX]
                    if (_newBugData != null)
                    {
                        _log = AddString + " " + RecordString + ": [Bug：" + StringTool.Clamp(_newBugData.Name.Text, 25) + "]";
                    }
                    else if (_oldBugData != null)
                    {
                        _log = AddString + " " + RecordString + ": [Bug：" + StringTool.Clamp(_oldBugData.Name.Text, 25) + "]";
                    }
                    break;

                //如果是[删除Record]
                case ChangeType.Delete:
                    //文字：添加 聊天记录: [Bug：XXXXXXXXX]
                    if (_newBugData != null)
                    {
                        _log = DeleteString + " " + RecordString + ": [Bug：" + StringTool.Clamp(_newBugData.Name.Text, 25) + "]";
                    }
                    else if (_oldBugData != null)
                    {
                        _log = DeleteString + " " + RecordString + ": [Bug：" + StringTool.Clamp(_oldBugData.Name.Text, 25) + "]";
                    }
                    break;

            }


            //返回值
            return _log;

        }
        #endregion

        #region [私有方法 - 语言]

        /// <summary>
        /// 当软件的[语言]改变时
        /// </summary>
        /// <param name="_languageType">是中文的性格？还是英文的性格？</param>
        public void OnLanguageChange(LanguageType _languageType)
        {
            switch (_languageType)
            {
                case LanguageType.Chinese:
                    DeleteString = "删除";
                    AddString = "添加";
                    ChangeString = "修改";
                    BugString = "Bug";
                    RecordString = "聊天记录";
                    break;


                case LanguageType.English:
                    DeleteString = "Delete";
                    AddString = "Add";
                    ChangeString = "Change";
                    BugString = "Bug";
                    RecordString = "Chat Record";
                    break;
            }
        }

        #endregion

        #region [私有方法 - 比较]

        /// <summary>
        /// 比较[旧Bug]和[新Bug]
        /// （看下新Bug相对旧Bug来说，进行了哪些修改？）
        /// </summary>
        /// <param name="_oldBugData">旧的Bug</param>
        /// <param name="_newBugData">新的Bug</param>
        /// <returns>修改了什么？</returns>
        public ChangeType CompareOldBugAndNewBug(BugData _oldBugData, BugData _newBugData)
        {
            /* [新建Bug] ：
               第一种情况：Old为空，New不为空
               第二种情况：Old不为空，New不为空，Old已删除，New未删除*/
            if ((_oldBugData == null && _newBugData != null && _newBugData.IsDelete != true) ||
                (_oldBugData != null && _newBugData != null && _oldBugData.IsDelete == true && _newBugData.IsDelete != true))
            {
                return ChangeType.Add;
            }


            /* [删除Bug] :
               第一种情况：Old不为空，并且New为空
               第二种情况：Old不为空，并且Old不为true，并且New不为空，并且New为true*/
            else if ((_oldBugData != null && _newBugData == null && _oldBugData.IsDelete != true) ||
                     (_oldBugData != null && _newBugData != null && _oldBugData.IsDelete != true && _newBugData.IsDelete == true))
            {
                return ChangeType.Delete;
            }


            /* [修改Bug] */
            else if (_oldBugData != null && _newBugData != null)
            {
                if (_oldBugData.Name.Text != _newBugData.Name.Text ||
                    _oldBugData.Progress != _newBugData.Progress ||
                    _oldBugData.Priority != _newBugData.Priority)
                {
                    return ChangeType.Change;
                }
            }


            return ChangeType.None;
        }


        /// <summary>
        /// 比较[旧Record]和[新Record]
        /// （看下新Record相对旧Record来说，进行了哪些修改？）
        /// </summary>
        /// <param name="_oldRecordData">旧的Record</param>
        /// <param name="_newRecordData">新的Record</param>
        /// <returns>修改了什么？</returns>
        public ChangeType CompareOldRecordAndNewRecord(RecordData _oldRecordData, RecordData _newRecordData)
        {

            /* [新建Record] ：
               第一种情况：Old为空，New不为空
               第二种情况：Old不为空，New不为空，Old已删除，New未删除*/
            if ((_oldRecordData == null && _newRecordData != null && _newRecordData.IsDelete != true) ||
                (_oldRecordData != null && _newRecordData != null && _oldRecordData.IsDelete == true && _newRecordData.IsDelete != true))
            {
                return ChangeType.Add;
            }


            /* [删除Record] :
               第一种情况：Old不为空，并且New为空
               第二种情况：Old不为空，并且Old不为true，并且New不为空，并且New为true*/
            else if ((_oldRecordData != null && _newRecordData == null && _oldRecordData.IsDelete != true) ||
                     (_oldRecordData != null && _newRecordData != null && _oldRecordData.IsDelete != true && _newRecordData.IsDelete == true))
            {
                return ChangeType.Delete;
            }


            return ChangeType.None;
        }

        #endregion

    }
}
