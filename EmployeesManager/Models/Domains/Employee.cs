using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManager.Models.Domains
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public DateTime? DismissalDate { get; set; }
        public decimal Salary { get; set; }
        public string Feedback { get; set; }
    }
}
