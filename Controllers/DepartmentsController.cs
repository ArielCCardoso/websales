using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CCardoso.SalesWeb.Models;

namespace CCardoso.SalesWeb.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> departments = new List<Department>();
            departments.Add(new Department { Id = 1, Name= "Agro" });
            departments.Add(new Department { Id = 2, Name= "Varejo" });

            return View(departments);
        }
    }
}
