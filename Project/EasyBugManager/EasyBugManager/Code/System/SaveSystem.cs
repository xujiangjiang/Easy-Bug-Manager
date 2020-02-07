/* By: 絮大王（sukiup@163.com）
   Time：2019年10月14日12:22:24*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyBugManager
{
    /// <summary>
    /// 用于保存和读取
    /// </summary>
    public class SaveSystem
    {
        #region [公开方法]
        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>是否读取成功？</returns>
        public bool Load()
        {
            AppManager.Datas.SettingsData.Language = (LanguageType)Properties.Settings.Default.Language;//语言
            AppManager.Datas.SettingsData.Sound = Properties.Settings.Default.Sound;//是否有声音？
            AppManager.Datas.SettingsData.Theme = (ThemeType)Properties.Settings.Default.Theme;//皮肤
            AppManager.Datas.SettingsData.Transparent = Properties.Settings.Default.Transparent;//透明度


            /* 设置窗口的尺寸 */
            //获取窗口尺寸的数据
            AppManager.Datas.SettingsData.WindowWidth = Properties.Settings.Default.WindowWidth;//窗口的宽度
            AppManager.Datas.SettingsData.WindowHeight = Properties.Settings.Default.WindowHeight;//窗口的高度
            //给窗口的高度、宽度赋值
            AppManager.MainWindow.Width = AppManager.Datas.SettingsData.WindowWidth;
            AppManager.MainWindow.Height = AppManager.Datas.SettingsData.WindowHeight;


            return true;
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <returns>是否保存成功？</returns>
        public bool Save()
        {
            Properties.Settings.Default.Language = (int)AppManager.Datas.SettingsData.Language;//语言
            Properties.Settings.Default.Sound = AppManager.Datas.SettingsData.Sound;//是否有声音？
            Properties.Settings.Default.Theme = (int)AppManager.Datas.SettingsData.Theme;//皮肤
            Properties.Settings.Default.Transparent = AppManager.Datas.SettingsData.Transparent;//透明度

            Properties.Settings.Default.WindowWidth = AppManager.Datas.SettingsData.WindowWidth;//窗口的宽度
            Properties.Settings.Default.WindowHeight = AppManager.Datas.SettingsData.WindowHeight;//窗口的高度

            //保存
            Properties.Settings.Default.Save();

            return true;
        }
        #endregion
    }
}
