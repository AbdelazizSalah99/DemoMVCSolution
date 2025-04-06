﻿using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.DepartmentModel;
using Demo.DataAccess.Models.Shared;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        // Get All
        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Set<TEntity>().ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        }
        // Get By Id
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);
        // Update
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity); // Update Locally
            return _dbContext.SaveChanges();
        }
        // Delete
        public int Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }
        // Insert
        public int Add(TEntity entity)
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }

    }
}
