using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Demo.Presentation.ViewModels.EmployeeViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService,
        ILogger<DepartmentsController> _logger,
        IWebHostEnvironment _environment
        , IDepartmentService departmentService) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }

        #region Create Employee
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid) 
            {
                try
                {

                    var employeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Salary = employeeViewModel.Salary,
                        IsActive = employeeViewModel.IsActive,
                        Email = employeeViewModel.Email,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        Gender = employeeViewModel.Gender,
                        EmployeeType = employeeViewModel.EmployeeType,
                        HiringDate = employeeViewModel.HiringDate,
                        DepartmentId = employeeViewModel.DepartmentId
                    };
                    int Result = _employeeService.CreateEmployee(employeeDto);
                    string Message;
                    if (Result > 0)
                    {
                        Message = $"Employee {employeeViewModel.Name} Is Created Successfully";
                    }
                    else
                    {
                        Message = $"Employee {employeeViewModel.Name} Can't Be Created";
                    }
                    TempData["Message"] = Message;
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
            return View(employeeViewModel);
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
            var employeeViewModel = new EmployeeViewModel()
            {
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId

            };
            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int? id,EmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UpdatedEmployee = new UpdatedEmployeeDto()
                    {
                        Id = id.Value,
                        Name = viewModel.Name,
                        Age = viewModel.Age,
                        Address = viewModel.Address,
                        Salary = viewModel.Salary,
                        IsActive = viewModel.IsActive,
                        Email = viewModel.Email,
                        PhoneNumber = viewModel.PhoneNumber,
                        HiringDate = viewModel.HiringDate,
                        Gender = viewModel.Gender,
                        EmployeeType = viewModel.EmployeeType,
                        DepartmentId = viewModel.DepartmentId
                    };
                    int Result = _employeeService.UpdateEmployee(UpdatedEmployee);
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
            return View(viewModel);
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
