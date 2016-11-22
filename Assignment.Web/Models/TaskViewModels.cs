using Assignment.Core.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Web.Models
{
    public class AddTaskViewModel
    {
        public AddTaskViewModel()
        {
            this.Number = "TK" + DateTime.Now.Ticks.ToString();
            this.Priority = TaskPriority.Middle;
        }

        [Required]
        [Display(Name = "任务编号")]
        public string Number { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "任务名称")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "优先级")]
        public TaskPriority Priority { get; set; }

        [Required]
        [Display(Name = "期望开始日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpBeginDate { get; set; }

        [Required]
        [Display(Name = "期望结束日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpEndDate { get; set; }

        [Display(Name = "执行人员")]
        public string AssignedTo { get; set; }

        [Display(Name = "任务描述")]
        [MaxLength(1024)]
        public string Desription { get; set; }
    }

    public class EditTaskViewModel
    {
        public int Id { get; set; }

        public TaskStatus Status { get; set; }

        [Required]
        [Display(Name = "任务编号")]
        public string Number { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "任务名称")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "优先级")]
        public TaskPriority Priority { get; set; }

        [Required]
        [Display(Name = "期望开始日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpBeginDate { get; set; }

        [Required]
        [Display(Name = "期望结束日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpEndDate { get; set; }

        [Display(Name = "执行人员")]
        public string AssignedTo { get; set; }

        [Display(Name = "任务描述")]
        [MaxLength(1024)]
        public string Desription { get; set; }
    }
}