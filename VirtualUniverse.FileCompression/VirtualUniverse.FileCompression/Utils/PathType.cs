using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VirtualUniverse.FileCompression.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/5/5 22:47:34；更新时间：
************************************************************************************/
namespace VirtualUniverse.FileCompression.Utils
{
    /// <summary>
    /// 类 描 述：路径类型
    /// </summary>
    internal static class PathType
    {   
        /// <summary>
        /// 获取路径指向的是文件还是目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static EnumPathType GetPathType(string path)
        {
            var pathType = EnumPathType.NoExits;
            if (Directory.Exists(path))
            {
                pathType = EnumPathType.DirectoryPath;
            }
            else if (File.Exists(path))
            {
                pathType = EnumPathType.FilePath;
            }
            return pathType;
        }
    }
}
