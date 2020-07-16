using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace AmazedPictureProcessing
{
    public class EncoderParameterModel
    {
        /// <summary>
        /// 向图像编码器传递参数时，该参数将封装在 EncoderParameter 对象中。 EncoderParameter 对象的一个字段是 GUID，用于指定参数的类别。 使用 Encoder 类的静态字段来检索包含所需类别的参数 Encoder。
        /// </summary>
        public long? ChrominanceTable { get; set; }
        /// <summary>
        /// 颜色深度，24L，32L
        /// </summary>
        public long? ColorDepth { get; set; }
        /// <summary>
        /// 压缩参数
        /// </summary>
        public CompressionEncoderValue? Compression { get; set; }
        /// <summary>
        /// 亮度表参数
        /// </summary>
        public long? LuminanceTable { get; set; }
        /// <summary>
        /// 质量参数。质量参数的有用值范围是从0到100。 指定的数字越小，压缩就越高，因此图像的质量越低。 0和100会向你显示最高质量的图像。
        /// </summary>
        public long? Quality { get; set; }
        /// <summary>
        /// 渲染方法参数
        /// </summary>
        public RenderMethodEncoderValue? RenderMethod { get; set; }
        /// <summary>
        /// 保存标志参数
        /// </summary>
        public long? SaveFlag { get; set; }
        /// <summary>
        /// 扫描方法参数
        /// </summary>
        public ScanMethodEncoderValue? ScanMethod { get; set; }
        /// <summary>
        /// 转换参数
        /// </summary>
        public TransformEncoderValue? Transformation { get; set; }
        /// <summary>
        /// 版本参数
        /// </summary>
        public VersionEncoderValue? Version { get; set; }
    }
    /// <summary>
    /// 压缩参数值枚举
    /// </summary>
    public enum CompressionEncoderValue
    {
        /// <summary>
        /// 指定 CCITT3 压缩方案。 可以作为属于压缩类别的参数传递到 TIFF 编码器。
        /// </summary>
        CompressionCCITT3 = EncoderValue.CompressionCCITT3,
        /// <summary>
        /// 指定 CCITT4 压缩方案。 可以作为属于压缩类别的参数传递到 TIFF 编码器。
        /// </summary>
        CompressionCCITT4 = EncoderValue.CompressionCCITT4,
        /// <summary>
        /// 指定 LZW 压缩方案。 可以作为属于压缩类别的参数传递到 TIFF 编码器。
        /// </summary>
        CompressionLZW = EncoderValue.CompressionLZW,
        /// <summary>
        /// 不指定压缩。 可以作为属于压缩类别的参数传递到 TIFF 编码器。
        /// </summary>
        CompressionNone = EncoderValue.CompressionNone,
        /// <summary>
        /// 指定 RLE 压缩方案。 可以作为属于压缩类别的参数传递到 TIFF 编码器。
        /// </summary>
        CompressionRle = EncoderValue.CompressionRle
    }
    /// <summary>
    /// 渲染参数值枚举
    /// </summary>
    public enum RenderMethodEncoderValue
    {
        /// <summary>
        /// 在 GDI+ 1.0 版中不使用。
        /// </summary>
        RenderNonProgressive = EncoderValue.RenderNonProgressive,
        /// <summary>
        /// 在 GDI+ 1.0 版中不使用。
        /// </summary>
        RenderProgressive = EncoderValue.RenderProgressive
    }
    /// <summary>
    /// 扫描方法参数值枚举
    /// </summary>
    public enum ScanMethodEncoderValue
    {
        /// <summary>
        /// 在 GDI+ 1.0 版中不使用。
        /// </summary>
        ScanMethodInterlaced = EncoderValue.ScanMethodInterlaced,
        /// <summary>
        /// 在 GDI+ 1.0 版中不使用。
        /// </summary>
        ScanMethodNonInterlaced = EncoderValue.ScanMethodNonInterlaced
    }
    /// <summary>
    /// 转换参数值枚举
    /// </summary>
    public enum TransformEncoderValue
    {
        /// <summary>
        /// 指定图像将要水平翻转（绕垂直轴）。 可以作为属于转换类别的参数传递到 JPEG 编码器。
        /// </summary>
        TransformFlipHorizontal = EncoderValue.TransformFlipHorizontal,
        /// <summary>
        /// 指定图像将要垂直翻转（绕水平轴）。 可以作为属于转换类别的参数传递到 JPEG 编码器。
        /// </summary>
        TransformFlipVertical = EncoderValue.TransformFlipVertical,
        /// <summary>
        /// 指定图像将围绕其中心旋转 180 度。 可以作为属于转换类别的参数传递到 JPEG 编码器。
        /// </summary>
        TransformRotate180 = EncoderValue.TransformRotate180,
        /// <summary>
        /// 指定图像将围绕其中心沿顺时针方向旋转 270 度。 可以作为属于转换类别的参数传递到 JPEG 编码器。
        /// </summary>
        TransformRotate270 = EncoderValue.TransformRotate270,
        /// <summary>指定图像将围绕其中心沿顺时针方向旋转 90 度。 可以作为属于转换类别的参数传递到 JPEG 编码器。
        /// </summary>
        TransformRotate90 = EncoderValue.TransformRotate90
    }
    /// <summary>
    /// 版本参数值枚举
    /// </summary>
    public enum VersionEncoderValue
    {
        /// <summary>
        /// 在 GDI+ 1.0 版中不使用。
        /// </summary>
        VersionGif87 = EncoderValue.VersionGif87,
        /// <summary>
        /// 在 GDI+ 1.0 版中不使用。
        /// </summary>
        VersionGif89 = EncoderValue.VersionGif89
    }
}
