using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.entity
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }

        public Project() { }

        public Project(int id, string projectName, string description, DateTime startDate, string status)
        {
            Id = id;
            ProjectName = projectName;
            Description = description;
            StartDate = startDate;
            Status = status;
        }
    }
}