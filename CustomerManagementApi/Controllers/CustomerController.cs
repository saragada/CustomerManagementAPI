using CustomerManagementApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementApi.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private static List<Customer> customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "John Doe", Email = "John@gmail.com", Phone = "123-456-7890" },
            new Customer { Id = 2, Name = "Jane Smith", Email = "Jane@gmail.com", Phone = "987-654-3210" }
        };
        [HttpGet("api/customers")]
        public IActionResult GetAllCustomers()
        {
            return Ok(customers);
        }
        [HttpGet("api/customers/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost("api/customers")]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            customer.Id = customers.Max(c => c.Id) + 1;
            customers.Add(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }
        [HttpPut("api/customers/{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.Name = updatedCustomer.Name;
            customer.Email = updatedCustomer.Email;
            customer.Phone = updatedCustomer.Phone;
            return Ok(customer);
        }
        [HttpDelete("api/customers/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            customers.Remove(customer);
            return NoContent();
        }
        }
}
