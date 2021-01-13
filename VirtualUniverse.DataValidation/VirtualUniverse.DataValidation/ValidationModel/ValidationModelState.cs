using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace VirtualUniverse.DataValidation.ValidationModel
{
    /// <summary>
    /// 验证模型状态
    /// </summary>
    public class ValidationModelState
    {
        /// <summary>
        /// 要验证的模型，不能为空
        /// </summary>
        private readonly object _model;
        /// <summary>
        /// 验证模型状态
        /// </summary>
        /// <param name="model">要验证的模型，不能为空</param>
        public ValidationModelState([NotNull] object model)
        {
            _model = model;
            GetPropertyInfosIncludeChildrenPropertyInfos(_model);
        }
        /// <summary>
        /// 模型是否验证通过
        /// </summary>
        public bool IsValid { get; private set; } = true;
        /// <summary>
        /// 模型验证结果
        /// </summary>
        public IList<ValidationResult> ValidResult { get; private set; }

        /// <summary>
        /// 模型验证结果
        /// </summary>
        public IList<ValidationResult> ErrorValidResult { get; private set; }

        /// <summary>
        /// 序列化错误结果
        /// </summary>
        /// <returns></returns>
        public virtual string FormatValidResult()
        {
            return JsonConvert.SerializeObject(RecursionGetValidResult(ValidResult));
        }

        private Dictionary<string, object> RecursionGetValidResult(IList<ValidationResult> validResult)
        {
            var result = new Dictionary<string, object>();
            for (int i = 0; i < validResult.Count; i++)
            {
                if (validResult[i].FieldVerifyResult is List<string>)
                {
                    var errorMessages = validResult[i].FieldVerifyResult as List<string>;
                    if (errorMessages.Any())
                    {
                        result.Add(validResult[i].FieldName, errorMessages);
                    }
                }
                else if (validResult[i].FieldVerifyResult is IList<ValidationResult>)
                {
                    var tempValidResult = RecursionGetValidResult(validResult[i].FieldVerifyResult as IList<ValidationResult>);
                    foreach (var temp in tempValidResult)
                    {
                        result.Add(temp.Key, temp.Value);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns></returns>
        public bool VerifyModel()
        {
            ValidResult = GetPropertyInfosIncludeChildrenPropertyInfos(_model);
            return IsValid;
        }
        /// <summary>
        /// 获取对象的所有属性
        /// </summary>
        /// <param name="model">验证结果</param>
        /// <param name="prefix">字段前缀</param>
        /// <returns><see cref="ValidationResult"/></returns>
        private IList<ValidationResult> GetPropertyInfosIncludeChildrenPropertyInfos(object model, string prefix = null)
        {
            var result = new List<ValidationResult>();
            foreach (var propertyInfo in model.GetType().GetProperties())
            {
                var validationResult = DistinguishCustomType(model, propertyInfo, prefix);
                if (validationResult != null)
                {
                    result.Add(validationResult);
                }
            }
            return result;
        }
        /// <summary>
        /// 分类（值类型属性、字符串类型属性）和（自定义类型属性）
        /// </summary>
        /// <param name="model">包含属性的对象</param>
        /// <param name="propertyInfo">要验证的属性</param>
        /// <param name="prefix">字段前缀</param>
        /// <returns><see cref="ValidationResult"/></returns>
        private ValidationResult DistinguishCustomType(object model, PropertyInfo propertyInfo, string prefix = null)
        {
            ValidationResult result;
            if (propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType == typeof(IFormFile))//获取值类型属性和字符串属性信息
            {
                result = GetAttributeValidationResult(model, propertyInfo, prefix);
            }
            else
            {
                result = GetAttributeValidationResult(model, propertyInfo, prefix);
                var list = result.FieldVerifyResult as IEnumerable<object>;
                if (result.FieldVerifyResult == null || (list != null && !list.Any()))
                {
                    result.FieldName = propertyInfo.Name;
                    AttributeIsNotValueTypeOrIsEnumerable(model, propertyInfo, result);
                }
            }
            return result;
        }
        /// <summary>
        /// 判断非值类型属性属性是否继承了IEnumerable接口，是，则表示该属性是一个可枚举列表，否，则表示该属性是一个非值类型对象
        /// </summary>
        /// <param name="model">包含属性的对象</param>
        /// <param name="propertyInfo">要验证的属性</param>
        /// <param name="result">验证结果</param>
        /// <returns></returns>
        private void AttributeIsNotValueTypeOrIsEnumerable(object model, PropertyInfo propertyInfo, ValidationResult result)
        {
            var hasEnumerableInterface = propertyInfo.PropertyType.GetInterface(nameof(IEnumerable)) != null;
            if (hasEnumerableInterface)
            {
                HandleEnumrableAttribute(model, propertyInfo, result);
            }
            else
            {
                HandleNotValueTypeAttribute(model, propertyInfo, result);
            }
        }
        /// <summary>
        /// 处理属性为非值类型的属性
        /// </summary>
        /// <param name="model">包含属性的对象</param>
        /// <param name="propertyInfo">要验证的属性</param>
        /// <param name="result">验证结果</param>
        /// <returns></returns>
        private void HandleNotValueTypeAttribute(object model, PropertyInfo propertyInfo, ValidationResult result)
        {
            var value = propertyInfo.GetValue(model);
            if (value != null)
            {
                foreach (var property in propertyInfo.PropertyType.GetProperties())
                {
                    DistinguishCustomType(model, property);
                    var verify = GetPropertyInfosIncludeChildrenPropertyInfos(property);
                    result.FieldName = propertyInfo.Name;
                    result.FieldVerifyResult = verify;
                }
            }
        }

        /// <summary>
        /// 处理属性类型为可枚举的属性
        /// </summary>
        /// <param name="model">包含属性的对象</param>
        /// <param name="propertyInfo">要验证的属性</param>
        /// <param name="result">验证结果</param>
        private void HandleEnumrableAttribute(object model, PropertyInfo propertyInfo, ValidationResult result)
        {
            var listValue = propertyInfo.GetValue(model);
            if (listValue != null)
            {
                var validResultList = new List<ValidationResult>();
                var fgfg = listValue as IEnumerable<object>;
                var index = 0;
                foreach (var item in fgfg)
                {
                    var fieldName = $"{propertyInfo.Name}[{index}]";
                    validResultList.Add(new ValidationResult
                    {
                        FieldName = fieldName,
                        FieldVerifyResult = GetPropertyInfosIncludeChildrenPropertyInfos(item, fieldName)
                    });
                    index++;
                }
                result.FieldVerifyResult = validResultList;
            }
        }

        /// <summary>
        /// 获取属性的验证结果
        /// </summary>
        /// <param name="model">包含属性的对象</param>
        /// <param name="propertyInfo">要验证的属性</param>
        /// <param name="prefix">字段前缀</param>
        /// <returns><see cref="ValidationResult"/></returns>
        private ValidationResult GetAttributeValidationResult(object model, PropertyInfo propertyInfo, string prefix = null)
        {
            var fieldName = string.IsNullOrEmpty(prefix) ? propertyInfo.Name : prefix + "." + propertyInfo.Name;
            ValidationResult verify = new ValidationResult
            {
                FieldName = fieldName,
                FieldVerifyResult = null
            };
            var attributes = propertyInfo.GetCustomAttributes();
            var errors = new List<string>();
            foreach (var attribute in attributes)
            {
                var error = GetValidationAttributeResult(model, propertyInfo, attribute);
                if (!string.IsNullOrWhiteSpace(error))
                {
                    errors.Add(error);
                }
            }
            verify.FieldVerifyResult = errors;
            return verify;
        }
        /// <summary>
        /// 验证具有继承自<see cref="ValidationAttribute"/>特性的属性，并返回验证结果
        /// </summary>
        /// <param name="model">包含属性的对象</param>
        /// <param name="propertyInfo">要验证的属性</param>
        /// <param name="attribute">属性的特性</param>
        private string GetValidationAttributeResult(object model, PropertyInfo propertyInfo, Attribute attribute)
        {
            string result = string.Empty;
            var validationAttribute = (attribute as ValidationAttribute);//判断特性是否继承自验证特性
            if (validationAttribute != null)
            {
                var value = propertyInfo.GetValue(model);
                if (!validationAttribute.IsValid(value))
                {
                    if (string.IsNullOrEmpty(validationAttribute.ErrorMessage))
                    {
                        ValidationContext validationContext = new ValidationContext(propertyInfo)
                        {
                            DisplayName = propertyInfo.Name
                        };
                        result = validationAttribute.GetValidationResult(value, validationContext).ErrorMessage;
                    }
                    else
                    {
                        var displayName = propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name;
                        result = validationAttribute.FormatErrorMessage(displayName);
                    }
                    IsValid = false;
                }
            }
            return result;
        }
    }
}
