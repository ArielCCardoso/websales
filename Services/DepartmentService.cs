using System.Linq;
using System.Collections.Generic;
using CCardoso.SalesWeb.Data;
using CCardoso.SalesWeb.Models;

namespace CCardoso.SalesWeb.Services
{
    public class DepartmentService
    {
        private readonly SalesWebContext _context;

        public DepartmentService(SalesWebContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(d => d.Name).ToList();
        }
    }
}
