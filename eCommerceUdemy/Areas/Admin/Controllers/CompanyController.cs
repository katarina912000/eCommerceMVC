using ECommDataAccess.Repository.IRepository;
using eCommerceUdemy.Models;
using ECommModels.Models;
using ECommModels.ViewModels;
using ECommUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerceUdemy.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Companyy> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return View(objCompanyList);
        }


        public IActionResult Upsert(int? id)
        {
            if (id == 0 || id == null)
            {
                //create
                return View(new Companyy());
            }
            else
            {
                //update
                Companyy company = _unitOfWork.Company.Get(u => u.Id == id);
                return View(company);

            }
        }
        [HttpPost]
        public IActionResult Upsert(Companyy obj)
        {
            if (ModelState.IsValid)
            {
                

                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Company created successtfully";

                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Company updated successtfully";
                }

                return RedirectToAction("Index");
            }
            else
            {
                
                return View(obj);

            }

        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var prodToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (prodToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
            _unitOfWork.Company.Remove(prodToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful" });

        }
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Companyy> prodList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = prodList });
        }
        #endregion
    }
}

