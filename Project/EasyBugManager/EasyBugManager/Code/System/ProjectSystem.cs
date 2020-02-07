/* By: 絮大王（sukiup@163.com）
   Time：2019年11月20日20:43:53*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LitJson;

namespace EasyBugManager
{
    /// <summary>
    /// 项目的系统（用于保存、读取项目）
    /// </summary>
    public class ProjectSystem
    {
        /* 属性：ProjectFolderPath([项目文件夹]的路径)(项目所在的文件夹)(就是.bugs文件所在的文件夹)

                 ProjectFilePath([项目文件]的路径)（ProjectFolderPath/项目名.bugs）
                 BugFilePath([Bug文件]的路径)（ProjectFolderPath/Bug/bug.json）
                 RecordFolderPath([记录文件夹]的路径)（ProjectFolderPath/Record/）
                 ImageFolderPath([图片文件夹]的路径)（ProjectFolderPath/Image/）
                 BackupFolderPath([备份文件夹]的路径)（ProjectFolderPath/Backup/）

                 ProjectFileSuffix([项目]文件的后缀名)
                 RecordFileSuffix([记录]文件的后缀名)*/

        /* 数据：ProjectData(项目的数据)*/


        /* 方法：CreateProject(创建项目)
                 LoadProject(读取项目)
                 SaveProject(保存项目)

                 LoadBugs(读取所有Bug)
                 SaveBugs(保存所有Bug)

                 LoadRecords(读取所有的记录)
                 SaveRecords(保存所有的记录)
                 LoadRecord(读取记录)
                 SaveRecord(保存记录)
                 
                 SaveImage(保存图片)*/





        private string projectFolderPath = "";//项目文件夹的路径(项目所在的文件夹)(就是.bugs文件所在的文件夹)




        #region [公开属性 - 路径]
        /// <summary>
        /// 项目文件夹的路径(项目所在的文件夹)
        /// (就是.bugs文件所在的文件夹)
        /// </summary>
        public string ProjectFolderPath
        {
            get { return projectFolderPath; }
            set { projectFolderPath = value; }
        }

        /// <summary>
        /// [项目文件]的路径(就是.bugs文件的路径)(文件夹+文件名+后缀)
        /// </summary>
        public string ProjectFilePath
        {
            get { return ProjectFolderPath +"/"+ ProjectData.FileName + ProjectFileSuffix; }
        }



        /// <summary>
        /// [Bug文件夹]的路径(就是bug.json文件所在的文件夹路径)
        /// </summary>
        public string BugFolderPath
        {
            get { return ProjectFolderPath + "/Bug"; }
        }

        /// <summary>
        /// [记录文件]所在的 文件夹的路径 ([记录文件夹]的路径)
        /// </summary>
        public string RecordFolderPath
        {
            get { return ProjectFolderPath + "/Record"; }
        }

        /// <summary>
        /// [图片文件]所在的 文件夹的路径 ([图片文件夹]的路径)
        /// </summary>
        public string ImageFolderPath
        {
            get { return ProjectFolderPath + "/Image"; }
        }

        /// <summary>
        /// [备份文件]所在的 文件夹的路径 ([备份文件夹]的路径)
        /// </summary>
        public string BackupFolderPath
        {
            get { return ProjectFolderPath + "/Backup"; }
        }
        #endregion

        #region [公开属性 - 后缀]
        /// <summary>
        /// [项目]文件的后缀名
        /// </summary>
        public string ProjectFileSuffix
        {
            get { return ".bugs"; }
        }

        /// <summary>
        /// [其他]文件的后缀名
        /// </summary>
        public string OtherFileSuffix
        {
            get { return ".json"; }
        }
        #endregion

        #region [公开属性 - 数据]
        /// <summary>
        /// 项目的数据
        /// </summary>
        public ProjectData ProjectData
        {
            get { return AppManager.Datas.ProjectData; }
            set { AppManager.Datas.ProjectData = value; }
        }

        #endregion




        #region [公开方法 - 项目]
        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="_chooseFolderPath">用户选择的路径</param>
        /// <param name="_projectName">项目的名字</param>
        /// <param name="_modeType">项目的模式</param>
        /// <returns>是否创建成功？</returns>
        public bool CreateProject(string _chooseFolderPath, string _projectName, ModeType _modeType)
        {
            try
            {
                /* 判断文件夹是否存在 */
                DirectoryInfo _directoryInfo = new DirectoryInfo(_chooseFolderPath);//文件夹的信息
                if (_directoryInfo.Exists == false)
                {
                    return false;
                }



                /* 去除ProjectName中的非法字符：
                    文件夹和文件的 名字中，不能包含：? * : " < > \ / |
                    并且，不能以空格开头*/
                string _projectFileName = StringTool.RemoveInvaildChat(_projectName);

                /* 如果项目名为空格，或者是去除了违规字符后为空 */
                if (_projectFileName == null || _projectFileName == "")
                {
                    //设置新的文件名字
                    _projectFileName = "New Bugs";
                }

                /*判断是否有相同的文件夹*/
                //判断是否有相同的文件夹(返回值是一个唯一的文件夹【xxxx/文件夹 (1)/】)
                string _onlyFolderPath = FolderTool.AvoidSameFolder(_chooseFolderPath + "/" +_projectFileName);
                //取到文件夹的名字（这个文件夹不会和任何文件夹重名）
                DirectoryInfo _onlyFolderInfo = new DirectoryInfo(_onlyFolderPath);
                _projectFileName = _onlyFolderInfo.Name;



                /* Project数据 */
                //创建ProjectData对象
                ProjectData _projectData = new ProjectData();
                _projectData.Id = DateTimeTool.DateTimeToLong(DateTime.UtcNow, TimeFormatType.YearMonthDayHourMinuteSecondMillisecond);
                _projectData.Name = _projectName;
                _projectData.FileName = _projectFileName;
                _projectData.ModeType = _modeType;

                //修改ProjectData属性
                ProjectData = _projectData;

                //工程文件夹的路径
                ProjectFolderPath = _chooseFolderPath + "/" + _projectFileName;



                /* 创建文件和文件夹 */
                //创建：[项目文件夹]、[Bug文件夹]、[记录文件夹]、[图片文件夹]、[备份文件夹]
                CreateFolders();
                //创建：[项目文件]、[Bug文件]
                CreateFiles();



                /* 保存Project数据 */
                bool _isSave = SaveProject();
                if (_isSave == false)
                {
                    return false;
                }


                /* 打开协同合作功能 */
                AppManager.Systems.CollaborationSystem.Handle(true);


                return true;
            }
            catch (Exception e)
            {
                //输出错误
                AppManager.Uis.ErrorUi.UiControl.TipContent = e.ToString();
                AppManager.Uis.ErrorUi.OpenOrClose(true);
                return false;
            }
        }

        /// <summary>
        /// 读取项目
        /// </summary>
        /// <param name="_projectFilePath">项目文件夹的路径(就是.bugs文件的路径)(如果为null，就代表使用当前的savePath属性+ProjectName属性)</param>
        /// <returns>是否读取成功？</returns>
        public bool LoadProject(string _projectFilePath=null)
        {
            
            try
            {
                /* 设置项目路径 */
                //如果参数为null
                if (_projectFilePath == null)
                {
                    _projectFilePath = ProjectFilePath;
                }


                /* 设置文件信息 */
                //FileInfo类 用于读取文件信息
                FileInfo _fileInfo = new FileInfo(_projectFilePath);

                /* 判断文件是否存在 */
                if (_fileInfo.Exists == false)//如果不存在
                {
                    return false;
                }

                //如果文件存在，修改projectFolderPath属性
                ProjectFolderPath = _fileInfo.DirectoryName;
                string _fileName = Path.GetFileNameWithoutExtension(_fileInfo.FullName);





                /* 读取ProjectData */
                //读取[项目名.bugs]的Json文本中的内容
                string _projectJsonText = File.ReadAllText(_projectFilePath);

                //然后把Json文本解析成ProjectBaseData对象
                ProjectBaseData _projectBaseData = null;
                try
                {
                    _projectBaseData = JsonMapper.ToObject<ProjectBaseData>(_projectJsonText);
                }
                catch (Exception e)
                {
                }

                //判断是否是.bug文件（如果没有Bug的名字，就不打开这个项目，并报错）
                if (_projectBaseData == null || _projectBaseData.Name == null || _projectBaseData.Name == "")
                {
                }
                else
                {
                    //把ProjectBaseData对象，转化为ProjectData对象
                    ProjectData _projectData = ProjectBaseData.BaseDataToData(_projectBaseData);
                    
                    //如果取到了数据
                    if (_projectData!=null)
                    {
                        //赋值
                        _projectData.FileName = _fileName;
                        this.ProjectData = _projectData;

                        //读取排序数据
                        AppManager.Systems.SortSystem.LoadSort();


                        //创建文件夹和文件
                        CreateFolders();
                        CreateFiles();


                        /* 打开协同合作功能 */
                        AppManager.Systems.CollaborationSystem.Handle(true);


                        //返回值
                        return true;
                    }
                }

                //返回值
                return false;
            }
            catch (Exception e)
            {
                //输出错误
                AppManager.Uis.ErrorUi.UiControl.TipContent = e.ToString();
                AppManager.Uis.ErrorUi.OpenOrClose(true);
                return false;
            }
        }

        /// <summary>
        /// 保存项目
        /// </summary>
        /// <returns>是否保存成功？</returns>
        public bool SaveProject()
        {
            try
            {
                if (ProjectFolderPath==null || ProjectFolderPath == "")
                {
                    return false;
                }



                //把ProjectData转换为ProjectBaseData
                ProjectBaseData _projectBaseData = ProjectBaseData.DataToBaseData(ProjectData);

                //把ProjectBaseData转换为json
                string _projectJsonText = JsonMapper.ToJson(_projectBaseData);

                //把json文件保存到[项目名.bugs]文件里
                File.WriteAllText(ProjectFilePath, _projectJsonText, Encoding.Default);

                //保存排序数据
                AppManager.Systems.SortSystem.SaveSort();




                return true;
            }
            catch (Exception e)
            {
                //输出错误
                AppManager.Uis.ErrorUi.UiControl.TipContent = e.ToString();
                AppManager.Uis.ErrorUi.OpenOrClose(true);
                return false;
            }
        }


        /// <summary>
        /// 关闭项目
        /// </summary>
        public void CloseProject()
        {
            /* 关闭协同合作功能 */
            AppManager.Systems.CollaborationSystem.Handle(false);

            /* 保存Project数据 */
            AppManager.Systems.ProjectSystem.SaveProject();

            /* 清空数据 */
            AppManager.Datas.ClearProjectData();
        }
        #endregion

        #region [公开方法 - 文件夹]
        /// <summary>
        /// 创建文件夹
        /// (创建：[项目文件夹]、[Bug文件夹]、[记录文件夹]、[图片文件夹]、[备份文件夹])
        /// </summary>
        /// <returns>是否创建成功？</returns>
        public void CreateFolders()
        {
            /* 判断[项目]文件夹是否存在，不存在就创建 */
            DirectoryInfo _directoryInfo = new DirectoryInfo(ProjectFolderPath);//文件夹的信息
            if (_directoryInfo.Exists == false)
            {
                _directoryInfo.Create();
            }


            /* 创建文件和文件夹 */
            //创建[项目]文件夹（xxxx/项目名/）
            DirectoryInfo _projectDirectoryInfo = new DirectoryInfo(ProjectFolderPath);
            if (_projectDirectoryInfo.Exists == false)
            {
                _projectDirectoryInfo.Create();
            }

            //创建bug文件夹（_projectFolderPath/Bug/）
            DirectoryInfo _bugDirectoryInfo = new DirectoryInfo(BugFolderPath);
            if (_bugDirectoryInfo.Exists == false)
            {
                _bugDirectoryInfo.Create();
            }

            //创建[Record]文件夹（_projectFolderPath/Record/）
            DirectoryInfo _recordDirectoryInfo = new DirectoryInfo(RecordFolderPath);
            if (_recordDirectoryInfo.Exists == false)
            {
                _recordDirectoryInfo.Create();
            }

            //创建[Image]文件夹（_projectFolderPath/Image/）
            DirectoryInfo _imageDirectoryInfo = new DirectoryInfo(ImageFolderPath);
            if (_imageDirectoryInfo.Exists == false)
            {
                _imageDirectoryInfo.Create();
            }

            //创建[Backup]文件夹（_projectFolderPath/Backup/）
            DirectoryInfo _backupDirectoryInfo = new DirectoryInfo(BackupFolderPath);
            if (_backupDirectoryInfo.Exists == false)
            {
                _backupDirectoryInfo.Create();
            }
        }


        /// <summary>
        /// 创建文件
        /// (创建：[项目文件]、[Bug文件])
        /// </summary>
        /// <returns>是否创建成功？</returns>
        public void CreateFiles()
        {
            //如果是工程文件是[普通模式]，就创建文件
            if (ProjectData.ModeType == ModeType.Default)
            {
                //创建[项目名.bugs]文件夹（_projectFolderPath/项目名.bugs）
                FileInfo _projectFileInfo = new FileInfo(ProjectFilePath);
                if (_projectFileInfo.Exists == false)
                {
                    File.WriteAllText(ProjectFilePath, "", Encoding.Default);
                }

                //创建[Bugs.json]文件（_projectFolderPath/Bug/Bugs.json）
                string _bugFilePath = BugFolderPath + "/Bugs" + OtherFileSuffix;
                FileInfo _bugFileInfo = new FileInfo(_bugFilePath);
                if (_bugFileInfo.Exists == false)
                {
                    File.WriteAllText(_bugFilePath, "", Encoding.Default);
                }

                //创建[Records.json]文件（_projectFolderPath/Record/Records.json）
                string _recordFilePath = RecordFolderPath + "/Records" + OtherFileSuffix;
                FileInfo _recordFileInfo = new FileInfo(_recordFilePath);
                if (_recordFileInfo.Exists == false)
                {
                    File.WriteAllText(_recordFilePath, "", Encoding.Default);
                }
            }
        }
        #endregion

    }
}
