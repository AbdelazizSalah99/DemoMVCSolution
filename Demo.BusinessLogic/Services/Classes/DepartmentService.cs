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
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        // Get ALl Departments

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            return departments.Select(D => D.ToDepartmentDto());
        }

        // Get Department By Id
        public DepartmentDetialsDto? GetDepartmentByID(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);

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
            _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }

        // Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var Department = _unitOfWork.DepartmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Remove(Department);
                int Result = _unitOfWork.SaveChanges();
                return Result > 0 ? true : false;
            }
        }


    }

}
