﻿using MVC_CRUD_LIST.Models;
using System.Collections.Generic;

namespace MVC_CRUD_LIST.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> SelectAllEmployees();
        List<Partner> SelectAllPartner();
        Employee SelectEmployeeById(int id);
        void InsertEmployee(Employee emp);
        void UpdateEmployee(Employee emp);
        void DeleteEmployee(int id);

    }
}