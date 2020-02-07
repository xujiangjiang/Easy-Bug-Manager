using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        #region 初始化+退出
        /// <summary>
        /// 当程序启动时
        /// </summary>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppManager.MainApp = this;
            AppManager.Awake();
        }

        /// <summary>
        /// 当程序退出时
        /// </summary>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AppManager.Exit();
        }
        #endregion
    }
}
