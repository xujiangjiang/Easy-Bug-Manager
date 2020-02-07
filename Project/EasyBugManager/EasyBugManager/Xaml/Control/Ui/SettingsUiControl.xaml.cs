using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyBugManager
{
    /// <summary>
    /// SettingsUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsUiControl : UserControl
    {
        /* 属性: Language(语言)
                 Sound(声音)
                 Theme(皮肤)
                 Transparent(透明度)
                 StaffPanelText(工作人员面板的文字)


           事件: LanguageChange(当语言改变时)
                 SoundChange(当声音改变时)
                 ThemeChange(当皮肤改变时)（0代表白；1代表黑；2代表猫咪）
                 TransparentChange(当透明度改变时)
                 
                 ClickCloseButton(点击Github按钮)
                 ClickGithubButton(点击Github按钮)
                 ClickExportButton(点击导出Excel按钮)
                 ClickMoreButton(点击More按钮)
                 ClickEpplusButton(点击Epplus按钮)
                 ClickLitjsonButton(点击Litjson按钮)
                 ClickUserManualButton(点击[用户手册]按钮)
                 ClickUpdateLogButton(点击[更新日志]按钮)
                 ClickToolButton(点击[工具]按钮)
                 ClickEmailButton(点击[电子邮件]按钮)*/



        public SettingsUiControl()
        {
            InitializeComponent();
        }



        #region 依赖项属性：Language
        /// <summary>
        /// 依赖项属性：语言
        /// </summary>
        public static DependencyProperty LanguageProperty;

        /// <summary>
        /// 公开属性：语言
        /// </summary>
        public LanguageType Language
        {
            get { return (LanguageType)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当LanguageProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnLanguageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //如果值发生改变了
            if ((LanguageType)e.OldValue != (LanguageType)e.NewValue) 
            {
                //获取控件
                SettingsUiControl _settingsUiControl = sender as SettingsUiControl;

                //判断
                switch ((LanguageType)e.NewValue) 
                {
                    //如果值为[英文]
                    case LanguageType.English:
                        //打开英语面板，关闭中文面板
                        _settingsUiControl.EnStackPanel.Visibility = Visibility.Visible;
                        _settingsUiControl.CnStackPanel.Visibility = Visibility.Collapsed;
                        //修改CheckGroup
                        _settingsUiControl.EnLanguageCheckGroup.CheckedIndex = 0;
                        _settingsUiControl.CnLanguageCheckGroup.CheckedIndex = 0;
                        break;

                    //如果值为[中文]
                    case LanguageType.Chinese:
                        //打开中文面板，关闭英文面板
                        _settingsUiControl.EnStackPanel.Visibility = Visibility.Collapsed;
                        _settingsUiControl.CnStackPanel.Visibility = Visibility.Visible;
                        //修改CheckGroup
                        _settingsUiControl.EnLanguageCheckGroup.CheckedIndex = 1;
                        _settingsUiControl.CnLanguageCheckGroup.CheckedIndex = 1;
                        break;
                }

                //触发事件
                _settingsUiControl.OnLanguageChange((LanguageType)e.NewValue);
            }
        }
        #endregion

        #region 依赖项属性：Sound
        /// <summary>
        /// 依赖项属性：声音
        /// </summary>
        public static DependencyProperty SoundProperty;

        /// <summary>
        /// 公开属性：声音
        /// </summary>
        public bool Sound
        {
            get { return (bool)GetValue(SoundProperty); }
            set { SetValue(SoundProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当SoundProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnSoundChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //如果值发生改变了
            if ((bool)e.OldValue != (bool)e.NewValue)
            {
                //获取控件
                SettingsUiControl _settingsUiControl = sender as SettingsUiControl;

                //判断
                switch ((bool)e.NewValue)
                {
                    //如果值为[开]
                    case true:
                        //修改CheckGroup
                        _settingsUiControl.EnSoundCheckGroup.CheckedIndex = 0;
                        _settingsUiControl.CnSoundCheckGroup.CheckedIndex = 0;
                        break;

                    //如果值为[关]
                    case false:
                        //修改CheckGroup
                        _settingsUiControl.EnSoundCheckGroup.CheckedIndex = 1;
                        _settingsUiControl.CnSoundCheckGroup.CheckedIndex = 1;
                        break;
                }

                //触发事件
                _settingsUiControl.OnSoundChange((bool)e.NewValue);
            }
        }
        #endregion

        #region 依赖项属性：Theme
        /// <summary>
        /// 依赖项属性：皮肤
        /// </summary>
        public static DependencyProperty ThemeProperty;

        /// <summary>
        /// 公开属性：皮肤
        /// </summary>
        public ThemeType Theme
        {
            get { return (ThemeType)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ThemeProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnThemeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //如果值发生改变了
            if ((ThemeType)e.OldValue != (ThemeType)e.NewValue)
            {
                //获取控件
                SettingsUiControl _settingsUiControl = sender as SettingsUiControl;

                //判断
                switch ((ThemeType)e.NewValue)
                {
                    //如果值为[白]
                    case ThemeType.White:
                        //修改CheckGroup
                        _settingsUiControl.EnThemeCheckGroup.CheckedIndex = 0;
                        _settingsUiControl.CnThemeCheckGroup.CheckedIndex = 0;
                        _settingsUiControl.EnThemeBearCheckGroup.CheckedIndex = 0;
                        _settingsUiControl.CnThemeBearCheckGroup.CheckedIndex = 0;
                        _settingsUiControl.EnThemeCatCheckGroup.CheckedIndex = -1;
                        _settingsUiControl.CnThemeCatCheckGroup.CheckedIndex = -1;
                        break;

                    //如果值为[黑]
                    case ThemeType.Dark:
                        //修改CheckGroup
                        _settingsUiControl.EnThemeCheckGroup.CheckedIndex = 1;
                        _settingsUiControl.CnThemeCheckGroup.CheckedIndex = 1;
                        _settingsUiControl.EnThemeBearCheckGroup.CheckedIndex = 1;
                        _settingsUiControl.CnThemeBearCheckGroup.CheckedIndex = 1;
                        _settingsUiControl.EnThemeCatCheckGroup.CheckedIndex = -1;
                        _settingsUiControl.CnThemeCatCheckGroup.CheckedIndex = -1;
                        break;

                    //如果值为[猫(白色)]
                    case ThemeType.Cat_White:
                        //修改CheckGroup
                        _settingsUiControl.EnThemeCheckGroup.CheckedIndex = -1;
                        _settingsUiControl.CnThemeCheckGroup.CheckedIndex = -1;
                        _settingsUiControl.EnThemeBearCheckGroup.CheckedIndex = -1;
                        _settingsUiControl.CnThemeBearCheckGroup.CheckedIndex = -1;
                        _settingsUiControl.EnThemeCatCheckGroup.CheckedIndex = 0;
                        _settingsUiControl.CnThemeCatCheckGroup.CheckedIndex = 0;
                        break;

                    //如果值为[猫(黑色)]
                    case ThemeType.Cat_Dark:
                        //修改CheckGroup
                        _settingsUiControl.EnThemeCheckGroup.CheckedIndex = -1;
                        _settingsUiControl.CnThemeCheckGroup.CheckedIndex = -1;
                        _settingsUiControl.EnThemeBearCheckGroup.CheckedIndex = -1;
                        _settingsUiControl.CnThemeBearCheckGroup.CheckedIndex = -1;
                        _settingsUiControl.EnThemeCatCheckGroup.CheckedIndex = 1;
                        _settingsUiControl.CnThemeCatCheckGroup.CheckedIndex = 1;
                        break;
                }

                //触发事件
                _settingsUiControl.OnThemeChange((ThemeType)e.NewValue);
            }
        }
        #endregion

        #region 依赖项属性：Transparent
        /// <summary>
        /// 依赖项属性：透明度
        /// </summary>
        public static DependencyProperty TransparentProperty;

        /// <summary>
        /// 公开属性：透明度
        /// </summary>
        public int Transparent
        {
            get { return (int)GetValue(TransparentProperty); }
            set { SetValue(TransparentProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当TransparentProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnTransparentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //如果值发生改变了
            if ((int)e.OldValue != (int)e.NewValue)
            {
                //获取控件
                SettingsUiControl _settingsUiControl = sender as SettingsUiControl;

                //触发事件
                _settingsUiControl.OnTransparentChange((int)e.NewValue);
            }
        }
        #endregion

        #region 依赖项属性：StaffPanelText
        /// <summary>
        /// 依赖项属性：工作人员面板的文字
        /// </summary>
        public static DependencyProperty StaffPanelTextProperty;

        /// <summary>
        /// 公开属性：工作人员面板的文字
        /// </summary>
        public string StaffPanelText
        {
            get { return (string)GetValue(StaffPanelTextProperty); }
            set { SetValue(StaffPanelTextProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当StaffPanelTextProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnStaffPanelTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion





        #region 路由事件：ClickCloseButton

        /// <summary>
        /// 路由事件：ClickCloseButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickCloseButtonEvent;


        /// <summary>
        /// 路由事件的属性：Click
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickCloseButton
        {
            //添加一条事件
            add { AddHandler(ClickCloseButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickCloseButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickCloseButton 路由事件
        /// </summary>
        private void OnClickCloseButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.ClickCloseButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickGithubButton

        /// <summary>
        /// 路由事件：ClickGithubButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickGithubButtonEvent;


        /// <summary>
        /// 路由事件的属性：Click
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickGithubButton
        {
            //添加一条事件
            add { AddHandler(ClickGithubButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickGithubButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickGithubButton 路由事件
        /// </summary>
        private void OnClickGithubButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.ClickGithubButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickExportButton

        /// <summary>
        /// 路由事件：ClickExportButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickExportButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickExportButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickExportButton
        {
            //添加一条事件
            add { AddHandler(ClickExportButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickExportButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickExportButton 路由事件
        /// </summary>
        private void OnClickExportButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.ClickExportButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickMoreButton

        /// <summary>
        /// 路由事件：ClickMoreButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickMoreButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickMoreButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickMoreButton
        {
            //添加一条事件
            add { AddHandler(ClickMoreButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickMoreButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickMoreButton 路由事件
        /// </summary>
        private void OnClickMoreButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.ClickMoreButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickEpplusButton

        /// <summary>
        /// 路由事件：ClickEpplusButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickEpplusButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickEpplusButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickEpplusButton
        {
            //添加一条事件
            add { AddHandler(ClickEpplusButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickEpplusButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickEpplusButton 路由事件
        /// </summary>
        private void OnClickEpplusButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.ClickEpplusButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickLitjsonButton

        /// <summary>
        /// 路由事件：ClickLitjsonButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickLitjsonButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickLitjsonButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickLitjsonButton
        {
            //添加一条事件
            add { AddHandler(ClickLitjsonButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickLitjsonButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickLitjsonButton 路由事件
        /// </summary>
        private void OnClickLitjsonButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.ClickLitjsonButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickUserManualButton

        /// <summary>
        /// 路由事件：ClickUserManualButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickUserManualButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickUserManualButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickUserManualButton
        {
            //添加一条事件
            add { AddHandler(ClickUserManualButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickUserManualButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickUserManualButton 路由事件
        /// </summary>
        private void OnClickUserManualButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.ClickUserManualButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickUpdateLogButton

        /// <summary>
        /// 路由事件：ClickUpdateLogButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickUpdateLogButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickUpdateLogButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickUpdateLogButton
        {
            //添加一条事件
            add { AddHandler(ClickUpdateLogButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickUpdateLogButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickUpdateLogButton 路由事件
        /// </summary>
        private void OnClickUpdateLogButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.ClickUpdateLogButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickToolButton

        /// <summary>
        /// 路由事件：ClickToolButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickToolButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickToolButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickToolButton
        {
            //添加一条事件
            add { AddHandler(ClickToolButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickToolButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickToolButton 路由事件
        /// </summary>
        private void OnClickToolButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.ClickToolButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ClickEmailButton

        /// <summary>
        /// 路由事件：ClickEmailButtonEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickEmailButtonEvent;


        /// <summary>
        /// 路由事件的属性：ClickEmailButton
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> ClickEmailButton
        {
            //添加一条事件
            add { AddHandler(ClickEmailButtonEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickEmailButtonEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickEmailButton 路由事件
        /// </summary>
        private void OnClickEmailButton()
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, true);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.ClickEmailButtonEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion




        #region 路由事件：LanguageChange

        /// <summary>
        /// 路由事件：LanguageChangeEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent LanguageChangeEvent;


        /// <summary>
        /// 路由事件的属性：LanguageChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<LanguageType> LanguageChange
        {
            //添加一条事件
            add { AddHandler(LanguageChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(LanguageChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 LanguageChange 路由事件
        /// </summary>
        /// <param name="_newValue">新的Language属性的值</param>
        private void OnLanguageChange(LanguageType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<LanguageType> args = new RoutedPropertyChangedEventArgs<LanguageType>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.LanguageChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：SoundChange

        /// <summary>
        /// 路由事件：SoundChangeEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent SoundChangeEvent;


        /// <summary>
        /// 路由事件的属性：SoundChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> SoundChange
        {
            //添加一条事件
            add { AddHandler(SoundChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(SoundChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 SoundChange 路由事件
        /// </summary>
        /// <param name="_newValue">新的Sound属性的值</param>
        private void OnSoundChange(bool _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.SoundChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：ThemeChange

        /// <summary>
        /// 路由事件：ThemeChangeEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ThemeChangeEvent;


        /// <summary>
        /// 路由事件的属性：ThemeChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<ThemeType> ThemeChange
        {
            //添加一条事件
            add { AddHandler(ThemeChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ThemeChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ThemeChange 路由事件
        /// </summary>
        /// <param name="_newValue">新的Theme属性的值</param>
        private void OnThemeChange(ThemeType _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<ThemeType> args = new RoutedPropertyChangedEventArgs<ThemeType>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.ThemeChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion

        #region 路由事件：TransparentChange

        /// <summary>
        /// 路由事件：TransparentChangeEvent
        /// （当点击按钮时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent TransparentChangeEvent;


        /// <summary>
        /// 路由事件的属性：TransparentChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> TransparentChange
        {
            //添加一条事件
            add { AddHandler(TransparentChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(TransparentChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 TransparentChange 路由事件
        /// </summary>
        /// <param name="_newValue">新的Transparent属性的值</param>
        private void OnTransparentChange(int _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<int> args = new RoutedPropertyChangedEventArgs<int>(_newValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = SettingsUiControl.TransparentChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }

        #endregion





        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static SettingsUiControl()
        {
            /*注册依赖项属性*/
            //注册IconProperty
            LanguageProperty = DependencyProperty.Register(
                "Language", //属性的名字
                typeof(LanguageType), //属性的类型
                typeof(SettingsUiControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                    //初始值
                    (LanguageType)LanguageType.None,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnLanguageChanged))
            );

            //注册SoundProperty
            SoundProperty = DependencyProperty.Register(
                "Sound", typeof(bool), typeof(SettingsUiControl),
                new FrameworkPropertyMetadata((bool)true, new PropertyChangedCallback(OnSoundChanged)));

            //注册ThemeProperty
            ThemeProperty = DependencyProperty.Register(
                "Theme", typeof(ThemeType), typeof(SettingsUiControl),
                new FrameworkPropertyMetadata((ThemeType)ThemeType.None, new PropertyChangedCallback(OnThemeChanged)));

            //注册TransparentProperty
            TransparentProperty = DependencyProperty.Register(
                "Transparent", typeof(int), typeof(SettingsUiControl),
                new FrameworkPropertyMetadata((int)100, new PropertyChangedCallback(OnTransparentChanged)));

            //注册StaffPanelTextProperty
            StaffPanelTextProperty = DependencyProperty.Register(
                "StaffPanelText", typeof(string), typeof(SettingsUiControl),
                new FrameworkPropertyMetadata((string)"", new PropertyChangedCallback(OnStaffPanelTextChanged)));





            /*注册路由事件*/
            //注册ClickCloseButtonEvent
            ClickCloseButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickCloseButton", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<bool>), //路由事件要处理的数据类型
                typeof(SettingsUiControl) //这个路由事件属于哪个控件？
            );

            //注册ClickGithubButtonEvent
            ClickGithubButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickGithubButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SettingsUiControl));

            //注册ClickExportButtonEvent
            ClickExportButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickExportButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SettingsUiControl));

            //注册ClickMoreButtonEvent
            ClickMoreButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickMoreButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SettingsUiControl));

            //注册ClickEpplusButtonEvent
            ClickEpplusButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickEpplusButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SettingsUiControl));

            //注册ClickLitjsonButtonEvent
            ClickLitjsonButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickLitjsonButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SettingsUiControl));

            //注册ClickUserManualButtonEvent
            ClickUserManualButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickUserManualButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SettingsUiControl));

            //注册ClickUpdateLogButtonEvent
            ClickUpdateLogButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickUpdateLogButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SettingsUiControl));

            //注册ClickToolButtonEvent
            ClickToolButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickToolButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SettingsUiControl));

            //注册ClickEmailButtonEvent
            ClickEmailButtonEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickEmailButton", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SettingsUiControl));




            //注册LanguageChangeEvent
            LanguageChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "LanguageChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<LanguageType>), typeof(SettingsUiControl));

            //注册SoundChangeEvent
            SoundChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "SoundChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SettingsUiControl));

            //注册ThemeChangeEvent
            ThemeChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ThemeChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<ThemeType>), typeof(SettingsUiControl));

            //注册TransparentChangeEvent
            TransparentChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "TransparentChange", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<int>), typeof(SettingsUiControl));

        }
        #endregion




        #region [事件 - 透明度]
        //当鼠标进入[透明度(英文)]面板时
        private void EnTransparentStackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            //显示透明度的文本
            this.EnTransparentBorder.Visibility = Visibility.Visible;
        }

        //当鼠标离开[透明度(英文)]面板时
        private void EnTransparentStackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            //显示透明度的文本
            this.EnTransparentBorder.Visibility = Visibility.Collapsed;
        }




        //当鼠标进入[透明度(中文)]面板时
        private void CnTransparentStackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            //显示透明度的文本
            this.CnTransparentBorder.Visibility = Visibility.Visible;
        }

        //当鼠标离开[透明度(中文)]面板时
        private void CnTransparentStackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            //显示透明度的文本
            this.CnTransparentBorder.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region [事件 - CheckGroup]
        //当[语言]的选中项改变时
        private void LanguageCheckGroup_CheckedChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //获取控件
            ImageCheckGroupControl _imageCheckGroupControl = sender as ImageCheckGroupControl;

            //判断
            switch (_imageCheckGroupControl.CheckedIndex) 
            {
                //如果为0
                case 0:
                    Language = LanguageType.English;
                    break;

                //如果为1
                case 1:
                    Language = LanguageType.Chinese;
                    break;
            }
        }

        //当[声音]的选中项改变时
        private void SoundCheckGroup_CheckedChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //获取控件
            ImageCheckGroupControl _imageCheckGroupControl = sender as ImageCheckGroupControl;

            //判断
            switch (_imageCheckGroupControl.CheckedIndex)
            {
                //如果为0
                case 0:
                    Sound = true;
                    break;

                //如果为1
                case 1:
                    Sound = false;
                    break;
            }
        }

        //当[皮肤]的选中项改变时
        private void ThemeCheckGroup_CheckedChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //获取控件
            ImageCheckGroupControl _imageCheckGroupControl = sender as ImageCheckGroupControl;

            //判断
            switch (_imageCheckGroupControl.CheckedIndex)
            {
                //如果为0
                case 0:
                    Theme = ThemeType.White;
                    break;

                //如果为1
                case 1:
                    Theme = ThemeType.Dark;
                    break;
            }
        }

        //当[皮肤DLC]的选中项改变时
        private void ThemeDlcCheckGroup_CheckedChange(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //获取控件
            ImageCheckGroupControl _imageCheckGroupControl = sender as ImageCheckGroupControl;

            //判断
            switch (_imageCheckGroupControl.CheckedIndex)
            {
                //如果为0
                case 0:
                    Theme = ThemeType.Cat_White;
                    break;

                //如果为1
                case 1:
                    Theme = ThemeType.Cat_Dark;
                    break;
            }
        }
        #endregion

        #region [事件 - 按钮]
        //当点击[Close]按钮时
        private void CloseButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickCloseButton();//触发事件
        }

        //当点击[Github]按钮时
        private void GithubButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickGithubButton();//触发事件
        }

        //当点击[Export]按钮时
        private void ExportButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickExportButton();//触发事件
        }

        //当点击[More]按钮时
        private void MoreButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //显示StaffText面板
            this.StaffTextGrid.Visibility = Visibility.Visible;

            //触发事件
            this.OnClickMoreButton();
        }

        //当点击[Epplus]按钮时
        private void EpplusButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //显示StaffText面板
            this.StaffTextGrid.Visibility = Visibility.Visible;

            //触发事件
            this.OnClickEpplusButton();
        }

        //当点击[Litjson]按钮时
        private void LitjsonButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //显示StaffText面板
            this.StaffTextGrid.Visibility = Visibility.Visible;

            //触发事件
            this.OnClickLitjsonButton();
        }

        //当点击[用户手册]按钮时
        private void UserManualButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //隐藏StaffText面板
            this.StaffTextGrid.Visibility = Visibility.Collapsed;

            //触发事件
            this.OnClickUserManualButton();
        }

        //当点击[更新日志]按钮时
        private void UpdateLogButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //隐藏StaffText面板
            this.StaffTextGrid.Visibility = Visibility.Collapsed;

            //触发事件
            this.OnClickUpdateLogButton();
        }

        //当点击[工具]按钮时
        private void ToolButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //隐藏StaffText面板
            this.StaffTextGrid.Visibility = Visibility.Collapsed;

            //触发事件
            this.OnClickToolButton();
        }

        //当点击[电子邮件]按钮时
        private void EmailButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //显示StaffText面板
            this.StaffTextGrid.Visibility = Visibility.Visible;

            //触发事件
            this.OnClickEmailButton();
        }



        //当点击[关闭Staff文本]按钮时
        private void ClickCloseStaffTextButtonControl_Click(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            //关闭StaffText面板
            this.StaffTextGrid.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region [事件 - Staff面板]
        //当鼠标进入[Staff]按钮或者按钮时
        private void StaffPanelControl_MouseEnter(object sender, MouseEventArgs e)
        {
            //打开Staff面板
            this.StaffGrid.Visibility = Visibility.Visible;
            AnimationTool.PlayGridOpacityAnimation(this.StaffGrid,null,1,0.25f);
        }

        //当鼠标离开[Staff]按钮或者按钮时
        private void StaffPanelControl_MouseLeave(object sender, MouseEventArgs e)
        {
            //关闭Staff面板
            this.StaffGrid.Visibility = Visibility.Collapsed;
            AnimationTool.PlayGridOpacityAnimation(this.StaffGrid, null, 0, 0.25f);
        }
        #endregion

        #region [事件 - 控件关闭/打开]
        //当控件[打开或者关闭]时
        private void SettingsUiUserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //关闭Staff面板的Text面板
            this.StaffTextGrid.Visibility = Visibility.Collapsed;
        }
        #endregion

    }
}
