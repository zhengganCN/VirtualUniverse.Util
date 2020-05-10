using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedMP3InfoReader
{
    /// <summary>
    /// ID3V1信息
    /// </summary>
    public class MP3_ID3V1
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 歌手
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// 专集
        /// </summary>
        public string Album { get; set; }
        /// <summary>
        /// 出品年代
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public string Reserve { get; set; }
        /// <summary>
        /// 音轨
        /// </summary>
        public string Track { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Genre { get; set; }
    }
}
