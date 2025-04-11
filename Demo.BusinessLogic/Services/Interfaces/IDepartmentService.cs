using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IDepartmentService
    {
        int CreateDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetialsDto? GetDepartmentByID(int id);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
    }
}