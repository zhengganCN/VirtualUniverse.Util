using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AmazedMath.MathException
{
    /// <summary>
    /// 随机数范围异常
    /// </summary>
    public class RandomRangeException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public RandomRangeException(string message) : base(message)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public RandomRangeException()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public RandomRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public override IDictionary Data => base.Data;
        /// <summary>
        /// 
        /// </summary>
        public override string HelpLink { get => base.HelpLink; set => base.HelpLink = value; }
        /// <summary>
        /// 
        /// </summary>
        public override string Message => base.Message;
        /// <summary>
        /// 
        /// </summary>
        public override string Source { get => base.Source; set => base.Source = value; }
        /// <summary>
        /// 
        /// </summary>
        public override string StackTrace => base.StackTrace;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Exception GetBaseException()
        {
            return base.GetBaseException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
