using Assignment.Core.Domain;
using Assignment.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Assignment.Web.Controllers
{
    [Authorize]
    public class AssignmentController : Controller
    {
        // GET: Assignment
        public ActionResult Index(string from)
        {
            int curUserId = Convert.ToInt32(User.Identity.GetUserId());
            DateTime fromDate,endDate;//起始日期和结束日期
            DateTime.TryParse(from,out fromDate);
            fromDate = fromDate == null || fromDate < DateTime.Today ? DateTime.Today : fromDate;
            endDate = fromDate.AddDays(7);

            UserService userRep = new UserService();

            List<User> usersWithTasks;
            ViewBag.IsAdmin = false;
            if (userRep.IsUserAdmin(new User() { Id = curUserId }))
            {
                ViewBag.IsAdmin = true;
                usersWithTasks = userRep.GetActiveUsers();
            }
            else
                usersWithTasks = new List<User>() { userRep.GetUserById(curUserId) };

            TaskService tService = new TaskService();
            foreach (var user in usersWithTasks)
            {
                user.AssignedTasks = tService.GetUserTasksForUser(user.Id, fromDate: fromDate, toDate: endDate);
            }
            ViewBag.FromDate = fromDate;
            var r = usersWithTasks.ToList();
            return View(usersWithTasks);
        }

        public ActionResult Details(int? userId)
        {
            //当前登录用户
            int curUserId = Convert.ToInt32(User.Identity.GetUserId());

            UserService userRep = new UserService();
            TaskService taskRep = new TaskService();
            User queryUser = userId.HasValue ? userRep.GetUserById(userId.Value) : null;
            List<Task> tasks = null;
            if (queryUser != null)
            {
                ViewBag.IsOwner = queryUser.Id == curUserId;
                ViewBag.IsAdmin = userRep.IsUserAdmin(new Core.Domain.User() { Id = curUserId });
                if (ViewBag.IsOwner || ViewBag.IsAdmin)
                {
                    //tasks = taskRep.GetWithTasks(userId.Value);
                }
            }
            ViewBag.QueryUser = queryUser;
            return View(tasks);
        }

        //public ActionResult ProcessTask(string userId, long taskId, string act)
        //{
            //if (act == "P" || act == "E")
            //{
            //    ITaskRepository taskRep = new TaskRepository();
            //    Task task = taskRep.FindById(taskId);

            //    if (task == null)
            //        return View((object)("任务没有找到，任务Id:" + taskId.ToString()));
            //    if (act == "P" && task.Status != TaskStatus.Assigned)
            //        return View((object)("任务当前状态不允许执行，任务状态:" + task.StatusText));
            //    else if (act == "E" && task.Status != TaskStatus.Processing)
            //        return View((object)("任务当前状态不允许结束，任务状态:" + task.StatusText));
            //    if (User.Identity.GetUserId() != task.AssignedUserId.ToString())
            //        return View((object)("当前没有权限执行该任务。"));

            //    if (act == "P")
            //    {
            //        task.Status = TaskStatus.Processing;
            //        task.BeginDate = DateTime.Now;
            //    }
            //    else
            //    {
            //        task.Status = TaskStatus.Finished;
            //        task.EndDate = DateTime.Now;
            //    }
            //    taskRep.Update(task);

            //    return RedirectToAction("Details", routeValues: new { userId = userId });
            //}
            //else
            //    return View((object)("执行指令错误，请指定：P(执行)或者E(结束)。"));
        //}
    }
}