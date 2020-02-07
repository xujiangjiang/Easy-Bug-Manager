/* By: 絮大王（sukiup@163.com）
   Time：2019年11月20日05:03:37*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EasyBugManager
{
    /// <summary>
    /// 皮肤的系统（用于更换皮肤）
    /// </summary>
    public class ThemeSystem
    {
        #region [公开属性]
        /// <summary>
        /// 皮肤的类型
        /// </summary>
        public ThemeType Theme
        {
            get { return AppManager.Datas.SettingsData.Theme; }
            set { AppManager.Datas.SettingsData.Theme = value; }
        }

        #endregion



        #region [公开方法]
        /// <summary>
        /// 按照[皮肤]进行一些处理 (设置图片和文字等)
        /// </summary>
        /// <param name="_language">皮肤</param>
        public void Handle(ThemeType _theme)
        {
            this.SetImage(_theme);//设置图片
            this.SetColor(_theme);//设置颜色
            this.SetOther(_theme);//设置其他
        }
        #endregion

        #region [私有方法]
        /// <summary>
        /// 按照皮肤设置图片
        /// </summary>
        /// <param name="_theme">皮肤</param>
        private void SetImage(ThemeType _theme)
        {
            string _dictionaryFilePath = "";

            //获得资源字典的文件
            switch (_theme)
            {
                case ThemeType.White:
                    _dictionaryFilePath = "/EasyBugManager;component/Xaml/Dictionary/Theme/White/ImageDictionary.xaml";
                    break;

                case ThemeType.Dark:
                    _dictionaryFilePath = "/EasyBugManager;component/Xaml/Dictionary/Theme/Dark/ImageDictionary.xaml";
                    break;

                case ThemeType.Cat_White:
                    _dictionaryFilePath = "/EasyBugManager;component/Xaml/Dictionary/Theme/Cat-White/ImageDictionary.xaml";
                    break;

                case ThemeType.Cat_Dark:
                    _dictionaryFilePath = "/EasyBugManager;component/Xaml/Dictionary/Theme/Cat-Dark/ImageDictionary.xaml";
                    break;
            }

            //创建1个新的资源字典
            ResourceDictionary _resourceDictionary = new ResourceDictionary();

            //设置资源字典的资源
            _resourceDictionary.Source = new Uri(_dictionaryFilePath, UriKind.Relative);

            //替换资源字典（替换App.xaml中的TextDictionary）
            AppManager.MainApp.Resources.MergedDictionaries[1] = _resourceDictionary;
        }

        /// <summary>
        /// 按照皮肤设置颜色
        /// </summary>
        /// <param name="_theme">皮肤</param>
        private void SetColor(ThemeType _theme)
        {
            /* 白色和黑夜皮肤 */
            {
                //白色系的皮肤
                if (_theme == ThemeType.White || _theme == ThemeType.Cat_White)
                {
                    //主界面
                    AppManager.Uis.MainUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
                    AppManager.MainWindow.BackgroudBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));


                    //List界面
                    AppManager.Uis.ListUi.UiControl.ProjectNameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFBEBEBE"));

                    AppManager.Uis.ListUi.UiControl.PreviousPageButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
                    AppManager.Uis.ListUi.UiControl.PreviousPageButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE6E6E6"));
                    AppManager.Uis.ListUi.UiControl.NextPageButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
                    AppManager.Uis.ListUi.UiControl.NextPageButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE6E6E6"));

                    AppManager.Uis.ListUi.UiControl.ProgressListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.ListUi.UiControl.ProgressListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE6E6E6"));
                    AppManager.Uis.ListUi.UiControl.PriorityListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.ListUi.UiControl.PriorityListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE6E6E6"));
                    AppManager.Uis.ListUi.UiControl.CreateTimeListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.ListUi.UiControl.CreateTimeListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE6E6E6"));
                    AppManager.Uis.ListUi.UiControl.UpdateTimeListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.ListUi.UiControl.UpdateTimeListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE6E6E6"));


                    //BugItem
                    for (int i = 0; i < AppManager.Uis.ListUi.UiControl.BugItems.Count; i++)
                    {
                        BugListItemControl _bugListItemControl = AppManager.Uis.ListUi.UiControl.BugItems[i];

                        _bugListItemControl.RedProgressButton.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A5FFFFFF"));
                        _bugListItemControl.GreyProgressButton.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE6E6E6"));
                        _bugListItemControl.GreyProgressButton.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A5FFFFFF"));

                        _bugListItemControl.BaseCheckControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#72FFFFFF"));
                        _bugListItemControl.BaseCheckControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#72dedede"));
                        _bugListItemControl.BaseCheckControl.CheckedBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                        _bugListItemControl.BaseCheckControl.CheckedBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dedede"));

                        _bugListItemControl.MoreButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                        _bugListItemControl.MoreButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE6E6E6"));

                        _bugListItemControl.RefreshButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                        _bugListItemControl.RefreshButtonControl.MouseLeaveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                        _bugListItemControl.RefreshButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB8B8B8"));
                        _bugListItemControl.RefreshButtonControl.MouseLeaveBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e5e5e5"));
                        _bugListItemControl.RefreshButtonControl.ButtonImageBorder.Background = _bugListItemControl.RefreshButtonControl.MouseLeaveBackground;
                        _bugListItemControl.RefreshButtonControl.ButtonImageBorder.BorderBrush = _bugListItemControl.RefreshButtonControl.MouseLeaveBorderBrush;

                        _bugListItemControl.GoToPageButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                        _bugListItemControl.GoToPageButtonControl.MouseLeaveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                        _bugListItemControl.GoToPageButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB8B8B8"));
                        _bugListItemControl.GoToPageButtonControl.MouseLeaveBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e5e5e5"));
                        _bugListItemControl.GoToPageButtonControl.ButtonImageBorder.Background = _bugListItemControl.RefreshButtonControl.MouseLeaveBackground;
                        _bugListItemControl.GoToPageButtonControl.ButtonImageBorder.BorderBrush = _bugListItemControl.RefreshButtonControl.MouseLeaveBorderBrush;

                        _bugListItemControl.DeletedButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c8c8c8"));
                        _bugListItemControl.DeletedButtonControl.MouseLeaveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c8c8c8"));

                        _bugListItemControl.ContextMenuControl.BaseBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                        _bugListItemControl.ContextMenuControl.BaseBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e8e8e8"));
                        _bugListItemControl.ContextMenuControl.ProgressBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                        _bugListItemControl.ContextMenuControl.ProgressBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e8e8e8"));
                        _bugListItemControl.ContextMenuControl.PriorityBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                        _bugListItemControl.ContextMenuControl.PriorityBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e8e8e8"));

                        _bugListItemControl.ContextMenuControl.Line1Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dfdfdf"));
                        _bugListItemControl.ContextMenuControl.Line2Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dfdfdf"));
                        _bugListItemControl.ContextMenuControl.Line3Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dfdfdf"));
                    }


                    //Bug界面
                    AppManager.Uis.BugUi.UiControl.UpdateNumberFrontTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bebebe"));
                    AppManager.Uis.BugUi.UiControl.UpdateNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bebebe"));
                    AppManager.Uis.BugUi.UiControl.UpdateNumberBehindTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bebebe"));
                    AppManager.Uis.BugUi.UiControl.StartTimeTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bebebe"));
                    AppManager.Uis.BugUi.UiControl.EndTimeTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bebebe"));

                    AppManager.Uis.BugUi.UiControl.LineBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e5e5e5"));

                    AppManager.Uis.BugUi.UiControl.BackButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF1F1F1"));
                    AppManager.Uis.BugUi.UiControl.BugNameButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF1F1F1"));


                    //同步界面
                    AppManager.Uis.SyncUi.UiControl.CnSyncButtonControl.MouseLeaveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF4F4F4"));
                    AppManager.Uis.SyncUi.UiControl.CnSyncButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE6E6E6"));
                    AppManager.Uis.SyncUi.UiControl.EnSyncButtonControl.MouseLeaveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF4F4F4"));
                    AppManager.Uis.SyncUi.UiControl.EnSyncButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE6E6E6"));

                    AppManager.Uis.SyncUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));


                    //其他界面
                    AppManager.Uis.SettingsUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.SettingsUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.CreateProjectUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.ExportUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.CreateBugUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.ChangeBugUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.DeleteBugTipUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.DeleteRecordTipUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.BaseTipUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.ImageUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.ErrorUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));

                    AppManager.Uis.CreateBugUi.UiControl.RelatedBugsListBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.CreateBugUi.UiControl.RelatedBugsListBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7d7d7d"));
                    AppManager.Uis.ChangeBugUi.UiControl.RelatedBugsListBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    AppManager.Uis.ChangeBugUi.UiControl.RelatedBugsListBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7d7d7d"));
                }

                //黑色系的皮肤
                else if (_theme == ThemeType.Dark || _theme == ThemeType.Cat_Dark)
                {
                    //主界面
                    AppManager.Uis.MainUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.MainWindow.BackgroudBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));


                    //List界面
                    AppManager.Uis.ListUi.UiControl.ProjectNameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a8a8a8"));

                    AppManager.Uis.ListUi.UiControl.PreviousPageButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.ListUi.UiControl.PreviousPageButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3d3d3d"));
                    AppManager.Uis.ListUi.UiControl.NextPageButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.ListUi.UiControl.NextPageButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3d3d3d"));

                    AppManager.Uis.ListUi.UiControl.ProgressListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.ListUi.UiControl.ProgressListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3d3d3d"));
                    AppManager.Uis.ListUi.UiControl.PriorityListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.ListUi.UiControl.PriorityListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3d3d3d"));
                    AppManager.Uis.ListUi.UiControl.CreateTimeListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.ListUi.UiControl.CreateTimeListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3d3d3d"));
                    AppManager.Uis.ListUi.UiControl.UpdateTimeListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.ListUi.UiControl.UpdateTimeListHeadButtonControl.ListHeadColorButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3d3d3d"));


                    //BugItem
                    for (int i = 0; i < AppManager.Uis.ListUi.UiControl.BugItems.Count; i++)
                    {
                        //BugItem
                        BugListItemControl _bugListItemControl = AppManager.Uis.ListUi.UiControl.BugItems[i];

                        _bugListItemControl.RedProgressButton.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A5222222"));
                        _bugListItemControl.GreyProgressButton.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3d3d3d"));
                        _bugListItemControl.GreyProgressButton.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A5222222"));

                        _bugListItemControl.BaseCheckControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#72222222"));
                        _bugListItemControl.BaseCheckControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#72515151"));
                        _bugListItemControl.BaseCheckControl.CheckedBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                        _bugListItemControl.BaseCheckControl.CheckedBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#515151"));

                        _bugListItemControl.MoreButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                        _bugListItemControl.MoreButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3d3d3d"));

                        _bugListItemControl.RefreshButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                        _bugListItemControl.RefreshButtonControl.MouseLeaveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                        _bugListItemControl.RefreshButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#515151"));
                        _bugListItemControl.RefreshButtonControl.MouseLeaveBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3d3d3d"));
                        _bugListItemControl.RefreshButtonControl.ButtonImageBorder.Background = _bugListItemControl.RefreshButtonControl.MouseLeaveBackground;
                        _bugListItemControl.RefreshButtonControl.ButtonImageBorder.BorderBrush = _bugListItemControl.RefreshButtonControl.MouseLeaveBorderBrush;

                        _bugListItemControl.GoToPageButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                        _bugListItemControl.GoToPageButtonControl.MouseLeaveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                        _bugListItemControl.GoToPageButtonControl.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#515151"));
                        _bugListItemControl.GoToPageButtonControl.MouseLeaveBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3d3d3d"));
                        _bugListItemControl.GoToPageButtonControl.ButtonImageBorder.Background = _bugListItemControl.RefreshButtonControl.MouseLeaveBackground;
                        _bugListItemControl.GoToPageButtonControl.ButtonImageBorder.BorderBrush = _bugListItemControl.RefreshButtonControl.MouseLeaveBorderBrush;

                        _bugListItemControl.DeletedButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#111111"));
                        _bugListItemControl.DeletedButtonControl.MouseLeaveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#111111"));

                        _bugListItemControl.ContextMenuControl.BaseBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                        _bugListItemControl.ContextMenuControl.BaseBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#515151"));
                        _bugListItemControl.ContextMenuControl.ProgressBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                        _bugListItemControl.ContextMenuControl.ProgressBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#515151"));
                        _bugListItemControl.ContextMenuControl.PriorityBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                        _bugListItemControl.ContextMenuControl.PriorityBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#515151"));
                        
                        _bugListItemControl.ContextMenuControl.Line1Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003d3d3d"));
                        _bugListItemControl.ContextMenuControl.Line2Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003d3d3d"));
                        _bugListItemControl.ContextMenuControl.Line3Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003d3d3d"));
                    }


                    //Bug界面
                    AppManager.Uis.BugUi.UiControl.UpdateNumberFrontTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8f8f8f"));
                    AppManager.Uis.BugUi.UiControl.UpdateNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8f8f8f"));
                    AppManager.Uis.BugUi.UiControl.UpdateNumberBehindTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8f8f8f"));
                    AppManager.Uis.BugUi.UiControl.StartTimeTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8f8f8f"));
                    AppManager.Uis.BugUi.UiControl.EndTimeTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8f8f8f"));

                    AppManager.Uis.BugUi.UiControl.LineBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3c3c3c"));

                    AppManager.Uis.BugUi.UiControl.BackButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2d2d2d"));
                    AppManager.Uis.BugUi.UiControl.BugNameButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2d2d2d"));


                    //同步界面
                    AppManager.Uis.SyncUi.UiControl.CnSyncButtonControl.MouseLeaveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2f2f2f"));
                    AppManager.Uis.SyncUi.UiControl.CnSyncButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#343434"));
                    AppManager.Uis.SyncUi.UiControl.EnSyncButtonControl.MouseLeaveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2f2f2f"));
                    AppManager.Uis.SyncUi.UiControl.EnSyncButtonControl.MouseEnterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#343434"));

                    AppManager.Uis.SyncUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));


                    //其他界面
                    AppManager.Uis.SettingsUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.SettingsUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.CreateProjectUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.ExportUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.CreateBugUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.ChangeBugUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.DeleteBugTipUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.DeleteRecordTipUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.BaseTipUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.ImageUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.ErrorUi.UiControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));

                    AppManager.Uis.CreateBugUi.UiControl.RelatedBugsListBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.CreateBugUi.UiControl.RelatedBugsListBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#363636"));
                    AppManager.Uis.ChangeBugUi.UiControl.RelatedBugsListBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                    AppManager.Uis.ChangeBugUi.UiControl.RelatedBugsListBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#363636"));
                }
            }



            /* 熊和猫咪皮肤 */
            {
                //熊的皮肤
                if (_theme == ThemeType.White || _theme == ThemeType.Dark)
                {
                    //BugItem
                    for (int i = 0; i < AppManager.Uis.ListUi.UiControl.BugItems.Count; i++)
                    {
                        BugListItemControl _bugListItemControl = AppManager.Uis.ListUi.UiControl.BugItems[i];
                        _bugListItemControl.GoToPageNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4669a5"));
                    }

                    //同步界面
                    AppManager.Uis.SyncUi.UiControl.NumberOfSyncContentTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4669a5"));
                    AppManager.Uis.SyncUi.UiControl.LastSyncTimeContentTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4669a5"));
                }

                //猫咪的皮肤
                else if(_theme == ThemeType.Cat_White || _theme == ThemeType.Cat_Dark)
                {
                    //BugItem
                    for (int i = 0; i < AppManager.Uis.ListUi.UiControl.BugItems.Count; i++)
                    {
                        BugListItemControl _bugListItemControl = AppManager.Uis.ListUi.UiControl.BugItems[i];
                        _bugListItemControl.GoToPageNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9e6c3a"));
                    }

                    //同步界面
                    AppManager.Uis.SyncUi.UiControl.NumberOfSyncContentTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f5ac00"));
                    AppManager.Uis.SyncUi.UiControl.LastSyncTimeContentTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f5ac00"));
                }
            }



            /*每个皮肤单独的颜色*/
            {
                switch (_theme)
                {
                    /*白色*/
                    case ThemeType.White:

                        //List界面
                        AppManager.Uis.ListUi.UiControl.ListTipControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4669a5"));

                        AppManager.Uis.ListUi.UiControl.PriorityHighBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a54646"));
                        AppManager.Uis.ListUi.UiControl.PriorityMidBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4669a5"));
                        AppManager.Uis.ListUi.UiControl.PriorityLowBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5e5e5e"));
                        AppManager.Uis.ListUi.UiControl.UndoneBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a54646"));


                        //BugItem
                        for (int i = 0; i < AppManager.Uis.ListUi.UiControl.BugItems.Count; i++)
                        {
                            BugListItemControl _bugListItemControl = AppManager.Uis.ListUi.UiControl.BugItems[i];
                            _bugListItemControl.RedProgressButton.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFeddbdb"));
                        }


                        //其他界面
                        AppManager.Uis.CreateProjectUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#852a2a"));
                        AppManager.Uis.ExportUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#852a2a"));
                        AppManager.Uis.CreateBugUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#852a2a"));
                        AppManager.Uis.ChangeBugUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#852a2a"));
                        AppManager.Uis.DeleteBugTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9b2424"));
                        AppManager.Uis.DeleteRecordTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9b2424"));
                        AppManager.Uis.BaseTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9b2424"));
                        break;



                    /*黑色*/
                    case ThemeType.Dark:
                        //List界面
                        AppManager.Uis.ListUi.UiControl.ListTipControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#535353"));

                        AppManager.Uis.ListUi.UiControl.PriorityHighBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a54646"));
                        AppManager.Uis.ListUi.UiControl.PriorityMidBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4669a5"));
                        AppManager.Uis.ListUi.UiControl.PriorityLowBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5e5e5e"));
                        AppManager.Uis.ListUi.UiControl.UndoneBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a54646"));


                        //BugItem
                        for (int i = 0; i < AppManager.Uis.ListUi.UiControl.BugItems.Count; i++)
                        {
                            BugListItemControl _bugListItemControl = AppManager.Uis.ListUi.UiControl.BugItems[i];
                            _bugListItemControl.RedProgressButton.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF642d30"));
                        }


                        //其他界面
                        AppManager.Uis.CreateProjectUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#852a2a"));
                        AppManager.Uis.ExportUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#852a2a"));
                        AppManager.Uis.CreateBugUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#852a2a"));
                        AppManager.Uis.ChangeBugUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#852a2a"));
                        AppManager.Uis.DeleteBugTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bbbbbb"));
                        AppManager.Uis.DeleteRecordTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bbbbbb"));
                        AppManager.Uis.BaseTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bbbbbb"));
                        break;






                    /*猫咪（白色）*/
                    case ThemeType.Cat_White:

                        //List界面
                        AppManager.Uis.ListUi.UiControl.ListTipControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e6c3a"));

                        AppManager.Uis.ListUi.UiControl.PriorityHighBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f5ba10"));
                        AppManager.Uis.ListUi.UiControl.PriorityMidBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#613f1d"));
                        AppManager.Uis.ListUi.UiControl.PriorityLowBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a4a4a4"));
                        AppManager.Uis.ListUi.UiControl.UndoneBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e6c3a"));


                        //BugItem
                        for (int i = 0; i < AppManager.Uis.ListUi.UiControl.BugItems.Count; i++)
                        {
                            BugListItemControl _bugListItemControl = AppManager.Uis.ListUi.UiControl.BugItems[i];

                            _bugListItemControl.RedProgressButton.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFd1b9a1"));
                        }


                        //其他界面
                        AppManager.Uis.CreateProjectUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e6c3a"));
                        AppManager.Uis.ExportUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e6c3a"));
                        AppManager.Uis.CreateBugUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e6c3a"));
                        AppManager.Uis.ChangeBugUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e6c3a"));
                        AppManager.Uis.DeleteBugTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e6c3a"));
                        AppManager.Uis.DeleteRecordTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e6c3a"));
                        AppManager.Uis.BaseTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e6c3a"));
                        break;



                    /*猫咪（黑色）*/
                    case ThemeType.Cat_Dark:

                        //List界面
                        AppManager.Uis.ListUi.UiControl.ListTipControl.BackgroundBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#535353"));

                        AppManager.Uis.ListUi.UiControl.PriorityHighBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e0ab12"));
                        AppManager.Uis.ListUi.UiControl.PriorityMidBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#724c26"));
                        AppManager.Uis.ListUi.UiControl.PriorityLowBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#535353"));
                        AppManager.Uis.ListUi.UiControl.UndoneBugNumberTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e0ab12"));


                        //BugItem
                        for (int i = 0; i < AppManager.Uis.ListUi.UiControl.BugItems.Count; i++)
                        {
                            BugListItemControl _bugListItemControl = AppManager.Uis.ListUi.UiControl.BugItems[i];

                            _bugListItemControl.RedProgressButton.MouseEnterBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#81661a"));
                        }


                        //其他界面
                        AppManager.Uis.CreateProjectUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e0ab12"));
                        AppManager.Uis.ExportUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e0ab12"));
                        AppManager.Uis.CreateBugUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e0ab12"));
                        AppManager.Uis.ChangeBugUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e0ab12"));
                        AppManager.Uis.DeleteBugTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e0ab12"));
                        AppManager.Uis.DeleteRecordTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e0ab12"));
                        AppManager.Uis.BaseTipUi.UiControl.TipTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e0ab12"));
                        break;
                }
            }



            /*文本框的颜色*/
            AnimationTool.PlayTextChangedAnimation(AppManager.Uis.ListUi.UiControl.SearchTextBox);
        }

        /// <summary>
        /// 按照皮肤设置其他
        /// </summary>
        /// <param name="_theme">皮肤</param>
        private void SetOther(ThemeType _theme)
        {
            /* 移动控件的位置 */
            {
                //熊的皮肤
                if (_theme == ThemeType.White || _theme == ThemeType.Dark)
                {
                    //[主界面]
                    Canvas.SetLeft(AppManager.Uis.MainUi.UiControl.BearBorder, 409);
                    Canvas.SetTop(AppManager.Uis.MainUi.UiControl.BearBorder, 114);
                    Canvas.SetLeft(AppManager.Uis.MainUi.UiControl.BearForegroundBorder, 409);
                    Canvas.SetTop(AppManager.Uis.MainUi.UiControl.BearForegroundBorder, 114);

                    Canvas.SetLeft(AppManager.Uis.MainUi.UiControl.CreateProjectButtonControl, 333);
                    Canvas.SetTop(AppManager.Uis.MainUi.UiControl.CreateProjectButtonControl, 178);
                    Canvas.SetLeft(AppManager.Uis.MainUi.UiControl.OpenProjectButtonControl, 342);
                    Canvas.SetTop(AppManager.Uis.MainUi.UiControl.OpenProjectButtonControl, 234);


                    //[列表界面]
                    Canvas.SetRight(AppManager.Uis.ListUi.UiControl.TopButtonGroupStackPanel, 63);
                    Canvas.SetTop(AppManager.Uis.ListUi.UiControl.TopButtonGroupStackPanel, 36);
                    Canvas.SetRight(AppManager.Uis.ListUi.UiControl.TopButtonForegroundGroupStackPanel, 63);
                    Canvas.SetTop(AppManager.Uis.ListUi.UiControl.TopButtonForegroundGroupStackPanel, 36);


                    //[同步界面]
                    Canvas.SetRight(AppManager.Uis.SyncUi.UiControl.TopButtonForegroundGroupStackPanel, 63);
                    Canvas.SetTop(AppManager.Uis.SyncUi.UiControl.TopButtonForegroundGroupStackPanel, 36);
                    Canvas.SetRight(AppManager.Uis.SyncUi.UiControl.TopButtonBackgroundGroupStackPanel, 63);
                    Canvas.SetTop(AppManager.Uis.SyncUi.UiControl.TopButtonBackgroundGroupStackPanel, 36);
                }

                //猫咪皮肤
                else if(_theme == ThemeType.Cat_White || _theme == ThemeType.Cat_Dark)
                {
                    //[主界面]
                    Canvas.SetLeft(AppManager.Uis.MainUi.UiControl.BearBorder, 384);
                    Canvas.SetTop(AppManager.Uis.MainUi.UiControl.BearBorder, 1);
                    Canvas.SetLeft(AppManager.Uis.MainUi.UiControl.BearForegroundBorder, 384);
                    Canvas.SetTop(AppManager.Uis.MainUi.UiControl.BearForegroundBorder, 1);

                    Canvas.SetLeft(AppManager.Uis.MainUi.UiControl.CreateProjectButtonControl, 255);
                    Canvas.SetTop(AppManager.Uis.MainUi.UiControl.CreateProjectButtonControl, 178);
                    Canvas.SetLeft(AppManager.Uis.MainUi.UiControl.OpenProjectButtonControl, 264);
                    Canvas.SetTop(AppManager.Uis.MainUi.UiControl.OpenProjectButtonControl, 234);


                    //[列表界面]
                    Canvas.SetRight(AppManager.Uis.ListUi.UiControl.TopButtonGroupStackPanel, -20);
                    Canvas.SetTop(AppManager.Uis.ListUi.UiControl.TopButtonGroupStackPanel, 36);
                    Canvas.SetRight(AppManager.Uis.ListUi.UiControl.TopButtonForegroundGroupStackPanel, -20);
                    Canvas.SetTop(AppManager.Uis.ListUi.UiControl.TopButtonForegroundGroupStackPanel, 36);


                    //[同步界面]
                    Canvas.SetRight(AppManager.Uis.SyncUi.UiControl.TopButtonForegroundGroupStackPanel, -20);
                    Canvas.SetTop(AppManager.Uis.SyncUi.UiControl.TopButtonForegroundGroupStackPanel, 36);
                    Canvas.SetRight(AppManager.Uis.SyncUi.UiControl.TopButtonBackgroundGroupStackPanel, -20);
                    Canvas.SetTop(AppManager.Uis.SyncUi.UiControl.TopButtonBackgroundGroupStackPanel, 36);
                }
            }
        }
        #endregion

        #region [公开方法 - RecordItem]

        /// <summary>
        /// 根据皮肤 设置RecordItem
        /// </summary>
        /// <param name="_recordListItem">要设置的RecordItem控件</param>
        public void SetRecordItem(RecordListItemControl _recordListItem)
        {
            //获取当前皮肤的类型
            ThemeType _theme = Theme;

            //白色系的皮肤
            if (_theme == ThemeType.White || _theme == ThemeType.Cat_White)
            {
                //RecordItem
                _recordListItem.BearBubbleBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                _recordListItem.BearBubbleBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9f9f9f"));
                _recordListItem.BugBubbleBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                _recordListItem.BugBubbleBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9f9f9f"));

                _recordListItem.BugTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9d9d9d"));
                _recordListItem.BearTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6f6f6f"));

                _recordListItem.TimeTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c2c2c2"));
            }

            //黑色系的皮肤
            else if (_theme == ThemeType.Dark || _theme == ThemeType.Cat_Dark)
            {
                //RecordItem
                _recordListItem.BearBubbleBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                _recordListItem.BearBubbleBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#737373"));
                _recordListItem.BugBubbleBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
                _recordListItem.BugBubbleBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6d6b6b"));

                _recordListItem.BugTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6d6b6b"));
                _recordListItem.BearTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9d9d9d"));

                _recordListItem.TimeTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#767676"));
            }
        }

        #endregion
    }
}
