using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.entity
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public string Status { get; set; }

        public Task() { }

        public Task(int taskId, string taskName, int projectId, int employeeId, string status)
        {
            TaskId = taskId;
            TaskName = taskName;
            ProjectId = projectId;
            EmployeeId = employeeId;
            Status = status;
        }
    }
}
