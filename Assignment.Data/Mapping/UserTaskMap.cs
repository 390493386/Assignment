using Assignment.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Assignment.Data.Mapping
{
    public class UserTaskMap : EntityTypeConfiguration<UserTask>
    {
        public UserTaskMap()
        {
            this.HasKey(ut => ut.Id);

            this.ToTable("UserTask");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.TaskId).HasColumnName("TaskId");

            //用户
            this.HasRequired(ut => ut.User).WithMany(u => u.AssignedTasks).HasForeignKey(ut => ut.UserId);
            //任务
            this.HasRequired(ut => ut.Task).WithMany(t => t.AssignedToUsers).HasForeignKey(ut => ut.TaskId);
        }
    }
}