using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CCardoso.SalesWeb.Services;
using CCardoso.SalesWeb.Models;
using CCardoso.SalesWeb.Models.ViewModels;

namespace CCardoso.SalesWeb.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            return View(_sellerService.FindAll());
        }
        public IActionResult Create()
        {
            var _ = new SellerFormViewModel { Departments = _departmentService.FindAll() };
            return View(_);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
