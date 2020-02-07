/* By: 絮大王（sukiup@163.com）
   Time：2019年12月9日00:28:15*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace EasyBugManager
{
    /// <summary>
    /// 导出的系统（用于将项目导出为Excel文件）
    /// </summary>
    public class ExportSystem
    {
        /// <summary>
        /// 将项目导出为Excel文件
        /// </summary>
        /// <param name="_path">路径（文件夹+文件名+文件后缀）</param>
        /// <returns>是否导出成功？</returns>
        public bool ExportExcel(string _path)
        {
            try
            {
                //获取Excel文件的信息（因为文件可能不存在，所以可能暂时取不到Excel文件的信息）
                FileInfo _fileInfo = new FileInfo(_path);

                //通过Excel文件的信息，打开Excel文件（如果Excel文件不存在，这个也不会报错）
                using (ExcelPackage _excelPackage = new ExcelPackage(_fileInfo))
                {

                    /* 获取要操作的表 */
                    //如果Excel文件中，没有任何表格(或者Excel文件不存在)
                    if (_excelPackage.Workbook.Worksheets.Count<=0)
                    {
                        //创建一张表格(创建Excel文件)
                        _excelPackage.Workbook.Worksheets.Add("Sheet1");
                    }

                    //获取我们要操作的Excel表格（获取第1张表）
                    ExcelWorksheet _worksheet = _excelPackage.Workbook.Worksheets[1];



                    /* 清空表格 */
                    _worksheet.Cells.Clear();


                    /* 写入数据：表头 */
                    _worksheet.Cells[1, 1].Value = "Progress";//往[第1行 第1列]中写入[完成度]数据
                    _worksheet.Cells[1, 2].Value = "Priority";//往[第1行 第2列]中写入[优先级]数据
                    _worksheet.Cells[1, 3].Value = "Title";//往[第1行 第3列]中写入[标题]数据
                    _worksheet.Cells[1, 4].Value = "CreateTime";//往[第1行 第4列]中写入[创建时间]数据
                    _worksheet.Cells[1, 5].Value = "UpdateTime";//往[第1行 第5列]中写入[更新时间]数据
                    _worksheet.Cells[1, 6].Value = "SolveTime";//往[第1行 第6列]中写入[完成时间]数据


                    /* 写入数据：Bug数据 */
                    //获取BugData
                    ObservableCollection<BugData> _allBugDatas = AppManager.Systems.BugSystem.BugDatas;//所有的Bug数据
                    List<BugData> _sortBugDatas = new List<BugData>();//排序后的Bug数据
                    for (int i = 0; i < _allBugDatas.Count; i++)
                    {
                        //Bug数据
                        BugData _bugData = _allBugDatas[i];

                        //判断这个Bug是否没有被删除
                        if (_bugData != null && _bugData.IsDelete != true)
                        {
                            _sortBugDatas.Add(_bugData);
                        }
                    }

                    //排序数据（按照创建时间，从前往后排序）
                    _sortBugDatas.Sort((bug1, bug2) =>
                    {
                        /* 这个Lamba表达式的返回值为int类型，意思是bug1和bug2比较的大小。(大的排后面)
                           如果不能理解这段代码，可以搜索"C# List 多权重排序" */

                        int _index = 0;

                        //对[创建时间]进行排序（从低到高）
                        _index += DateTime.Compare(bug1.CreateTime, bug2.CreateTime);

                        return _index;
                    });


                    //写入数据
                    for (int i = 0; i < _sortBugDatas.Count; i++)
                    {
                        BugData _bugData = _sortBugDatas[i];//Bug数据

                        int _row = i + 2;//行数(从第2行开始)

                        _worksheet.Cells[_row, 1].Value = _bugData.Progress.ToString();//往[第_currentRow行 第1列]中写入[完成度]数据
                        _worksheet.Cells[_row, 2].Value = _bugData.Priority.ToString();//往[第_currentRow行 第2列]中写入[优先级]数据
                        _worksheet.Cells[_row, 3].Value = _bugData.Name.Text;//往[第_currentRow行 第3列]中写入[标题]数据
                        _worksheet.Cells[_row, 4].Value = DateTimeTool.DateTimeToString(_bugData.CreateTime.ToLocalTime(), TimeFormatType.YearMonthDayHourMinuteSecond);//往[第_currentRow行 第4列]中写入[创建时间]数据
                        _worksheet.Cells[_row, 5].Value = DateTimeTool.DateTimeToString(_bugData.UpdateTime.ToLocalTime(), TimeFormatType.YearMonthDayHourMinuteSecond);//往[第_currentRow行 第5列]中写入[更新时间]数据
                        if (_bugData.Progress == ProgressType.Solved)
                        {
                            _worksheet.Cells[_row, 6].Value = DateTimeTool.DateTimeToString(_bugData.SolveTime.ToLocalTime(), TimeFormatType.YearMonthDayHourMinuteSecond);//往[第_currentRow行 第6列]中写入[完成时间]数据
                        }
                    }


                    /* 保存表格 */
                    _excelPackage.Save();

                }//关闭Excel文件


                return true;
            }
            catch (Exception e)
            {
                //输出错误
                AppManager.Uis.ErrorUi.UiControl.TipContent = e.ToString();
                AppManager.Uis.ErrorUi.OpenOrClose(true);

                return false;
            }
        }
    }
}
