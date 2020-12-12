using BulkyBook.DataAccess.Repository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null)
            {
                return View(coverType);
            }
            coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
           
       
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                if(coverType.Id == 0)
                {
                    _unitOfWork.CoverType.Add(coverType);
                }
                else
                {
                    _unitOfWork.CoverType.Update(coverType);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var allDbObjs = _unitOfWork.CoverType.GetAll();
            return Json(new { data = allDbObjs });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var coverType = _unitOfWork.CoverType.Get(id);
            if(coverType == null)
            {
                return Json(new { success = false, message = "Error in deleting" });
            }
            _unitOfWork.CoverType.Remove(coverType);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleting successfull" });
        }
        #endregion
    }
}
