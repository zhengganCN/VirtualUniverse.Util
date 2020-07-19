using AmazedFileCompression;
using NUnit.Framework;
using System.IO.Compression;

namespace NUnitTestAmazedFileCompression
{
    public class TestGZipCompression
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(@"C:\Users\93281\Pictures\Width1920Height1080\708e9e2a-3a75-484a-b890-45e37c6a2184.jpg")]
        public void TestFileCompression(string path)
        {
            GZipCompression gZipCompression = new GZipCompression();
            var gZipParamterModel = new GZipCompressParamterModel();
            gZipCompression.Compression(path, gZipParamterModel);
        }

        [Test]
        [TestCase(@"C:\Users\93281\Pictures\Width1920Height1080\708e9e2a-3a75-484a-b890-45e37c6a2184.jpg.gz")]
        public void TestFileDecompression(string path)
        {
            GZipCompression gZipCompression = new GZipCompression();
            var gZipDecompressParamter = new GZipDecompressParamterModel
            {
                DecompressdDirectoryPath = @"C:\Users\93281\Desktop"
            };
            gZipCompression.Decompress(path, gZipDecompressParamter);
        }
    }
}