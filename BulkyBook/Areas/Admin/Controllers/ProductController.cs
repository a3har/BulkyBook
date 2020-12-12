﻿using BulkyBook.DataAccess.Repository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i=> new SelectListItem
                {Text =i.Name,
                Value=i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null)
            {
                return View(productVM);
            }

            productVM.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if(productVM.Product == null)
            {
                return NotFound();
            }
            return View(productVM);         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product product)
        {
            if (ModelState.IsValid)
            {
                if(product.Id == 0)
                {
                    _unitOfWork.Product.Add(product);
                }
                else
                {
                    _unitOfWork.Product.Update(product);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(product);
            }
        }

        #region API CALLS

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Product.Get(id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return Json(new { success = true, message="Delete successfull" });
  
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var AllObjs = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new { data = AllObjs });
        }

        #endregion
    }
}
