using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AmazedIdPool
{
    public static class IdPool
    {
        /// <summary>
        /// 对象锁
        /// </summary>
        private static readonly object ObjectLock = new object();
        /// <summary>
        /// 触发补充Id池的种子；当Id池中id的数量小于 IdPoolSize * Seed 时，触发补充Id操作
        /// </summary>
        public static float Seed { get; set; } = 0.2f;
        /// <summary>
        /// Id池大小
        /// </summary>
        public static int IdPoolSize { get; set; } = 10000;
        /// <summary>
        /// id队列
        /// </summary>
        private static readonly Queue<long> Ids = new Queue<long>(IdPoolSize);
        /// <summary>
        /// 是否正在生成id
        /// </summary>
        private static bool IsGenerateIds = false;
        /// <summary>
        /// 获取id池的第一个id，当id池不足时，通过匿名委托函数获取新生成的ID，补充Id池
        /// </summary>
        /// <param name="gainIdsFunc">当Id池中的Id数量不够，执行该匿名委托，该匿名委托用于生成id，补充id池的id数量</param>
        /// <returns></returns>
        public static long GainId(Func<List<long>> gainIdsFunc)
        {
            if (Seed > 1 && Seed <= 0)
            {
                throw new ArgumentException(nameof(Seed) + "属性不能大于1或小于等于0");
            }
            if (IdPoolSize < 1000)
            {
                throw new ArgumentException(nameof(IdPoolSize) + "属性不能小于1000");
            }
            if (Ids.Count <= Ids.Count * Seed && !IsGenerateIds)
            {
                Task.Run(() =>
                {
                    IsGenerateIds = true;
                    var newIds = gainIdsFunc.Invoke();
                    foreach (var newId in newIds)
                    {
                        Ids.Enqueue(newId);
                    }
                    IsGenerateIds = false;
                });
            }
            lock (ObjectLock)
            {
                while (Ids.Count == 0) ;
                return Ids.Dequeue();
            }
        }
    }
}
