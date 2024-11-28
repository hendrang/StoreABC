using StoreABC.Models;

namespace StoreABC.Repositories.Interfaces
{
    public interface IEmployeesRepository : IRepository<Employee>
    {
        Task<Employee> GetAnyEmployeeAsync();
    }
}
