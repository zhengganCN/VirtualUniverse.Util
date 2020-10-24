using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazedService.FtpOperationUtil
{
    /// <summary>
    /// ftp操作类
    /// </summary>
    public static class FtpOperation
    {
        /// <summary>
        /// 递归获取根路径下的所有的文件夹
        /// </summary>
        /// <param name="param">启动参数</param>
        /// <param name="path">根路径</param>
        /// <returns></returns>
        public static IList<FtpListItem> GetFtpDirectories(FtpStratupParam param, string path)
        {
            return GetFtpListItems(param, path).Where(entity => entity.Type == FtpFileSystemObjectType.Directory).ToList();
        }
        /// <summary>
        /// 递归获取根路径下的所有的文件(不包括目录)
        /// </summary>
        /// <param name="param">启动参数</param>
        /// <param name="path">根路径</param>
        /// <returns></returns>
        public static IList<FtpListItem> GetFtpFiles(FtpStratupParam param, string path)
        {
            return GetFtpListItems(param, path).Where(entity => entity.Type == FtpFileSystemObjectType.File).ToList();
        }

        /// <summary>
        /// 递归获取根路径下的所有的文件(包括目录)
        /// </summary>
        /// <param name="param">启动参数</param>
        /// <param name="path">根路径</param>
        /// <returns></returns>
        public static IList<FtpListItem> GetFtpListItems(FtpStratupParam param, string path)
        {
            FtpClient ftpClient = new FtpClient(param.Host, param.UserName, param.Password);
            return GetFtpListItems(ftpClient, path);
        }

        private static IList<FtpListItem> GetFtpListItems(FtpClient ftpClient, string path)
        {
            var ftpListItems = ftpClient.GetListing(path);
            var result = new List<FtpListItem>(ftpListItems);
            foreach (var ftpListItem in ftpListItems.Where(entity => entity.Type == FtpFileSystemObjectType.Directory))
            {
                result.AddRange(GetFtpListItems(ftpClient, ftpListItem.FullName));
            }
            return result;
        }
    }
}
