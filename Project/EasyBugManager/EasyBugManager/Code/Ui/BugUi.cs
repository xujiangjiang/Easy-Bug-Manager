/* By: 絮大王（xudawang@vip.163.com）
   Time：2019年11月14日10:34:06*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace EasyBugManager
{
    /// <summary>
    /// [Bug界面]的逻辑
    /// </summary>
    public class BugUi
    {
        private bool isSubmit = false;//标识符：是否发送了？


        #region [公开属性]
        /// <summary>
        /// [Bug界面]的控件
        /// </summary>
        public BugUiControl UiControl
        {
            get { return AppManager.MainWindow.BugUiControl; }
        }
        #endregion


        #region [事件]

        #region [事件 - 按钮]

        /// <summary>
        /// 当点击[返回]按钮的时候
        /// </summary>
        public void ClickBackButton()
        {
            //关闭这个界面
            this.OpenOrClose(false);

            //打开ListUi
            AppManager.Uis.ListUi.OpenOrClose(true);
        }

        /// <summary>
        /// 当点击[进度]按钮的时候
        /// </summary>
        public void ClickProgressButton()
        {
            //打开[修改Bug界面]
            AppManager.Uis.ChangeBugUi.UiControl.BugData = AppManager.Datas.OtherData.ShowBugItemData.Data;
            AppManager.Uis.ChangeBugUi.OpenOrClose(true);
        }

        /// <summary>
        /// 当点击[优先级]按钮的时候
        /// </summary>
        public void ClickPriorityButton()
        {
            //打开[修改Bug界面]
            ClickProgressButton();
        }

        /// <summary>
        /// 当点击[Bug名字]按钮的时候
        /// </summary>
        public void ClickBugNameButton()
        {
            //打开[修改Bug界面]
            ClickProgressButton();
        }
        #endregion [事件 - 按钮]

        #region [事件 - 输入框]
        /// <summary>
        /// 当点击[提交]按钮的时候
        /// </summary>
        public void ClickSubmitButton()
        {
            /* 如果输入框的文字为空，就返回 */
            if ((this.UiControl.InputBoxText == null || this.UiControl.InputBoxText == "")
                && (this.UiControl.InputBoxImagePaths == null || this.UiControl.InputBoxImagePaths.Count <= 0))
                return;

            //修改标识符
            isSubmit = true;

            //显示[省略号]
            UiControl.IsShowSubmitButtonAnimation = true;

            //获取相关的数据
            BugData _bugData = AppManager.Datas.OtherData.ShowBugItemData.Data;
            string _content = UiControl.InputBoxText;
            ObservableCollection<string> _images = UiControl.InputBoxImagePaths;

            //创建数据
            AppManager.Systems.RecordSystem.AddRecord(_bugData, _content, _images);

            //删除所有的数据
            UiControl.InputBoxText = "";
            UiControl.InputBoxImagePaths = new ObservableCollection<string>();
            UiControl.RecordInputBoxControl.UpdateImageCurrentNumber();

            //把ListBox的滚动条设置到最下面
            UiControl.RecordListBoxScrollToEnd();

            //修改Bug的更新次数
            AppManager.Systems.BugSystem.SetUpdateNumber(_bugData);
            AppManager.Systems.BugSystem.SaveBug(AppManager.Systems.CollaborationSystem.ModeType, _bugData.Id);
        }

        /// <summary>
        /// 当点击[选择图片]按钮的时候
        /// </summary>
        public void ClickChooseImageButton()
        {
            /* 如果InputBox中的图片数，已经有了4张，那么就返回 */
            if (this.UiControl.InputBoxImagePaths.Count >= 4) return;


            /* OpenFileDialog类，用于打开文件对话框 */
            OpenFileDialog _openFileDialog = new OpenFileDialog();

            /* 设置文件过滤 */
            _openFileDialog.Filter = "图片文件|*.jpg;*.png";

            /* 设置其他 */
            _openFileDialog.Title = "选择图片";

            /* 调用OpenFileDialog.ShowDialog()方法，显示[打开文件对话框]
               这个方法有一个bool?类型的返回值
               返回值为true，代表用户选择了文件；否则就代表用户没有选择文件 */
            bool? _isChooseFile = _openFileDialog.ShowDialog();

            /* 获取用户打开的文件 的路径 */
            if (_isChooseFile == true)
            {
                //把路径赋值给ProjectSystem系统的SavePath属性
                string _filePath = _openFileDialog.FileName;

                //把这个图片添加到图片列表中
                this.UiControl.InputBoxImagePaths.Add(_filePath);

                //显示“选择图片”按钮
                UiControl.RecordInputBoxControl.ShowChooseImageButton();

                //更新"当前图片个数"
                UiControl.RecordInputBoxControl.UpdateImageCurrentNumber();
            }
        }

        /// <summary>
        /// 当点击[输入框]的[图片]按钮的时候
        /// </summary>
        /// <param name="_imagePath">点击的 图片的路径</param>
        public void ClickInputBoxImageButton(string _imagePath)
        {
            //打开[图片界面]
            AppManager.Systems.ImageSystem.ShowImagePath = _imagePath;
            AppManager.Uis.ImageUi.OpenOrClose(true);
        }

        /// <summary>
        /// 当点击[输入框]的[删除图片]按钮的时候
        /// </summary>
        /// <param name="_imagePath">点击的 图片的路径</param>
        public void ClickInputBoxDeleteImageButton(string _imagePath)
        {
            //把这个图片从图片列表中移除
            this.UiControl.InputBoxImagePaths.Remove(_imagePath);

            //更新"当前图片个数"
            UiControl.RecordInputBoxControl.UpdateImageCurrentNumber();
        }
        #endregion

        #region [事件 - 记录]

        /// <summary>
        /// 当点击[记录列表的Item]的[删除]按钮的时候
        /// </summary>
        /// <param name="_source">触发事件的RecordData对象</param>
        public void ClickRecordListItemDeleteButton(RecordData _source)
        {
            //如果用户选择了Bug
            if (_source != null)
            {
                //如果[还要提示]
                if (AppManager.Datas.AppData.IsNotAgainShowDeleteRecordTip == false)
                {
                    //显示删除Bug界面
                    AppManager.Uis.DeleteRecordTipUi.UiControl.Data = _source;
                    AppManager.Uis.DeleteRecordTipUi.UiControl.Text = StringTool.Clamp(_source.Content, 25);
                    AppManager.Uis.DeleteRecordTipUi.OpenOrClose(true);
                }

                //如果[不再提示]
                else
                {
                    //直接删除Bug
                    AppManager.Systems.RecordSystem.RemoveRecord(_source);
                }
            }
        }

        /// <summary>
        /// 当点击[记录列表的Item]的[图片]按钮的时候
        /// </summary>
        /// <param name="_imagePath">点击的 图片的路径</param>
        /// <param name="_source">触发事件的RecordData对象</param>
        public void ClickRecordListItemImageButton(string _imagePath, RecordData _source)
        {
            //打开[图片界面]
            AppManager.Systems.ImageSystem.ShowImagePath = _imagePath;
            AppManager.Uis.ImageUi.OpenOrClose(true);
        }
        #endregion

        #region [事件 - 值改变]
        /// <summary>
        /// 当[是否显示Bug的回复？]改变时
        /// </summary>
        /// <param name="_oldValue">旧的值</param>
        /// <param name="_newValue">新的值</param>
        public void IsShowBugReplyChange(bool _oldValue, bool _newValue)
        {
            if (AppManager.Datas.OtherData.ShowBugItemData != null)
            {
                //获取当前Bug
                BugData _bugData = AppManager.Datas.OtherData.ShowBugItemData.Data;

                //赋值Bug里的ShowRecords属性
                AppManager.Systems.RecordSystem.SetShowRecords(_bugData, AppManager.Systems.RecordSystem.IsShowBugReply);

                //把ListBox的滚动条设置到最下面
                UiControl.RecordListBoxScrollToEnd();
            }
        }

        /// <summary>
        /// 当[是否显示提交按钮的动画？]改变时
        /// </summary>
        /// <param name="_oldValue">旧的值</param>
        /// <param name="_newValue">新的值</param>
        public void IsShowSubmitButtonAnimationChange(bool _oldValue, bool _newValue)
        {
            //如果[省略号]关闭了，并且刚刚发送了一条信息
            if (_newValue == false && isSubmit == true)
            {
                //修改标识符
                isSubmit = false;

                //获取到所有的记录
                ObservableCollection<RecordItemData> _showRecordItemDatas =
                    AppManager.Systems.RecordSystem.ShowRecordItemDatas;

                //回复一条记录：在回复Bug时，加一个判断条件：如果Bug的记录列表中 的最后一项是Bear说的话，那么才回复，否则不回复。
                if (_showRecordItemDatas != null && _showRecordItemDatas.Count > 1
                                                 && _showRecordItemDatas[_showRecordItemDatas.Count - 2].Type ==
                                                 RecordType.Bear)
                {
                    AppManager.Systems.RecordSystem.SetShowRecords(AppManager.Datas.OtherData.ShowBugItemData.Data,
                        AppManager.Datas.OtherData.IsShowBugReply);

                    //把ListBox的滚动条设置到最下面
                    UiControl.RecordListBoxScrollToEnd();
                }
            }

        }
        #endregion

        #endregion


        #region [公开方法]

        #region [公开方法 - 打开or关闭]
        /// <summary>
        /// 打开或者关闭 界面
        /// </summary>
        /// <param name="_isOpen">是否打开？</param>
        public void OpenOrClose(bool _isOpen)
        {
            /* 打开/关闭界面 */
            switch (_isOpen)
            {
                //如果是打开
                case true:
                    this.UiControl.Visibility = Visibility.Visible;//打开界面
                    AppManager.Uis.OpenOrCloseBackground(true);//打开背景
                    break;

                //如果是关闭
                case false:
                    this.UiControl.Visibility = Visibility.Collapsed;//关闭界面
                    break;
            }

            /* 数据处理 */
            switch (_isOpen)
            {
                //如果是关闭
                case false:
                    this.UiControl.InputBoxImagePaths = new ObservableCollection<string>();
                    this.UiControl.InputBoxText = "";
                    AppManager.Datas.OtherData.ShowBugItemData = null;
                    AppManager.Datas.OtherData.ShowRecordItemDatas = new ObservableCollection<RecordItemData>();
                    UiControl.RecordInputBoxControl.HideChooseImageButton();//隐藏“选择图片”按钮
                    UiControl.RecordInputBoxControl.UpdateImageCurrentNumber();//更新"当前图片个数"
                    break;

                //如果是打开
                case true:
                    //赋值文字
                    UiControl.UpdateNumberBehindText = AppManager.Systems.LanguageSystem.UpdateNumberBehindText;
                    UiControl.UpdateNumberFrontText = AppManager.Systems.LanguageSystem.UpdateNumberFrontText;
                    break;
            }
        }
        #endregion [公开方法 - 打开or关闭]

        #endregion
    }
}
