using Assignment.Core;
using Assignment.Core.Domain;
using Assignment.Services;
using Assignment.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Assignment.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private static readonly List<SelectListItem> priorities = new List<SelectListItem>()
        {
            new SelectListItem { Text = Task.GetPriorityText(TaskPriority.Highest), Value = TaskPriority.Highest.ToString() },
            new SelectListItem { Text = Task.GetPriorityText(TaskPriority.Higher), Value = TaskPriority.Higher.ToString() },
            new SelectListItem { Text = Task.GetPriorityText(TaskPriority.High), Value = TaskPriority.High.ToString() },
            new SelectListItem { Text = Task.GetPriorityText(TaskPriority.Middle), Value = TaskPriority.Middle.ToString() },
            new SelectListItem { Text = Task.GetPriorityText(TaskPriority.Low), Value = TaskPriority.Low.ToString() },
            new SelectListItem { Text = Task.GetPriorityText(TaskPriority.Lower), Value = TaskPriority.Lower.ToString() },
            new SelectListItem { Text = Task.GetPriorityText(TaskPriority.Lowest), Value = TaskPriority.Lowest.ToString() },
        };

        public static List<SelectListItem> GetMembersSelectList()
        {
            UserService userService = new UserService();
            var users = userService.GetActiveUsers();
            List<SelectListItem> selections = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "无",Value = "NULL"}
            };
            if (users != null)
            {
                foreach (User user in users)
                {
                    selections.Add(new SelectListItem() { Text = user.UserName, Value = user.Id.ToString() });
                }
            }
            return selections;
        }

        // GET: Task
        public ActionResult Index()
        {
            int curUserId = Convert.ToInt32(User.Identity.GetUserId());

            UserService userService = new UserService();
            IPagedList<Task> tasks = null;
            ViewBag.IsAdmin = false;
            if (userService.IsUserAdmin(new User() { Id = curUserId })) ;
            {
                ViewBag.IsAdmin = true;
                tasks = new TaskService().GetAllTasks(loadWithAssigners:true);
            }

            return View(tasks);
        }

        public ActionResult Add()
        {
            AddTaskViewModel task = new AddTaskViewModel();
            ViewBag.Priorities = priorities;
            ViewBag.Users = GetMembersSelectList();

            return View(task);
        }

        [HttpPost]
        public ActionResult Add(AddTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Priorities = priorities;
                ViewBag.Users = GetMembersSelectList();
                return View(model);
            }
            if (model.ExpBeginDate > model.ExpEndDate)
            {
                ModelState.AddModelError("ExpEndDate", "期望结束日期应该晚于期望开始日期");
                ViewBag.Priorities = priorities;
                ViewBag.Users = GetMembersSelectList();
                return View(model);
            }

            Task task = new Task();
            task.Number = model.Number;
            task.Name = model.Name;
            task.ExpBeginDate = model.ExpBeginDate.Value;
            task.ExpEndDate = model.ExpEndDate.Value;
            task.Priority = model.Priority;
            task.Description = model.Desription;
            TaskService rep = new TaskService();
            rep.CreateTask(task);

            List<User> assignedToUsers = null;
            int assignedUserId;
            if (model.AssignedTo != "NULL" && int.TryParse(model.AssignedTo, out assignedUserId))
            {
                assignedToUsers = new List<User>() { new User() { Id = assignedUserId } };
                rep.AssignTaskToUsers(task, assignedToUsers);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Show(int? id)
        {
            EditTaskViewModel model = null;

            TaskService taskRep = new TaskService();
            Task task = id.HasValue ? taskRep.GetTaskById(id.Value, includeAssigners: true) : null;
            if (task != null)
            {
                model = new EditTaskViewModel()
                {
                    Id = task.Id,
                    Status = task.Status.Value,
                    Name = task.Name,
                    Number = task.Number,
                    Priority = task.Priority.Value,
                    ExpBeginDate = task.ExpBeginDate,
                    ExpEndDate = task.ExpEndDate,
                    AssignedTo = task.AssignedToUsers == null ? "NULL" : task.AssignedToUsers.FirstOrDefault().UserId.ToString(),
                    Desription = task.Description
                };
                ViewBag.IsAdmin = new UserService().IsUserAdmin(new User() { Id = User.Identity.GetUserId<int>() });
                ViewBag.Priorities = priorities;
                ViewBag.Users = GetMembersSelectList();
            }
            return View(model);
        }

        public ActionResult Update(EditTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Priorities = priorities;
                ViewBag.Users = GetMembersSelectList();
                return View(model);
            }
            if (model.ExpBeginDate > model.ExpEndDate)
            {
                ModelState.AddModelError("ExpEndDate", "期望结束日期应该晚于期望开始日期");
                ViewBag.Priorities = priorities;
                ViewBag.Users = GetMembersSelectList();
                return View(model);
            }

            TaskService rep = new TaskService();
            Task task = rep.GetTaskById(model.Id);
            task.Name = model.Name;
            task.ExpBeginDate = model.ExpBeginDate.Value;
            task.ExpEndDate = model.ExpEndDate.Value;
            task.Priority = model.Priority;

            List<User> assignedToUsers = null;
            int assignedUserId;
            if (model.AssignedTo != "NULL" && int.TryParse(model.AssignedTo, out assignedUserId))
            {
                assignedToUsers = new List<User>() { new User() { Id = assignedUserId } };
                rep.RemoveAssignersFromTask(task);
                rep.AssignTaskToUsers(task, assignedToUsers);
            }
            if (assignedToUsers == null)
            {
                task.BeginDate = null;
                task.EndDate = null;
            }
            task.Description = model.Desription;
            rep.UpdateTask(task);

            return RedirectToAction("Index");
        }
    }
}