using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AmazedMP3InfoReader
{
    public class MP3Reader
    {
        public MP3Reader()
        { 
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// 获取MP3的ID3V1数据；返回值为NULL，表示无ID3V1数据
        /// </summary>
        /// <param name="path">mp3文件路径</param>
        /// <remarks></remarks>
        /// <returns></returns>
        public MP3_ID3V1 GetMP3_ID3V1(string path,string charset="GBK")
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("路径不能为空字符串或为NULL");
            }
            if (!File.Exists(path))
            {
                throw new ArgumentException("文件不存在");
            }
            var fileStream = File.OpenRead(path);
            var id3v1 = new byte[128];
            fileStream.Position = fileStream.Length - 128;
            fileStream.Read(id3v1, 0, 128);
            if (Encoding.UTF8.GetString(id3v1.Take(3).ToArray()) == "TAG")
            {
                return new MP3_ID3V1
                {
                    Title = Encoding.GetEncoding(charset).GetString(id3v1.Skip(3).Take(30).ToArray()).TrimEnd('\0'),
                    Artist = Encoding.GetEncoding(charset).GetString(id3v1.Skip(33).Take(30).ToArray()).TrimEnd('\0'),
                    Album = Encoding.GetEncoding(charset).GetString(id3v1.Skip(63).Take(30).ToArray()).TrimEnd('\0'),
                    Year = Encoding.GetEncoding(charset).GetString(id3v1.Skip(93).Take(4).ToArray()).TrimEnd('\0'),
                    Comment = Encoding.GetEncoding(charset).GetString(id3v1.Skip(97).Take(28).ToArray()).TrimEnd('\0'),
                    Reserve = Encoding.GetEncoding(charset).GetString(id3v1.Skip(125).Take(1).ToArray()).TrimEnd('\0'),
                    Track = Encoding.GetEncoding(charset).GetString(id3v1.Skip(126).Take(1).ToArray()).TrimEnd('\0'),
                    Genre = Encoding.GetEncoding(charset).GetString(id3v1.Skip(127).Take(1).ToArray()).TrimEnd('\0'),
                };
            }
            else
            {
                return null;
            }
        }
    }
}
