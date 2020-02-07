/* By: 絮大王（sukiup@163.com）
   Time：2020年1月8日12:18:38*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 文件的工具
    /// </summary>
    public static class FileTool
    {
        /// <summary>
        /// 判断是否有相同的文件
        /// </summary>
        /// <param name="_filePath">文件的路径</param>
        /// <returns>如果没有相同的文件，就返回文件原本的路径；如果有相同的文件，就返回一个没有相同文件的文件路径（xxxx/文件名 (1).文件后缀）</returns>
        public static string AvoidSameFile(string _filePath)
        {
            //获取文件的信息
            FileInfo _fileInfo = new FileInfo(_filePath);

            /*判断是否有重名的文件*/
            //如果文件不存在
            if (_fileInfo.Exists == false)
            {
                return _filePath;
            }

            //如果文件已存在
            else
            {
                //取到文件的名字和后缀
                string _folderPath = _fileInfo.DirectoryName;//文件夹路径
                string _fileName = Path.GetFileNameWithoutExtension(_filePath);//文件名
                string _fileSuffix = _fileInfo.Extension;//文件后缀

                //容器：是否有重复的文件夹
                bool _isHaveSameFile = true;

                //容器：文件夹名字的标识符 - 【xxxx/文件名 (1).文件后缀】
                int _index = 0;





                //判断是否有重名的文件
                while (_isHaveSameFile)
                {
                    //标识符+1
                    _index++;

                    //新的文件路径【xxxx/文件名 (1).文件后缀】
                    string _newFilePath = _folderPath + "/" + _fileName + " (" + _index + ")" + _fileSuffix;

                    //判断是否有重名的文件
                    FileInfo _newFileInfo = new FileInfo(_newFilePath);
                    _isHaveSameFile = _newFileInfo.Exists;

                    //如果没有重复文件
                    if (_isHaveSameFile == false)
                    {
                        //返回新的路径
                        return _newFilePath;
                    }
                }



                return _folderPath + "/" + _fileName + " (999)" + _fileSuffix;
            }

        }



    }
}
