using System;
using System.Collections.Generic;
using Project_Management.dao;
using Project_Management.entity;
using Project_Management.myexceptions;
using Project_Management.dao;
using Project_Management.entity;
using Project_Management.myexceptions;

using Task = Project_Management.entity.Task;

namespace Project_Management
{
    class Program
    {
        static void Main(string[] args)
        {
            IProjectRepository repo = new ProjectRepositoryImpl();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Project Management System Menu ---");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Add Project");
                Console.WriteLine("3. Add Task");
                Console.WriteLine("4. Assign Project to Employee");
                Console.WriteLine("5. Assign Task within Project to Employee");
                Console.WriteLine("6. Delete Employee");
                Console.WriteLine("7. Delete Project");
                Console.WriteLine("8. List Tasks for an Employee in a Project");
                Console.WriteLine("9. Exit");
                Console.Write("Select an option: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            try
                            {
                                //Console.Write("Enter Employee ID: ");
                                //int empId = int.Parse(Console.ReadLine());
                                Console.Write("Enter Employee ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int empId))
                                {
                                    Console.WriteLine("Invalid employee ID.");
                                    break;
                                }

                                Console.Write("Enter Name: ");
                                string name = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(name) || int.TryParse(name, out _))
                                {
                                    Console.WriteLine("Invalid name. Must be alphabetic.");
                                    break;
                                }

                                Console.Write("Enter Designation: ");
                                string designation = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(designation) || int.TryParse(designation, out _))
                                {
                                    Console.WriteLine("Invalid designation. Must be alphabetic.");
                                    break;
                                }

                                Console.Write("Enter Gender (Male/Female/Other): ");
                                string gender = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(gender) || int.TryParse(gender, out _))
                                {
                                    Console.WriteLine("Invalid gender. Must be alphabetic.");
                                    break;
                                }

                                Console.Write("Enter Salary: ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal salary) || salary <= 0)
                                {
                                    Console.WriteLine("Invalid salary. Must be a positive number.");
                                    break;
                                }

                                Employee emp = new Employee(empId, name, designation, gender, salary, null);
                                bool empCreated = repo.CreateEmployee(emp);
                                Console.WriteLine(empCreated ? "Employee added successfully." : "Failed to add employee.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while adding employee: " + ex.Message);
                            }
                            break;


                        case 2:
                            try
                            {
                                Console.Write("Enter Project ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int projId))
                                {
                                    Console.WriteLine("Invalid Project ID.");
                                    break;
                                }

                                Console.Write("Enter Project Name: ");
                                string projName = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(projName) || int.TryParse(projName, out _))
                                {
                                    Console.WriteLine("Invalid project name.");
                                    break;
                                }

                                Console.Write("Enter Description: ");
                                string desc = Console.ReadLine();

                                Console.Write("Enter Start Date (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                                {
                                    Console.WriteLine("Invalid date format.");
                                    break;
                                }

                                Console.Write("Enter Status (started/dev/build/test/deployed): ");
                                string status = Console.ReadLine().ToLower();
                                string[] validStatuses = { "started", "dev", "build", "test", "deployed" };
                                if (!Array.Exists(validStatuses, s => s == status))
                                {
                                    Console.WriteLine("Invalid status.");
                                    break;
                                }

                                Project proj = new Project(projId, projName, desc, startDate, status);
                                bool projCreated = repo.CreateProject(proj);
                                Console.WriteLine(projCreated ? "Project added." : "Failed to add project.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while adding project: " + ex.Message);
                            }
                            break;


                        case 3:
                            try
                            {
                                Console.Write("Enter Task ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int taskId))
                                {
                                    Console.WriteLine("Invalid Task ID.");
                                    break;
                                }

                                Console.Write("Enter Task Name: ");
                                string taskName = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(taskName))
                                {
                                    Console.WriteLine("Invalid task name.");
                                    break;
                                }

                                Console.Write("Enter Project ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int taskProjId))
                                {
                                    Console.WriteLine("Invalid Project ID.");
                                    break;
                                }

                                Console.Write("Enter Employee ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int taskEmpId))
                                {
                                    Console.WriteLine("Invalid Employee ID.");
                                    break;
                                }

                                Console.Write("Enter Status (Assigned/Started/Completed): ");
                                string taskStatus = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(taskStatus))
                                {
                                    Console.WriteLine("Invalid status.");
                                    break;
                                }

                                Task task = new Task(taskId, taskName, taskProjId, taskEmpId, taskStatus);
                                bool taskCreated = repo.CreateTask(task);
                                Console.WriteLine(taskCreated ? "Task added." : "Failed to add task.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while adding task: " + ex.Message);
                            }
                            break;


                        case 4:
                            try
                            {
                                Console.Write("Enter Project ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int assignProjId))
                                {
                                    Console.WriteLine("Invalid Project ID.");
                                    break;
                                }

                                Console.Write("Enter Employee ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int assignEmpId))
                                {
                                    Console.WriteLine("Invalid Employee ID.");
                                    break;
                                }

                                bool projAssigned = repo.AssignProjectToEmployee(assignProjId, assignEmpId);
                                Console.WriteLine(projAssigned ? "Project assigned to employee." : "Assignment failed.");
                            }
                            catch (EmployeeNotFoundException ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while assigning project: " + ex.Message);
                            }
                            break;


                        case 5:
                            try
                            {
                                Console.Write("Enter Task ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int assignTaskId))
                                {
                                    Console.WriteLine("Invalid Task ID.");
                                    break;
                                }

                                Console.Write("Enter Project ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int assignProj))
                                {
                                    Console.WriteLine("Invalid Project ID.");
                                    break;
                                }

                                Console.Write("Enter Employee ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int assignEmp))
                                {
                                    Console.WriteLine("Invalid Employee ID.");
                                    break;
                                }

                                bool taskAssigned = repo.AssignTaskInProjectToEmployee(assignTaskId, assignProj, assignEmp);
                                Console.WriteLine(taskAssigned ? "Task assigned to employee." : "Assignment failed.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while assigning task: " + ex.Message);
                            }
                            break;


                        case 6:
                            try
                            {
                                Console.Write("Enter Employee ID to delete: ");
                                if (!int.TryParse(Console.ReadLine(), out int delEmpId))
                                {
                                    Console.WriteLine("Invalid Employee ID.");
                                    break;
                                }

                                bool empDeleted = repo.DeleteEmployee(delEmpId);
                                Console.WriteLine(empDeleted ? "Employee deleted." : "Failed to delete employee.");
                            }
                            catch (EmployeeNotFoundException ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while deleting employee: " + ex.Message);
                            }
                            break;


                        case 7:
                            try
                            {
                                Console.Write("Enter Project ID to delete: ");
                                if (!int.TryParse(Console.ReadLine(), out int delProjId))
                                {
                                    Console.WriteLine("Invalid Project ID.");
                                    break;
                                }

                                bool projDeleted = repo.DeleteProject(delProjId);
                                Console.WriteLine(projDeleted ? "Project deleted." : "Failed to delete project.");
                            }
                            catch (ProjectNotFoundException ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while deleting project: " + ex.Message);
                            }
                            break;


                        case 8:
                            try
                            {
                                Console.Write("Enter Employee ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int searchEmpId))
                                {
                                    Console.WriteLine("Invalid Employee ID.");
                                    break;
                                }

                                Console.Write("Enter Project ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int searchProjId))
                                {
                                    Console.WriteLine("Invalid Project ID.");
                                    break;
                                }

                                List<Task> tasks = repo.GetAllTasks(searchEmpId, searchProjId);
                                if (tasks.Count == 0)
                                    Console.WriteLine("No tasks found.");
                                else
                                {
                                    Console.WriteLine("\nTasks Assigned:");
                                    foreach (var t in tasks)
                                        Console.WriteLine($"Task ID: {t.TaskId}, Name: {t.TaskName}, Status: {t.Status}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while retrieving tasks: " + ex.Message);
                            }
                            break;


                        case 9:
                            exit = true;
                            Console.WriteLine("Exiting the application...");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please select from the menu.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                }
            }
        }
    }
}
