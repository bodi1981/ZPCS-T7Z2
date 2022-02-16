using EmployeesManager.Models.Domains;
using EmployeesManager.Models.Wrappers;
using EmployeesManager.Models.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManager
{
    public static class Repository
    {
        public static List<EmployeeWrapper> GetEmployees(int employeegroupId = 0)
        {
            using (var dbContext = new EmpMgrDbContext())
            {
                var employees = dbContext.Employees as IQueryable<Employee>;

                switch (employeegroupId)
                {
                    case 0:
                        return employees.ToList().Select(x => x.ToWrapper()).ToList();
                    case 1:
                        return employees.Where(x => x.DismissalDate == null).ToList().Select(x => x.ToWrapper()).ToList();
                    case 2:
                        return employees.Where(x => x.DismissalDate != null).ToList().Select(x => x.ToWrapper()).ToList();
                    default:
                        return new List<EmployeeWrapper>();
                }
            }
        }
        public static void EditEmployee(EmployeeWrapper employee)
        {
            using (var dbContext = new EmpMgrDbContext())
            {
                var editedEmployee = dbContext.Employees.Where(x => x.Id == employee.Id).FirstOrDefault();
                editedEmployee.FirstName = employee.FirstName;
                editedEmployee.LastName = employee.LastName;
                editedEmployee.EmploymentDate = employee.EmploymentDate;
                editedEmployee.DismissalDate = employee.DismissalDate;
                editedEmployee.Salary = editedEmployee.Salary;
                editedEmployee.Feedback = editedEmployee.Feedback;

                dbContext.SaveChanges();
            }
        }
        public static void AddEmployee(EmployeeWrapper employee)
        {
            using (var dbContext = new EmpMgrDbContext())
            {
                dbContext.Employees.Add(employee.ToDAO());
                dbContext.SaveChanges();
            }
        }
        public static void DissambleEmployee(EmployeeWrapper employee)
        {
            using (var dbContext = new EmpMgrDbContext())
            {
                var selectedEmployee = dbContext.Employees.Where(x => x.Id == employee.Id).FirstOrDefault();
                selectedEmployee.DismissalDate = DateTime.Now.Date;
                dbContext.SaveChanges();
            }
        }
    }
}
