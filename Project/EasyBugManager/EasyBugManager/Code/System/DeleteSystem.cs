/* By: 絮大王（sukiup@163.com）
   Time：2019年12月8日14:28:49*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 删除的系统
    /// </summary>
    public class DeleteSystem
    {
        /* 这个类用于在每次启动软件时，删除不需要的文件。
           (因为如果直接删除的话，会报错。所以先标记IsDelete为true，然后在下次软件启动的时候，再由[DeleteSystem系统]真正的把图片删除)*/

        /* 方法： DeleteFile(清除（删除）所有要删除的文件)*/



        #region [公开方法]
        /// <summary>
        /// 清除（删除）所有要删除的文件
        /// </summary>
        public void DeleteFile()
        {
            try
            {
                //清除（删除）所有要删除的图片文件
                DeleteImageFile();

                //清除（删除）所有要删除的记录文件
                DeleteRecordFile(AppManager.Systems.ProjectSystem.ProjectData.ModeType);

                //清除（删除）所有要删除的Bug文件
                DeleteBugFile(AppManager.Systems.ProjectSystem.ProjectData.ModeType);
            }
            catch (Exception e)
            {
                //输出错误
                AppManager.Uis.ErrorUi.UiControl.TipContent = e.ToString();
                AppManager.Uis.ErrorUi.OpenOrClose(true);
            }
        }
        #endregion


        #region [私有方法]
        /// <summary>
        /// 清除（删除）所有要删除的图片文件
        /// </summary>
        private void DeleteImageFile()
        {
            //取到图片文件夹的信息
            DirectoryInfo _imageDirectoryInfo = new DirectoryInfo(AppManager.Systems.ImageSystem.ImageFolderPath);

            //取到所有的图片文件
            FileInfo[] _imageFileInfos = _imageDirectoryInfo.GetFiles();

            //容器：所有要删除的图片文件
            List<FileInfo> _deleteImageFileInfos = new List<FileInfo>();

            //遍历所有的图片文件
            for (int i = 0; i < _imageFileInfos.Length; i++)
            {

                try
                {
                    //取到图片文件的名字
                    string _fileName = Path.GetFileNameWithoutExtension(_imageFileInfos[i].FullName);

                    //把[图片名字]转换为[RecordId]
                    long _recordId = AppManager.Systems.ImageSystem.ImageFileNameToRecordId(_fileName);

                    //如果转换成功
                    if (_recordId > -1)
                    {
                        //获取这个记录文件
                        RecordData _recordData = AppManager.Systems.RecordSystem.GetRecordData(_recordId);

                        //如果这个记录被标记为删除
                        if (_recordData != null && _recordData.IsDelete == true)
                        {
                            //标记这个图片为要删除的图片
                            _deleteImageFileInfos.Add(_imageFileInfos[i]);
                        }
                    }
                }
                catch (Exception e)
                {
                }

            }

            //删除所有已经删除的记录文件
            for (int i = 0; i < _deleteImageFileInfos.Count; i++)
            {
                try
                {
                    File.Delete(_deleteImageFileInfos[i].FullName);
                }
                catch (Exception e)
                {
                }
            }

        }


        /// <summary>
        /// 清除（删除）所有要删除的记录文件
        /// </summary>
        /// <param name="_modeType">项目模式的类型</param>
        private void DeleteRecordFile(ModeType _modeType)
        {
            //获取到所有的记录
            ObservableCollection<RecordData> _recordDatas = AppManager.Systems.RecordSystem.RecordDatas;

            //要删除的记录
            List<RecordData> _deleteRecordDatas = new List<RecordData>();

            //遍历所有的记录
            for (int i = 0; i < _recordDatas.Count; i++)
            {
                try
                {
                    //如果这个记录要删除
                    if (_recordDatas[i].IsDelete == true)
                    {
                        if (_modeType == ModeType.Collaboration)
                        {
                            //获取记录的文件路径
                            string _recordFilePath = AppManager.Systems.RecordSystem.RecordFolderPath
                                                     + "/Record - " + _recordDatas[i].Id + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                            //如果存在，就删除
                            File.Delete(_recordFilePath);
                        }

                        //把记录标记为要删除的记录
                        _deleteRecordDatas.Add(_recordDatas[i]);
                    }

                }
                catch (Exception e)
                {
                }

            }

            //把所有要删除的记录，从列表中删除
            for (int i = 0; i < _deleteRecordDatas.Count; i++)
            {
                _recordDatas.Remove(_deleteRecordDatas[i]);
            }

        }


        /// <summary>
        /// 清除（删除）所有要删除的Bug文件
        /// </summary>
        /// <param name="_modeType">项目模式的类型</param>
        private void DeleteBugFile(ModeType _modeType)
        {
            //获取到所有的Bug
            ObservableCollection<BugData> _bugDatas = AppManager.Systems.BugSystem.BugDatas;

            //要删除的Bug
            List<BugData> _deleteBugDatas = new List<BugData>();

            //遍历所有的Bug
            for (int i = 0; i < _bugDatas.Count; i++)
            {
                try
                {
                    //如果这个Bug要删除
                    if (_bugDatas[i].IsDelete == true)
                    {
                        if (_modeType == ModeType.Collaboration)
                        {
                            //获取Bug的文件路径
                            string _bugFilePath = AppManager.Systems.BugSystem.BugFolderPath
                                                  + "/Bug - " + _bugDatas[i].Id + AppManager.Systems.ProjectSystem.OtherFileSuffix;

                            //如果存在，就删除
                            File.Delete(_bugFilePath);
                        }

                        //把Bug标记为要删除的Bug
                        _deleteBugDatas.Add(_bugDatas[i]);
                    }

                }
                catch (Exception e)
                {
                }

            }

            //把所有要删除的Bug，从列表中删除
            for (int i = 0; i < _deleteBugDatas.Count; i++)
            {
                _bugDatas.Remove(_deleteBugDatas[i]);
            }
        }

        #endregion
    }
}
