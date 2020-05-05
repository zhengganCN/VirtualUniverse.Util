using AmazedMP3InfoReader;
using NUnit.Framework;

namespace NUnitTestMP3InfoReader
{
    public class MP3ReaderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            MP3Reader mP3Reader = new MP3Reader();
            var iD3V1 = mP3Reader.GetMP3_ID3V1(@"E:\QMDownload\Ayo97 _ 周思涵 - 感谢你曾来过.mp3");
            Assert.IsNotNull(iD3V1);
        }
    }
}