﻿using ECommDataAccess.Repository.IRepository;
using eCommerceUdemy.Models;
using ECommModels.Models;
using ECommModels.ViewModels;
using ECommUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerceUdemy.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = SD.Role_Admin)]

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            
            return View(objProductList);
        }
       

        public IActionResult Upsert(int? id)
        {
           
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.
                GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()

                }),
                Product = new Product()
            };

            if (id==0 || id==null)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);

            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
               
                if(file!=null)
                {
                    string fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string productPath=Path.Combine(wwwRootPath,@"images\product");

                    if(!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath,obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);    
                        }
                    }

                    using(var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.ImageUrl = @"\images\product\" + fileName;
                }
                else
                {
                    obj.Product.ImageUrl = "";
                }

                if(obj.Product.Id==0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product created successtfully";

                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product updated successtfully";
                }
               
                return RedirectToAction("Index");
            }
            else
            {
                obj.CategoryList = _unitOfWork.Category.
                 GetAll().Select(u => new SelectListItem
                 {
                     Text = u.Name,
                     Value = u.Id.ToString()

                 });
                  
                return View(obj);

            }

        }
       
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var prodToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if(prodToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, prodToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(prodToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful" });

        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll( )
        {
            List<Product> prodList= _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data=prodList });
        }
        #endregion

    }
}
