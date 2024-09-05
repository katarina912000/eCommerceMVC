using ECommDataAccess.Repository;
using ECommDataAccess.Repository.IRepository;
using eCommerceUdemy.Models;
using ECommModels.Models;
using ECommUtility ;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());

            }

            IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category");

            return View(objProductList);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new() {
            
                Product= _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId= productId

            };

            
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            cart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == cart.ProductId);
            if (cartFromDb != null)
            {
                cartFromDb.Count += cart.Count;
                _unitOfWork.ShoppingCart.Update(cart);
               // _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(cart);
                _unitOfWork.Save();

                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());

            }
            TempData["success"] = "Card updated successfully";
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
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
