using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedMP3InfoReader.MP3Tag
{
    public class MP3TagHeader
    {
        /// <summary>
        /// 必须为"ID3"否则认为标签不存在
        /// </summary>
        public string Header { get => "ID3"; }
        /// <summary>
        /// 版本号;ID3V2.3就记录03,ID3V2.4就记录04
        /// </summary>
        public string Ver { get; set; }
        /// <summary>
        /// 副版本号;此版本记录为00
        /// </summary>
        public string Revision { get; set; }
        /// <summary>
        /// 存放标志的字节，这个版本只定义了三位
        /// </summary>
        public string Flag { get; set; }
        /// <summary>
        /// 标签大小，包括标签帧和标签头。（不包括扩展标签头的10个字节）
        /// </summary>
        public int Size { get; set; }
    }
}
