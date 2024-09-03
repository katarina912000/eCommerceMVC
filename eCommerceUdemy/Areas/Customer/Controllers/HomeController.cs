using ECommDataAccess.Repository;
using ECommDataAccess.Repository.IRepository;
using eCommerceUdemy.Models;
using ECommModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eCommerceUdemy.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category");

            return View(objProductList);
        }
        public IActionResult Details(int id)
        {
            Product Product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "Category");

            return View(Product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
