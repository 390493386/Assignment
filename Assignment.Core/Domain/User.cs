using System.Collections.Generic;

namespace Assignment.Core.Domain
{
    public enum UserSex
    {
        Secret,
        Male,
        Female
    };

    public enum UserStatus
    {
        Active,
        Locked,
        Deleted
    }

    public partial class User : BaseEntity
    {
        public User()
        {
            Sex = UserSex.Secret;
            Status = UserStatus.Active;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual UserSex? Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string Tel { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual UserStatus? Status { get; set; }

        /// <summary>
        /// 创建者id
        /// </summary>
        public virtual int? CreateId { get; set; }

        /// <summary>
        /// 所在用户组（多个）
        /// </summary>
        public virtual ICollection<UserGroup> InGroups { get; set; }

        /// <summary>
        /// 分配的任务
        /// </summary>
        public virtual ICollection<UserTask> AssignedTasks { get; set; }
    }
}
