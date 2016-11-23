using Assignment.Core.Domain;
using System.Collections.Generic;

namespace Assignment.Core.Service
{
    public partial interface IUserService
    {
        #region User
        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUserById(int userId);

        /// <summary>
        /// 根据account获取用户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        User GetUserByAccount(string account);

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="userStatus"></param>
        /// <param name="loadWithGroups"></param>
        /// <param name="loadWithTasks"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<User> GetAllUsers(string account = null,
            UserStatus? userStatus = null,
            bool loadWithGroups = false,
            bool loadWithTasks = false,
            int pageIndex = 1, int pageSize = int.MaxValue);

        /// <summary>
        /// 获取激活用户
        /// </summary>
        /// <returns></returns>
        List<User> GetActiveUsers();

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        void CreateUser(User user);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        void UpdateUser(User user);

        /// <summary>
        /// 验证用户密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool ValidatePsd(User user, string password);

        /// <summary>
        /// 判断用户是否为管理员
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool IsUserAdmin(User user);
        #endregion

        #region Group
        /// <summary>
        /// 创建组
        /// </summary>
        /// <param name="group"></param>
        void CreateGroup(Group group);

        /// <summary>
        /// 根据group code获取组信息
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        Group GetGroupByCode(string groupCode);
        #endregion

        #region User group
        /// <summary>
        /// 将用户加入指定的用户组
        /// </summary>
        /// <param name="user"></param>
        /// <param name="groupCode"></param>
        void AddUserToGroup(User user, string groupCode);

        /// <summary>
        /// 将用户从指定的用户组移除
        /// </summary>
        /// <param name="user"></param>
        /// <param name="groupCode"></param>
        void RemoveUserFromGroup(User user, string groupCode);
        #endregion
    }
}
