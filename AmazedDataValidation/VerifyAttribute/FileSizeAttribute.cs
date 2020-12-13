﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmazedDataValidation.VerifyAttribute
{
    /// <summary>
    /// 文件大小验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileSizeAttribute : ValidationAttribute
    {
        /// <summary>
        /// 文件大小
        /// </summary>
        public int Size = 1000;
        /// <summary>
        /// 大小单位，默认值为KB
        /// </summary>
        public EnumSizeUnit Unit = EnumSizeUnit.UnitKB;
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
                return ValidFileSize(file);
            }
            else if (value is IFormFileCollection)
            {
                foreach (var file in value as IFormFileCollection)
                {
                    if (!ValidFileSize(file))
                    {
                        return false;
                    }
                }
                return true;
            }
            return true;
        }
        /// <summary>
        /// 验证文件大小是否符合要求
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidFileSize(IFormFile file)
        {
            var maxBytes = 0;
            switch (Unit)
            {
                case EnumSizeUnit.UnitByte:
                    maxBytes = Size;
                    break;
                case EnumSizeUnit.UnitKB:
                    maxBytes = Size * 1024;
                    break;
                case EnumSizeUnit.UnitMB:
                    maxBytes = Size * 1024 * 1024;
                    break;
                default:
                    break;
            }
            return file.Length <= maxBytes;
        }

        /// <summary>
        /// 文件大小单位枚举
        /// </summary>
        public enum EnumSizeUnit
        {
            /// <summary>
            /// Byte
            /// </summary>
            UnitByte = 1,
            /// <summary>
            /// KB
            /// </summary>
            UnitKB = 2,
            /// <summary>
            /// MB
            /// </summary>
            UnitMB = 3
        }
    }
}