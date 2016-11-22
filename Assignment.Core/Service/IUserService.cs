using Assignment.Core.Domain;
using System.Collections.Generic;

namespace Assignment.Core.Service
{
    public partial interface IUserService
    {
        #region User
        User GetUserById(int userId);
        User GetUserByAccount(string account);
        IPagedList<User> GetAllUsers(string account = null,
            UserStatus? userStatus = null,
            bool loadWithGroups = false,
            bool loadWithTasks = false,
            int pageIndex = 1, int pageSize = int.MaxValue);
        List<User> GetActiveUsers();
        void CreateUser(User user);
        void UpdateUser(User user);
        bool ValidatePsd(User user, string password);
        bool IsUserAdmin(User user);
        #endregion

        #region Group
        void CreateGroup(Group group);
        Group GetGroupByCode(string groupCode);
        #endregion

        #region User group
        void AddUserToGroup(User user, string groupCode);
        void RemoveUserFromGroup(User user, string groupCode);
        #endregion
    }
}
