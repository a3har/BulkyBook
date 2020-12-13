using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public void Update(Product product)
        {
            var ObjFromDb = _db.Products.FirstOrDefault(s => s.Id == product.Id);
            if (ObjFromDb != null)
            {
                if (product.ImageUrl != null)
                {
                    ObjFromDb.ImageUrl = product.ImageUrl;
                }
                ObjFromDb.Title = product.Title;
                ObjFromDb.Author = product.Author;
                ObjFromDb.Price = product.Price;
                ObjFromDb.ListPrice = product.ListPrice;
                ObjFromDb.Price50 = product.Price50;
                ObjFromDb.Price100 = product.Price100;
                ObjFromDb.ISBN = product.ISBN;
                ObjFromDb.Description = product.Description;
                ObjFromDb.CategoryId = product.CategoryId;
                ObjFromDb.CoverTypeId = product.CoverTypeId;
            }
        }
    }
}
