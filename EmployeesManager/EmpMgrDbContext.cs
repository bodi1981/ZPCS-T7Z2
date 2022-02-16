using EmployeesManager.Models.Configurations;
using EmployeesManager.Models.Domains;
using System;
using System.Data.Entity;
using System.Linq;

namespace EmployeesManager
{
    public class EmpMgrDbContext : DbContext
    {
        public EmpMgrDbContext()
            : base($"Server={DBConfig.Server};Database={DBConfig.Database};User Id={DBConfig.Id}; Password={DBConfig.Password};")
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
        }
    }

}