using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Entities;
using ChinookSystem.DAL;
using ChinookSystem.ViewModels;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class EmployeeController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<EmployeeCustomerList> Employee_EmployeeCustomerList()
        {
            using (var context = new ChinookSystemContext())
            {
				var results = from x in context.Employees
							   where x.Title.Contains("Sales Support")
							   orderby x.LastName, x.FirstName
							   select new EmployeeCustomerList
							   {
								   EmployeeName = x.LastName + ", " + x.FirstName,
								   Title = x.Title,
								   CustomersSupportCount = x.Customers.Count(),
								   CustomerList = (from y in x.Customers
												   select new CustomerSupportItem
												   {
													   CustomerName = y.LastName + ", " + y.FirstName,
													   Phone = y.Phone,
													   City = y.City,
													   State = y.State
												   }).ToList()
							   };
				return results.ToList();
			}
        }
    }
}
