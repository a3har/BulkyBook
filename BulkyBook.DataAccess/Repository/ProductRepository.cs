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
                ObjFromDb.ListPrice50 = product.ListPrice50;
                ObjFromDb.ListPrice100 = product.ListPrice100;
                ObjFromDb.ISBN = product.ISBN;
                ObjFromDb.Description = product.Description;
                ObjFromDb.CategoryId = product.CategoryId;
                ObjFromDb.CoverTypeId = product.CoverTypeId;
            }
        }
    }
}
