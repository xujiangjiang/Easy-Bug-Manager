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
    /// ImageCheckGroupControl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageCheckGroupControl : UserControl
    {
        /* 属性: CheckControlNumber(Check的数量。如果是2，就是2个Check；如果是3，就是3个Check；如果是0，就是0个check)
         *       CheckedIndex(选中的是哪个Check)(Check的索引，从0开始)
         *       Padding(Check之间的间隔)
                 PressAnimationSize(按下按钮时 动画缩小的尺寸)
                 Orientation(排列方式)（水平或者垂直）
                 
                 Width1(第1个Check：宽度)
                 Height1(第1个Check：高度)
                 MouseEnterImage1(第1个Check：鼠标进入时的图片) 
                 MouseLeaveImage1(第1个Check：鼠标移出时的图片)
                 CheckImage1(第1个Check：选中时的图片)

                 Width2(第2个Check：宽度)
                 Height2(第2个Check：高度)
                 MouseEnterImage2(第2个Check：鼠标进入时的图片) 
                 MouseLeaveImage2(第2个Check：鼠标移出时的图片)
                 CheckImage2(第2个Check：选中时的图片)

                 Width3(第3个Check：宽度)
                 Height3(第3个Check：高度)
                 MouseEnterImage3(第3个Check：鼠标进入时的图片) 
                 MouseLeaveImage3(第3个Check：鼠标移出时的图片)
                 CheckImage3(第3个Check：选中时的图片)

           事件: CheckedChange(当选中的Check改变的时候)(参数是int类型，是被选中的Check的索引)
                 ClickCheck(当某个Check控件被点击时)(参数是int类型，是被点击的Check的索引)
           
           备注：这个Check组里，有0-3个Check。
                一个Check有2个状态（UnCheck-未选中、Checked-选中）*/



        public ImageCheckGroupControl()
        {
            InitializeComponent();
        }



        #region 依赖项属性：CheckControlNumber

        /// <summary>
        /// 依赖项属性：Check的数量（如果是2，就是2个Check；如果是3，就是3个Check）
        /// </summary>
        public static DependencyProperty CheckControlNumberProperty;

        /// <summary>
        /// 公开属性：Check的数量（如果是2，就是2个Check；如果是3，就是3个Check）
        /// </summary>
        public int CheckControlNumber
        {
            get { return (int)GetValue(CheckControlNumberProperty); }
            set { SetValue(CheckControlNumberProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当CheckControlNumberProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnCheckControlNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //取到控件
            ImageCheckGroupControl _imageCheckGroupControl = sender as ImageCheckGroupControl;

            //看看新的值是多少
            switch ((int)e.NewValue)
            {
                //如果是0，就让3个Check控件都不显示
                case 0:
                    _imageCheckGroupControl.ImageCheckControl1.Visibility = Visibility.Collapsed;
                    _imageCheckGroupControl.ImageCheckControl2.Visibility = Visibility.Collapsed;
                    _imageCheckGroupControl.ImageCheckControl3.Visibility = Visibility.Collapsed;
                    break;

                //如果是1，只显示第一个Check控件
                case 1:
                    _imageCheckGroupControl.ImageCheckControl1.Visibility = Visibility.Visible;
                    _imageCheckGroupControl.ImageCheckControl2.Visibility = Visibility.Collapsed;
                    _imageCheckGroupControl.ImageCheckControl3.Visibility = Visibility.Collapsed;
                    break;

                //如果是2，只显示第1个和第2个Check控件
                case 2:
                    _imageCheckGroupControl.ImageCheckControl1.Visibility = Visibility.Visible;
                    _imageCheckGroupControl.ImageCheckControl2.Visibility = Visibility.Visible;
                    _imageCheckGroupControl.ImageCheckControl3.Visibility = Visibility.Collapsed;
                    break;

                //如果是3，就让3个Check控件都显示
                case 3:
                    _imageCheckGroupControl.ImageCheckControl1.Visibility = Visibility.Visible;
                    _imageCheckGroupControl.ImageCheckControl2.Visibility = Visibility.Visible;
                    _imageCheckGroupControl.ImageCheckControl3.Visibility = Visibility.Visible;
                    break;
            }
        }
        #endregion

        #region 依赖项属性：CheckedIndex

        /// <summary>
        /// 依赖项属性：选中的是哪个Check(Check的索引)
        /// </summary>
        public static DependencyProperty CheckedIndexProperty;

        /// <summary>
        /// 公开属性：选中的是哪个Check(Check的索引)
        /// </summary>
        public int CheckedIndex
        {
            get { return (int)GetValue(CheckedIndexProperty); }
            set { SetValue(CheckedIndexProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当CheckedIndexProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnCheckedIndexChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //取到控件
            ImageCheckGroupControl _imageCheckGroupControl = sender as ImageCheckGroupControl;

            //如果数字改变了，就把另外的2个Check控件的IsChecked属性设置为false
            switch ((int)e.NewValue)
            {
                //如果选择了-1个Check控件
                case -1:
                    _imageCheckGroupControl.ImageCheckControl1.IsChecked = false;
                    _imageCheckGroupControl.ImageCheckControl2.IsChecked = false;
                    _imageCheckGroupControl.ImageCheckControl3.IsChecked = false;
                    break;

                //如果选中了第1个Check控件
                case 0:
                    _imageCheckGroupControl.ImageCheckControl1.IsChecked = true;
                    _imageCheckGroupControl.ImageCheckControl2.IsChecked = false;
                    _imageCheckGroupControl.ImageCheckControl3.IsChecked = false;
                    break;

                //如果选中了第2个Check控件
                case 1:
                    _imageCheckGroupControl.ImageCheckControl1.IsChecked = false;
                    _imageCheckGroupControl.ImageCheckControl2.IsChecked = true;
                    _imageCheckGroupControl.ImageCheckControl3.IsChecked = false;
                    break;

                //如果选中了第3个Check控件
                case 2:
                    _imageCheckGroupControl.ImageCheckControl1.IsChecked = false;
                    _imageCheckGroupControl.ImageCheckControl2.IsChecked = false;
                    _imageCheckGroupControl.ImageCheckControl3.IsChecked = true;
                    break;
            }


            //然后触发CheckedChange事件
            _imageCheckGroupControl.OnCheckedChange((int)e.OldValue, (int)e.NewValue);

        }
        #endregion

        #region 依赖项属性：Padding

        /// <summary>
        /// 依赖项属性：Check之间的间隔
        /// </summary>
        public static DependencyProperty PaddingProperty;

        /// <summary>
        /// 公开属性：Check之间的间隔
        /// </summary>
        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当PaddingProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnPaddingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 依赖项属性：PressAnimationSize

        /// <summary>
        /// 依赖项属性：按下按钮时 动画缩小的尺寸
        /// </summary>
        public static DependencyProperty PressAnimationSizeProperty;

        /// <summary>
        /// 公开属性：图标的宽度
        /// </summary>
        public Point PressAnimationSize
        {
            get { return (Point)GetValue(PressAnimationSizeProperty); }
            set { SetValue(PressAnimationSizeProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当IconHeightProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnPressAnimationSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：Orientation

        /// <summary>
        /// 依赖项属性：按下按钮时 动画缩小的尺寸
        /// </summary>
        public static DependencyProperty OrientationProperty;

        /// <summary>
        /// 公开属性：图标的宽度
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当OrientationProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnOrientationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion


        #region 依赖项属性：Width1
        /// <summary>
        /// 依赖项属性：第1个Check-宽度
        /// </summary>
        public static DependencyProperty Width1Property;

        /// <summary>
        /// 公开属性：第1个Check-宽度
        /// </summary>
        public double Width1
        {
            get { return (double)GetValue(Width1Property); }
            set { SetValue(Width1Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当Width1Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnWidth1Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：Height1
        /// <summary>
        /// 依赖项属性：第1个Check-高度
        /// </summary>
        public static DependencyProperty Height1Property;

        /// <summary>
        /// 公开属性：第1个Check-高度
        /// </summary>
        public double Height1
        {
            get { return (double)GetValue(Height1Property); }
            set { SetValue(Height1Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当Height1Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnHeight1Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：MouseEnterImage1
        /// <summary>
        /// 依赖项属性：第1个Check-鼠标进入时的图片
        /// </summary>
        public static DependencyProperty MouseEnterImage1Property;

        /// <summary>
        /// 公开属性：第1个Check-鼠标进入时的图片
        /// </summary>
        public ImageBrush MouseEnterImage1
        {
            get { return (ImageBrush)GetValue(MouseEnterImage1Property); }
            set { SetValue(MouseEnterImage1Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当ContentProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnMouseEnterImage1Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：MouseLeaveImage1
        /// <summary>
        /// 依赖项属性：第1个Check-鼠标移出时的图片
        /// </summary>
        public static DependencyProperty MouseLeaveImage1Property;

        /// <summary>
        /// 公开属性：第1个Check-鼠标移出时的图片
        /// </summary>
        public ImageBrush MouseLeaveImage1
        {
            get { return (ImageBrush)GetValue(MouseLeaveImage1Property); }
            set { SetValue(MouseLeaveImage1Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当MouseLeaveImage1Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnMouseLeaveImage1Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：CheckImage1
        /// <summary>
        /// 依赖项属性：第1个Check-选中时的图片
        /// </summary>
        public static DependencyProperty CheckImage1Property;

        /// <summary>
        /// 公开属性：第1个Check-选中时的图片
        /// </summary>
        public ImageBrush CheckImage1
        {
            get { return (ImageBrush)GetValue(CheckImage1Property); }
            set { SetValue(CheckImage1Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当CheckImage1Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnCheckImage1Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion


        #region 依赖项属性：Width2
        /// <summary>
        /// 依赖项属性：第2个Check-宽度
        /// </summary>
        public static DependencyProperty Width2Property;

        /// <summary>
        /// 公开属性：第2个Check-宽度
        /// </summary>
        public double Width2
        {
            get { return (double)GetValue(Width2Property); }
            set { SetValue(Width2Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当Width2Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnWidth2Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：Height2
        /// <summary>
        /// 依赖项属性：第2个Check-高度
        /// </summary>
        public static DependencyProperty Height2Property;

        /// <summary>
        /// 公开属性：第2个Check-高度
        /// </summary>
        public double Height2
        {
            get { return (double)GetValue(Height2Property); }
            set { SetValue(Height2Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当Height2Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnHeight2Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：MouseEnterImage2
        /// <summary>
        /// 依赖项属性：第2个Check-鼠标进入时的图片
        /// </summary>
        public static DependencyProperty MouseEnterImage2Property;

        /// <summary>
        /// 公开属性：第2个Check-鼠标进入时的图片
        /// </summary>
        public ImageBrush MouseEnterImage2
        {
            get { return (ImageBrush)GetValue(MouseEnterImage2Property); }
            set { SetValue(MouseEnterImage2Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当MouseEnterImage2Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnMouseEnterImage2Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：MouseLeaveImage2
        /// <summary>
        /// 依赖项属性：第2个Check-鼠标移出时的图片
        /// </summary>
        public static DependencyProperty MouseLeaveImage2Property;

        /// <summary>
        /// 公开属性：第2个Check-鼠标移出时的图片
        /// </summary>
        public ImageBrush MouseLeaveImage2
        {
            get { return (ImageBrush)GetValue(MouseLeaveImage2Property); }
            set { SetValue(MouseLeaveImage2Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当MouseLeaveImage2Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnMouseLeaveImage2Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：CheckImage2
        /// <summary>
        /// 依赖项属性：第2个Check-选中时的图片
        /// </summary>
        public static DependencyProperty CheckImage2Property;

        /// <summary>
        /// 公开属性：第2个Check-选中时的图片
        /// </summary>
        public ImageBrush CheckImage2
        {
            get { return (ImageBrush)GetValue(CheckImage2Property); }
            set { SetValue(CheckImage2Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当CheckImage2Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnCheckImage2Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion


        #region 依赖项属性：Width3
        /// <summary>
        /// 依赖项属性：第3个Check-宽度
        /// </summary>
        public static DependencyProperty Width3Property;

        /// <summary>
        /// 公开属性：第3个Check-宽度
        /// </summary>
        public double Width3
        {
            get { return (double)GetValue(Width3Property); }
            set { SetValue(Width3Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当Width3Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnWidth3Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：Height3
        /// <summary>
        /// 依赖项属性：第3个Check-高度
        /// </summary>
        public static DependencyProperty Height3Property;

        /// <summary>
        /// 公开属性：第3个Check-高度
        /// </summary>
        public double Height3
        {
            get { return (double)GetValue(Height3Property); }
            set { SetValue(Height3Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当Height3Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnHeight3Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：MouseEnterImage3
        /// <summary>
        /// 依赖项属性：第3个Check-鼠标进入时的图片
        /// </summary>
        public static DependencyProperty MouseEnterImage3Property;

        /// <summary>
        /// 公开属性：第3个Check-鼠标进入时的图片
        /// </summary>
        public ImageBrush MouseEnterImage3
        {
            get { return (ImageBrush)GetValue(MouseEnterImage3Property); }
            set { SetValue(MouseEnterImage3Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当MouseEnterImage3Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnMouseEnterImage3Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region 依赖项属性：MouseLeaveImage3
        /// <summary>
        /// 依赖项属性：第3个Check-鼠标移出时的图片
        /// </summary>
        public static DependencyProperty MouseLeaveImage3Property;

        /// <summary>
        /// 公开属性：第3个Check-鼠标移出时的图片
        /// </summary>
        public ImageBrush MouseLeaveImage3
        {
            get { return (ImageBrush)GetValue(MouseLeaveImage3Property); }
            set { SetValue(MouseLeaveImage3Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当MouseLeaveImage3Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnMouseLeaveImage3Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region 依赖项属性：CheckImage3
        /// <summary>
        /// 依赖项属性：第3个Check-选中时的图片
        /// </summary>
        public static DependencyProperty CheckImage3Property;

        /// <summary>
        /// 公开属性：第3个Check-选中时的图片
        /// </summary>
        public ImageBrush CheckImage3
        {
            get { return (ImageBrush)GetValue(CheckImage3Property); }
            set { SetValue(CheckImage3Property, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当CheckImage3Property依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnCheckImage3Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion





        #region 路由事件：CheckedChange
        /// <summary>
        /// 路由事件：CheckedChangeEvent
        /// （当选中时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent CheckedChangeEvent;


        /// <summary>
        /// 路由事件的属性：CheckedChange
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> CheckedChange
        {
            //添加一条事件
            add { AddHandler(CheckedChangeEvent, value); }

            //移除一条事件
            remove { RemoveHandler(CheckedChangeEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 CheckedChange 路由事件
        /// </summary>
        /// <param name="_oldValue">旧的值</param>
        /// <param name="_newValue">新的值(当前被选中的Check的索引)</param>
        private void OnCheckedChange(int _oldValue, int _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<int> args = new RoutedPropertyChangedEventArgs<int>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ImageCheckGroupControl.CheckedChangeEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion

        #region 路由事件：ClickCheck
        /// <summary>
        /// 路由事件：ClickCheckEvent
        /// （当某个Check被点击时，触发此事件）
        /// </summary>
        public static readonly RoutedEvent ClickCheckEvent;


        /// <summary>
        /// 路由事件的属性：ClickCheck
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> ClickCheck
        {
            //添加一条事件
            add { AddHandler(ClickCheckEvent, value); }

            //移除一条事件
            remove { RemoveHandler(ClickCheckEvent, value); }
        }


        /// <summary>
        /// 这个方法，用于触发 ClickCheck 路由事件
        /// </summary>
        /// <param name="_oldValue">旧的值</param>
        /// <param name="_newValue">新的值(当前被点击的Check的索引)</param>
        private void OnClickCheck(int _oldValue, int _newValue)
        {
            //创建路由事件参数
            RoutedPropertyChangedEventArgs<int> args = new RoutedPropertyChangedEventArgs<int>(_oldValue, _newValue);

            //设置这是哪个路由事件？
            args.RoutedEvent = ImageCheckGroupControl.ClickCheckEvent;

            //引发这个路由事件
            RaiseEvent(args);
        }
        #endregion




        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static ImageCheckGroupControl()
        {
            /*注册依赖项属性*/
            //注册MouseLeaveImage1Property
            MouseLeaveImage1Property = DependencyProperty.Register(
                "MouseLeaveImage1", //属性的名字
                typeof(ImageBrush), //属性的类型
                typeof(ImageCheckGroupControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (ImageBrush)null,
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnMouseLeaveImage1Changed))
            );

            //注册MouseEnterImage1Property
            MouseEnterImage1Property = DependencyProperty.Register(
                "MouseEnterImage1", typeof(ImageBrush), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnMouseEnterImage1Changed)));

            //注册CheckImage1Property
            CheckImage1Property = DependencyProperty.Register(
                "CheckImage1", typeof(ImageBrush), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnCheckImage1Changed)));

            //注册Width1Property
            Width1Property = DependencyProperty.Register(
                "Width1", typeof(double), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnWidth1Changed)));

            //注册Height1Property
            Height1Property = DependencyProperty.Register(
                "Height1", typeof(double), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnHeight1Changed)));



            //注册MouseLeaveImage2Property
            MouseLeaveImage2Property = DependencyProperty.Register(
                "MouseLeaveImage2", typeof(ImageBrush), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnMouseLeaveImage2Changed)));

            //注册MouseEnterImage2Property
            MouseEnterImage2Property = DependencyProperty.Register(
                "MouseEnterImage2", typeof(ImageBrush), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnMouseEnterImage2Changed)));

            //注册CheckImage2Property
            CheckImage2Property = DependencyProperty.Register(
                "CheckImage2", typeof(ImageBrush), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnCheckImage2Changed)));

            //注册Width2Property
            Width2Property = DependencyProperty.Register(
                "Width2", typeof(double), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnWidth2Changed)));

            //注册Height2Property
            Height2Property = DependencyProperty.Register(
                "Height2", typeof(double), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnHeight2Changed)));



            //注册MouseLeaveImage3Property
            MouseLeaveImage3Property = DependencyProperty.Register(
                "MouseLeaveImage3", typeof(ImageBrush), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnMouseLeaveImage3Changed)));

            //注册MouseEnterImage3Property
            MouseEnterImage3Property = DependencyProperty.Register(
                "MouseEnterImage3", typeof(ImageBrush), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnMouseEnterImage3Changed)));

            //注册CheckImage3Property
            CheckImage3Property = DependencyProperty.Register(
                "CheckImage3", typeof(ImageBrush), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((ImageBrush)null, new PropertyChangedCallback(OnCheckImage3Changed)));

            //注册Width3Property
            Width3Property = DependencyProperty.Register(
                "Width3", typeof(double), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnWidth3Changed)));

            //注册Height3Property
            Height3Property = DependencyProperty.Register(
                "Height3", typeof(double), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnHeight3Changed)));



            //注册PressAnimationSizeProperty
            PressAnimationSizeProperty = DependencyProperty.Register(
                "PressAnimationSize", typeof(Point), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((Point)new Point(0.75, 0.75), new PropertyChangedCallback(OnPressAnimationSizeChanged)));

            //注册CheckControlNumberProperty
            CheckControlNumberProperty = DependencyProperty.Register(
                "CheckControlNumber", typeof(int), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnCheckControlNumberChanged)));

            //注册CheckedIndexProperty
            CheckedIndexProperty = DependencyProperty.Register(
                "CheckedIndex", typeof(int), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnCheckedIndexChanged)));

            //注册PaddingProperty
            PaddingProperty = DependencyProperty.Register(
                "Padding", typeof(Thickness), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((Thickness)new Thickness(0), new PropertyChangedCallback(OnPaddingChanged)));

            //注册OrientationProperty
            OrientationProperty = DependencyProperty.Register(
                "Orientation", typeof(Orientation), typeof(ImageCheckGroupControl),
                new FrameworkPropertyMetadata((Orientation)Orientation.Horizontal, new PropertyChangedCallback(OnOrientationChanged)));







            /*注册路由事件*/
            //注册ClickEvent
            CheckedChangeEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "CheckedChange", //事件的名字
                RoutingStrategy.Bubble, //路由事件的类型（是冒泡还是隧道？Bubble是冒泡，Tunnel是隧道）
                typeof(RoutedPropertyChangedEventHandler<int>), //路由事件要处理的数据类型
                typeof(ImageCheckGroupControl) //这个路由事件属于哪个控件？
            );

            //注册ClickCheckEvent
            ClickCheckEvent = System.Windows.EventManager.RegisterRoutedEvent(
                "ClickCheck", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<int>), typeof(ImageCheckGroupControl));
        }
        #endregion





        #region [事件]
        //当第1个Check控件被选中时
        private void ImageCheckControl1_Checked(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.CheckedIndex = 0;
        }

        //当第2个Check控件被选中时
        private void ImageCheckControl2_Checked(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.CheckedIndex = 1;
        }

        //当第3个Check控件被选中时
        private void ImageCheckControl3_Checked(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.CheckedIndex = 2;
        }





        //当第1个Check控件被点击时
        private void ImageCheckControl1_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickCheck(-1, 0);//触发事件
        }

        //当第2个Check控件被点击时
        private void ImageCheckControl2_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickCheck(-1, 1);//触发事件
        }

        //当第3个Check控件被点击时
        private void ImageCheckControl3_OnClick(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            this.OnClickCheck(-1, 2);//触发事件
        }
        #endregion


    }
}
