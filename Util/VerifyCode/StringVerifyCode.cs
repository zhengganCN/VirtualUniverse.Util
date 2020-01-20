using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Util.Math;

namespace Util.VerifyCode
{
    /// <summary>
    /// 
    /// </summary>
    public class StringVerifyCode
    {
        private Bitmap Bitmap { get; set; }
        private Graphics Graphics { get; set; }
        private int ImageWidth { get; set; }
        private int ImageHeight { get; set; }
        private PointF TextPointF { get; set; }
        private Font TextFont { get; set; }
        private EnumPosition TextPosition { get; set; }
        /// <summary>
        /// 初始化位图参数
        /// </summary>
        /// <param name="imageWidth">位图宽度</param>
        /// <param name="imageHeight">位图高度</param>
        public StringVerifyCode(int imageWidth, int imageHeight)
        {
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
            Bitmap = new Bitmap(imageWidth, imageHeight);
            Graphics = Graphics.FromImage(Bitmap);
            SetTextFont();
        }
        /// <summary>
        /// 生成验证码位图
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="brush">笔刷</param>
        /// <returns></returns>
        public Bitmap Generate(string text, Brush brush = null)
        {
            if (brush == null)
            {
                brush = new SolidBrush(Color.Black);
            }
            if (!string.IsNullOrEmpty(text))
            {
                SetTextPosition(text.Length);
                Graphics.DrawString(text, TextFont, brush, TextPointF);
            }
            TextFont.Dispose();
            brush.Dispose();
            Graphics.Dispose();
            return Bitmap;
        }
        /// <summary>
        /// 设置文本字体
        /// </summary>
        /// <param name="font">字体，font为null时，默认设置font的实例为new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold)</param>
        public void SetTextFont(Font font = null)
        {
            if (font == null)
            {
                TextFont = new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold);
            }
            else
            {
                TextFont = font;
            }
        }
        /// <summary>
        /// 设置干扰直线和竖线
        /// </summary>
        /// <param name="horizontalLineNumber">横线的数量</param>
        /// <param name="verticalLineNumber">竖线的数量</param>
        /// <param name="brush">笔刷，brush为null时，默认设置brush为black</param>
        public void SetInterferenceLine(int horizontalLineNumber = 2,
            int verticalLineNumber = 0, Brush brush = null)
        {
            if (brush == null)
            {
                brush = new SolidBrush(Color.Black);
            }
            Pen pen = new Pen(brush);
            var h = 0;
            if (horizontalLineNumber > 0)
            {
                h = ImageHeight / (horizontalLineNumber + 1);
            }
            for (int i = 0; i < horizontalLineNumber; i++)
            {
                Point startPoint = new Point(0, h * (i + 1));
                Point endPoint = new Point(ImageWidth, h * (i + 1));
                Graphics.DrawLine(pen, startPoint, endPoint);
            }
            var w = 0;
            if (verticalLineNumber > 0)
            {
                w = ImageWidth / (verticalLineNumber + 1);
            }
            for (int i = 0; i < verticalLineNumber; i++)
            {
                Point startPoint = new Point(w * (i + 1), 0);
                Point endPoint = new Point(w * (i + 1), ImageHeight);
                Graphics.DrawLine(pen, startPoint, endPoint);
            }
            brush.Dispose();
            pen.Dispose();
        }
        /// <summary>
        /// 设置干扰点
        /// </summary>
        /// <param name="pointNumber">干扰点数量</param>
        /// <param name="radius">半径</param>
        /// <param name="brush">笔刷，brush为null时，默认设置brush为black</param>
        /// <param name="colors">为圆形设置可选颜色，通过随机函数选取，colors不为空时，brush参数不生效</param>
        public void SetInterferencePoint(int pointNumber = 20, int radius = 5, Brush brush = null, Color[] colors=null)
        {
            if (colors != null)
            {
                if (colors.Length == 0)
                {
                    throw new ArgumentException(nameof(colors) + StringResource._47B50496_4949_443F_8D22_15D008056E32);
                }
                RandomNumber random = new RandomNumber();
                for (int i = 0; i < pointNumber; i++)
                {
                    var index = random.GenerateRandom(0, colors.Length - 1);
                    Brush randomBrush = new SolidBrush(colors[index]);
                    GenerateEllipse(radius, randomBrush);
                    randomBrush.Dispose();
                }
            }
            else
            {
                if (brush == null)
                {
                    brush = new SolidBrush(Color.Black);
                }
                for (int i = 0; i < pointNumber; i++)
                {
                    GenerateEllipse(radius, brush);
                }
                brush.Dispose();
            }
        }
        /// <summary>
        /// 生成圆形
        /// </summary>
        /// <param name="radius">半径</param>
        /// <param name="brush">笔刷</param>
        private void GenerateEllipse(int radius, Brush brush)
        {
            RandomNumber random = new RandomNumber();
            var x = random.GenerateRandom(0, ImageWidth);
            var y = random.GenerateRandom(0, ImageHeight);
            Graphics.FillEllipse(brush, x, y, radius, radius);
        }
        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="backgroundColor">背景色</param>
        public void SetBackgroundColor(Color backgroundColor)
        {
            Graphics.Clear(backgroundColor);
        }
        /// <summary>
        /// 设置文本位置
        /// </summary>
        /// <param name="position">文本位置枚举值</param>
        public void SetTextPosition(EnumPosition position = EnumPosition.Center)
        {
            TextPosition = position;
        }
        /// <summary>
        /// 设置文本位置
        /// </summary>
        /// <param name="textLength">文本字数</param>
        private void SetTextPosition(int textLength)
        {
            var textWidth = textLength * TextFont.Size;
            var textHeight = TextFont.Height;
            switch (TextPosition)
            {
                case EnumPosition.LeftCenter:
                    if (ImageHeight < textHeight)
                    {
                        TextPointF = new PointF(0, (ImageHeight - textHeight) / 2);
                    }
                    else
                    {
                        TextPointF = new PointF(0, 0);
                    }
                    break;
                case EnumPosition.TopCenter:
                    if (textWidth < ImageWidth)
                    {
                        TextPointF = new PointF((ImageWidth - textWidth) / 2, 0);
                    }
                    else
                    {
                        TextPointF = new PointF(0, 0);
                    }
                    break;
                case EnumPosition.RightCenter:
                    if (textWidth < ImageWidth)
                    {
                        TextPointF = new PointF(ImageWidth - textWidth, (ImageHeight - textHeight) / 2);
                    }
                    else
                    {
                        TextPointF = new PointF(0, (ImageHeight - textHeight) / 2);
                    }
                    break;
                case EnumPosition.BottomCenter:
                    if (textWidth < ImageWidth)
                    {
                        TextPointF = new PointF((ImageWidth - textWidth) / 2, ImageHeight - textHeight);
                    }
                    else
                    {
                        TextPointF = new PointF(0, ImageHeight - textHeight);
                    }
                    break;
                case EnumPosition.LeftTop:
                    TextPointF = new PointF(0, 0);
                    break;
                case EnumPosition.RightTop:
                    if (textWidth < ImageWidth)
                    {
                        TextPointF = new PointF(ImageWidth - textWidth, 0);
                    }
                    else
                    {
                        TextPointF = new PointF(0, 0);
                    }
                    break;
                case EnumPosition.LeftBottom:
                    TextPointF = new PointF(0, ImageHeight - textHeight);
                    break;
                case EnumPosition.RightBottom:
                    if (textWidth < ImageWidth)
                    {
                        TextPointF = new PointF(ImageWidth - textWidth, ImageHeight - textHeight);
                    }
                    else
                    {
                        TextPointF = new PointF(0, ImageHeight - textHeight);
                    }
                    break;
                case EnumPosition.Center:
                    if (textWidth < ImageWidth)
                    {
                        TextPointF = new PointF((ImageWidth - textWidth) / 2, (ImageHeight - textHeight) / 2);
                    }
                    else
                    {
                        TextPointF = new PointF(0, (ImageHeight - textHeight) / 2);
                    }
                    break;
                default:
                    if (textWidth < ImageWidth)
                    {
                        TextPointF = new PointF((ImageWidth - textWidth) / 2, (ImageHeight - textHeight) / 2);
                    }
                    else
                    {
                        TextPointF = new PointF(0, (ImageHeight - textHeight) / 2);
                    }
                    break;
            }
        }
        /// <summary>
        /// 位置枚举
        /// </summary>
        public enum EnumPosition
        {
            /// <summary>
            /// 左中
            /// </summary>
            [Description("左中")]
            LeftCenter = 1,
            /// <summary>
            /// 上中
            /// </summary>
            [Description("上中")]
            TopCenter = 2,
            /// <summary>
            /// 右中
            /// </summary>
            [Description("右中")]
            RightCenter = 3,
            /// <summary>
            /// 下中
            /// </summary>
            [Description("下中")]
            BottomCenter = 4,
            /// <summary>
            /// 左上
            /// </summary>
            [Description("左上")]
            LeftTop = 5,
            /// <summary>
            /// 右上
            /// </summary>
            [Description("右上")]
            RightTop = 6,
            /// <summary>
            /// 左下
            /// </summary>
            [Description("左下")]
            LeftBottom = 7,
            /// <summary>
            /// 右下
            /// </summary>
            [Description("右下")]
            RightBottom = 8,
            /// <summary>
            /// 居中
            /// </summary>
            [Description("居中")]
            Center = 9
        }
    }
}
