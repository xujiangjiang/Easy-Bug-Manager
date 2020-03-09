/* By: 絮大王（sukiup@163.com）
   Time：2019年11月30日16:19:26*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// ObservableCollection集合的工具
    /// </summary>
    public static class ObservableCollectionTool
    {
        /// <summary>
        /// 把ObservableCollection集合，转换为List集合
        /// </summary>
        /// <typeparam name="T">集合中数据的类型</typeparam>
        /// <param name="_observableCollection">要转换的ObservableCollection集合</param>
        /// <returns>转换后的List集合</returns>
        public static List<T> ObservableCollectionToList<T>(ObservableCollection<T> _observableCollection)
        {
            //容器：List集合
            List<T> _list = new List<T>();

            //遍历ObservableCollection集合
            if (_observableCollection != null)
            {
                for (int i = 0; i < _observableCollection.Count; i++)
                {
                    _list.Add(_observableCollection[i]);
                }
            }

            //返回值
            return _list;
        }


        /// <summary>
        /// 把List集合，转换为ObservableCollection集合
        /// </summary>
        /// <typeparam name="T">集合中数据的类型</typeparam>
        /// <param name="_list">要转换的List集合</param>
        /// <returns>转换后的ObservableCollection集合</returns>
        public static ObservableCollection<T> ListToObservableCollection<T>(List<T> _list)
        {
            //容器：ObservableCollection集合
            ObservableCollection<T> _observableCollection = new ObservableCollection<T>();

            //遍历List集合
            if (_list != null)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _observableCollection.Add(_list[i]);
                }
            }

            //返回值
            return _observableCollection;
        }
    }
}
