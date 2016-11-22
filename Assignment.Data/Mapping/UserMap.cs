using Assignment.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Assignment.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.Account)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(255);

            this.ToTable("User");
            this.Property(t => t.Account).HasColumnName("Account");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Tel).HasColumnName("Tel");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateId).HasColumnName("CreateId");
        }
    }
}
