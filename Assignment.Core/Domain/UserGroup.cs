namespace Assignment.Core.Domain
{
    public partial class UserGroup : BaseEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// 组id
        /// </summary>
        public virtual int GroupId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 组
        /// </summary>
        public virtual Group Group { get; set; }
    }
}
