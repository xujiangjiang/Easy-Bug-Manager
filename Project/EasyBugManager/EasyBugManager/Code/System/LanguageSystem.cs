/* By: 絮大王（sukiup@163.com）
   Time：2019年11月15日12:46:21*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EasyBugManager
{
    /// <summary>
    /// 语言的系统（用于更换语言）
    /// </summary>
    public class LanguageSystem : INotifyPropertyChanged
    {

        #region 字段
        /*Tip界面*/
        private string errorTipTitle = "错误！";//[打开项目错误]的提示标题
        private string openProjectErrorTipContent = "抱歉，这不是Easy Bug Manager软件的项目文件";//[打开项目错误]的提示内容
        private string unableToExportTipContent = "无法将项目导出为Excel文件！请先打开一个项目，然后再进行导出。";//[导出Excel错误]的提示内容

        /*创建项目界面*/
        private string noProjectNameTip = "( 请输入项目名呀~ )";//[没有项目名]的提示
        private string noSaveLocationTip = "( 请选择保存的文件夹呀~ )";//[没有保存路径]的提示
        private string unknownErrorTip = "( 抱歉呀，出现了未知的错误 )";//[未知的错误]的提示

        /*创建or修改Bug界面*/
        private string noBugNameTip = "( 请输入Bug的名字呀~ )";//[没有Bug名字]的提示

        /*导出界面*/
        private string exportSucceededTipTitle = "导出成功！";//[导出成功]的提示标题

        /*图片界面*/
        private string notOpenFileErrorTip = "打开文件出错";//[打开文件出错]的提示

        /*同步界面*/
        private string collaborativeModeTestTipTitle = "提示";//[协同合作模式 测试]的提示标题
        private string collaborativeModeTestTipContent = "目前 [协同合作模式] 只是一个测试功能，\n可能有很多不完善的地方，也许会有一些Bug。\n希望可以多包容，欢迎给我们建议，非常感谢！";//[协同合作模式 测试]的提示内容
        private string collaborativeModeString = "协同合作模式";//[协同合作模式]的文字

        /*Bug界面*/
        private string updateNumberFrontText = "(经过 ";//[更新次数]前面的文字
        private string updateNumberBehindText = " 次更新)";//[更新次数]后面的文字

        /*最近项目界面*/
        private string latelyProjectTipTitle = "是否把此项目从列表中移除？";//[最近项目]的提示标题
        private string latelyProjectTipContent = "项目文件不存在";//[最近项目]的提示内容
        private string latelyProjectItemOpenFolderString = "打开文件夹";//[最近项目Item]的[打开文件夹]文字
        private string latelyProjectItemRemoveString = "从列表中移除";//[最近项目Item]的[从列表中移除]文字

        #endregion



        #region [公开属性 - Tip界面]
        /// <summary>
        /// [错误]的提示标题
        /// </summary>
        public string ErrorTipTitle
        {
            get { return errorTipTitle; }
            set
            {
                errorTipTitle = value;
                PropertyChange("ErrorTipTitle");//更新Ui
            }
        }



        /// <summary>
        /// [打开项目错误]的提示标题
        /// </summary>
        public string OpenProjectErrorTipContent
        {
            get { return openProjectErrorTipContent; }
            set
            {
                openProjectErrorTipContent = value;
                PropertyChange("OpenProjectErrorTipContent");//更新Ui
            }
        }

        /// <summary>
        /// [导出Excel错误]的提示内容
        /// </summary>
        public string UnableToExportTipContent
        {
            get { return unableToExportTipContent; }
            set
            {
                unableToExportTipContent = value;
                PropertyChange("UnableToExportTipContent");//更新Ui
            }
        }
        #endregion

        #region [公开属性 - 创建项目界面]
        /// <summary>
        /// [没有项目名]的提示
        /// </summary>
        public string NoProjectNameTip
        {
            get { return noProjectNameTip; }
            set
            {
                noProjectNameTip = value;
                PropertyChange("NoProjectNameTip");//更新Ui
            }
        }

        /// <summary>
        /// [没有保存路径]的提示
        /// </summary>
        public string NoSaveLocationTip
        {
            get { return noSaveLocationTip; }
            set
            {
                noSaveLocationTip = value;
                PropertyChange("NoSaveLocationTip");//更新Ui
            }
        }

        /// <summary>
        /// [未知的错误]的提示
        /// </summary>
        public string UnknownErrorTip
        {
            get { return unknownErrorTip; }
            set
            {
                unknownErrorTip = value;
                PropertyChange("UnknownErrorTip");//更新Ui
            }
        }
        #endregion

        #region [公开属性 - 创建or修改Bug界面]

        /// <summary>
        /// [没有Bug名字]的提示
        /// </summary>
        public string NoBugNameTip
        {
            get { return noBugNameTip; }
            set
            {
                noBugNameTip = value;
                PropertyChange("NoBugNameTip");//更新Ui
            }
        }

        #endregion

        #region [公开属性 - 导出界面]
        /// <summary>
        /// [导出成功]的提示标题
        /// </summary>
        public string ExportSucceededTipTitle
        {
            get { return exportSucceededTipTitle; }
            set
            {
                exportSucceededTipTitle = value;
                PropertyChange("ExportSucceededTipTitle");//更新Ui
            }
        }

        #endregion

        #region [公开属性 - 图片界面]
        /// <summary>
        /// [打开文件出错]的提示标题
        /// </summary>
        public string NotOpenFileErrorTip
        {
            get { return notOpenFileErrorTip; }
            set
            {
                notOpenFileErrorTip = value;
                PropertyChange("NotOpenFileErrorTip");//更新Ui
            }
        }

        #endregion

        #region [公开属性 - 同步界面]
        /// <summary>
        /// [协同合作模式 测试]的提示标题
        /// </summary>
        public string CollaborativeModeTestTipTitle
        {
            get { return collaborativeModeTestTipTitle; }
            set
            {
                collaborativeModeTestTipTitle = value;
                PropertyChange("CollaborativeModeTestTipTitle");//更新Ui
            }
        }

        /// <summary>
        /// [协同合作模式 测试]的提示内容
        /// </summary>
        public string CollaborativeModeTestTipContent
        {
            get { return collaborativeModeTestTipContent; }
            set
            {
                collaborativeModeTestTipContent = value;
                PropertyChange("CollaborativeModeTestTipContent");//更新Ui
            }
        }

        /// <summary>
        /// [协同合作模式]的文字
        /// </summary>
        public string CollaborativeModeString
        {
            get { return collaborativeModeString; }
            set
            {
                collaborativeModeString = value;
                PropertyChange("CollaborativeModeString");//更新Ui
            }
        }
        #endregion

        #region [公开属性 - Bug界面]

        /// <summary>
        /// [更新次数]前面的文字
        /// </summary>
        public string UpdateNumberFrontText
        {
            get { return updateNumberFrontText; }
            set { updateNumberFrontText = value; }
        }

        /// <summary>
        /// [更新次数]后面的文字
        /// </summary>
        public string UpdateNumberBehindText
        {
            get { return updateNumberBehindText; }
            set { updateNumberBehindText = value; }
        }

        #endregion

        #region [公开属性 - 最近项目界面]

        /// <summary>
        /// [最近项目]的提示标题
        /// </summary>
        public string LatelyProjectTipTitle
        {
            get { return latelyProjectTipTitle; }
            set { latelyProjectTipTitle = value; }
        }

        /// <summary>
        /// [最近项目]的提示内容
        /// </summary>
        public string LatelyProjectTipContent
        {
            get { return latelyProjectTipContent; }
            set { latelyProjectTipContent = value; }
        }

        /// <summary>
        /// [最近项目Item]的[打开文件夹]文字
        /// </summary>
        public string LatelyProjectItemOpenFolderString
        {
            get { return latelyProjectItemOpenFolderString; }
            set { latelyProjectItemOpenFolderString = value; }
        }

        /// <summary>
        /// [最近项目Item]的[从列表中移除]文字
        /// </summary>
        public string LatelyProjectItemRemoveString
        {
            get { return latelyProjectItemRemoveString; }
            set { latelyProjectItemRemoveString = value; }
        }
        #endregion


        #region [公开属性 - 数据]

        /// <summary>
        /// 语言的类型
        /// </summary>
        public LanguageType LanguageType
        {
            get { return AppManager.Datas.SettingsData.Language; }
            set { AppManager.Datas.SettingsData.Language = value; }
        }

        #endregion






        #region [公开方法]
        /// <summary>
        /// 按照[语言]进行一些处理 (设置图片和文字等)
        /// </summary>
        /// <param name="_language">语言</param>
        public void Handle(LanguageType _language)
        {
            this.SetImage(_language);//设置图片
            this.SetText(_language);//设置文字
            this.SetOther(_language);//设置其他
        }
        #endregion

        #region [私有方法]
        /// <summary>
        /// 按照语言设置图片
        /// </summary>
        /// <param name="_language">语言</param>
        private void SetImage(LanguageType _language)
        {
            string _dictionaryFilePath = "";//字典文件的路径
            string _themeType = "";//皮肤的类型


            //获得资源字典的路径
            switch (AppManager.Datas.SettingsData.Theme)
            {
                case ThemeType.White:
                    _themeType = "White";
                    break;

                case ThemeType.Dark:
                    _themeType = "Dark";
                    break;

                case ThemeType.Cat_White:
                    _themeType = "Cat-White";
                    break;

                case ThemeType.Cat_Dark:
                    _themeType = "Cat-Dark";
                    break;
            }

            switch (_language)
            {
                case LanguageType.Chinese:
                    _dictionaryFilePath = "/EasyBugManager;component/Xaml/Dictionary/Theme/" + _themeType + "/ChineseTextDictionary.xaml";
                    break;

                case LanguageType.English:
                    _dictionaryFilePath = "/EasyBugManager;component/Xaml/Dictionary/Theme/" + _themeType + "/EnglishTextDictionary.xaml";
                    break;
            }

            //创建1个新的资源字典
            ResourceDictionary _resourceDictionary = new ResourceDictionary();

            //设置资源字典的资源
            _resourceDictionary.Source = new Uri(_dictionaryFilePath, UriKind.Relative);

            //替换资源字典（替换App.xaml中的TextDictionary）
            AppManager.MainApp.Resources.MergedDictionaries[2] = _resourceDictionary;
        }

        /// <summary>
        /// 按照语言设置文字
        /// </summary>
        /// <param name="_language">语言</param>
        private void SetText(LanguageType _language)
        {
            switch (_language)
            {
                /*中文*/
                case LanguageType.Chinese:
                    ErrorTipTitle = "错误！";
                    OpenProjectErrorTipContent = "抱歉，这不是Easy Bug Manager软件的项目文件";
                    UnableToExportTipContent = "无法将项目导出为Excel文件！\n请先打开一个项目，然后再进行导出";

                    NoProjectNameTip = "( 请输入项目名呀~ )";
                    NoSaveLocationTip = "( 请选择保存的文件夹呀~ )";
                    UnknownErrorTip = "( 抱歉呀，出现了未知的错误 )";

                    NoBugNameTip = "( 请输入Bug的名字呀~ )";

                    UpdateNumberFrontText = "(经过 ";
                    UpdateNumberBehindText = " 次更新)";

                    ExportSucceededTipTitle = "导出成功！";

                    NotOpenFileErrorTip = "打开文件出错";

                    CollaborativeModeTestTipTitle = "提示";
                    CollaborativeModeTestTipContent = "[协同合作模式] 目前处于公测阶段，\n可能有很多不完善的地方。\n希望可以多包容，欢迎给我们建议，非常感谢！";
                    CollaborativeModeString = "协同合作模式";

                    LatelyProjectTipTitle = "项目文件不存在";
                    LatelyProjectTipContent = "是否把此项目从列表中移除？";
                    LatelyProjectItemOpenFolderString = "打开文件夹";
                    LatelyProjectItemRemoveString = "从列表中移除";

                    AppManager.Datas.AppData.BugNameMaxLength = 100;
                    break;

                /*英文*/
                case LanguageType.English:
                    ErrorTipTitle = "Error！";
                    OpenProjectErrorTipContent = "Sorry, this is not a project file for the Easy Bug Manager software.";
                    UnableToExportTipContent = "Unable to export project to Excel file! \nPlease open a project before exporting.";

                    NoProjectNameTip = "( Please enter the project name. )";
                    NoSaveLocationTip = "( Please choose a saved folder. )";
                    UnknownErrorTip = "( Sorry, an unknown error occurred. )";

                    NoBugNameTip = "( Please enter the bug name. )";

                    UpdateNumberFrontText = "(After ";
                    UpdateNumberBehindText = " updates)";

                    ExportSucceededTipTitle = "Export succeeded!";

                    NotOpenFileErrorTip = "Error opening file.";

                    CollaborativeModeTestTipTitle = "Tip";
                    CollaborativeModeTestTipContent = "[Collaborative Mode] is currently in beta test, there may be some defects.\nHope you can put up with us more, welcome to give us more suggestions, thank you very much!";
                    CollaborativeModeString = "Collaborative Mode";

                    LatelyProjectTipTitle = "The project file does not exist";
                    LatelyProjectTipContent = "Do you want to remove this project from the list?";
                    LatelyProjectItemOpenFolderString = "Open folder";
                    LatelyProjectItemRemoveString = "Remove from list";

                    AppManager.Datas.AppData.BugNameMaxLength = 200;
                    break;
            }
        }

        /// <summary>
        /// 按照语言设置其他
        /// </summary>
        /// <param name="_language">语言</param>
        private void SetOther(LanguageType _language)
        {
            /*设置性格数据*/
            AppManager.Systems.TemperamentSystem.CreateTemperamentData(_language);

            /*设置同步数据*/
            AppManager.Systems.CollaborationSystem.OnLanguageChange(_language);

            /*设置最近数据*/
            AppManager.Systems.LatelySystem.OnLanguageChange(_language);

            /*界面*/
            switch (_language)
            {
                //如果是中文
                case LanguageType.Chinese:
                    //[创建项目界面]
                    AppManager.Uis.CreateProjectUi.UiControl.EnAdvancedOptionsButton.Visibility = Visibility.Collapsed;
                    AppManager.Uis.CreateProjectUi.UiControl.CnAdvancedOptionsButton.Visibility = Visibility.Visible;
                    Canvas.SetLeft(AppManager.Uis.CreateProjectUi.UiControl.IsCollaborationModeCheckControl, 185);
                    Canvas.SetLeft(AppManager.Uis.CreateProjectUi.UiControl.IsCollaborationModeBorder, 62);

                    //[同步界面]
                    AppManager.Uis.SyncUi.UiControl.EnSyncButtonGrid.Visibility = Visibility.Collapsed;
                    AppManager.Uis.SyncUi.UiControl.CnSyncButtonGrid.Visibility = Visibility.Visible;
                    AppManager.Uis.SyncUi.UiControl.SyncLogTitleTextBlock.Text = "同步日志";
                    AppManager.Uis.SyncUi.UiControl.NumberOfSyncTitleTextBlock.Text = "同步次数 :  ";
                    AppManager.Uis.SyncUi.UiControl.LastSyncTimeTitleTextBlock.Text = "最后一次同步 :  ";
                    break;

                //如果是英文
                case LanguageType.English:
                    //[创建项目界面]
                    AppManager.Uis.CreateProjectUi.UiControl.EnAdvancedOptionsButton.Visibility = Visibility.Visible;
                    AppManager.Uis.CreateProjectUi.UiControl.CnAdvancedOptionsButton.Visibility = Visibility.Collapsed;
                    Canvas.SetLeft(AppManager.Uis.CreateProjectUi.UiControl.IsCollaborationModeCheckControl, 220);
                    Canvas.SetLeft(AppManager.Uis.CreateProjectUi.UiControl.IsCollaborationModeBorder, 64);

                    //[同步界面]
                    AppManager.Uis.SyncUi.UiControl.EnSyncButtonGrid.Visibility = Visibility.Visible;
                    AppManager.Uis.SyncUi.UiControl.CnSyncButtonGrid.Visibility = Visibility.Collapsed;
                    AppManager.Uis.SyncUi.UiControl.SyncLogTitleTextBlock.Text = "Sync logs";
                    AppManager.Uis.SyncUi.UiControl.NumberOfSyncTitleTextBlock.Text = "Number of syncs :  ";
                    AppManager.Uis.SyncUi.UiControl.LastSyncTimeTitleTextBlock.Text = "Last sync :  ";
                    break;
            }
        }
        #endregion





        #region 数据的双向绑定-更新方法

        /// <summary>
        /// 当属性改变的时候，就触发此方法
        /// </summary>
        /// <param name="propertyName">发生改变的属性的名字</param>
        private void PropertyChange(string propertyName)
        {
            if (PropertyChanged != null)//如果此事件被监听
            {
                //就发送通知
                //参数1：是哪个数据类的对象发生了改变？
                //参数2：发生改变的属性名
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 系统会自动监听此事件
        /// 如果此事件触发了，系统就会去通知相应的控件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion 数据的双向绑定-更新方法
    }
}
