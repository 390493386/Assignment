using System;
using System.Collections.Generic;

namespace Assignment.Core.Domain
{
    public enum TaskPriority
    {
        Highest, //最高
        Higher,  //较高
        High,    //高
        Middle,  //中等
        Low,     //低
        Lower,   //较低
        Lowest   //最低
    }

    public enum TaskStatus
    {
        Saved,      //已保存
        Assigned,   //已分配
        Processing, //正在执行
        Finished    //已完成
    }

    public partial class Task:BaseEntity
    {
        public Task()
        {
            this.Priority = TaskPriority.Middle;
            this.Status = TaskStatus.Saved;
        }

        /// <summary>
        /// 任务编号
        /// </summary>
        public virtual string Number { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 任务优先级
        /// </summary>
        public virtual TaskPriority? Priority { get; set; }

        /// <summary>
        /// 任务优先级（字符串）
        /// </summary>
        public virtual string PriorityText
        {
            get
            {
                return this.Priority.HasValue ? GetPriorityText(this.Priority.Value) : null;
            }
        }

        /// <summary>
        /// 任务状态
        /// </summary>
        public virtual TaskStatus? Status { get; set; }

        /// <summary>
        /// 状态（字符串）
        /// </summary>
        public virtual string StatusText
        {
            get
            {
                return this.Status.HasValue ? GetStatusText(this.Status.Value) : null;
            }
        }

        /// <summary>
        /// 任务期望开始时间
        /// </summary>
        public virtual DateTime? ExpBeginDate { get; set; }

        /// <summary>
        /// 任务期望结束日期
        /// </summary>
        public virtual DateTime? ExpEndDate { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? BeginDate { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// 分配目标人员
        /// </summary>
        public virtual ICollection<UserTask> AssignedToUsers { get; set; }

        public static string GetStatusText(TaskStatus status)
        {
            switch (status)
            {
                case TaskStatus.Saved:
                    return "等待分配";
                case TaskStatus.Assigned:
                    return "已分配";
                case TaskStatus.Processing:
                    return "正在执行";
                case TaskStatus.Finished:
                    return "已完成";
                default:
                    return null;
            }
        }

        public static string GetPriorityText(TaskPriority priority)
        {
            switch (priority)
            {
                case TaskPriority.Highest:
                    return "最高";
                case TaskPriority.Higher:
                    return "较高";
                case TaskPriority.High:
                    return "高";
                case TaskPriority.Middle:
                    return "中等";
                case TaskPriority.Low:
                    return "低";
                case TaskPriority.Lower:
                    return "较低";
                case TaskPriority.Lowest:
                    return "最低";
                default:
                    return null;
            }
        }
    }
}
