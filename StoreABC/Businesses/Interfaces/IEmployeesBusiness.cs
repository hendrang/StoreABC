using StoreABC.ViewModels;

namespace StoreABC.Businesses.Interfaces
{
    public interface IEmployeesBusiness
    {
        Task<EmployeeViewModel> GetAnyEmployee();
    }
}
