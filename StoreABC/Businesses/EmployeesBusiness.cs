using Mapster;
using StoreABC.Businesses.Interfaces;
using StoreABC.Models;
using StoreABC.Repositories.Interfaces;
using StoreABC.ViewModels;

namespace StoreABC.Businesses
{
    public class EmployeesBusiness: IEmployeesBusiness
    {
        private IEmployeesRepository _employeesRepository;
        public EmployeesBusiness(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<EmployeeViewModel> GetAnyEmployee()
        {
            Employee employeeModel = await _employeesRepository.GetAnyEmployeeAsync();
            EmployeeViewModel employeeViewModel = employeeModel.Adapt<EmployeeViewModel>();

            return employeeViewModel;
        }
    }
}
