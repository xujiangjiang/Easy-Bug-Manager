/* By: 絮大王（sukiup@163.com）
   Time：2019年10月18日02:57:05 */

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
    /// 查找[ListBox控件中Item]的工具
    /// （通过Data，可以查找到ListBox中对应的Item）
    /// </summary>
    public static class FindListBoxItemTool
    {

        #region [公开方法 - 根据数据对象，获取ListItem控件]
        /// <summary>
        /// 根据数据对象，获取列表中的Item控件
        /// </summary>
        /// <param name="_data">要查找的数据</param>
        /// <param name="_listBox">要查找的ListBox</param>
        /// <param name="_itemName">（DataTemplate中的）Item控件的名字</param>
        /// <returns>列表的Item控件(DataTemplate中的控件)</returns>
        public static ItemControl GetListItemControl<Data,ItemControl>(ListBox _listBox, string _itemName, Data _data)
        {

            /* 第1步：根据Data获取ListBoxItem
             * 这里使用ListBox控件中的ItemContainerGenerator.ContainerFromItem()方法，
               可以通过数据对象，获取对应的ListBoxItem控件的对象
            */
            ListBoxItem _listBoxItem = (ListBoxItem)(_listBox.ItemContainerGenerator.ContainerFromItem(_data));//根据数据，获取对应的ListBoxItem



            /* 第2步：如果没有找到符合Data的Item，就返回null */
            if (_listBoxItem == null) return default(ItemControl);




            /* 第3步：把ListBoxItem强制转换为BugListItemControl控件
               这里使用了知识点：查找由 DataTemplate 生成的元素
               https://docs.microsoft.com/zh-cn/dotnet/framework/wpf/data/how-to-find-datatemplate-generated-elements
            */

            //获取这个 ListBoxItem 中的 ContentPresenter(内容显示控件)
            ContentPresenter _contentPresenter = FindVisualChild<ContentPresenter>(_listBoxItem);

            //获取内容控件中的 数据模板对象
            DataTemplate _dataTemplate = _contentPresenter.ContentTemplate;

            //在数据模板中，找到Item控件
            ItemControl _itemControl = (ItemControl)_dataTemplate.FindName(_itemName, _contentPresenter);




            return _itemControl;
        }
        #endregion

        #region [私有方法 - 查找一个元素下的所有子元素]
        /// <summary>
        /// 用于查找一个元素下的所有子元素
        /// ————————————————————————
        /// 泛型：要查找的子元素是什么类型的？
        /// 参数：要查找哪个元素下的子元素？（指定一个父元素）
        /// </summary>
        private static childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        #endregion
    }
}
