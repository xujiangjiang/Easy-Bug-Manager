/* By: 絮大王（xudawang@vip.163.com）
   Time：2019年11月24日05:29:47*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyBugManager
{
    /// <summary>
    /// 图片的系统（用于保存、读取图片）
    /// </summary>
    public class ImageSystem
    {
        /* 属性：ImageFolderPath([图片文件夹]的路径)（ProjectFolderPath/Image/）*/

        /* 方法：SaveImage(保存图片)
                
                 AddImage(添加图片)
                 DeleteImage(删除图片)
                 
                 GetImageFilePath(根据ImageId，来获取图片文件的路径)*/



        #region [公开属性 - 路径]
        /// <summary>
        /// [图片文件]所在的 文件夹的路径 ([图片文件夹]的路径)
        /// </summary>
        public string ImageFolderPath
        {
            get { return AppManager.Systems.ProjectSystem.ImageFolderPath; }
        }
        #endregion

        #region [公开属性 - 数据]

        /// <summary>
        /// 显示的图片(路径)
        /// </summary>
        public string ShowImagePath
        {
            get { return AppManager.Uis.ImageUi.UiControl.ImagePath; }
            set { AppManager.Uis.ImageUi.UiControl.ImagePath = value; }
        }
        #endregion





        #region [公开方法 - 修改]
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="_recordId">记录的编号</param>
        /// <param name="_imagePath">图片的完整路径（包括文件夹+文件名+文件后缀）</param>
        public void AddImageFile(long _recordId, string _imagePath)
        {
            //通过RecordId，获取到Bug对象
            RecordData _recordData = AppManager.Systems.RecordSystem.GetRecordData(_recordId);

            //保存图片，并获取到图片的新名字
            string _newImageName = this.CopyImageFile(_imagePath, _recordData);

            //然后，把新的图片名字，添加到记录里
            _recordData.Images.Add(_newImageName);

            //更新Ui
            _recordData.BearRecordItemData.ImagePaths = AppManager.Systems.ImageSystem.GetImagePaths(_recordData.Images);
        }
        #endregion

        #region [公开方法 - 路径]
        /// <summary>
        /// 通过Image的文件名+文件后缀，获取Image的路径(文件夹+文件名+文件后缀)
        /// (把ImageId.jpg设置为XXXXX/Image/ImageId.jpg)
        /// </summary>
        /// <param name="_images">Image的文件名+文件后缀</param>
        /// <returns>Image的路径(文件夹+文件名+文件后缀)</returns>
        public ObservableCollection<string> GetImagePaths(ObservableCollection<string> _images)
        {
            //容器：要显示的Image（Image的绝对路径）
            ObservableCollection<string> _imagePaths = new ObservableCollection<string>();

            if (_images != null)
            {
                //遍历所有的Image
                for (int i = 0; i < _images.Count; i++)
                {
                    string _imagePath = GetImagePath(_images[i]);//图片的绝对路径 (绝对路径：XXXX/项目名/Image/ImageId.png)
                    _imagePaths.Add(_imagePath);//添加到_showImages集合中
                }
            }

            //返回值
            return _imagePaths;
        }

        /// <summary>
        /// 通过Image的文件名+文件后缀，获取Image的路径(文件夹+文件名+文件后缀)
        /// (把ImageId.jpg设置为XXXXX/Image/ImageId.jpg)
        /// </summary>
        /// <param name="_image">Image的文件名+文件后缀</param>
        /// <returns>Image的路径(文件夹+文件名+文件后缀)</returns>
        public string GetImagePath(string _image)
        {
            if (_image != null)
            {
                string _imagePath = AppManager.Systems.ImageSystem.ImageFolderPath + "/" + _image;//图片的绝对路径 (绝对路径：XXXX/项目名/Image/ImageId.png)
                return _imagePath;
            }

            return "";
        }
        #endregion

        #region [公开方法 - 转换]
        /// <summary>
        /// 把[Image文件名字]转换为[RecordId]
        /// </summary>
        /// <param name="_imageFileName">Image文件的名字（只有文件名字，没有后缀）</param>
        /// <returns>Image所在的Record的编号 (-1代表没有转换成功)</returns>
        public long ImageFileNameToRecordId(string _imageFileName)
        {
            //如果名字里含有(字符，就去除掉名字中" (1)"的字符
            if (_imageFileName.Contains(" ("))
            {
                for (int j = 0; j < 10; j++)
                {
                    _imageFileName = _imageFileName.Replace(" (" + j + ")", "");
                }
            }

            //把[图片名字]转换为[RecordId]
            long _recordId = -1;
            bool _isParseOk = long.TryParse(_imageFileName, out _recordId);

            //如果转换成功
            if (_isParseOk == true)
            {
                return _recordId;
            }
            else
            {
                return -1;
            }
        }

        #endregion

        #region [私有方法 - 保存]

        /// <summary>
        /// 复制图片
        /// (复制1张图片到Image文件夹，并把图片的名字更改为ImageId.png)
        /// </summary>
        /// <param name="_imagePath">图片的完整路径（包括文件夹+文件名+文件后缀）</param>
        /// <param name="_recordData">这个图片是属于哪个记录的？</param>
        /// <returns>返回：图片的新名字(ImageId.png)</returns>
        private string CopyImageFile(string _imagePath, RecordData _recordData)
        {
            try
            {
                //获取文件的信息
                FileInfo _fileInfo = new FileInfo(_imagePath);

                //容器：图片的新名字
                string _newImageName = "";

                //如果这个文件存在
                if (_fileInfo.Exists == true)
                {
                    //图片的名字(Extension属性是文件名的后缀名)
                    _newImageName = _recordData.Id.ToString();

                    //图片的新路径
                    string _newImagePath = ImageFolderPath + "/" + _newImageName + _fileInfo.Extension;
                    _newImagePath = FileTool.AvoidSameFile(_newImagePath);

                    //取到新的图片名字
                    _newImageName = Path.GetFileNameWithoutExtension(_newImagePath) + _fileInfo.Extension;

                    //然后把原图片，复制到新的路径地址上
                    byte[] _oldImageBytes = File.ReadAllBytes(_imagePath);//取到旧的图片的二进制数据
                    File.WriteAllBytes(_newImagePath, _oldImageBytes);//把旧图片的二进制信息，写进新图片文件中
                }

                //返回值
                return _newImageName;
            }
            catch (Exception e)
            {
                //输出错误
                AppManager.Uis.ErrorUi.UiControl.TipContent = e.ToString();
                AppManager.Uis.ErrorUi.OpenOrClose(true);

                return "";
            }

        }

        #endregion

    }
}
