using Assignment.Core;
using Assignment.Core.Data;
using Assignment.Core.Domain;
using Assignment.Core.Service;
using Assignment.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Assignment.Services
{
    public partial class UserService : IUserService
    {
        #region Fields
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<UserGroup> _userGroupRepository;
        #endregion

        #region Ctor
        //TODO:Remove the constructor after using Ioc and Data referance
        public UserService()
        {
            AssignmentDbContext context = new AssignmentDbContext();
            _userRepository = new EfRepository<User>(context);
            _groupRepository = new EfRepository<Group>(context);
            _userGroupRepository = new EfRepository<UserGroup>(context); ;
        }

        public UserService(IRepository<User> userRepository,
            IRepository<Group> groupRepository,
            IRepository<UserGroup> userGroupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _userGroupRepository = userGroupRepository;
        }
        #endregion

        #region Methods

        #region User
        public User GetUserById(int userId)
        {
            if (userId == 0)
                return null;

            return _userRepository.GetById(userId);
        }
        public User GetUserByAccount(string account)
        {
            if (String.IsNullOrWhiteSpace(account))
                return null;

            return _userRepository.Table.FirstOrDefault(u => u.Account == account);
        }
        public IPagedList<User> GetAllUsers(string account = null,
            UserStatus? userStatus = null,
            bool loadWithGroups = false,
            bool loadWithTasks = false,
            int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var resultSet = _userRepository.Table;

            if (!String.IsNullOrWhiteSpace(account))
                resultSet = resultSet.Where(u => u.Account == account);
            if (userStatus.HasValue)
                resultSet = resultSet.Where(u => u.Status == userStatus);
            if (loadWithGroups)
                resultSet = resultSet.Include(u => u.InGroups);
            if (loadWithTasks)
                resultSet = resultSet.Include(u => u.AssignedTasks);

            return new PagedList<User>(resultSet, pageIndex, pageSize);
        }
        public List<User> GetActiveUsers()
        {
            return _userRepository.Table.Where(u => u.Status == UserStatus.Active).ToList();
        }
        public void CreateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            _userRepository.Insert(user);
            _userRepository.Save();
        }
        public void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            _userRepository.Insert(user);
            _userRepository.Save();
        }
        public bool ValidatePsd(User user, string password)
        {
            return user != null && user.Password == password ? true : false;
        }
        public bool IsUserAdmin(User user)
        {
            if (user == null || user.Id == 0)
                return false;
            var userGroup = from ug in _userGroupRepository.Table
                            join g in _groupRepository.Table on ug.GroupId equals g.Id
                            where ug.UserId == user.Id && g.Code == "admin"
                            select ug;
            if (userGroup != null && userGroup.Count() > 0)
                return true;

            return false;
        }
        #endregion

        #region Group
        public void CreateGroup(Group group)
        {
            if (group == null)
                throw new ArgumentNullException("group");

            _groupRepository.Insert(group);
            _groupRepository.Save();
        }
        public Group GetGroupByCode(string groupCode)
        {
            if (String.IsNullOrWhiteSpace(groupCode))
                return null;

            return _groupRepository.Table.FirstOrDefault(g => g.Code == groupCode);
        }
        #endregion

        #region User group
        public void AddUserToGroup(User user, string groupCode)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (String.IsNullOrWhiteSpace(groupCode))
                throw new ArgumentException("groupCode");
            var group = GetGroupByCode(groupCode);
            if (group == null)
                throw new InvalidOperationException(String.Format("Group(code:{0}) not exists.", groupCode));
            _userGroupRepository.Insert(new UserGroup() { UserId = user.Id, GroupId = group.Id });
            _userGroupRepository.Save();
        }
        public void RemoveUserFromGroup(User user, string groupCode)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (String.IsNullOrWhiteSpace(groupCode))
                throw new ArgumentException("groupCode");
            var group = GetGroupByCode(groupCode);
            if (group == null)
                throw new InvalidOperationException(String.Format("Group(code:{0}) not exists.", groupCode));
            var userGroup = _userGroupRepository.Table.FirstOrDefault(ug => ug.UserId == user.Id && ug.GroupId == group.Id);
            _userGroupRepository.Delete(userGroup);
            _userGroupRepository.Save();
        }
        #endregion

        #endregion
    }
}
