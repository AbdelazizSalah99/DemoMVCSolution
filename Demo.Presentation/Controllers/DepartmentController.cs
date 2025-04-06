using Demo.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService departmentService) : Controller
    {
        public IActionResult Index()
        {
            var departments = departmentService.GetAllDepartments();
            return View(departments);
        }
    }
}
