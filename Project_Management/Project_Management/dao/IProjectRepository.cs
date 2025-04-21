using Project_Management.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Project_Management.entity.Task;


namespace Project_Management.dao
{
    public interface IProjectRepository
    {
        bool CreateEmployee(Employee emp);
        bool CreateProject(Project project);
        bool CreateTask(Task task);
        bool AssignProjectToEmployee(int projectId, int employeeId);
        bool AssignTaskInProjectToEmployee(int taskId, int projectId, int employeeId);
        bool DeleteEmployee(int employeeId);
        bool DeleteProject(int projectId);
        List<Task> GetAllTasks(int employeeId, int projectId);
    }
}