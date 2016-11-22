using System.Collections.Generic;

namespace Assignment.Core.Domain
{
    /// <summary>
    /// Group
    /// </summary>
    public partial class Group : BaseEntity
    {
        /// <summary>
        /// 组代码，唯一标识
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 组内用户
        /// </summary>
        public virtual ICollection<UserGroup> Users { get; set; }
    }
}
