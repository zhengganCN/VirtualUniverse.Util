using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace AmazedFileCompression
{
    /// <summary>
    /// GZip压缩与解压缩（单文件压缩）
    /// </summary>
    public class GZipCompression
    {

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="gZipParamterModel"></param>
        /// <returns></returns>
        public bool Compression(string filePath,GZipCompressParamterModel gZipParamterModel)
        {
            var result = false;
            var pathType = GetPathType(filePath);
            switch (pathType)
            {
                case PathType.FilePath:
                    CompressionFile(filePath, gZipParamterModel);
                    result = true;
                    break;
                case PathType.DirectoryPath:
                    throw new ArgumentException($"参数{nameof(filePath)}不能为目录");
                case PathType.NoExits:
                    throw new ArgumentException($"参数{nameof(filePath)}所指向的文件不存在");
            }
            return result;
        }
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="gzipPath">gzip压缩文件路径</param>
        /// <param name="gZipDecompressParamter">GZip解压参数</param>
        /// <returns></returns>
        public bool Decompress(string gzipPath, GZipDecompressParamterModel gZipDecompressParamter)
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
        private void CompressionFile(string filePath,GZipCompressParamterModel gZipParamterModel)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            var path = GenerateOutputPath(filePath,gZipParamterModel);
            using FileStream originalFileStream = fileInfo.OpenRead();
            using FileStream compressedFileStream = File.Create(path);
            using GZipStream gZipStream = new GZipStream(compressedFileStream, gZipParamterModel.CompressionLevel);
            originalFileStream.CopyTo(gZipStream);
        }

        private string GenerateOutputPath(string filePath,GZipCompressParamterModel gZipParamterModel)
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
        private void DecompressFile(string gzipPath, string outputDirectory)
        {
            FileInfo fileInfo = new FileInfo(gzipPath);
            using FileStream fs = fileInfo.OpenRead();
            string fileName = fileInfo.FullName;
            string outputFileName = Path.Combine(outputDirectory, fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length));
            using FileStream decompressedFS = File.Create(outputFileName);
            using GZipStream decompressionStream = new GZipStream(fs, CompressionMode.Decompress);
            decompressionStream.CopyTo(decompressedFS);
        }

        /// <summary>
        /// 获取路径指向的是文件还是目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private PathType GetPathType(string path)
        {
            var pathType = PathType.NoExits;
            if (Directory.Exists(path))
            {
                pathType = PathType.DirectoryPath;
            }
            else if(File.Exists(path))
            {
                pathType = PathType.FilePath;
            }
            return pathType;
        }

        private enum PathType
        {
            [Description("文件路径")]
            FilePath=1,
            [Description("目录路径")]
            DirectoryPath=2,
            [Description("路径不存在")]
            NoExits=3
        }
    }
}
