using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        // Get ALl Departments

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();

            return departments.Select(D => D.ToDepartmentDto());
        }

        // Get Department By Id
        public DepartmentDetialsDto? GetDepartmentByID(int id)
        {
            var department = _departmentRepository.GetById(id);

            // Manual Mapping
            // Auto Mapper
            // Constructor Mapping
            // Extension Methods
            return department is null ? null : department.ToDepartmentDetialsDto();

        }

        // Create new department
        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            return _departmentRepository.Add(department);

        }

        // Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var Department = _departmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                int Result = _departmentRepository.Remove(Department);
                return Result > 0 ? true : false;
            }
        }


    }

}
