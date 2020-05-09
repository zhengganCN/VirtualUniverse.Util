using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedMP3InfoReader
{
    public class MP3Info
    {
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 专集
        /// </summary>
        public string Album { get; set; }
        /// <summary>
        /// 音轨
        /// </summary>
        public string Track { get; set; }
        /// <summary>
        /// 年代
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 音频时长
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 歌手
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public string Reserve { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Genre { get; set; }
    }
}
