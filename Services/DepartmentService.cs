using System.Linq;
using System.Collections.Generic;
using CCardoso.SalesWeb.Data;
using CCardoso.SalesWeb.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CCardoso.SalesWeb.Services
{
    public class DepartmentService
    {
        private readonly SalesWebContext _context;

        public DepartmentService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(d => d.Name).ToListAsync();
        }
    }
}
