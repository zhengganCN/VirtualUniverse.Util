using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedID3Analysis.MP3Tag
{
    /// <summary>
    /// 帧头
    /// </summary>
    public class MP3FrameHeader
    {
        /// <summary>
        /// 用四个字符标识一个帧
        /// </summary>
        public string FrameID { get; set; }
        /// <summary>
        /// 帧内容的大小，不包括帧头，不得小于1
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 存放标志，只定义了6位
        /// </summary>
        public byte[] Flags { get; set; }
    }
}
