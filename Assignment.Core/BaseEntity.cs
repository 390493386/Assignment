using System;

namespace Assignment.Core
{
    public abstract class BaseEntity : BaseEntity<int>
    {
    }

    public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public BaseEntity()
        {
            Id = default(TKey);
            CreatedAt = LastUpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdatedAt { get; set; }
    }
}
