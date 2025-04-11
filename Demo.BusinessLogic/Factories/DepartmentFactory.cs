using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;
using Demo.DataAccess.Models.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department d) => new DepartmentDto()
        {
            Id = d.Id,
            Code = d.Code,
            Description = d.Description,
            Name = d.Name,
            DateOfCreation = DateOnly.FromDateTime(d.CreatedOn)
        };

        public static DepartmentDetialsDto ToDepartmentDetialsDto(this Department department) => new DepartmentDetialsDto()
        {
            Id = department.Id,
            Name = department.Name,
            Code = department.Code,
            Description = department.Description,
            DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
            CreatedBy = department.CreatedBy,
            LastModifiedBy = department.LastModifiedBy,
            LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn)
        };

        public static Department ToEntity(this CreatedDepartmentDto departmentDto)=>new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        
        public static Department ToEntity(this UpdatedDepartmentDto departmentDto) => new Department()
        {
            Id = departmentDto.Id,
            Name = departmentDto.Name,
            Code = departmentDto.Code,
            CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly()),
            Description = departmentDto.Description
        };
        
    }
}
