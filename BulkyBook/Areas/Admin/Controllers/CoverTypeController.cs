using BulkyBook.DataAccess.Repository;
using BulkyBook.Models;
using BulkyBook.Utilities;
using Dapper;
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
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameters);
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
                var parameters = new DynamicParameters();
                parameters.Add("@Name", coverType.Name);
                if (coverType.Id == 0)
                {
                    
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create,parameters) ;
                }
                else
                {
                    parameters.Add("@Id",coverType.Id );
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update,parameters);
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
            var allDbObjs = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType_GetAll,null);
            return Json(new { data = allDbObjs });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get,parameters);
            if(coverType == null)
            {
                return Json(new { success = false, message = "Error in deleting" });
            }
            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete,parameters);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleting successfull" });
        }
        #endregion
    }
}
