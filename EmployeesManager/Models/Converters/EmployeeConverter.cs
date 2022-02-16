using EmployeesManager.Models.Domains;
using EmployeesManager.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManager.Models.Converters
{
    public static class EmployeeConverter
    {
        public static Employee ToDAO(this EmployeeWrapper employeeWrapper)
        {
            return new Employee
            {
                Id = employeeWrapper.Id,
                FirstName = employeeWrapper.FirstName,
                LastName = employeeWrapper.LastName,
                EmploymentDate = employeeWrapper.EmploymentDate,
                DismissalDate = employeeWrapper.DismissalDate,
                Salary = employeeWrapper.Salary,
                Feedback = employeeWrapper.Feedback
            };
        }

        public static EmployeeWrapper ToWrapper(this Employee employee)
        {
            return new EmployeeWrapper
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmploymentDate = employee.EmploymentDate,
                DismissalDate = employee.DismissalDate,
                Salary = employee.Salary,
                Feedback = employee.Feedback
            };
        }


    }
}
