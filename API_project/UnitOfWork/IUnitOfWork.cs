using static API_project.Repositories.IGenericRepository;
using API_project.Models;
using API_project.Repositories;
using System;
namespace API_project.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Employee> Employees { get; }
        IGenericRepository<Department> Departments { get; }
        void Save();
    }
}
