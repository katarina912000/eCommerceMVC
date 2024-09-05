using ECommDataAccess.Repository.IRepository;
using ECommModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceUdemy.Areas.Admin.Controllers
{
    [Area("admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
           

            return View();
        }
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<OrderHeader> objOrder = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            return Json(new { data = objOrder });
        }
        #endregion
    }
}
