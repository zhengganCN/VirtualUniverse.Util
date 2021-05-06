using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Text;
using VirtualUniverse.FileCompression.Models;
using VirtualUniverse.FileCompression.Utils;

namespace VirtualUniverse.FileCompression
{
    /// <summary>
    /// GZip压缩与解压缩（单文件压缩）
    /// </summary>
    public static class GZipCompression
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="path"></param>
        /// <param name="gZipCompressParamter"></param>
        /// <returns></returns>
        public static bool Compression(string path, GZipCompressParamter gZipCompressParamter)
        {
            var result = false;
            var pathType = PathType.GetPathType(path);
            switch (pathType)
            {
                case EnumPathType.FilePath:
                    CompressionFile(path, gZipCompressParamter);
                    result = true;
                    break;
                case EnumPathType.DirectoryPath:
                    throw new ArgumentException($"参数{nameof(path)}不能为目录");
                case EnumPathType.NoExits:
                    throw new ArgumentException($"参数{nameof(path)}所指向的文件不存在");
            }
            return result;
        }
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="gzipPath">gzip压缩文件路径</param>
        /// <param name="gZipDecompressParamter">GZip解压参数</param>
        /// <returns></returns>
        public static bool Decompress(string gzipPath, GZipDecompressParamter gZipDecompressParamter)
        {

            bool result;
            if (string.IsNullOrEmpty(gZipDecompressParamter.DecompressdDirectoryPath))
            {
                FileInfo fileInfo = new FileInfo(gzipPath);
                DecompressFile(gzipPath, fileInfo.Directory.FullName);
                result = true;
            }
            else
            {
                var existsDirectory = Directory.Exists(gZipDecompressParamter.DecompressdDirectoryPath);//判断是否存在输出目录
                if (existsDirectory)
                {
                    DecompressFile(gzipPath, gZipDecompressParamter.DecompressdDirectoryPath);
                    result = true;
                }
                else
                {
                    throw new ArgumentException($"参数{nameof(gZipDecompressParamter.DecompressdDirectoryPath)}的路径不存在");
                }
            }
            return result;
        }
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="gZipParamterModel"></param>
        private static void CompressionFile(string filePath, GZipCompressParamter gZipParamterModel)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            var path = GenerateOutputPath(filePath, gZipParamterModel);
            using FileStream originalFileStream = fileInfo.OpenRead();
            using FileStream compressedFileStream = File.Create(path);
            using GZipStream gZipStream = new GZipStream(compressedFileStream, gZipParamterModel.CompressionLevel);
            originalFileStream.CopyTo(gZipStream);
        }

        /// <summary>
        /// 生成输出路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="gZipParamterModel"></param>
        /// <returns></returns>
        private static string GenerateOutputPath(string filePath, GZipCompressParamter gZipParamterModel)
        {
            string extension = ".gz";
            string path;
            if (!string.IsNullOrEmpty(gZipParamterModel.CompressedFilePath))
            {
                path = gZipParamterModel.CompressedFilePath + extension;
            }
            else
            {
                path = filePath + extension;
            }
            return path;
        }
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="gzipPath"></param>
        /// <param name="outputDirectory"></param>
        private static void DecompressFile(string gzipPath, string outputDirectory)
        {
            FileInfo fileInfo = new FileInfo(gzipPath);
            using FileStream fs = fileInfo.OpenRead();
            string fileName = fileInfo.FullName;
            string outputFileName = Path.Combine(outputDirectory, fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length));
            using FileStream decompressedFS = File.Create(outputFileName);
            using GZipStream decompressionStream = new GZipStream(fs, CompressionMode.Decompress);
            decompressionStream.CopyTo(decompressedFS);
        }
    }
}
