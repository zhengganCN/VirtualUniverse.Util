using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace VirtualUniverse.DirectoryOperation.Operations
{
    /// <summary>
    /// 目录操作
    /// </summary>
    public static class DirectoryOperation
    {
        /// <summary>
        /// 获取文件夹路径
        /// </summary>
        /// <returns></returns>
        public static IList<string> ReadDirectories(string path)
        {
            var directoryPaths = Directory.GetDirectories(path);
            return GetDirectories(directoryPaths);
        }
        /// <summary>
        /// 获取文件夹路径
        /// </summary>
        /// <param name="path">根目录</param>
        /// <param name="deep">递归深度；当为null时或小于等于0时，不递归目录</param>
        /// <returns></returns>
        public static IList<string> ReadDirectories(string path, int? deep = null)
        {
            var directoryPaths = Directory.GetDirectories(path);
            return GetDirectories(directoryPaths, deep);
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
                var childDirectoryPaths = ReadDirectories(directoryPath, deep);
                allOfDirectoryPaths.AddRange(childDirectoryPaths);
            }
            return allOfDirectoryPaths;
        }
        /// <summary>
        /// 获取文件夹路径
        /// </summary>
        /// <param name="path">根目录</param>
        /// <param name="predicate">过滤条件</param>
        /// <returns></returns>
        public static IList<string> ReadDirectories(string path, Func<string, bool> predicate)
        {
            var directoryPaths = Directory.GetDirectories(path).Where(predicate);
            return GetDirectories(directoryPaths.ToList());
        }
        /// <summary>
        /// 获取文件夹路径
        /// </summary>
        /// <param name="path">根目录</param>
        /// <param name="deep">递归深度；当为null时或小于等于0时，不递归目录</param>
        /// <param name="predicate">过滤条件</param>
        /// <returns></returns>
        public static IList<string> ReadDirectories(string path, int deep, Func<string, bool> predicate)
        {
            var directoryPaths = Directory.GetDirectories(path).Where(predicate);
            return GetDirectories(directoryPaths.ToList(), deep);
        }
    }
}
