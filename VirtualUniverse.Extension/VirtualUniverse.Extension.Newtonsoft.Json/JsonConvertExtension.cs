using Newtonsoft.Json;
using System;
using VirtualUniverse.Extension.Newtonsoft.Json.ContractResolvers;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/8 17:41:29；更新时间：
************************************************************************************/
namespace VirtualUniverse.Extension.Newtonsoft.Json
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    public static class JsonConvertExtension
    {
        /// <summary>
        /// 排除或保留特定字段后序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="paramsObject">要保留或排除的参数对象</param>
        /// <param name="retain">true:表示props是需要保留的字段  false：表示props是要排除的字段</param>
        /// <param name="settings">序列化设置</param>
        /// <returns></returns>
        public static string SerializeObject<T>(T value, Func<T, object> paramsObject, bool retain = false, JsonSerializerSettings settings = null)
        {
            if (settings is null)
            {
                settings = new JsonSerializerSettings();
            }
            var properties = paramsObject.Invoke(value).GetType().GetProperties();
            var props = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                props[i] = properties[i].Name;
            }
            settings.ContractResolver = new LimitPropsContractResolver(props, retain);
            return JsonConvert.SerializeObject(value, settings);
        }
        /// <summary>
        /// 排除或保留特定字段后序列化对象
        /// </summary>
        /// <param name="value">要序列化的对象</param>
        /// <param name="propertyNames">要保留或排除的属性名</param>
        /// <param name="retain">true:表示props是需要保留的字段  false：表示props是要排除的字段</param>
        /// <param name="settings">序列化设置</param>
        /// <returns></returns>
        public static string SerializeObject(object value, string[] propertyNames, bool retain = false, JsonSerializerSettings settings = null)
        {
            if (settings is null)
            {
                settings = new JsonSerializerSettings();
            }
            settings.ContractResolver = new LimitPropsContractResolver(propertyNames, retain);
            return JsonConvert.SerializeObject(value, settings);
        }
    }
}
