/* By: 絮大王（sukiup@163.com）
   Time：2019年10月10日04:57:09*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 用于存放所有的系统
    /// </summary>
    public class Systems
    {
        private AudioSystem audioSystem;//[音效]的系统
        private SaveSystem saveSystem;//[存档]的系统
        private LanguageSystem languageSystem;//[语言]的系统
        private ThemeSystem themeSystem;//[皮肤]的系统
        private DlcSystem dlcSystem;//[DLC]的系统

        private ProjectSystem projectSystem;//[项目]的系统
        private BugSystem bugSystem;//[Bug]的系统
        private RecordSystem recordSystem;//[记录]的系统
        private ImageSystem imageSystem;//[图片]的系统

        private SortSystem sortSystem;//[排序]的系统
        private PageSytem pageSytem;//[页面]的系统
        private SearchSystem searchSystem;//[搜索]的系统

        private RelatedSystem relatedSystem;//[相关]的系统

        private TemperamentSystem temperamentSystem;//[性格]的系统

        private DeleteSystem deleteSystem;//[删除文件]的系统
        private ExportSystem exportSystem;//[导出]的系统
        private BackupSystem backupSystem;//[备份]的系统
        private CollaborationSystem collaborationSystem;//[协同合作]的系统




        #region 属性
        /// <summary>
        /// [音效]的系统
        /// </summary>
        public AudioSystem AudioSystem
        {
            get { return audioSystem; }
        }

        /// <summary>
        /// [存档]的系统
        /// </summary>
        public SaveSystem SaveSystem
        {
            get { return saveSystem; }
        }

        /// <summary>
        /// [语言]的系统
        /// </summary>
        public LanguageSystem LanguageSystem
        {
            get { return languageSystem; }
        }

        /// <summary>
        /// [皮肤]的系统
        /// </summary>
        public ThemeSystem ThemeSystem
        {
            get { return themeSystem; }
        }

        /// <summary>
        /// [DLC]的系统
        /// </summary>
        public DlcSystem DlcSystem
        {
            get { return dlcSystem; }
        }



        /// <summary>
        /// [项目]的系统
        /// </summary>
        public ProjectSystem ProjectSystem
        {
            get { return projectSystem; }
        }

        /// <summary>
        /// [Bug]的系统
        /// </summary>
        public BugSystem BugSystem
        {
            get { return bugSystem; }
        }

        /// <summary>
        /// [记录]的系统
        /// </summary>
        public RecordSystem RecordSystem
        {
            get { return recordSystem; }
        }

        /// <summary>
        /// [图片]的系统
        /// </summary>
        public ImageSystem ImageSystem
        {
            get { return imageSystem; }
        }



        /// <summary>
        /// [排序]的系统
        /// </summary>
        public SortSystem SortSystem
        {
            get { return sortSystem; }
        }

        /// <summary>
        /// [页面]的系统
        /// </summary>
        public PageSytem PageSytem
        {
            get { return pageSytem; }
        }

        /// <summary>
        /// [搜索]的系统
        /// </summary>
        public SearchSystem SearchSystem
        {
            get { return searchSystem; }
        }



        /// <summary>
        /// [相关]的系统
        /// </summary>
        public RelatedSystem RelatedSystem
        {
            get { return relatedSystem; }
        }



        /// <summary>
        /// [性格]的系统
        /// </summary>
        public TemperamentSystem TemperamentSystem
        {
            get { return temperamentSystem; }
        }



        /// <summary>
        /// [删除文件]的系统
        /// </summary>
        public DeleteSystem DeleteSystem
        {
            get { return deleteSystem; }
        }

        /// <summary>
        /// [导出]的系统
        /// </summary>
        public ExportSystem ExportSystem
        {
            get { return exportSystem; }
        }

        /// <summary>
        /// [备份]的系统
        /// </summary>
        public BackupSystem BackupSystem
        {
            get { return backupSystem; }
        }

        /// <summary>
        /// [协同合作]的系统
        /// </summary>
        public CollaborationSystem CollaborationSystem
        {
            get { return collaborationSystem; }
        }
        #endregion

        #region 构造方法
        public Systems()
        {
            audioSystem = new AudioSystem();
            saveSystem = new SaveSystem();
            languageSystem = new LanguageSystem();
            themeSystem = new ThemeSystem();
            dlcSystem = new DlcSystem();

            projectSystem = new ProjectSystem();
            bugSystem = new BugSystem();
            recordSystem = new RecordSystem();
            imageSystem = new ImageSystem();

            sortSystem = new SortSystem();
            pageSytem = new PageSytem();
            searchSystem = new SearchSystem();

            relatedSystem = new RelatedSystem();

            temperamentSystem = new TemperamentSystem();

            deleteSystem = new DeleteSystem();
            exportSystem = new ExportSystem();
            backupSystem = new BackupSystem();
            collaborationSystem = new CollaborationSystem();
        }
        #endregion

    }
}
