﻿using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
          var dept = _departmentService.GetAll().Where(x=>x.IsDeleted != true);
            return View(dept);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Add(department);
                    return RedirectToAction(nameof(Index));
                }ModelState.AddModelError("DepartmentError", "ValidationError");
                return View(department);
            }
            catch(Exception ex) {
                ModelState.AddModelError("DepartmentError", ex.Message);
                return View(department);
            }

            
        }
        public IActionResult Details(int? id, string viewName = "Details")
        {
            var dept = _departmentService.GetById(id);
            if (dept is null) { 
            return NotFound();
            }
            return View(viewName,dept);
        }
        [HttpGet]
       public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }
        [HttpPost]
        public IActionResult Update(int? id, Department department)
        {
            if(department.Id != id)
            {
                return RedirectToAction("NotFoundPage", null, "Home");
            }
            _departmentService.Update(department);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var dept = _departmentService.GetById(id);
            if(dept is null)
            {
                return RedirectToAction("NotFoundPage", null, "Home");

            }
            dept.IsDeleted = true;
            _departmentService.Update(dept);
            //_departmentService.Delete(dept);
            return RedirectToAction(nameof(Index));
        }
    }
}
