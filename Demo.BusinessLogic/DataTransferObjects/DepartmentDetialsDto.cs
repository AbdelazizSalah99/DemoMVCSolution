using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObjects
{
    public class DepartmentDetialsDto
    {
        #region Constructor - Based Mapping
        //// Constructor - Based Mapping
        //public DepartmentDetialsDto(Department department)
        //{
        //    Id = department.Id;
        //    Name = department.Name;
        //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn);
        //} 
        #endregion
        public int Id { get; set; } 
        public int CreatedBy { get; set; } 
        public DateOnly DateOfCreation { get; set; }
        public int LastModifiedBy { get; set; } 
        public DateOnly LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        
    }
}
