using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/3/6 14:49:19；更新时间：
************************************************************************************/
namespace VirtualUniverse.FTP.FluentFTP
{
    /// <summary>
    /// 类说明：Ftp上下文
    /// </summary>
    public abstract class FtpContext : IDisposable
    {
        private bool disposedValue;

        public FtpClient FtpClient { get; private set; }
        private FtpConfigurationBuilder FtpConfigurationBuilder { get; set; } = new FtpConfigurationBuilder();
        protected FtpContext()
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            OnConfiguration(FtpConfigurationBuilder);
            FtpClient = InstantiationFtpClient();
        }
        /// <summary>
        /// 实例化FtpClient
        /// </summary>
        /// <returns></returns>
        private FtpClient InstantiationFtpClient()
        {
            FtpClient = new FtpClient
            {
                Host = FtpConfigurationBuilder.Host
            };
            if (FtpConfigurationBuilder.Port.HasValue)
            {
                FtpClient.Port = FtpConfigurationBuilder.Port.Value;
            }
            if (!string.IsNullOrWhiteSpace(FtpConfigurationBuilder.UserName) &&
                !string.IsNullOrWhiteSpace(FtpConfigurationBuilder.Password))
            {
                FtpClient.Credentials = new NetworkCredential(FtpConfigurationBuilder.UserName,
                    FtpConfigurationBuilder.Password);
            }
            return FtpClient;
        }
        /// <summary>
        /// ftp配置
        /// </summary>
        /// <param name="backgroundServiceBuilder"></param>
        protected abstract void OnConfiguration(FtpConfigurationBuilder backgroundServiceBuilder);

        /// <summary>
        /// 获取指定路径下的所有的文件夹
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public IList<FtpListItem> GetFtpDirectories(string path, bool isRecursion)
        {
            return GetFtpListItems(path, isRecursion).Where(entity => entity.Type == FtpFileSystemObjectType.Directory).ToList();
        }
        /// <summary>
        /// 获取路径下的所有的文件夹
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="dirCond">目录过滤条件</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public IList<FtpListItem> GetFtpDirectories(string path, Func<FtpListItem, bool> dirCond, bool isRecursion)
        {
            var result = GetFtpListItems(path, dirCond, isRecursion).Where(entity => entity.Type == FtpFileSystemObjectType.Directory).ToList();
            return result;
        }
        /// <summary>
        /// 获取路径下的所有的文件(不包括目录)
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public IList<FtpListItem> GetFtpFiles(string path, bool isRecursion)
        {
            return GetFtpListItems(path, isRecursion).Where(entity => entity.Type == FtpFileSystemObjectType.File).ToList();
        }

        /// <summary>
        /// 获取路径下的所有的文件(不包括目录)
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="dirCond">目录过滤条件</param>
        /// <param name="fileCond">文件过滤条件</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public IList<FtpListItem> GetFtpFiles(string path, Func<FtpListItem, bool> dirCond, Func<FtpListItem, bool> fileCond, bool isRecursion)
        {
            var result = GetFtpListItems(path, dirCond, isRecursion).Where(entity => entity.Type == FtpFileSystemObjectType.File).Where(fileCond).ToList();
            return result;
        }
        /// <summary>
        /// 获取路径下的所有的文件(包括目录)
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public IList<FtpListItem> GetFtpListItems(string path, bool isRecursion)
        {
            var result = GetFtpListItems(path, null, isRecursion);
            return result;
        }

        /// <summary>
        /// 获取路径下的所有的文件(包括目录)
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="dirCond">目录过滤条件</param>
        /// <param name="isRecursion">是否递归获取</param>
        /// <returns></returns>
        public IList<FtpListItem> GetFtpFilesDirectory(string path, Func<FtpListItem, bool> dirCond, bool isRecursion)
        {
            var result = GetFtpListItems(path, dirCond, isRecursion);
            return result;
        }

        private IList<FtpListItem> GetFtpListItems(string path, Func<FtpListItem, bool> dirCond, bool isRecursion)
        {
            var ftpListItems = FtpClient.GetListing(path);
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
                    result.AddRange(GetFtpListItems(ftpListItem.FullName, dirCond, isRecursion));
                }
            }
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // 释放托管状态(托管对象)
                }
                FtpClient.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
