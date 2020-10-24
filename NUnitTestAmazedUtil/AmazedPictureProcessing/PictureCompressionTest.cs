using AmazedPictureProcessing;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace NUnitTestAmazedUtil.AmazedPictureProcessing
{
    /// <summary>
    /// Õº∆¨—πÀı≤‚ ‘
    /// </summary>
    public class PictureCompressionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        
        [Test]
        [TestCase(@"C:\Users\93281\Pictures\708e9e2a-3a75-484a-b890-45e37c6a2184.jpg", 25L)]
        [TestCase(@"C:\Users\93281\Pictures\708e9e2a-3a75-484a-b890-45e37c6a2184.jpg", 50L)]
        [TestCase(@"C:\Users\93281\Pictures\708e9e2a-3a75-484a-b890-45e37c6a2184.jpg", 75L)]
        [TestCase(@"C:\Users\93281\Pictures\708e9e2a-3a75-484a-b890-45e37c6a2184.jpg", 100L)]
        public void CompressionTest(string path,long quality)
        {
            PictureCompression compression = new PictureCompression();
            var image= Image.FromFile(path);
            EncoderParameterModel model = new EncoderParameterModel
            {
                Quality = quality
            };
            var stream = compression.PicCompression(image, new Size(120,50), model,ImageFormat.Jpeg);
            Bitmap bitmap = new Bitmap(stream);
            bitmap.Save(@"C:\Users\93281\Pictures\" + Guid.NewGuid() + "." +quality + ".jpg", ImageFormat.Jpeg);
            Assert.Pass();
        }
    }
}