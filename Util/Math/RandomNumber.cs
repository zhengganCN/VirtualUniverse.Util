using System;
using System.Collections.Generic;
using System.Text;
using Util.Math.MathException;

namespace Util.Math
{
    /// <summary>
    /// 随机数
    /// </summary>
    public class RandomNumber
    {
        private readonly Random random;
        /// <summary>
        /// 构造函数
        /// </summary>
        public RandomNumber()
        {
           random= new Random();
        }
        
        /// <summary>
        /// 生成整型随机数
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="isContainerMinValue"></param>
        /// <param name="isContainerMaxValue"></param>
        /// <returns></returns>
        public int GenerateRandom(int minValue, int maxValue, bool isContainerMinValue = true, bool isContainerMaxValue = true)
        {
            CheckRandomRange(minValue, maxValue, isContainerMinValue, isContainerMaxValue);
            if (isContainerMinValue && isContainerMaxValue)
            {
                return random.Next(minValue, maxValue + 1);
            }
            else if (!isContainerMinValue && isContainerMaxValue)
            {
                return random.Next(minValue + 1, maxValue + 1);
            }
            else if (isContainerMinValue && !isContainerMaxValue)
            {
                return random.Next(minValue, maxValue);
            }
            else if (!isContainerMinValue && !isContainerMaxValue)
            {
                return random.Next(minValue + 1, maxValue);
            }
            return -1;
        }
        
        /// <summary>
        /// 验证minValue和maxValue的取值范围是否符合规范
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="isContainerMinValue"></param>
        /// <param name="isContainerMaxValue"></param>
        private void CheckRandomRange(int minValue, int maxValue, bool isContainerMinValue, bool isContainerMaxValue)
        {
            if (isContainerMinValue && isContainerMaxValue)
            {
                if (minValue > maxValue)
                {
                    throw (new RandomRangeException(RandomRangeExceptionInfoFormat(minValue,maxValue,isContainerMinValue,isContainerMaxValue, "的值必须小于等于")));
                }
            }
            else if (isContainerMinValue && !isContainerMaxValue)
            {
                if (minValue >= maxValue)
                {
                    throw (new RandomRangeException(RandomRangeExceptionInfoFormat(minValue, maxValue, isContainerMinValue, isContainerMaxValue, "的值必须小于")));
                }
            }
            else if (!isContainerMinValue && isContainerMaxValue)
            {
                if (minValue + 1 > maxValue)
                {
                    throw (new RandomRangeException(RandomRangeExceptionInfoFormat(minValue, maxValue, isContainerMinValue, isContainerMaxValue, "的值加一后必须小于等于")));
                }
            }
            else if (!isContainerMinValue && !isContainerMaxValue)
            {
                if (minValue + 1 >= maxValue)
                {
                    throw (new RandomRangeException(RandomRangeExceptionInfoFormat(minValue, maxValue, isContainerMinValue, isContainerMaxValue, "的值加一后必须小于")));
                }
            }
        }
        
        /// <summary>
        /// 格式化异常信息
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="isContainerMinValue"></param>
        /// <param name="isContainerMaxValue"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private string RandomRangeExceptionInfoFormat(int minValue, int maxValue, bool isContainerMinValue, bool isContainerMaxValue,string operation)
        {
            return string.Format(
                "当{0}为{1}时且当{2}为{3}时，{4}{5}{6}的值",
                nameof(isContainerMinValue),
                isContainerMinValue,
                nameof(isContainerMaxValue),
                isContainerMaxValue,
                nameof(minValue),
                operation,
                nameof(maxValue)
            );
        }
        
        /// <summary>
        /// 随机生成双精度浮点数
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="isContainerMinValue"></param>
        /// <param name="isContainerMaxValue"></param>
        /// <returns></returns>
        public double GenerateRandom(double minValue, double maxValue, bool isContainerMinValue = true, bool isContainerMaxValue = true)
        {
            CheckRandomRange(minValue, maxValue, isContainerMinValue, isContainerMaxValue);
            var number = random.NextDouble();
            return number * (maxValue - minValue) + minValue;
        }
        
        /// <summary>
        /// 验证minValue和maxValue的取值范围是否符合规范
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="isContainerMinValue"></param>
        /// <param name="isContainerMaxValue"></param>
        private void CheckRandomRange(double minValue, double maxValue, bool isContainerMinValue, bool isContainerMaxValue)
        {
            if (isContainerMinValue && isContainerMaxValue)
            {
                if (minValue > maxValue)
                {
                    throw (new RandomRangeException(RandomRangeExceptionInfoFormat(minValue, maxValue, isContainerMinValue, isContainerMaxValue, "的值必须小于等于")));
                }
            }
            else if (isContainerMinValue && !isContainerMaxValue)
            {
                if (minValue >= maxValue)
                {
                    throw (new RandomRangeException(RandomRangeExceptionInfoFormat(minValue, maxValue, isContainerMinValue, isContainerMaxValue, "的值必须小于")));
                }
            }
            else if (!isContainerMinValue && isContainerMaxValue)
            {
                if (minValue + 0.000000000000001 > maxValue)
                {
                    throw (new RandomRangeException(RandomRangeExceptionInfoFormat(minValue, maxValue, isContainerMinValue, isContainerMaxValue, "的值加0.000000000000001后必须小于等于")));
                }
            }
            else if (!isContainerMinValue && !isContainerMaxValue)
            {
                if (minValue + 0.000000000000001 >= maxValue)
                {
                    throw (new RandomRangeException(RandomRangeExceptionInfoFormat(minValue, maxValue, isContainerMinValue, isContainerMaxValue, "的值加0.000000000000001后必须小于")));
                }
            }
        }
        
        /// <summary>
        /// 格式化异常信息
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="isContainerMinValue"></param>
        /// <param name="isContainerMaxValue"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private string RandomRangeExceptionInfoFormat(double minValue, double maxValue, bool isContainerMinValue, bool isContainerMaxValue, string operation)
        {
            return string.Format(
                "当{0}为{1}时且当{2}为{3}时，{4}{5}{6}的值",
                nameof(isContainerMinValue),
                isContainerMinValue,
                nameof(isContainerMaxValue),
                isContainerMaxValue,
                nameof(minValue),
                operation,
                nameof(maxValue)
            );
        }       
    }
}
