﻿using System;
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            //AcceptedAtAction(nameof(Delete));
            return RedirectToAction(nameof(Index));
            //return AcceptedAtAction(nameof(Index)).ExecuteResultAsync(ControllerContext);
        }
    }
}
