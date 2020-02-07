/* By: 絮大王（sukiup@163.com）
   Time：2020年1月5日09:30:36*/

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
    /// 备份的系统（用于备份[bug.json]、[project.bugs]文件）
    /// </summary>
    public class BackupSystem
    {
        /* 属性：BackupFolderPath([备份文件夹]的路径)（ProjectFolderPath/Backup/）*/

        /* 方法：Backup(备份) */


        #region [公开属性 - 路径]
        /// <summary>
        /// [备份文件]所在的 文件夹的路径 ([备份文件夹]的路径)
        /// </summary>
        public string BackupFolderPath
        {
            get { return AppManager.Systems.ProjectSystem.BackupFolderPath; }
        }


        /// <summary>
        /// ["工程"备份文件]所在的 文件夹的路径 ([备份文件夹]的路径)
        /// </summary>
        public string ProjectBackupFolderPath
        {
            get { return AppManager.Systems.ProjectSystem.BackupFolderPath + "/Project"; }
        }


        /// <summary>
        /// ["Bug"备份文件]所在的 文件夹的路径 ([备份文件夹/Bug]的路径)
        /// </summary>
        private string BugBackupFolderPath
        {
            get { return AppManager.Systems.ProjectSystem.BackupFolderPath+"/Bug"; }
        }


        /// <summary>
        /// ["Record"备份文件]所在的 文件夹的路径 ([备份文件夹/Record]的路径)
        /// </summary>
        private string RecordBackupFolderPath
        {
            get { return AppManager.Systems.ProjectSystem.BackupFolderPath + "/Record"; }
        }
        #endregion

        #region [公开方法 - 备份]
        /// <summary>
        /// 备份[工程]
        ///（备份[project.bugs]文件）
        /// </summary>
        public void BackupProject()
        {
            /* 格式：Backup/Project/年月日时分秒.bugs*/

            try
            {
                /*创建[Backup]文件夹（_projectFolderPath/Backup/）*/
                DirectoryInfo _projectBackupDirectoryInfo = new DirectoryInfo(ProjectBackupFolderPath);
                //如果没有文件夹，就创建文件夹
                if (_projectBackupDirectoryInfo.Exists == false)
                {
                    _projectBackupDirectoryInfo.Create();
                }


                /*获取【工程】文件的个数（如果文件超过10个，就删除最早创建的那个文件）*/
                FileInfo[] _projectBackupFileInfos = _projectBackupDirectoryInfo.GetFiles();
                if (_projectBackupFileInfos.Length > 10)
                {
                    //找到Project备份文件中，最早创建的那个文件
                    FileInfo _firstFileInfo = FindFirstBackupFile(_projectBackupFileInfos);

                    //删除最早的1个文件
                    if (_firstFileInfo != null)
                    {
                        File.Delete(_firstFileInfo.FullName);
                    }
                }



                /* 获取要备份的文件的路径 */
                //当前的时间（年月日时分秒）
                string _nowDateTimeString = DateTimeTool.DateTimeToString(DateTime.UtcNow, TimeFormatType.YearMonthDayHourMinuteSecondMillisecond);

                //文件的备份路径
                string _projectFilePath = ProjectBackupFolderPath+"/"
                                          + _nowDateTimeString+AppManager.Systems.ProjectSystem.ProjectFileSuffix;


                /* 进行备份 */
                //获取Bug文件和Project文件的内容
                string _projectText = File.ReadAllText(AppManager.Systems.ProjectSystem.ProjectFilePath);

                //把Bug文件和Project文件的内容，复制到备份文件中
                File.WriteAllText(_projectFilePath, _projectText, Encoding.Default);

            }
            catch (Exception e)
            {
            }
        }



        /// <summary>
        /// 备份[Bug]
        ///（备份[Bug/Bugs.json]文件）
        /// </summary>
        public void BackupBug()
        {
            /* 格式：Backup/Bug/年月日时分秒.json */

            try
            {

                /*创建[Backup/Bug]文件夹（_projectFolderPath/Backup/Bug/）*/
                DirectoryInfo _bugBackupDirectoryInfo = new DirectoryInfo(BugBackupFolderPath);
                //如果没有文件夹，就创建文件夹
                if (_bugBackupDirectoryInfo.Exists == false)
                {
                    _bugBackupDirectoryInfo.Create();
                }

                /*获取【Bug】文件的个数（如果文件超过10个，就删除最早创建的那个文件）*/
                FileInfo[] _bugBackupFileInfos = _bugBackupDirectoryInfo.GetFiles();
                if (_bugBackupFileInfos!=null && _bugBackupFileInfos.Length > 10)
                {
                    //找到Bug备份文件中，最早创建的那个文件
                    FileInfo _firstFileInfo = FindFirstBackupFile(_bugBackupFileInfos);

                    //删除最早的1个文件
                    if (_firstFileInfo!=null)
                    {
                        File.Delete(_firstFileInfo.FullName);
                    }
                }





                /* 获取要备份的文件的路径 */
                //当前的时间（年月日时分秒）
                string _nowDateTimeString = DateTimeTool.DateTimeToString(DateTime.UtcNow, TimeFormatType.YearMonthDayHourMinuteSecondMillisecond);

                //文件的备份路径
                string _bugFilePath = BugBackupFolderPath + "/"
                                      + _nowDateTimeString + AppManager.Systems.ProjectSystem.OtherFileSuffix;





                /* 进行备份 */
                //取到所有的Bug数据
                ObservableCollection<BugData> _bugDatas = AppManager.Systems.BugSystem.BugDatas;

                //把BugData转换为BugBaseData
                List<BugBaseData> _bugBaseDatas = new List<BugBaseData>();
                for (int i = 0; i < _bugDatas.Count; i++)
                {
                    BugBaseData _bugBaseData = BugBaseData.DataToBaseData(_bugDatas[i]);
                    if (_bugBaseData!=null)
                    {
                        _bugBaseDatas.Add(_bugBaseData);
                    }
                }

                //把BugBaseData转换为json
                string _bugsJsonText = JsonMapper.ToJson(_bugBaseDatas);

                //把json文件保存到[Bugs.json]文件里
                File.WriteAllText(_bugFilePath, _bugsJsonText, Encoding.Default);

            }
            catch (Exception e)
            {
            }
        }



        /// <summary>
        /// 备份[Record]
        ///（备份[Record/Records.json]文件）
        /// </summary>
        public void BackupRecord()
        {
            /* 格式：Backup/Record/年月日时分秒.json */
            try
            {

                /*创建[Backup/Record]文件夹（_projectFolderPath/Backup/Record/）*/
                DirectoryInfo _recordBackupDirectoryInfo = new DirectoryInfo(RecordBackupFolderPath);
                //如果没有文件夹，就创建文件夹
                if (_recordBackupDirectoryInfo.Exists == false)
                {
                    _recordBackupDirectoryInfo.Create();
                }

                /*获取【Record】文件的个数（如果文件超过10个，就删除最早创建的那个文件）*/
                FileInfo[] _recordBackupFileInfos = _recordBackupDirectoryInfo.GetFiles();
                if (_recordBackupFileInfos != null && _recordBackupFileInfos.Length > 10)
                {
                    //找到Record备份文件中，最早创建的那个文件
                    FileInfo _firstFileInfo = FindFirstBackupFile(_recordBackupFileInfos);

                    //删除最早的1个文件
                    if (_firstFileInfo != null)
                    {
                        File.Delete(_firstFileInfo.FullName);
                    }
                }





                /* 获取要备份的文件的路径 */
                //当前的时间（年月日时分秒）
                string _nowDateTimeString = DateTimeTool.DateTimeToString(DateTime.UtcNow, TimeFormatType.YearMonthDayHourMinuteSecondMillisecond);

                //文件的备份路径
                string _recordFilePath = RecordBackupFolderPath + "/"
                                      + _nowDateTimeString + AppManager.Systems.ProjectSystem.OtherFileSuffix;





                /* 进行备份 */
                //取到所有的Record数据
                ObservableCollection<RecordData> _recordDatas = AppManager.Systems.RecordSystem.RecordDatas;

                //把RecordData转换为RecordBaseData
                List<RecordBaseData> _recordBaseDatas = new List<RecordBaseData>();
                for (int i = 0; i < _recordDatas.Count; i++)
                {
                    RecordBaseData _recordBaseData = RecordBaseData.DataToBaseData(_recordDatas[i]);
                    if (_recordBaseData!=null)
                    {
                        _recordBaseDatas.Add(_recordBaseData);
                    }
                }

                //把RecordBaseData转换为json
                string _recordsJsonText = JsonMapper.ToJson(_recordBaseDatas);

                //把json文件保存到[Records.json]文件里
                File.WriteAllText(_recordFilePath, _recordsJsonText, Encoding.Default);

            }
            catch (Exception e)
            {
            }
        }
        #endregion


        #region [私有方法]
        /// <summary>
        /// 从一组文件中，找到最早创建的文件
        /// </summary>
        /// <param name="_fileInfos">所有的文件</param>
        /// <returns>最早创建的文件（如果为null，就代表没有任何的文件名字，能被转换为long格式的数据）</returns>
        public static FileInfo FindFirstBackupFile(FileInfo[] _fileInfos)
        {
            //容器：最早的文件，以及最早的文件创建时间
            FileInfo _firstFileInfo = null;
            long _firstFileCreateTime = -1;

            //找到最早的1个文件
            for (int i = 0; i < _fileInfos.Length; i++)
            {
                //获取到备份文件的名字
                string _fileName = Path.GetFileNameWithoutExtension(_fileInfos[i].FullName);

                //把Bug的名字，转换成时间（long格式的时间）
                long _fileCreateTime = -1;
                bool _isParseOk = long.TryParse(_fileName, out _fileCreateTime);

                //比较时间的大小
                if (_isParseOk == true)
                {
                    if (_firstFileCreateTime < 0 || _fileCreateTime < _firstFileCreateTime)
                    {
                        _firstFileInfo = _fileInfos[i];
                        _firstFileCreateTime = _fileCreateTime;
                    }
                }
            }

            //返回最早的文件
            return _firstFileInfo;
        }

        #endregion
    }
}
