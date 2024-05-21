using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;

namespace Backend.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BaseContext _context;
        public CategoryRepository(BaseContext context)
        {
            _context = context;
        }
        public void Add(CouponCategory category)
        {
            _context.CouponCategory.Add(category);
            _context.SaveChanges();
        }

        public IEnumerable<CouponCategory> GetAll()
        {
            return _context.CouponCategory.ToList();
        }

        public CouponCategory GetById(int id)
        {
            return _context.CouponCategory.Find(id);
        }

        public void Remove(int id)
        {
            var category = _context.CouponCategory.Find(id);
            if (category != null)
            {
                _context.CouponCategory.Remove(category);
                _context.SaveChanges();
            }
        }
        public void Update(CouponCategory category)
        {
            var existingCategory = _context.CouponCategory.Find(category.CategoryId);
            if (existingCategory != null)
            {
                _context.Entry(existingCategory).CurrentValues.SetValues(category);
                _context.SaveChanges();
            }
        }
    }
}