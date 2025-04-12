using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentsController(IDepartmentService _departmentService , 
        ILogger<DepartmentsController> _logger , 
        IWebHostEnvironment _environment) : Controller
    {
        // BaseUrl/Departmens/Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        #region Create Department
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        //[ValidateAntiForgeryToken] // Action Filter
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if(ModelState.IsValid) // Server side validation
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        DateOfCreation = departmentViewModel.DateOfCreation,
                        Description = departmentViewModel.Description
                    };
                    int Result = _departmentService.CreateDepartment(departmentDto);
                    string Message;
                    if (Result > 0)
                    { 
                        Message = $"Department {departmentViewModel.Name} Is Created Successfully"; 
                    }
                    else
                    {
                        Message = $"Department {departmentViewModel.Name} Can't Be Created";
                    }
                    TempData["Message"] = Message;
                    return RedirectToAction(nameof(Index), new { Message });
                }
                catch(Exception ex)
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
            return View(departmentViewModel);

        }


        #endregion

        #region Details Of Department

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentByID(id.Value);
            if (department is null) return NotFound();
            return View(department);
        }
        #endregion

        #region Edit Department

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentByID(id.Value);
            if (department is null) return NotFound();

            var departmentViewModel = new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.DateOfCreation
            };
            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UpdatedDepartment = new UpdatedDepartmentDto()
                    {
                        Id = id,
                        Code = viewModel.Code,
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.DateOfCreation
                    };
                    int Result = _departmentService.UpdateDepartment(UpdatedDepartment);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't Be Updated");
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        //1. Development => log Error in Console And Returns Same view with error massage
                        ModelState.AddModelError(string.Empty, ex.Message);

                    }
                    else
                    {
                        //2. Deployment => log Error in File | Table in Database And Reteurn Error view
                        _logger.LogError(ex.Message);
                        return View("ErrorView" , ex);
                    }
                }
            }
            return View(viewModel);
        }
        #endregion

        #region Delete Department
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest();
            try 
            {
                bool Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can't Be Deleted");
                    return RedirectToAction(nameof(Delete) , new {id});
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    //1. Development => log Error in Console And Returns Same view with error massage
                    ModelState.AddModelError(string.Empty, "Department Can't Be Deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }
                else
                {
                    //2. Deployment => log Error in File | Table in Database And Reteurn Error view
                    _logger.LogError("Department Can't Be Deleted");
                    return View("ErrorView" , ex);
                }

            }
        }

        #endregion
    }
}
