using Assignment.Core;
using Assignment.Core.Data;
using Assignment.Core.Domain;
using Assignment.Core.Service;
using Assignment.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Assignment.Services
{
    public class TaskService : ITaskService
    {
        #region Fields
        private readonly IRepository<Task> _taskRepository;
        private readonly IRepository<UserTask> _userTaskRepository;
        #endregion

        #region Ctor
        //TODO:Remove the constructor after using Ioc and Data referance
        public TaskService()
        {
            AssignmentDbContext context = new AssignmentDbContext();
            _taskRepository = new EfRepository<Task>(context);
            _userTaskRepository = new EfRepository<UserTask>(context);
        }

        public TaskService(IRepository<Task> taskRepository,
            IRepository<UserTask> userTaskRepository)
        {
            _taskRepository = taskRepository;
            _userTaskRepository = userTaskRepository;
        }
        #endregion

        #region Task
        public void CreateTask(Task task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            task.Status = TaskStatus.Saved;
            _taskRepository.Insert(task);
            _taskRepository.Save();
        }
        public IPagedList<Task> GetAllTasks(TaskStatus[] status = null,
            bool loadWithAssigners = false,
            int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var resultSet = _taskRepository.Table;
            if (status != null && status.Length > 0)
                resultSet.Where(t => t.Status != null && status.Contains(t.Status.Value));
            if (loadWithAssigners)
                resultSet = resultSet.Include(t => t.AssignedToUsers);
            return new PagedList<Task>(resultSet.OrderBy(t=>t.Id), pageIndex, pageSize);
        }
        public Task GetTaskById(int id, bool includeAssigners = false)
        {
            if (!includeAssigners)
                return _taskRepository.GetById(id);

            return _taskRepository.Table.Where(t => t.Id == id).Include(t => t.AssignedToUsers).FirstOrDefault();
        }
        public void UpdateTask(Task task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            _taskRepository.Update(task);
            _taskRepository.Save();
        }
        #endregion

        #region User task
        public void AssignTaskToUsers(Task task, IEnumerable<User> users)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (users == null)
                throw new ArgumentNullException("users");
            foreach (var user in users)
            {
                _userTaskRepository.Insert(new UserTask() { UserId = user.Id, TaskId = task.Id });
            }
            _userTaskRepository.Save();
            task.Status = TaskStatus.Assigned;
            _taskRepository.Update(task);
            _taskRepository.Save();
        }
        public void RemoveAssignersFromTask(Task task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            var userTasks = _userTaskRepository.Table.Where(ut=>ut.TaskId == task.Id);
            if (userTasks != null)
            {
                _userTaskRepository.Delete(userTasks);
                _userTaskRepository.Save();
            }
        }
        public List<UserTask> GetUserTasksForUser(int userId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            fromDate = fromDate.HasValue ? fromDate : DateTime.Today;
            toDate = toDate.HasValue ? toDate : DateTime.Today.AddDays(7);
            var resultSet = from ut in _userTaskRepository.Table
                            join t in _taskRepository.Table on ut.TaskId equals t.Id
                            where ut.UserId == userId && t.ExpBeginDate >= fromDate && t.ExpEndDate < toDate
                            select ut;
            return resultSet.ToList();
        }
        #endregion
    }
}
