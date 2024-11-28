using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StoreABC.Businesses.Interfaces;
using StoreABC.Options;
using StoreABC.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreABC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private CustomOption _customOptionsValue;
        private IEmployeesBusiness _employeesBusiness;
        private ILogger<EmployeesController> _logger;

        public EmployeesController(IOptions<CustomOption> customOption, IEmployeesBusiness employeesBusiness, ILogger<EmployeesController> logger)
        {
            _customOptionsValue = customOption.Value;
            _employeesBusiness = employeesBusiness;
            _logger = logger;
        }

        [HttpGet]
        [Route("AnyEmployee")]
        public async Task<IActionResult> GetAnyEmployee()
        {
            _logger.LogInformation("Check your StoreABC console - You should see THIS!");
            EmployeeViewModel employeeViewModel = await _employeesBusiness.GetAnyEmployee();

            return employeeViewModel is null ? NotFound() : Ok(employeeViewModel);
        }

        [HttpGet]
        [Route("Exception")]
        public async Task<IActionResult> Exception()
        {
            throw new Exception("This should not be shown to public");

            //Uncomment below to test how properly handled exception looks like

            //try
            //{
            //    throw new Exception("Properly handled exception with meaningful message for caller.");
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string connString = _customOptionsValue.ConnectionString;
            return new string[] { "value1", "value2", connString };
        }

        /*
         Below are the default API created.
        */

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
