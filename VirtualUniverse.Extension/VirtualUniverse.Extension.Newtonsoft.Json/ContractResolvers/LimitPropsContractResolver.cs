using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/8 17:45:30；更新时间：
************************************************************************************/
namespace VirtualUniverse.Extension.Newtonsoft.Json.ContractResolvers
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class LimitPropsContractResolver : DefaultContractResolver
    {
        readonly string[] props = null;

        readonly bool retain;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="props">传入的属性数组</param>
        /// <param name="retain">true:表示props是需要保留的字段  false：表示props是要排除的字段</param>
        public LimitPropsContractResolver(string[] props, bool retain = true)
        {
            //指定要序列化属性的清单
            this.props = props;
            this.retain = retain;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
            //只保留清单有列出的属性
            return list.Where(p =>
            {
                if (retain)
                {
                    return props.Contains(p.PropertyName);
                }
                else
                {
                    return !props.Contains(p.PropertyName);
                }
            }).ToList();
        }
    }
}
