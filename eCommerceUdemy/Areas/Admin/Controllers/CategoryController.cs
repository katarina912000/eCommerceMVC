using ECommDataAccess.Repository.IRepository;
using eCommerceUdemy.Data;
using eCommerceUdemy.Models;
using ECommUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceUdemy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Can't match name and display order");
            }
            //if ( obj.Name!=null && obj.Name.ToLower() == "test")

            //{
            //    ModelState.AddModelError("", "Test case is invalid value");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successtfully";
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category? catfromdb = _unitOfWork.Category.Get(u => u.Id == id);
            if (catfromdb == null)
            {
                return NotFound();

            }

            return View(catfromdb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj.Name == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successtfully";

            return RedirectToAction("Index");

        }


        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)

            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);


            if (categoryFromDb == null)
            {
                return NotFound();

            }

            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successtfully";

                return RedirectToAction("Index");
            }

            return View();

        }
    }
}
