using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public ValidationModelState(object model)
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
        public IList<AmazedValidationResult> ValidResult { get; private set; }
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
        /// <returns><see cref="AmazedValidationResult"/></returns>
        private IList<AmazedValidationResult> GetPropertyInfosIncludeChildrenPropertyInfos(object model)
        {
            var result = new List<AmazedValidationResult>();
            foreach (var propertyInfo in model.GetType().GetProperties())
            {
                var validationResult = DistinguishCustomType(model, propertyInfo);
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
        /// <returns><see cref="AmazedValidationResult"/></returns>
        private AmazedValidationResult DistinguishCustomType(object model, PropertyInfo propertyInfo)
        {
            AmazedValidationResult result;
            if (propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType == typeof(string))//获取值类型属性和字符串属性信息
            {
                result = GetAttributeValidationResult(model, propertyInfo);
            }
            else
            {
                result = GetAttributeValidationResult(model, propertyInfo);
                var list = result.FieldVerifyResult as IEnumerable<object>;
                if (result.FieldVerifyResult == null || (list != null && list.Count() == 0))
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
        private void AttributeIsNotValueTypeOrIsEnumerable(object model, PropertyInfo propertyInfo, AmazedValidationResult result)
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
        private void HandleNotValueTypeAttribute(object model, PropertyInfo propertyInfo, AmazedValidationResult result)
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
        private void HandleEnumrableAttribute(object model, PropertyInfo propertyInfo, AmazedValidationResult result)
        {
            var listValue = propertyInfo.GetValue(model);
            if (listValue != null)
            {
                var validResultList = new List<AmazedValidationResult>();
                var fgfg = listValue as IEnumerable<object>;
                var index = 0;
                foreach (var item in fgfg)
                {
                    validResultList.Add(new AmazedValidationResult
                    {
                        FieldName = index.ToString(),
                        FieldVerifyResult = GetPropertyInfosIncludeChildrenPropertyInfos(item)
                    });
                }
                result.FieldVerifyResult = validResultList;
            }
        }

        /// <summary>
        /// 获取属性的验证结果
        /// </summary>
        /// <param name="model">包含属性的对象</param>
        /// <param name="propertyInfo">要验证的属性</param>
        /// <returns><see cref="AmazedValidationResult"/></returns>
        private AmazedValidationResult GetAttributeValidationResult(object model, PropertyInfo propertyInfo)
        {
            AmazedValidationResult verify = new AmazedValidationResult
            {
                FieldName = propertyInfo.Name,
                FieldVerifyResult = null
            };
            var attributes = propertyInfo.GetCustomAttributes();
            foreach (var attribute in attributes)
            {
                verify.FieldVerifyResult = GetValidationAttributeResult(model, propertyInfo, attribute).ToList();
            }
            return verify;
        }
        /// <summary>
        /// 验证具有继承自<see cref="ValidationAttribute"/>特性的属性，并返回验证结果
        /// </summary>
        /// <param name="model">包含属性的对象</param>
        /// <param name="propertyInfo">要验证的属性</param>
        /// <param name="attribute">属性的特性</param>
        private IList<string> GetValidationAttributeResult(object model, PropertyInfo propertyInfo, Attribute attribute)
        {
            var result = new List<string>();
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
                        result.Add(validationAttribute.GetValidationResult(value, validationContext).ErrorMessage);
                    }
                    else
                    {
                        result.Add(validationAttribute.ErrorMessage);
                    }
                    IsValid = false;
                }
            }
            return result;
        }
    }
}
