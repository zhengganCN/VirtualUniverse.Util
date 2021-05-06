using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using VirtualUniverse.FileCompression.Models;
using VirtualUniverse.FileCompression.Utils;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/5/5 22:31:28；更新时间：
************************************************************************************/
namespace VirtualUniverse.FileCompression
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    public static class ZipCompression
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="zipCompressParamter">压缩参数</param>
        /// <returns></returns>
        public static bool Compression(string path, ZipCompressParamter zipCompressParamter)
        {
            var result = false;
            var pathType = PathType.GetPathType(path);
            switch (pathType)
            {
                case EnumPathType.FilePath:
                    CompressionFile(path, zipCompressParamter);
                    result = true;
                    break;
                case EnumPathType.DirectoryPath:
                    CompressionDirectory(path, zipCompressParamter);
                    break;
                case EnumPathType.NoExits:
                    throw new ArgumentException($"参数{nameof(path)}所指向的文件或目录不存在");
            }
            return result;
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="zipCompressParamter">压缩参数</param>
        private static void CompressionFile(string path, ZipCompressParamter zipCompressParamter)
        {
            FileInfo fileInfo = new FileInfo(path);
            var directory = Directory.CreateDirectory(fileInfo.FullName.Remove(fileInfo.FullName.Length - fileInfo.Extension.Length));
            var descPath = Path.Combine(fileInfo.DirectoryName, directory.Name, fileInfo.Name);
            File.Copy(path, descPath);
            var desPath = zipCompressParamter.CompressedFilePath + ".zip";
            ZipFile.CreateFromDirectory(directory.FullName, desPath, zipCompressParamter.CompressionLevel, zipCompressParamter.CreateDirectoryInCompressionFile);
            Directory.Delete(directory.FullName, true);
        }

        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="zipCompressParamter"></param>
        private static void CompressionDirectory(string directoryName, ZipCompressParamter zipCompressParamter)
        {
            var desPath = zipCompressParamter.CompressedFilePath + ".zip";
            ZipFile.CreateFromDirectory(directoryName, desPath, zipCompressParamter.CompressionLevel, zipCompressParamter.CreateDirectoryInCompressionFile);
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="zipPath">zip压缩文件路径</param>
        /// <param name="zipDecompressParamter">Zip解压参数</param>
        /// <returns></returns>
        public static void Decompress(string zipPath, ZipDecompressParamter zipDecompressParamter)
        {
            if (string.IsNullOrWhiteSpace(zipDecompressParamter.DecompressdDirectoryPath))
            {
                var fileInfo = new FileInfo(zipPath);
                var directoryPath = fileInfo.FullName.Remove(fileInfo.FullName.Length - fileInfo.Extension.Length);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                zipDecompressParamter.DecompressdDirectoryPath = directoryPath;
            }
            ZipFile.ExtractToDirectory(zipPath, zipDecompressParamter.DecompressdDirectoryPath);
        }
    }
}
