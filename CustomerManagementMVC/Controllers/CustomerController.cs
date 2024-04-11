using CustomerManagementMVC.Models;
using CustomerManagementMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;

namespace CustomerManagementMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly XmlService _xmlService;

        public CustomerController(ApplicationDbContext context, XmlService xmlService)
        {
            _context = context;
            _xmlService = xmlService;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.Include(c => c.Addresses).FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.Include(c => c.Addresses).FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers.FirstOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var customer = _context.Customers.Find(id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Import(string xmlFilePath)
        {
            var customer = _xmlService.ImportClientsFromXml(xmlFilePath);
            _context.AddRange(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Client/Export
        [HttpGet]
        public IActionResult Export()
        {
            return View();
        }

        // GET: Client/Export
        [HttpPost]
        public IActionResult Export(string sortBy)
        {
            var customers = _context.Customers.ToList();
            List<Customer> sortedCustomers = null;

            if (sortBy == "name")
            {
                sortedCustomers = customers.OrderBy(c => c.Name).ToList();
            }
            else if (sortBy == "dob")
            {
                sortedCustomers = customers.OrderBy(c => c.BirthDate).ToList();
            }
            else
            {
                // Default sorting by name
                sortedCustomers = customers.OrderBy(c => c.Name).ToList();
            }

            var json = JsonSerializer.Serialize(sortedCustomers);

            byte[] fileBytes = Encoding.UTF8.GetBytes(json);
            return File(fileBytes, "application/json", "clients.json");
        }

    }
}
