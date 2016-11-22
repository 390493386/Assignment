using Assignment.Core.Domain;
using System;
using System.Collections.Generic;

namespace Assignment.Core.Service
{
    public partial interface ITaskService
    {
        #region Task
        void CreateTask(Task task);
        IPagedList<Task> GetAllTasks(TaskStatus[] status = null,
            bool loadWithAssigners = false,
            int pageIndex = 1, int pageSize = int.MaxValue);
        Task GetTaskById(int id, bool includeAssigners = false);
        void UpdateTask(Task task);
        #endregion

        #region User task
        void AssignTaskToUsers(Task task, IEnumerable<User> users);
        void RemoveAssignersFromTask(Task task);
        List<UserTask> GetUserTasksForUser(int userId, DateTime? fromDate = null, DateTime? toDate = null);
        #endregion
    }
}
