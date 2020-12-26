using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualUniverse.ID3Analysis
{
    /// <summary>
    /// MP3信息
    /// </summary>
    public class MP3Info
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        public string Subtitle { get; set; }
        /// <summary>
        /// 分级
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 参与创作的艺术家
        /// </summary>
        public string ArtistsInvolvedInCreation { get; set; }
        /// <summary>
        /// 唱片集艺术家
        /// </summary>
        public string AlbumArtist { get; set; }
        /// <summary>
        /// 唱片集
        /// </summary>
        public string Album { get; set; }
        /// <summary>
        /// 年代
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// 音轨
        /// </summary>
        public string Track { get; set; }
        /// <summary>
        /// 歌曲图片的base64编码
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 音乐流派
        /// </summary>
        public string Genre { get; set; }
        /// <summary>
        /// 时长,单位秒
        /// </summary>
        public long Time { get; set; }
        /// <summary>
        /// 比特率
        /// </summary>
        public string BitRate { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// 编码人员
        /// </summary>
        public string Coder { get; set; }
        /// <summary>
        /// 作者url
        /// </summary>
        public string AuthorUrl { get; set; }
        /// <summary>
        /// 版权
        /// </summary>
        public string Copyright { get; set; }
        /// <summary>
        /// 作曲者
        /// </summary>
        public string Composer { get; set; }
        /// <summary>
        /// 指挥者
        /// </summary>
        public string Commander { get; set; }
        /// <summary>
        /// 组说明
        /// </summary>
        public string GroupDescription { get; set; }
        /// <summary>
        /// 氛围
        /// </summary>
        public string Atmosphere { get; set; }
        /// <summary>
        /// 部分设置
        /// </summary>
        public string PartSet { get; set; }
        /// <summary>
        /// 初始调性
        /// </summary>
        public string InitialTone { get; set; }
        /// <summary>
        /// 每分钟节拍数
        /// </summary>
        public string BeatsPerMinute { get; set; }
        /// <summary>
        /// MP3文件大小,单位byte
        /// </summary>
        public long Size { get; set; }
    }
}
