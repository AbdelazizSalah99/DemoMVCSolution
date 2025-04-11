using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService,
        ILogger<DepartmentsController> _logger,
        IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        }

        #region Create Employee
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    int Result = _employeeService.CreateEmployee(employeeDto);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't Be Created");
                    }
                }
                catch (Exception ex)
                {
                    // Log Exception
                    if (_environment.IsDevelopment())
                    {
                        //1. Development => log Error in Console And Returns Same view with error massage
                        ModelState.AddModelError(string.Empty, ex.Message);

                    }
                    else
                    {
                        //2. Deployment => log Error in File | Table in Database And Reteurn Error view
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(employeeDto);
        }
        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            return View(employee);
        }
        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeViewModel = new UpdatedEmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
            };
            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id,UpdatedEmployeeDto employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Result = _employeeService.UpdateEmployee(employee);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't Be Updated");
                    }


                }
                catch (Exception ex)
                {
                    // Log Exception
                    if (_environment.IsDevelopment())
                    {
                        //1. Development => log Error in Console And Returns Same view with error massage
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        //2. Deployment => log Error in File | Table in Database And Reteurn Error view
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(employee);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _employeeService.DeleteEmployee(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Can't Be Deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    //1. Development => log Error in Console And Returns Same view with error massage
                    ModelState.AddModelError(string.Empty, "Employee Can't Be Deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }
                else
                {
                    //2. Deployment => log Error in File | Table in Database And Reteurn Error view
                    _logger.LogError("Employee Can't Be Deleted");
                    return View("ErrorView", ex);
                }

            }
        }

        #endregion

    }
}
