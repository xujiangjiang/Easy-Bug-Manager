/* By: 絮大王（sukiup@163.com）
   Time：2020年1月8日12:47:42*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 文件夹的工具
    /// </summary>
    public static class FolderTool
    {
        /// <summary>
        /// 判断是否有相同的文件夹
        /// </summary>
        /// <param name="_folderPath">文件夹的路径</param>
        /// <returns>如果没有相同的文件夹，就返回文件夹原本的路径；如果有相同的文件夹，就返回一个没有相同文件夹的文件夹路径（xxxx/文件夹 (1)/）</returns>
        public static string AvoidSameFolder(string _folderPath)
        {
            //获取文件夹的信息
            DirectoryInfo _directoryInfo = new DirectoryInfo(_folderPath);

            /*判断是否有重名的文件夹*/
            //如果文件夹不存在
            if (_directoryInfo.Exists == false)
            {
                return _folderPath;
            }

            //如果文件夹已存在
            else
            {
                //取到文件夹的名字
                string _previousFolderPath = _directoryInfo.Parent.FullName;//父级文件夹的路径
                string _folderName = _directoryInfo.Name;//文件夹的名字

                //容器：是否有重复的文件夹
                bool _isHaveSameFolder = true;

                //容器：文件夹名字的标识符 - 【xxxx/文件夹名 (1)/】
                int _index = 0;





                //判断是否有重名的文件夹
                while (_isHaveSameFolder)
                {
                    //标识符+1
                    _index++;

                    //新的文件夹路径【xxxx/文件夹名/】
                    string _newFolderPath = _previousFolderPath + "/" + _folderName + " (" + _index + ")";

                    //判断是否有重名的文件夹
                    DirectoryInfo _newDirectoryInfo = new DirectoryInfo(_newFolderPath);
                    _isHaveSameFolder = _newDirectoryInfo.Exists;

                    //如果没有重复文件夹
                    if (_isHaveSameFolder == false)
                    {
                        //返回新的路径
                        return _newFolderPath;
                    }
                }



                return _previousFolderPath + "/" + _folderName + " (999)";
            }

        }
    }
}
