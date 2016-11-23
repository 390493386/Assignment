using Assignment.Core.Domain;
using System;
using System.Collections.Generic;

namespace Assignment.Core.Service
{
    public partial interface ITaskService
    {
        #region Task
        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="task"></param>
        void CreateTask(Task task);

        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <param name="status"></param>
        /// <param name="loadWithAssigners"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<Task> GetAllTasks(TaskStatus[] status = null,
            bool loadWithAssigners = false,
            int pageIndex = 1, int pageSize = int.MaxValue);

        /// <summary>
        /// 根据id获取任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeAssigners"></param>
        /// <returns></returns>
        Task GetTaskById(int id, bool includeAssigners = false);

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="task"></param>
        void UpdateTask(Task task);
        #endregion

        #region User task
        /// <summary>
        /// 把任务分配给用户
        /// </summary>
        /// <param name="task"></param>
        /// <param name="users"></param>
        void AssignTaskToUsers(Task task, IEnumerable<User> users);

        /// <summary>
        /// 将任务分配人从任务移除
        /// </summary>
        /// <param name="task"></param>
        void RemoveAssignersFromTask(Task task);

        /// <summary>
        /// 获取指定人员的任务
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        List<UserTask> GetUserTasksForUser(int userId, DateTime? fromDate = null, DateTime? toDate = null);
        #endregion
    }
}
