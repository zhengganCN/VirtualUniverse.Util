using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AmazedID3Analysis
{
    enum ID3V2FrameContentType
    {
        [Description("image type")]
        ImageTypeFrameContent,
        [Description("text type")]
        TextTypeFrameContent
    }
}
