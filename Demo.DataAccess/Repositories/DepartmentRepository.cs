using Demo.DataAccess.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories
{
    // primary constructor .net 8 C# 12
    public class DepartmentRepository(ApplicationDbContext dbContext) : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;


        // CRUD Operations
        // Get All
        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Departments.ToList();
            else
                return _dbContext.Departments.AsNoTracking().ToList();
        }
        // Get By Id
        public Department? GetById(int id) => _dbContext.Departments.Find(id);
        // Update
        public int Update(Department department)
        {
            _dbContext.Departments.Update(department); // Update Locally
            return _dbContext.SaveChanges();
        }
        // Delete
        public int Remove(Department department)
        {
            _dbContext.Remove(department);
            return _dbContext.SaveChanges();
        }
        // Insert
        public int Add(Department department)
        {
            _dbContext.Add(department);
            return _dbContext.SaveChanges();
        }

    }
}
