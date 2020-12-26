using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VirtualUniverse.ID3Analysis
{
    enum ID3V2FrameInfo
    {
        [Description("Audio encryption")]
        AENC,
        [Description("Attached picture")]
        APIC,
        [Description("Comments")]
        COMM,
        [Description("Commercial frame")]
        COMR,
        [Description("Encryption method registration")]
        ENCR,
        [Description("Equalization")]
        EQUA,
        [Description("Event timing codes")]
        ETCO,
        [Description("General encapsulated object")]
        GEOB,
        [Description("Group identification registration")]
        GRID,
        [Description("Involved people list")]
        IPLS,
        [Description("Linked information")]
        LINK,
        [Description("Music CD identifier")]
        MCDI,
        [Description("MPEG location lookup table")]
        MLLT,
        [Description("Ownership frame")]
        OWNE,
        [Description("Private frame")]
        PRIV,
        [Description("Play counter")]
        PCNT,
        [Description("Popularimeter")]
        POPM,
        [Description("Position synchronisation frame")]
        POSS,
        [Description("Recommended buffer size")]
        RBUF,
        [Description("Relative volume adjustment")]
        RVAD,
        [Description("Reverb")]
        RVRB,
        [Description("Synchronized lyric/text")]
        SYLT,
        [Description("Synchronized tempo codes")]
        SYTC,
        [Description("Album/Movie/Show title")]
        TALB,
        [Description("BPM (beats per minute)")]
        TBPM,
        [Description("Composer")]
        TCOM,
        [Description("Content type")]
        TCON,
        [Description("Copyright message")]
        TCOP,
        [Description("Date")]
        TDAT,
        [Description("Playlist delay")]
        TDLY,
        [Description("Encoded by")]
        TENC,
        [Description("Lyricist/Text writer")]
        TEXT,
        [Description("File type")]
        TFLT,
        [Description("Time")]
        TIME,
        [Description("Content group description")]
        TIT1,
        [Description("Title/songname/content description")]
        TIT2,
        [Description("Subtitle/Description refinement")]
        TIT3,
        [Description("Initial key")]
        TKEY,
        [Description("Language(s)")]
        TLAN,
        [Description("Length")]
        TLEN,
        [Description("Media type")]
        TMED,
        [Description("Original album/movie/show title")]
        TOAL,
        [Description("Original filename")]
        TOFN,
        [Description("Original lyricist(s)/text writer(s)")]
        TOLY,
        [Description("Original artist(s)/performer(s)")]
        TOPE,
        [Description("Original release year")]
        TORY,
        [Description("File owner/licensee")]
        TOWN,
        [Description("Lead performer(s)/Soloist(s)")]
        TPE1,
        [Description("Band/orchestra/accompaniment")]
        TPE2,
        [Description("Conductor/performer refinement")]
        TPE3,
        [Description("Interpreted, remixed, or otherwise modified by")]
        TPE4,
        [Description("Part of a set")]
        TPOS,
        [Description("Publisher")]
        TPUB,
        [Description("Track number/Position in set")]
        TRCK,
        [Description("Recording dates")]
        TRDA,
        [Description("Internet radio station name")]
        TRSN,
        [Description("Internet radio station owner")]
        TRSO,
        [Description("Size")]
        TSIZ,
        [Description("ISRC (international standard recording code)")]
        TSRC,
        [Description("Software/Hardware and settings used for encoding")]
        TSSE,
        [Description("Year")]
        TYER,
        [Description("User defined text information frame")]
        TXXX,
        [Description("Unique file identifier")]
        UFID,
        [Description("Terms of use")]
        USER,
        [Description("Unsychronized lyric/text transcription")]
        USLT,
        [Description("Commercial information")]
        WCOM,
        [Description("Copyright/Legal information")]
        WCOP,
        [Description("Official audio file webpage")]
        WOAF,
        [Description("Official artist/performer webpage")]
        WOAR,
        [Description("Official audio source webpage")]
        WOAS,
        [Description("Official internet radio station homepage")]
        WORS,
        [Description("Payment")]
        WPAY,
        [Description("Publishers official webpage")]
        WPUB,
        [Description("User defined URL link frame")]
        WXXX,
        [Description("")]
        TCMP
    }
}
