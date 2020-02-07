/* By: 絮大王（sukiup@163.com）
   Time：2019年11月24日22:47:20*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// DLC的系统（根据DLC，来进行一些操作）
    /// </summary>
    public class DlcSystem
    {
        #region [公开属性 - 数据]
        /// <summary>
        /// DLC的类型
        /// </summary>
        public DlcType DlcType { get; set; }

        #endregion

        #region [公开方法]
        /// <summary>
        /// 按照[皮肤]进行一些处理 (设置图片和文字等)
        /// </summary>
        /// <param name="_dlc">皮肤</param>
        public void Handle(DlcType _dlc)
        {
            //赋值
            DlcType = _dlc;

            //处理
            switch (_dlc)
            {
                //如果没有任何的DLC
                case DlcType.None:
                    //[设置界面]
                    AppManager.Uis.SettingsUi.UiControl.CnThemeGrid.Visibility = Visibility.Visible;
                    AppManager.Uis.SettingsUi.UiControl.CnThemeBearGrid.Visibility = Visibility.Collapsed;
                    AppManager.Uis.SettingsUi.UiControl.CnThemeCatGrid.Visibility = Visibility.Collapsed;
                    AppManager.Uis.SettingsUi.UiControl.EnThemeGrid.Visibility = Visibility.Visible;
                    AppManager.Uis.SettingsUi.UiControl.EnThemeBearGrid.Visibility = Visibility.Collapsed;
                    AppManager.Uis.SettingsUi.UiControl.EnThemeCatGrid.Visibility = Visibility.Collapsed;
                    break;


                //如果是[CatTheme]DLC
                case DlcType.CatTheme:
                    //[设置界面]
                    AppManager.Uis.SettingsUi.UiControl.CnThemeGrid.Visibility = Visibility.Collapsed;
                    AppManager.Uis.SettingsUi.UiControl.CnThemeBearGrid.Visibility = Visibility.Visible;
                    AppManager.Uis.SettingsUi.UiControl.CnThemeCatGrid.Visibility = Visibility.Visible;
                    AppManager.Uis.SettingsUi.UiControl.EnThemeGrid.Visibility = Visibility.Collapsed;
                    AppManager.Uis.SettingsUi.UiControl.EnThemeBearGrid.Visibility = Visibility.Visible;
                    AppManager.Uis.SettingsUi.UiControl.EnThemeCatGrid.Visibility = Visibility.Visible;
                    break;
            }

            //设置皮肤
            switch (_dlc)
            {
                //如果没有任何的DLC
                case DlcType.None:
                    //取到皮肤
                    ThemeType _themeType = AppManager.Systems.ThemeSystem.Theme;
                    
                    //如果是猫咪皮肤，就把皮肤设置为默认皮肤
                    if (_themeType == ThemeType.Cat_White || _themeType == ThemeType.Cat_Dark)
                    {
                        AppManager.Systems.ThemeSystem.Theme = ThemeType.White;
                    }
                    break;
            }
        }

        #endregion
    }
}
