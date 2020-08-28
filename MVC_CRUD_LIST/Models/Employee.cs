using System.ComponentModel.DataAnnotations;
using System;

namespace MVC_CRUD_LIST.Models
{
    public class Employee
    {
        [Display(Name="Employee ID")]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int PartnerID { get; set; }
        public string Partner { get; set; }
        
    }

    public class Partner
    {
        [Display(Name = "Employee ID")]
        public int Pid { get; set; }
        public int PartnerEmpid { get; set; }
        public int EmpId { get; set; }

    }

    public class Emp_list
    {
        [Display(Name = "Employee ID")]
        public int Eid { get; set; }
        public string Name { get; set; }

    }
}