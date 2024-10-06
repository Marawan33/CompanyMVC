using Company.Data.Models;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService,IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public IActionResult Index(string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp))
            {
              var emp =  _employeeService.GetAll();
                return View(emp);
            }
            else
            {
                var emp = _employeeService.GetEmployeeByName(searchInp);
                return View(emp);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Department = _departmentService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Add(employee);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("DepartmentError", "ValidationError");
                return View(employee);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DepartmentError", ex.Message);
                return View(employee);
            }
        }
        }
}
