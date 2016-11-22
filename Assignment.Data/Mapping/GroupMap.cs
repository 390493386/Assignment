using Assignment.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Assignment.Data.Mapping
{
    public class GroupMap : EntityTypeConfiguration<Group>
    {
        public GroupMap()
        {
            this.HasKey(g => g.Id);

            this.Property(g => g.Code)
                .IsRequired()
                .HasMaxLength(30);
            this.Property(g=>g.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.ToTable("Group");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}