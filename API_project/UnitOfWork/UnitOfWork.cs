using API_project.Data;
using API_project.Repositories;
using static API_project.Repositories.IGenericRepository;
using API_project.Models;
using System;

namespace API_project.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OfficeContext _context;
        private GenericRepository<Employee> _employeeRepository;
        private GenericRepository<Department> _departmentRepository;

        public UnitOfWork(OfficeContext context)
        {
            _context = context;
        }

        public IGenericRepository<Employee> Employees
        {
            get
            {
                return _employeeRepository ??= new GenericRepository<Employee>(_context);
            }
        }

        public IGenericRepository<Department> Departments
        {
            get
            {
                return _departmentRepository ??= new GenericRepository<Department>(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
