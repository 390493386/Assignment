using Assignment.Core.Domain;
using Assignment.Data.Mapping;
using System.Collections.Generic;
using System.Data.Entity;

namespace Assignment.Data
{
    public class AssignmentDbContext : DbContext
    {
        static AssignmentDbContext()
        {
            Database.SetInitializer<AssignmentDbContext>(new AssignmentDbInitializer());
        }

        public AssignmentDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new GroupMap());
            modelBuilder.Configurations.Add(new UserGroupMap());
            modelBuilder.Configurations.Add(new TaskMap());
            modelBuilder.Configurations.Add(new UserTaskMap());
        }
    }

    public class AssignmentDbInitializer : DropCreateDatabaseAlways<AssignmentDbContext>
    {
        protected override void Seed(AssignmentDbContext context)
        {
            InitializeData(context);
            base.Seed(context);
        }

        private void InitializeData(AssignmentDbContext context)
        {
            Group adminGroup = new Group()
            {
                Code = "admin",
                Name = "管理员"
            };
            Group memberGroup = new Group()
            {
                Code = "user",
                Name = "普通用户"
            };
            User admin = new User()
            {
                Account = "admin",
                UserName = "管理员",
                Password = "Admin@123"
            };
            //TODO:Ioc
            //IUserService uService = new UserService();
            ////uService.CreateUserWithGroups(admin, new List<Group> { adminGroup, memberGroup });
            context.Set<Group>().AddRange(new List<Group>() { adminGroup, memberGroup });
            context.Set<User>().Add(admin);
            context.SaveChanges();
            context.Set<UserGroup>().AddRange(new List<UserGroup>() { new UserGroup() { UserId = admin.Id, GroupId = adminGroup.Id }, new UserGroup() { UserId = admin.Id, GroupId = memberGroup.Id } });
            context.SaveChanges();
        }
    }
}
