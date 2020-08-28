using MVC_CRUD_LIST.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC_CRUD_LIST.Repository
{
    public static class EmployeeList
    {
        static List<Employee> employeeList =  new List<Employee>();
        static List<Partner> PartnerList = new List<Partner>();
        static List<Emp_list> EmpList = new List<Emp_list>();
        static int i = 0,j=0;



        public static List<Employee> SelectEmployeeList()
        {       
            return employeeList;
        }
        
             public static List<Partner> SelectPartnerList()
        {
            return PartnerList;
        }

        public static void InsertEmployeeList(Employee emp)
        {
            if (EmpList != null)
            {
                bool containsItem = EmpList.Any(item => item.Name == emp.Name);
                int eid1 = 0;
                int pid1 = 0;


                //check whether employee exists in table if not add to empList
                if (!containsItem)
                {
                    i++;
                    EmpList.Add(new Emp_list() { Eid = i, Name = emp.Name });
                    eid1 = i;
                }
                else
                {
                    var id = EmpList.Where(element => element.Name == emp.Name).ToList();
                    eid1 = id[0].Eid;
                }
                if (!(EmpList.Any(item => item.Name == emp.Partner)) && emp.Partner != null)
                {
                    i++;
                    EmpList.Add(new Emp_list() { Eid = i, Name = emp.Partner });
                    pid1 = i;
                }
                else
                {
                    if (emp.Partner != null)
                    {
                        var id = EmpList.Where(element => element.Name == emp.Partner).ToList();
                        pid1 = id[0].Eid;
                    }

                }


                var chk = PartnerList.Where(elem => (elem.EmpId == eid1 || elem.EmpId == pid1 || elem.PartnerEmpid == eid1 || elem.PartnerEmpid == pid1)).ToList();
                if (chk.Count == 0)
                {
                    if (emp.Partner != null)
                    {
                        var check = employeeList.Where(element => element.EmployeeID == eid1).ToList();
                        foreach (Employee item in check)
                        {
                            item.Partner = emp.Partner;
                            item.PartnerID = pid1;
                        }

                        j++;
                        PartnerList.Add(new Partner() { Pid = j, EmpId = eid1, PartnerEmpid = pid1 });
                        var checkct = employeeList.Where(element => (element.EmployeeID == eid1 || element.EmployeeID == pid1)).ToList();
                        if (checkct.Count != 0 && checkct[0].Partner == null)
                        {
                            checkct[0].Name = emp.Name;
                            checkct[0].EmployeeID = eid1;
                            checkct[0].Partner = emp.Partner;
                            checkct[0].PartnerID = pid1;
                        }
                        else if (check.Count == 0)
                            employeeList.Add(new Employee() { EmployeeID = eid1, Name = emp.Name, Partner = emp.Partner, PartnerID = pid1 });
                    }
                    else
                    {
                        var check = employeeList.Where(element => (element.EmployeeID == eid1 || element.EmployeeID == pid1)).ToList();
                        if (check.Count != 0 && check[0].Partner == null)
                        {

                            check[0].Partner = emp.Partner;
                            check[0].PartnerID = pid1;
                        }

                        else
                            employeeList.Add(new Employee() { EmployeeID = eid1, Name = emp.Name, Partner = emp.Partner, PartnerID = 0 });
                    }
                }
            }
            //generateList(PartnerList, EmpList);
        }



        public static void generateList(List<Partner> PartnerList, List<Emp_list> EmpList)
        {

            var query = (from elist in EmpList
                        join plist in PartnerList on elist.Eid equals plist.EmpId
                        select new { EmployeeID = elist.Eid, Name = elist.Name,PartnerID= plist.PartnerEmpid}).AsEnumerable(); 

            var query1 = (from elist in EmpList
                        join plist in PartnerList on elist.Eid equals plist.PartnerEmpid
                        select new { EmployeeID = elist.Eid, Partner = elist.Name, PartnerID = plist.PartnerEmpid }).AsEnumerable();


        }

        public static void UpdateEmployeeList(Employee emp)
        {
            Employee employeeUpdate = employeeList.Find(x => x.EmployeeID == emp.EmployeeID);

            bool containsItem = EmpList.Any(item => item.Name == emp.Name);
            int eid1 = 0;
            int pid1 = 0;
            if (!containsItem)
            {
                i++;
                EmpList.Add(new Emp_list() { Eid = i, Name = emp.Name });
                eid1 = i;
            }
            else
            {
                var id = EmpList.Where(element => element.Name == emp.Name).ToList();
                eid1 = id[0].Eid;
            }
            if (!(EmpList.Any(item => item.Name == emp.Partner)) && emp.Partner != null)
            {
                i++;
                EmpList.Add(new Emp_list() { Eid = i, Name = emp.Partner });
                pid1 = i;
            }
            else
            {
                if (emp.Partner != null)
                {
                    var id = EmpList.Where(element => element.Name == emp.Partner).ToList();
                    pid1 = id[0].Eid;
                }

            }

            var checkpartner = PartnerList.Where(element => element.PartnerEmpid == pid1).ToList();

            if (checkpartner.Count == 0)
            {
                Partner partnerUpdate = PartnerList.Find(x => (x.EmpId == emp.EmployeeID || x.PartnerEmpid == emp.PartnerID));
                if (partnerUpdate != null)
                {
                    partnerUpdate.EmpId = eid1;
                    partnerUpdate.PartnerEmpid = pid1;
                }
                else
                {
                    j++;
                    PartnerList.Add(new Partner() { Pid = j, EmpId = eid1, PartnerEmpid = pid1 });
                }

                employeeUpdate.Name = emp.Name;
                employeeUpdate.Partner = emp.Partner;
                employeeUpdate.PartnerID = pid1;

            }

        }

        public static void DeleteEmployeeList(int id)
        {
            Employee empDelete = employeeList.Find(x => x.EmployeeID == id);

            employeeList.Remove(empDelete);
            PartnerList.Remove(PartnerList.Find(x => (x.EmpId == id || x.PartnerEmpid == id)));
            EmpList.Remove(EmpList.Find(x => (x.Eid == id)));
        }
    }
}