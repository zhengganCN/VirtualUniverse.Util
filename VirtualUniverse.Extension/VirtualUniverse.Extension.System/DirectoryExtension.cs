using System;
using System.Collections.Generic;
using System.IO;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/24 23:37:37；更新时间：
************************************************************************************/
namespace VirtualUniverse.Extension.System
{
    /// <summary>
    /// 类说明：目录扩展
    /// </summary>
    public static class DirectoryExtension
    {
        /// <summary>
        /// 获取文件夹路径
        /// </summary>
        /// <param name="path">根目录</param>
        /// <param name="deep">递归深度；当为null时或小于等于0时，不递归目录</param>
        /// <returns></returns>
        public static IList<string> GetDirectories(string path, int? deep = null)
        {
            var directoryPaths = Directory.GetDirectories(path);
            return GetDirectories(directoryPaths, deep);
        }

        /// <summary>
        /// 递归获取目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IList<string> GetDirectoriesByRecursive(string path)
        {
            var paths = new List<string>();
            var directories = Directory.GetDirectories(path);
            paths.AddRange(directories);
            foreach (var directory in directories)
            {
                paths.AddRange(GetDirectoriesByRecursive(directory));
            }
            return paths;
        }

       
        /// <summary>
        /// 获取文件夹路径
        /// </summary>
        /// <param name="directoryPaths">目录路径</param>
        /// <param name="deep">递归深度；当为null时或小于等于0时，不递归目录</param>
        /// <returns></returns>
        private static List<string> GetDirectories(IList<string> directoryPaths, int? deep = null)
        {
            var allOfDirectoryPaths = new List<string>();
            allOfDirectoryPaths.AddRange(directoryPaths);
            if (deep.HasValue && --deep < 0)
            {
                return allOfDirectoryPaths;
            }
            foreach (var directoryPath in directoryPaths)
            {
                var childDirectoryPaths = GetDirectories(directoryPath, deep);
                allOfDirectoryPaths.AddRange(childDirectoryPaths);
            }
            return allOfDirectoryPaths;
        }
    }
}
