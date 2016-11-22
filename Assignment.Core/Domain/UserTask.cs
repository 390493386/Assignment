namespace Assignment.Core.Domain
{
    public partial class UserTask : BaseEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        public virtual int TaskId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public virtual Task Task { get; set; }
    }
}
