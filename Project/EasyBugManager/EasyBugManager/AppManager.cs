/*
    App：Easy Bug Manager
    By：絮大王（XuDaWang）
    Email：xudawang@vip.163.com
    Steam：https://store.steampowered.com/app/1175080/Easy_Bug_Manager/
    Github：https://github.com/xujiangjiang/Easy-Bug-Manager
    Time：2019.09.27
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// App的所有属性和逻辑
    /// </summary>
    public static class AppManager
    {
        private static App mainApp;//App的逻辑
        private static MainWindow mainWindow;//窗口的逻辑

        /* 子系统 */
        private static Datas datas;//所有的数据
        private static Systems systems;//所有的逻辑
        private static Uis uis;//所有的界面


        #region 属性
        /// <summary>
        /// App的逻辑
        /// </summary>
        public static App MainApp
        {
            get { return mainApp; }
            set { mainApp = value; }
        }

        /// <summary>
        /// 窗口的逻辑
        /// </summary>
        public static MainWindow MainWindow
        {
            get { return mainApp.MainWindow as MainWindow; }
        }




        /// <summary>
        /// 所有的数据
        /// </summary>
        public static Datas Datas
        {
            get { return datas; }
        }

        /// <summary>
        /// 所有的逻辑
        /// </summary>
        public static Systems Systems
        {
            get { return systems; }
        }

        /// <summary>
        /// 所有的界面
        /// </summary>
        public static Uis Uis
        {
            get { return uis; }
        }
        #endregion


        #region 构造方法
        static AppManager() 
        {
            datas = new Datas();
            uis = new Uis();
            systems = new Systems();
        }
        #endregion


        #region 初始化
        /// <summary>
        /// 启动程序（在Start()之前）
        /// </summary>
        public static void Awake()
        {
            systems = new Systems();
        }


        /// <summary>
        /// 初始化程序
        /// </summary>
        public static void Start()
        {
            /* 数据绑定赋值 */
            MainWindow.DataContext = Datas;//设置[数据源]

            /* 读取数据 */
            Systems.SaveSystem.Load();

            /* 进行一些操作 */
            Systems.DlcSystem.Handle(DlcType.None);//进行一些和[DLC相关]的操作
            Systems.ThemeSystem.Handle(Datas.SettingsData.Theme);//设置皮肤
            Systems.LanguageSystem.Handle(Datas.SettingsData.Language);//设置语言

            /* 打开主界面 */
            Uis.MainUi.OpenOrClose(true);
        }


        /// <summary>
        /// 退出程序
        /// </summary>
        public static void Exit()
        {
            /* 保存数据 */
            systems.SaveSystem.Save();//保存App数据

            /* 关闭协同合作功能 */
            systems.CollaborationSystem.Handle(false);
        }
        #endregion


        #region 测试
        /// <summary>
        /// 测试用的
        /// </summary>
        public static void Test()
        {
            //ObservableCollection<BugData> _bugDatas = new ObservableCollection<BugData>();


            //_bugDatas.Add(new BugData()
            //{
            //    Id = 0,
            //    Priority = PriorityType.High,
            //    Progress = ProgressType.Deprecat,
            //    CreateTime = new DateTime(2009, 10, 20, 12, 00, 00),
            //    UpdateTime = new DateTime(2009, 10, 20, 12, 00, 00),
            //});


            //AppManager.Datas.ProjectData.BugDatas = _bugDatas;

        }

        #endregion
    }

}
