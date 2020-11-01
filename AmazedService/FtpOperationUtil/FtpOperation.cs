using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AmazedService.FtpOperationUtil
{
    /// <summary>
    /// ftp操作类
    /// </summary>
    public static class FtpOperation
    {
        /// <summary>
        /// 获取FTP客户端
        /// </summary>
        /// <returns></returns>
        public static FtpClient GetFtpClient(FtpStratupParam param)
        {
            return new FtpClient(param.Host, param.UserName, param.Password);
        }
        /// <summary>
        /// 获取指定路径下的所有的文件夹
        /// </summary>
        /// <param name="ftpClient">ftp客户端</param>
        /// <param name="path">路径</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public static  IList<FtpListItem> GetFtpDirectories(FtpClient ftpClient, string path, bool isRecursion)
        {
            return GetFtpListItems(ftpClient, path, isRecursion).Where(entity => entity.Type == FtpFileSystemObjectType.Directory).ToList();
        }
        /// <summary>
        /// 获取路径下的所有的文件夹
        /// </summary>
        /// <param name="ftpClient">ftp客户端</param>
        /// <param name="path">路径</param>
        /// <param name="dirCond">目录过滤条件</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public static  IList<FtpListItem> GetFtpDirectories(FtpClient ftpClient, string path, Func<FtpListItem, bool> dirCond, bool isRecursion)
        {
            var result = GetFtpListItems(ftpClient, path, dirCond, isRecursion).Where(entity => entity.Type == FtpFileSystemObjectType.Directory).ToList();
            return result;
        }
        /// <summary>
        /// 获取路径下的所有的文件(不包括目录)
        /// </summary>
        /// <param name="ftpClient">ftp客户端</param>
        /// <param name="path">路径</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public static  IList<FtpListItem> GetFtpFiles(FtpClient ftpClient, string path, bool isRecursion)
        {
            return GetFtpListItems(ftpClient, path, isRecursion).Where(entity => entity.Type == FtpFileSystemObjectType.File).ToList();
        }

        /// <summary>
        /// 获取路径下的所有的文件(不包括目录)
        /// </summary>
        /// <param name="ftpClient">ftp客户端</param>
        /// <param name="path">路径</param>
        /// <param name="dirCond">目录过滤条件</param>
        /// <param name="fileCond">文件过滤条件</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public static  IList<FtpListItem> GetFtpFiles(FtpClient ftpClient, string path, Func<FtpListItem, bool> dirCond, Func<FtpListItem, bool> fileCond, bool isRecursion)
        {
            var result = GetFtpListItems(ftpClient, path, dirCond, isRecursion).Where(entity => entity.Type == FtpFileSystemObjectType.File).Where(fileCond).ToList();
            return result;
        }

        /// <summary>
        /// 获取路径下的所有的文件(包括目录)
        /// </summary>
        /// <param name="ftpClient">ftp客户端</param>
        /// <param name="path">路径</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public static  IList<FtpListItem> GetFtpListItems(FtpClient ftpClient, string path, bool isRecursion)
        {
            var result = GetFtpListItems(ftpClient, path, null, isRecursion);
            return result;
        }

        /// <summary>
        /// 获取路径下的所有的文件(包括目录)
        /// </summary>
        /// <param name="ftpClient">ftp客户端</param>
        /// <param name="path">路径</param>
        /// <param name="dirCond">目录过滤条件</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public static  IList<FtpListItem> GetFtpFilesDirectory(FtpClient ftpClient, string path, Func<FtpListItem, bool> dirCond, bool isRecursion)
        {
            var result = GetFtpListItems(ftpClient, path, dirCond, isRecursion);
            return result;
        }

         private static IList<FtpListItem> GetFtpListItems(FtpClient ftpClient, string path, Func<FtpListItem, bool> dirCond, bool isRecursion)
        {
            var ftpListItems = ftpClient.GetListing(path);
            var result = new List<FtpListItem>(ftpListItems);
            IEnumerable<FtpListItem> directories = ftpListItems.Where(entity => entity.Type == FtpFileSystemObjectType.Directory);
            if (dirCond != null)
            {
                directories = directories.Where(dirCond);
            }
            if (isRecursion)
            {
                foreach (var ftpListItem in directories)
                {
                    result.AddRange(GetFtpListItems(ftpClient, ftpListItem.FullName, dirCond, isRecursion));
                }
            }
            return result;
        }
    }
}
