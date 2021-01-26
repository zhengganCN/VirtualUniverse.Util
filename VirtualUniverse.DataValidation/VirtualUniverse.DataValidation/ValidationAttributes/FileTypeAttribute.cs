using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 文件类型验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class FileTypeAttribute : ValidationAttribute
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public string[] FileType { get; set; }
        /// <summary>
        /// 是否验证通过
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value is IFormFile)
            {
                var file = value as IFormFile;
                return ValidFileType(file);
            }
            else if (value is IFormFileCollection)
            {
                var files = value as IFormFileCollection;
                foreach (var file in files)
                {
                    if (!ValidFileType(file))
                    {
                        return false;
                    }
                }
                return true;
            }
            return true;
        }
        /// <summary>
        /// 验证文件类型
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidFileType(IFormFile file)
        {
            var splits = file.FileName.Split('.');
            if (splits.Length >= 2)
            {
                string fileType = splits[^1].ToUpper();
                if (FileType.Any(entity => entity.ToUpper() == fileType))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
