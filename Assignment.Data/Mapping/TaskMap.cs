using Assignment.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Assignment.Data.Mapping
{
    public class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.Number).IsRequired().HasMaxLength(50);
            this.Property(t => t.Name).IsRequired().HasMaxLength(255);
            this.Property(t => t.Priority).IsRequired();
            this.Property(t => t.Status).IsRequired();
            this.Property(t => t.ExpBeginDate).IsRequired();
            this.Property(t => t.ExpEndDate).IsRequired();
            this.Property(t => t.Description).HasMaxLength(1024);

            this.ToTable("Task");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ExpBeginDate).HasColumnName("ExpBeginDate");
            this.Property(t => t.ExpEndDate).HasColumnName("ExpEndDate");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BeginDate).HasColumnName("BeginDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
        }
    }
}