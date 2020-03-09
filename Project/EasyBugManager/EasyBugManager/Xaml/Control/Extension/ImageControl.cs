using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EasyBugManager
{
    /// <summary>
    /// 图片控件
    ///（这个控件继承了WPF原版的Image类，并扩展了这个类）
    ///（WPF默认的Image控件绑定Source后，图片文件会被占用。用这个控件去代替原版的Image控件，就可以解决这个问题）
    /// </summary>
    public class ImageControl : Image
    {
        /* 1. 感谢：感谢CSDN博主[颜家大饼]!
                   这个控件参考了“颜家大饼”大佬的思路和代码！
                   原文链接：https://blog.csdn.net/u012559285/article/details/76887341
                
           2. 遇到的问题：如果我们使用WPF自带的Image控件，用Image.Source属性绑定了一个图片路径后。
                         这个图片就无法被删除了，会显示“文件正在被使用”。
                         只有当把软件关闭时，才能够删除这个图片。
                      
           3. 为什么会出现这个问题：这里引用“颜家大饼”大佬的原文：
                                  WPF默认的Image控件绑定Source后，图片文件会被占用，此时删除或者使用另一Image控件绑定该图片，将引起文件被占用异常。
                                  甚至当Image控件删除后仍会存在文件被占用的异常。
                                  资料给出的解释是Image控件没有Dispose方法导致文件未被释放，这一问题给图片绑定的便捷操作带来了很多麻烦。
                               
           4. 如何解决这个问题：当Image控件取到一个图片路径后，就读取图片路径中的图片文件，取到图片文件的二进制字节（byte[]）。
                               然后把图片文件的字节，转化为Bitmap，然后让Image控件显示Bitmap就可以啦。
                               这样Image控件就不再引用原来的图片文件，我们就可以删除图片文件啦！

           5. 怎么做：所以我们重写了Image控件中的Source属性，当Source属性的值发生改变时，我们就去读取图片文件的字节，把字节转化为Bitmap，然后再把Bitmap交给Image进行显示。
                           
             
           6. 如何使用：这个类因为继承了Image类，所以本质上就是个Image控件。
                       所以和普通的Image控件的使用方法一模一样，真的一模一样。
                       直接在XAML中写：<ImageControl Source=""/>
                       */







        #region 依赖项属性：Source（重写）
        /// <summary>
        /// 依赖项属性：图片的路径（文件夹+文件名+文件后缀）
        /// </summary>
        public new static DependencyProperty SourceProperty;

        /// <summary>
        /// 公开属性：图片的路径（文件夹+文件名+文件后缀）
        /// </summary>
        public new string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// 依赖项属性发生改变时，触发的事件：
        /// 当SourceProperty依赖项属性，的属性值发生改变的时候，调用这个方法
        /// </summary>
        /// <param name="sender">依赖项对象</param>
        /// <param name="e">依赖项属性改变事件 的参数（里面有这个属性的新的值，和旧的值）</param>
        private static void OnSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //获取控件
            Image _image = (Image)sender;

            //如果字符串不正确，或者文件不存在就算了
            if (e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()) ||
                File.Exists(e.NewValue.ToString()) == false)
            {
                _image.Source = null;
                return;
            }


            //如果字符串正确
            try
            {
                //读取文件中的二进制数据
                byte[] bytes = File.ReadAllBytes(e.NewValue.ToString());

                //把图片文件的二进制数据，转化为BitmapImage
                BitmapImage _bitmapImage = new BitmapImage();
                _bitmapImage.CacheOption = BitmapCacheOption.OnLoad;

                _bitmapImage.BeginInit();
                _bitmapImage.StreamSource = new MemoryStream(bytes);
                _bitmapImage.EndInit();

                //让Image控件显示BitmapImage，这样Image控件就不会读取图片啦！
                _image.Source = _bitmapImage;
            }
            catch (Exception)
            {
                _image.Source = null;
            }
        }
        #endregion



        #region 静态构造方法：注册依赖项属性 和 路由事件
        /// <summary>
        /// 静态构造方法：在里面注册依赖项属性 和 路由事件
        /// </summary>
        static ImageControl()
        {
            /*注册依赖项属性*/
            //注册SourceProperty
            SourceProperty = DependencyProperty.Register(
                "Source", //属性的名字
                typeof(string), //属性的类型
                typeof(ImageControl), //这个属性属于哪个控件？
                new FrameworkPropertyMetadata( //属性的初始值和回调函数
                                               //初始值
                    (string)"",
                    //当属性的值发生改变时，调用什么方法？
                    new PropertyChangedCallback(OnSourceChanged))
            );
        }
        #endregion

    }
}
