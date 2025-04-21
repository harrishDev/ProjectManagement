using Project_Management.dao;
using Project_Management.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading.Tasks;
using Task = Project_Management.entity.Task;
using System.Collections.Generic;
using Project_Management.myexceptions;

namespace Project_Management_Test
{
    [TestFixture]
    public class ProjectTest
    {
        private IProjectRepository repo;

        [SetUp]
        public void Setup()
        {
            repo = new ProjectRepositoryImpl();
        }

        [Test]
        public void Test_CreateEmployee_Success()
        {
            var emp = new Employee
            {
                Id = new Random().Next(1000, 9999),
                Name = "Test Employee",
                Designation = "Developer",
                Gender = "Other",
                Salary = 60000
            };

            bool result = repo.CreateEmployee(emp);
            Assert.IsTrue(result, "Employee should be created successfully.");
        }

        [Test]
        public void Test_CreateTask_Success()
        {
            var task = new Task
            {
                TaskId = new Random().Next(1000, 9999),
                TaskName = "UI Design",
                ProjectId = 1,    
                EmployeeId = 103,   
                Status = "completed"
            };

            bool result = repo.CreateTask(task);
            Assert.IsTrue(result, "Task should be created successfully.");
        }
        [Test]
        public void Test_GetAllTasks_ReturnsTasks()
        {
            int employeeId = 1;  
            int projectId = 1;   

            List<Task> tasks = repo.GetAllTasks(employeeId, projectId);

            Assert.IsInstanceOf<List<Task>>(tasks, "Should return a list of tasks.");
            Assert.That(tasks.Count, Is.GreaterThanOrEqualTo(0), "Task list should not be null.");
        }
        [Test]
        public void Test_EmployeeNotFoundException()
        {
            int nonExistentEmpId = 99999;

            var ex = Assert.Throws<EmployeeNotFoundException>(() =>
            {
                repo.AssignProjectToEmployee(1, nonExistentEmpId);
            });

            Assert.That(ex.Message, Does.Contain("Employee"), "throw EmployeeNotFoundException.");
        }

        
        [Test]
        public void Test_ProjectNotFoundException()
        {
            int nonExistentProjectId = 99999;

            var ex = Assert.Throws<ProjectNotFoundException>(() =>
            {
                repo.DeleteProject(nonExistentProjectId);
            });

            Assert.That(ex.Message, Does.Contain("Project"), "throw ProjectNotFoundException.");
        }
    }
}
