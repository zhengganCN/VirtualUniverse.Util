using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedID3Analysis.MP3Tag
{
    public class MP3TagFrame
    {
        public MP3FrameHeader MP3FrameHeader { get; set; }
        public MP3FrameContent MP3FrameContent { get; set; }
    }
}
