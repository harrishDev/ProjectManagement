using Microsoft.Data.SqlClient;
using Project_Management.entity;
using Project_Management.myexceptions;
using Project_Management.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Project_Management.entity.Task;

namespace Project_Management.dao
{
    public class ProjectRepositoryImpl : IProjectRepository
    {

        //string connectionString = DBPropertyUtil.GetConnectionString("db.properties");

        // string connectionString = "Server=DESKTOP-HNVF699;Database=ProjectManagement;TrustServerCertificate=True;Integrated Security=True;";
        public bool CreateEmployee(Employee emp)
        {
            try
            {
                string query = @"SET IDENTITY_INSERT Employee ON;
                                 INSERT INTO Employee (employee_id, name, designation, gender, salary)
                                 VALUES (@employee_id, @name, @designation, @gender, @salary);
                                 SET IDENTITY_INSERT Employee OFF;";
                using (SqlConnection conn = DBConnUtil.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@employee_id", emp.Id);
                    cmd.Parameters.AddWithValue("@name", emp.Name);
                    cmd.Parameters.AddWithValue("@designation", emp.Designation);
                    cmd.Parameters.AddWithValue("@gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@salary", emp.Salary);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not connect to the database.");
                //Console.WriteLine("Details: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error in CreateEmployee: " + ex.Message);
                return false;
            }
        }

        public bool CreateProject(Project project)
        {
            try
            {
                string query = @"SET IDENTITY_INSERT Project ON;
                                 INSERT INTO Project (project_id, Projectname, Description, startdate, status)
                                 VALUES (@project_id, @Projectname, @Description, @startdate, @status);
                                 SET IDENTITY_INSERT Project OFF";
                using (SqlConnection conn = DBConnUtil.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@project_id", project.Id);
                    cmd.Parameters.AddWithValue("@Projectname", project.ProjectName);
                    cmd.Parameters.AddWithValue("@Description", project.Description);
                    cmd.Parameters.AddWithValue("@startdate", project.StartDate);
                    cmd.Parameters.AddWithValue("@status", project.Status);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in CreateProject: " + ex.Message);
                return false;
            }
        }

        public bool CreateTask(Task task)
        {
            try
            {
                string query = @"SET IDENTITY_INSERT Task ON;
                                 INSERT INTO Task (task_id, task_name, project_id, employee_id, status)
                                 VALUES (@task_id, @task_name, @project_id, @employee_id, @status);
                                 SET IDENTITY_INSERT Task OFF";
                using (SqlConnection conn = DBConnUtil.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@task_id", task.TaskId);
                    cmd.Parameters.AddWithValue("@task_name", task.TaskName);
                    cmd.Parameters.AddWithValue("@project_id", task.ProjectId);
                    cmd.Parameters.AddWithValue("@employee_id", task.EmployeeId);
                    cmd.Parameters.AddWithValue("@status", task.Status);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in CreateTask: " + ex.Message);
                return false;
            }
        }

        public bool AssignProjectToEmployee(int project_id, int employee_id)
        {
            try
            {
                string query = @"UPDATE Employee SET project_id = @project_id WHERE employee_id = @employee_id";
                using (SqlConnection conn = DBConnUtil.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@project_id", project_id);
                    cmd.Parameters.AddWithValue("@employee_id", employee_id);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new EmployeeNotFoundException("Employee not found");

                    return true;
                }
            }
            catch (EmployeeNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AssignProjectToEmployee: " + ex.Message);
                return false;
            }
        }

        public bool AssignTaskInProjectToEmployee(int task_id, int project_id, int employee_id)
        {
            try
            {
                string query = @"UPDATE Task SET employee_id = @employee_id WHERE task_id = @task_id";
                using (SqlConnection conn = DBConnUtil.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@employee_id", employee_id);
                    cmd.Parameters.AddWithValue("@task_id", task_id);
                    cmd.Parameters.AddWithValue("@project_id", project_id); // optional
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AssignTaskInProjectToEmployee: " + ex.Message);
                return false;
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                string query = @"DELETE FROM Employee WHERE employee_id = @employee_id";
                using (SqlConnection conn = DBConnUtil.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new EmployeeNotFoundException("Employee not found");

                    return true;
                }
            }
            catch (EmployeeNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DeleteEmployee: " + ex.Message);
                return false;
            }
        }

        public bool DeleteProject(int projectId)
        {
            try
            {
                string query = @"DELETE FROM Project WHERE project_id = @project_id";
                using (SqlConnection conn = DBConnUtil.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new ProjectNotFoundException("Project not found");

                    return true;
                }
            }
            catch (ProjectNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DeleteProject: " + ex.Message);
                return false;
            }
        }

        public List<Task> GetAllTasks(int employeeId, int projectId)
        {
            List<Task> tasks = new List<Task>();
            try
            {
                string query = @"SELECT * FROM Task WHERE employee_id = @employee_id AND project_id = @project_id";
                using (SqlConnection conn = DBConnUtil.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);
                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Task task = new Task
                            (
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetInt32(2),
                                reader.GetInt32(3),
                                reader.GetString(4)
                            );
                            tasks.Add(task);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetAllTasks: " + ex.Message);
            }

            return tasks;
        }
    }
}
