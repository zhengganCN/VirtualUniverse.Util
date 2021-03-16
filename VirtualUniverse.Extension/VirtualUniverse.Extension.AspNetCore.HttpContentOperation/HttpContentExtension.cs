﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/3/6 20:57:07；更新时间：
************************************************************************************/
namespace VirtualUniverse.Extension.AspNetCore.HttpContentOperation
{
    /// <summary>
    /// 类说明：HttpContent扩展
    /// </summary>
    public static class HttpContentExtension
    {

        /// <summary>
        /// HttpContent组装
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="type">content-type</param>
        /// <returns><see cref="HttpContent"/></returns>
        public static HttpContent GainHttpContent(object data, EnumContentType type)
        {
            HttpContent content = null;
            switch (type)
            {
                case EnumContentType.ApplicationJson:
                    content = SendJsonContent(data);
                    break;
                case EnumContentType.MultipartFormData:
                    content = SetUpMultipartFormDataContent(data);
                    break;
            }
            return content;
        }

        private static HttpContent SetUpMultipartFormDataContent(object data)
        {
            var content = new MultipartFormDataContent();
            var properties = data.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(IFormFile))
                {
                    var file = property.GetValue(data) as IFormFile;
                    StreamContent stream = new StreamContent(file.OpenReadStream());
                    content.Add(stream, file.Name, file.FileName);
                }
                else if (property.PropertyType == typeof(IFormFileCollection))
                {
                    var files = property.GetValue(data) as IFormFileCollection;
                    foreach (var file in files)
                    {
                        StreamContent stream = new StreamContent(file.OpenReadStream());
                        content.Add(stream, file.Name, file.FileName);
                    }
                }
                else
                {
                    StringContent stream = new StringContent(property.GetValue(data) as string);
                    content.Add(stream, property.Name);
                }
            }
            return content;
        }

        private static HttpContent SendJsonContent(object data)
        {
            return JsonContent.Create(data);
        }
    }
}
