using VirtualUniverse.ID3Analysis.MP3Tag;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VirtualUniverse.ID3Analysis
{
    /// <summary>
    /// 
    /// </summary>
    public class MP3Reader
    {
        /// <summary>
        /// 
        /// </summary>
        public MP3Reader()
        { 
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// 获取MP3的ID3V1数据；返回值为NULL，表示无ID3V1数据
        /// </summary>
        /// <param name="path">mp3文件路径</param>
        /// <param name="charset">MP3元数据的编码类型</param>
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
                    Title = GetDecodedData(id3v1.Skip(3).Take(30).ToArray(), charset),
                    Artist = GetDecodedData(id3v1.Skip(33).Take(30).ToArray(), charset),
                    Album = GetDecodedData(id3v1.Skip(63).Take(30).ToArray(), charset),
                    Year = GetDecodedData(id3v1.Skip(93).Take(4).ToArray(), charset),
                    Comment = GetDecodedData(id3v1.Skip(97).Take(28).ToArray(), charset),
                    Reserve = GetDecodedData(id3v1.Skip(125).Take(1).ToArray(), charset),
                    Track = GetDecodedData(id3v1.Skip(126).Take(1).ToArray(), charset),
                    Genre = GetDecodedData(id3v1.Skip(127).Take(1).ToArray(), charset)
                };
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获取MP3的ID3V2数据；返回值为NULL，表示无ID3V2数据
        /// </summary>
        /// <param name="path">mp3文件路径</param>
        /// <returns></returns>
        public MP3_ID3V2 GetMP3_ID3V2(string path)
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
            var iD3V2 = new MP3_ID3V2
            {
                MP3TagHeader = new MP3TagHeader(),
                MP3TagFrames = new List<MP3TagFrame>()
            };
            if (GetHeaderId(fileStream, 0, 3) == iD3V2.MP3TagHeader.Header)
            {
                iD3V2.MP3TagHeader.Size = GetTagSize(GetByteData(fileStream, 6, 4));
                int offset = 10;
                var frameHeaderIds = Enum.GetNames(typeof(ID3V2FrameInfo));
                while (true)
                {
                    var frameHeaderId = GetHeaderId(fileStream, offset, 4);
                    //判断是否存在该帧的帧头
                    if (frameHeaderIds.Contains(frameHeaderId))
                    {
                        var header = new MP3FrameHeader
                        {
                            FrameID = frameHeaderId,
                            Size = GetFrameSize(GetByteData(fileStream, offset += 4, 4)),
                            Flags = GetByteData(fileStream, offset += 4, 2)
                        };
                        var content = new MP3FrameContent
                        {
                            FrameContent = DecodFrameData(frameHeaderId, fileStream, offset += 2, header.Size)
                        };
                        offset += header.Size;
                        iD3V2.MP3TagFrames.Add(new MP3TagFrame
                        {
                            MP3FrameHeader = header,
                            MP3FrameContent = content
                        });
                    }
                    if (offset >= iD3V2.MP3TagHeader.Size || Encoding.ASCII.GetBytes(frameHeaderId).Count(entity => entity == 0) == 4)
                    {
                        break;
                    }
                }
                return iD3V2;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public MP3Info GetMP3Info(string path)
        {
            var iD3V1 = GetMP3_ID3V1(path);
            var iD3V2 = GetMP3_ID3V2(path);
            var fileStream = File.OpenRead(path);
            var mp3 = new MP3Info
            {
                Picture = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.APIC))?.MP3FrameContent.FrameContent,
                Subtitle = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TIT3))?.MP3FrameContent.FrameContent,
                Time = ComputeMP3Time(fileStream, iD3V1, iD3V2),
                PartSet = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TIT3))?.MP3FrameContent.FrameContent,
                Album = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TALB))?.MP3FrameContent.FrameContent,
                AlbumArtist = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TOAL))?.MP3FrameContent.FrameContent,
                ArtistsInvolvedInCreation = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TPE1))?.MP3FrameContent.FrameContent,
                Atmosphere = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TDAT))?.MP3FrameContent.FrameContent,
                AuthorUrl = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.WXXX))?.MP3FrameContent.FrameContent,
                BeatsPerMinute = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TBPM))?.MP3FrameContent.FrameContent,
                BitRate = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TBPM))?.MP3FrameContent.FrameContent,
                Coder = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.ETCO))?.MP3FrameContent.FrameContent,
                Commander = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.COMM))?.MP3FrameContent.FrameContent,
                Composer = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TCOM))?.MP3FrameContent.FrameContent,
                Copyright = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TCOP))?.MP3FrameContent.FrameContent,
                Genre = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TCON))?.MP3FrameContent.FrameContent,
                GroupDescription = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.GRID))?.MP3FrameContent.FrameContent,
                InitialTone = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.LINK))?.MP3FrameContent.FrameContent,
                Publisher = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TPUB))?.MP3FrameContent.FrameContent,
                Level = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TLEN))?.MP3FrameContent.FrameContent,
                Remark = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.MCDI))?.MP3FrameContent.FrameContent,
                Title = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TIT2))?.MP3FrameContent.FrameContent,
                Track = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TRCK))?.MP3FrameContent.FrameContent,
                Year = iD3V2.MP3TagFrames.FirstOrDefault(entity => entity.MP3FrameHeader.FrameID == nameof(ID3V2FrameInfo.TYER))?.MP3FrameContent.FrameContent,
                Size = fileStream.Length
            };
            return mp3;
        }
        private long ComputeMP3Time(FileStream fileStream, MP3_ID3V1 iD3V1, MP3_ID3V2 iD3V2)
        {
            long radioFrameSize = fileStream.Length;
            if (iD3V1 != null)
            {
                radioFrameSize -= 128;
            }
            if (iD3V2 != null)
            {
                radioFrameSize -= (iD3V2.MP3TagHeader.Size + 10);
            }
            fileStream.Seek(iD3V2 != null ? (iD3V2.MP3TagHeader.Size + 10) : 0, SeekOrigin.Begin);
            var temp = new byte[radioFrameSize];
            fileStream.Read(temp, 0, (int)radioFrameSize);
            var flag = temp.Take(4).ToArray();
            string bit = "";
            foreach (var item in flag)
            {
                bit += Convert.ToString(item, 2).PadLeft(8, '0');
            }
            var bitRateBitString = bit.Substring(16, 4);
            int bitRate = 320;
            switch (bitRateBitString)
            {
                case "1110":
                    bitRate = 320;
                    break;
                case "1001":
                    bitRate = 128;
                    break;
            }
            return (radioFrameSize * 8) / (bitRate * 1000);
        }
        
        private string DecodFrameData(string frameHeaderId, FileStream fileStream, int offset, int count)
        {
            var result = "";
            switch (GetID3V2FrameContentType(frameHeaderId))
            {
                case ID3V2FrameContentType.TextTypeFrameContent:
                    result = GetTextData(fileStream, offset, count);
                    break;
                case ID3V2FrameContentType.ImageTypeFrameContent:
                    result = GetImageData(fileStream, offset, count);
                    break;
            }
            return result;
        }
        private string GetTextData(FileStream fileStream, int offset, int count)
        {
            var buffer = new byte[count];
            fileStream.Seek(offset, SeekOrigin.Begin);
            fileStream?.Read(buffer, 0, count);
            if (buffer[0] == 1)
            {
                buffer = buffer.Skip(1).Take(count - 1).ToArray();
            }
            string result;
            if (buffer[0] == 0)
            {
                result = Encoding.ASCII.GetString(buffer).Trim().Trim('\0');
            }
            else if (buffer.Length%2!=0)
            {
                result = Encoding.GetEncoding("GBK").GetString(buffer).Trim().Trim('\0');
            }
            else
            {
                result = Encoding.Unicode.GetString(buffer).Trim().Trim('\0');                
            }
            return result;
        }
        private string GetImageData(FileStream fileStream, int offset, int count)
        {
            var buffer = new byte[count];
            fileStream.Seek(offset, SeekOrigin.Begin);
            fileStream?.Read(buffer, 0, count);
            int index = 0;
            while (index < count)
            {
                if (buffer[index] == 0xFF && buffer[index + 1] == 0xD8)//判断是否是jpg格式图片
                {
                    break;
                }
                else if (buffer[index] == 0x89 && buffer[index + 1] == 0x50 && buffer[index + 2] == 0x4E && buffer[index + 3] == 0x47) //判断是否是png格式图片
                {
                    break;
                }
                index++;
            }
            buffer = buffer.Skip(index).Take(buffer.Length - index).ToArray();
            var base64 = Convert.ToBase64String(buffer);
            return base64;
        }
        private string GetHeaderId(FileStream fileStream, int offset, int count)
        {
            var buffer = new byte[count];
            fileStream.Seek(offset, SeekOrigin.Begin);
            fileStream?.Read(buffer, 0, count);
            return Encoding.ASCII.GetString(buffer);
        }
        private byte[] GetByteData(FileStream fileStream, int offset, int count)
        {
            var buffer =new byte[count];
            fileStream.Seek(offset, SeekOrigin.Begin);
            fileStream?.Read(buffer, 0, count);
            return buffer;
        }
        /// <summary>
        /// 获取标签大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private int GetTagSize(byte[] size)
        {
            return size[0] * 0x200000 + size[1] * 0x4000 + size[2] * 0x80 + size[3];
        }
        /// <summary>
        /// 获取帧大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private int GetFrameSize(byte[] size)
        {
            return size[0] * 0x1000000 + size[1] * 0x10000 + size[2] * 0x100 + size[3];
        }

        private string GetDecodedData(byte[] dataBytes,string charset)
        {
            return Encoding.GetEncoding(charset).GetString(dataBytes).TrimEnd('\0');
        }
        private ID3V2FrameContentType GetID3V2FrameContentType(string frameHeaderId)
        {
            var type = frameHeaderId switch
            {
                nameof(ID3V2FrameInfo.AENC) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.APIC) => ID3V2FrameContentType.ImageTypeFrameContent,
                nameof(ID3V2FrameInfo.COMM) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.COMR) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.ENCR) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.EQUA) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.ETCO) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.GEOB) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.GRID) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.IPLS) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.LINK) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.MCDI) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.MLLT) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.OWNE) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.PRIV) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.PCNT) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.POPM) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.POSS) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.RBUF) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.RVAD) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.RVRB) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.SYLT) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.SYTC) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TALB) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TBPM) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TCOM) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TCON) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TCOP) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TDAT) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TDLY) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TENC) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TEXT) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TFLT) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TIME) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TIT1) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TIT2) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TIT3) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TKEY) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TLAN) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TLEN) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TMED) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TOAL) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TOFN) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TOLY) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TOPE) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TORY) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TOWN) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TPE1) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TPE2) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TPE3) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TPE4) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TPOS) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TPUB) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TRCK) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TRDA) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TRSN) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TRSO) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TSIZ) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TSRC) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TSSE) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TYER) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TXXX) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.UFID) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.USER) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.USLT) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.WCOM) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.WCOP) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.WOAF) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.WOAR) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.WOAS) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.WORS) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.WPAY) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.WPUB) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.WXXX) => ID3V2FrameContentType.TextTypeFrameContent,
                nameof(ID3V2FrameInfo.TCMP) => ID3V2FrameContentType.TextTypeFrameContent,
                _ => ID3V2FrameContentType.TextTypeFrameContent,
            };
            return type;
        }

    }
}
