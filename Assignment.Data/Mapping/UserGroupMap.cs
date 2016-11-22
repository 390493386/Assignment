using Assignment.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Assignment.Data.Mapping
{
    public class UserGroupMap : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupMap()
        {
            this.HasKey(ug => ug.Id);

            this.ToTable("UserGroup");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.GroupId).HasColumnName("GroupId");

            //用户
            this.HasRequired(ug => ug.User).WithMany(u => u.InGroups).HasForeignKey(ug => ug.UserId);
            //组
            this.HasRequired(ug => ug.Group).WithMany(g => g.Users).HasForeignKey(ug => ug.GroupId);
        }
    }
}