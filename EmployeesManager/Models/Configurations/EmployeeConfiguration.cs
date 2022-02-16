using EmployeesManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManager.Models.Configurations
{
    class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("dbo.Employees");
            HasKey(x => x.Id);
            Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();
            Property(x => x.EmploymentDate)
                .IsRequired();

        }
    }
}
