using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace AmazedPictureProcessing
{
    /// <summary>
    /// 图片压缩
    /// GDI + 具有内置编码器和解码器支持以下文件类型：
    /// BMP、GIF、JPEG、PNG、TIFF
    /// GDI + 还提供了支持以下文件类型的内置解码器：
    /// WMF、EMF、ICON
    /// </summary>
    public class PictureCompression
    {
        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="imageStream">原图流</param>
        /// <param name="destSize">输出图像的大小，当width为0，height不为0时，根据height的值与原图按比例设置width的值；当height为0，width不为0时，根据width的值与原图按比例设置height的值；当height为0，width为0时，直接取原图的width和height</param>
        /// <param name="model">图像编码参数模型</param>
        /// <param name="imageFormat">图像的保存格式</param>
        /// <returns></returns>
        public Stream PicCompression(Stream imageStream, Size destSize, EncoderParameterModel model, ImageFormat imageFormat)
        {
            var image = Image.FromStream(imageStream);
            image = GenerateSizePic(image, destSize);
            if (model == null)
            {
                throw new ArgumentNullException($"参数{nameof(model)}不能为空");
            }
            if (imageFormat == null)
            {
                throw new ArgumentNullException($"参数{nameof(imageFormat)}不能为空");
            }
            ImageCodecInfo imageCodecInfo = GetEncoder(imageFormat);
            EncoderParameters encoderParameters = new EncoderParameters(GetEncoderParameterModelValueNumbers(model));
            MemoryStream ms = new MemoryStream();
            ConstructorEncoderParameters(model, encoderParameters);
            image.Save(ms, imageCodecInfo, encoderParameters);
            return ms;
        }
        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="image">原图</param>
        /// <param name="destSize">输出图像的大小，当width为0，height不为0时，根据height的值与原图按比例设置width的值；当height为0，width不为0时，根据width的值与原图按比例设置height的值；当height为0，width为0时，直接取原图的width和height</param>
        /// <param name="model">图像编码参数模型</param>
        /// <param name="imageFormat">图像的保存格式</param>
        /// <returns></returns>
        public Stream PicCompression(Image image, Size destSize, EncoderParameterModel model, ImageFormat imageFormat)
        {
            image = GenerateSizePic(image, destSize);
            if (model == null)
            {
                throw new ArgumentNullException($"参数{nameof(model)}不能为空");
            }
            if (imageFormat == null)
            {
                throw new ArgumentNullException($"参数{nameof(imageFormat)}不能为空");
            }
            ImageCodecInfo imageCodecInfo = GetEncoder(imageFormat);
            EncoderParameters encoderParameters = new EncoderParameters(GetEncoderParameterModelValueNumbers(model));
            MemoryStream ms = new MemoryStream();
            ConstructorEncoderParameters(model, encoderParameters);
            image.Save(ms, imageCodecInfo, encoderParameters);
            return ms;
        }
        /// <summary>
        /// 构造图像编码参数
        /// </summary>
        /// <param name="model">图像编码参数模型</param>
        /// <param name="encoderParameters">图像编码参数</param>
        private void ConstructorEncoderParameters(EncoderParameterModel model, EncoderParameters encoderParameters)
        {
            int index = 0;
            Encoder encoder;
            if (model.Quality.HasValue)
            {
                encoder = Encoder.Quality;
                EncoderParameter encoderParameter = new EncoderParameter(encoder, model.Quality.Value);
                encoderParameters.Param[index] = encoderParameter;
                index++;
            }
            if (model.ChrominanceTable.HasValue)
            {
                encoder = Encoder.ChrominanceTable;
                EncoderParameter encoderParameter = new EncoderParameter(encoder, model.ChrominanceTable.Value);
                encoderParameters.Param[index] = encoderParameter;
                index++;
            }
            if (model.ColorDepth.HasValue)
            {
                encoder = Encoder.ColorDepth;
                EncoderParameter encoderParameter = new EncoderParameter(encoder, (long)model.ColorDepth.Value);
                encoderParameters.Param[index] = encoderParameter; 
                index++;
            }
            if (model.Compression.HasValue)
            {
                encoder = Encoder.Compression;
                EncoderParameter encoderParameter = new EncoderParameter(encoder, (long)model.Compression.Value);
                encoderParameters.Param[index] = encoderParameter; 
                index++;
            }
            if (model.LuminanceTable.HasValue)
            {
                encoder = Encoder.LuminanceTable;
                EncoderParameter encoderParameter = new EncoderParameter(encoder, model.LuminanceTable.Value);
                encoderParameters.Param[index] = encoderParameter; 
                index++;
            }
            if (model.RenderMethod.HasValue)
            {
                encoder = Encoder.RenderMethod;
                EncoderParameter encoderParameter = new EncoderParameter(encoder, (long)model.RenderMethod.Value);
                encoderParameters.Param[index] = encoderParameter; 
                index++;
            }
            if (model.SaveFlag.HasValue)
            {
                encoder = Encoder.SaveFlag;
                EncoderParameter encoderParameter = new EncoderParameter(encoder, model.SaveFlag.Value);
                encoderParameters.Param[index] = encoderParameter; 
                
                index++;
            }
            if (model.ScanMethod.HasValue)
            {
                encoder = Encoder.ScanMethod;
                EncoderParameter encoderParameter = new EncoderParameter(encoder, (long)model.ScanMethod.Value);
                encoderParameters.Param[index] = encoderParameter; 
                index++;
            }
            if (model.Transformation.HasValue)
            {
                encoder = Encoder.Transformation;
                EncoderParameter encoderParameter = new EncoderParameter(encoder, (long)model.Transformation.Value);
                encoderParameters.Param[index] = encoderParameter; 
                index++;
            }
            if (model.Version.HasValue)
            {
                encoder = Encoder.Version;
                EncoderParameter encoderParameter = new EncoderParameter(encoder, (long)model.Version.Value);
                encoderParameters.Param[index] = encoderParameter;
            }
        }
        /// <summary>
        /// 获取图像编码参数模型有值的个数
        /// </summary>
        /// <param name="model">图像编码参数模型</param>
        /// <returns></returns>
        private int GetEncoderParameterModelValueNumbers(EncoderParameterModel model)
        {
            var result = 0;
            var propertyInfos = model.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetValue(model)!=null)
                {
                    result++;
                }
            }
            return result;
        }
        
        /// <summary>
        /// 按比例放大缩小图像
        /// </summary>
        /// <param name="image">原图</param>
        /// <param name="destSize">输出图像的大小</param>
        /// <returns></returns>
        private Bitmap GenerateSizePic(Image image,Size destSize)
        {
            var srcRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle destRectangle = GenerateDestRectangle(ref destSize, image.Size);
            var bitmap = new Bitmap(destRectangle.Width, destRectangle.Height);
            using Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Transparent);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, destRectangle, srcRectangle, GraphicsUnit.Pixel);
            return bitmap;
        }
        /// <summary>
        /// 生成目标图像的矩形大小
        /// </summary>
        /// <param name="destSize">输出图像的大小</param>
        /// <param name="imageSize">原图大小</param>
        /// <returns></returns>
        private Rectangle GenerateDestRectangle(ref Size destSize, Size imageSize)
        {
            var rectangle = new Rectangle
            {
                X = 0,
                Y = 0,
                Width = destSize.Width,
                Height = destSize.Height
            };
            if (destSize.Width == 0 & destSize.Height != 0)
            {
                rectangle.Width = (destSize.Height * imageSize.Width) / imageSize.Height;
            }
            else if (destSize.Width != 0 & destSize.Height != 0)
            {
                rectangle.Height = destSize.Height;
                rectangle.Width = destSize.Width;
            }
            else if (destSize.Width != 0 & destSize.Height == 0)
            {
                rectangle.Height = (destSize.Width * imageSize.Height) / imageSize.Width;
            }
            return rectangle;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
