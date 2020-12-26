using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.Algorithm.MathComputer.Services;

namespace VirtualUniverse.Algorithm.MathComputer
{
    /// <summary>
    /// 数学工具类
    /// </summary>
    public static class MathUtil
    {
        /// <summary>
        /// 生成整型随机数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="isContainerMinValue">是否包含最小值</param>
        /// <param name="isContainerMaxValue">是否包含最大值</param>
        /// <returns></returns>
        public static int Random(int minValue, int maxValue, bool isContainerMinValue = true, bool isContainerMaxValue = true)
        {
            var randomNumberService=new RandomNumberService();
            return randomNumberService.GenerateRandom(minValue, maxValue, isContainerMinValue, isContainerMaxValue);
        }
        /// <summary>
        /// 生成双精度浮点型随机数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="isContainerMinValue">是否包含最小值</param>
        /// <param name="isContainerMaxValue">是否包含最大值</param>
        /// <returns></returns>
        public static double Random(double minValue, double maxValue, bool isContainerMinValue = true, bool isContainerMaxValue = true)
        {
            var randomNumberService = new RandomNumberService();
            return randomNumberService.GenerateRandom(minValue, maxValue, isContainerMinValue, isContainerMaxValue);
        }
    }
}
