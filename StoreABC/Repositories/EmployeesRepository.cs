using Microsoft.EntityFrameworkCore;
using StoreABC.Models;
using StoreABC.Repositories.Interfaces;

namespace StoreABC.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private StoreABCContext _dbContext;

        public EmployeesRepository(StoreABCContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetAnyEmployeeAsync()
        {
            Employee emp = await _dbContext.Employees.FirstAsync();
            return emp;
        }
    }
}
