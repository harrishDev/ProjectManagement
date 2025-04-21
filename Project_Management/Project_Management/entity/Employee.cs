using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public decimal Salary { get; set; }
        public int? ProjectId { get; set; }

        public Employee() { }

        public Employee(int id, string name, string designation, string gender, decimal salary, int? projectId)
        {
            Id = id;
            Name = name;
            Designation = designation;
            Gender = gender;
            Salary = salary;
            ProjectId = projectId;
        }
    }
}
