using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace VirtualUniverse.Algorithm.UniqueGenerate
{
    /// <summary>
    /// 雪花算法
    /// </summary>
    public static class SnowflakeAlgorithm
    {
        /// <summary>
        /// 机器ID
        /// </summary>
        private static long _workerId = 0L;
        /// <summary>
        /// 时间戳bit长度，最大支持使用从1970年至以后139年的时间
        /// </summary>
        private const int TimestampBitsLength = 42;
        /// <summary>
        /// 机器位bit长度，最大支持2048台机器使用
        /// </summary>
        private const int WorkerIdBitsLength = 11;
        /// <summary>
        /// 序列号bit长度，每毫秒最大支持生成1024个ID
        /// </summary>
        private const int SequenceBitsLength = 10;
        /// <summary>
        /// 互斥锁
        /// </summary>
        private static readonly Mutex mutex = new Mutex();
        /// <summary>
        /// 偏移时间，这是一个避免重复的随机量，自行设定，不要大于当前时间戳，该值确定后不能改变，否则可能会生成重复ID
        /// </summary>
        private static readonly DateTime OffsetTime=DateTime.Parse("2020/08/26 08:00:00.000");

        private static long Sequence = 0L;
        /// <summary>
        /// 最大机器ID
        /// </summary>
        private static readonly long MaxWorkerId = -1L ^ (-1L << WorkerIdBitsLength);
        /// <summary>
        /// 一毫秒内可以产生计数，如果达到该值则等到下一毫秒在进行生成
        /// </summary>
        private static readonly long MaxSequence = -1L ^ (-1L << SequenceBitsLength);
        /// <summary>
        /// 记录上一次生成时间戳的时间
        /// </summary>
        private static long LastTimestamp = -1L;
        /// <summary>
        /// 生成ID
        /// </summary>
        /// <param name="workerId">工作ID</param>
        /// <returns></returns>
        public static long GenerateId(long workerId)
        {
            mutex.WaitOne();
            _workerId = workerId;
            if (_workerId >= MaxWorkerId || _workerId < 0)
                throw new Exception(string.Format("机器ID不能小于0或机器ID不能大于等于最大机器ID", MaxWorkerId));
            long timestamp = TimeGen();
            if (LastTimestamp == timestamp)//同一毫秒中生成ID
            { //用&运算计算该毫秒内产生的计数是否已经到达上限
                if ((Sequence++) > MaxSequence)
                {
                    timestamp = NextMillis(LastTimestamp);//一毫秒内产生的ID计数已达上限，等待下一毫秒
                }
            }
            else//不同毫秒生成ID
            {
                Sequence = 0; //计数清0
            }
            if (timestamp < LastTimestamp)//如果当前时间戳比上一次生成ID时时间戳还小，抛出异常，因为不能保证现在生成的ID之前没有生成过
            {
                throw new Exception(string.Format("发生时钟回调，请刷新时钟；偏差时间为 {0} 毫秒，请等待 {0} 毫秒，或调整系统时间，把系统时间加上 {0} 毫秒", LastTimestamp - timestamp));
            }
            LastTimestamp = timestamp; //把当前时间戳保存为最后生成ID的时间戳
            long nextId = (timestamp << (WorkerIdBitsLength + SequenceBitsLength)) | _workerId << SequenceBitsLength | Sequence;
            mutex.ReleaseMutex();
            return nextId;
        }

        /// <summary>
        /// 获取下一毫秒时间戳
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private static long NextMillis(long lastTimestamp)
        {
            long timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns></returns>
        private static long TimeGen()
        {
            var ss = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() - new DateTimeOffset(OffsetTime).ToUnixTimeMilliseconds();
            return ss;
        }

    }
}
