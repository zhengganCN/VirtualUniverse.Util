using AmazedID3Analysis.MP3Tag;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedID3Analysis
{
    /// <summary>
    /// ID3V2信息
    /// </summary>
    public class MP3_ID3V2
    {
        /// <summary>
        /// 标签头
        /// </summary>
        public MP3TagHeader MP3TagHeader { get; set; }
        /// <summary>
        /// 标签帧
        /// </summary>
        public List<MP3TagFrame> MP3TagFrames { get; set; }
       // /// <summary>
       // /// 版本号;ID3V2.3就记录03,ID3V2.4就记录04
       // /// </summary>
       // public string Version { get; set; }
       // /// <summary>
       // /// 副版本号;此版本记录为00
       // /// </summary>
       // public string Revision { get; set; }
       // /// <summary>
       // /// 存放标志的字节，这个版本只定义了三位
       // /// </summary>
       // public string Flag { get; set; }
       // /// <summary>
       // /// 标签大小，包括标签帧和标签头。（不包括扩展标签头的10个字节）
       // /// </summary>
       // public int Size { get; set; }
       ///// <summary>
       ///// 作者
       ///// </summary>
       // public string Author { get; set; }
       // /// <summary>
       // /// 专集
       // /// </summary>
       // public string Album { get; set; }
       // /// <summary>
       // /// 音轨
       // /// </summary>
       // public string Track { get; set; }
       // /// <summary>
       // /// 年代
       // /// </summary>
       // public int Year { get; set; }
       // /// <summary>
       // /// 类型
       // /// </summary>
       // public string Type { get; set; }
       // /// <summary>
       // /// 备注
       // /// </summary>
       // public string Remarks { get; set; }
       // /// <summary>
       // /// 音频时长
       // /// </summary>
       // public int Time { get; set; }
         
    }
}
