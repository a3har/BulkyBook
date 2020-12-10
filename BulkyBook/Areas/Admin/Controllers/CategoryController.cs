using BulkyBook.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var AllObjs = _unitOfWork.Category.GetAll();
            return Json(new { data = AllObjs });
        }

        #endregion
    }
}
