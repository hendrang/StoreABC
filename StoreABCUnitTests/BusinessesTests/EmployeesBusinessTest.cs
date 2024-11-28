using Moq;
using StoreABC.Businesses;
using StoreABC.Models;
using StoreABC.Repositories.Interfaces;
using StoreABC.ViewModels;

namespace StoreABCUnitTests
{
    public class EmployeesBusinessTest
    {
        private Mock<IEmployeesRepository> _employeeMockRepository;
        private EmployeesBusiness _employeesBusiness;

        public EmployeesBusinessTest()
        {
            _employeeMockRepository = new Mock<IEmployeesRepository>();

            _employeesBusiness = new EmployeesBusiness(_employeeMockRepository.Object);
        }

        [Fact]
        public async void GetAnyEmployee_WhenCalled_ReturnViewModelObject()
        {
            //Arrange
            Employee employeeModel = new Employee() 
            {
                Id = 99,
                Name = "Test",
                City = "ABC City",
                Age = 99
            };

            _employeeMockRepository.Setup(e => e.GetAnyEmployeeAsync()).Returns(Task.FromResult(employeeModel));

            //Act
            EmployeeViewModel employeeViewModel = await _employeesBusiness.GetAnyEmployee();

            //Assert
            Assert.NotNull(employeeViewModel);
            Assert.Equal("ABC City", employeeViewModel.City);

        }
    }
}