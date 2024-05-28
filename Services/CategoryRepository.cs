using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

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
            _context.CouponCategories.Add(category);
            _context.SaveChanges();
        }

        public IEnumerable<CouponCategory> GetAll()
        {
            return _context.CouponCategories.ToList();
        }

        public CouponCategory GetById(int id)
        {
            return _context.CouponCategories.Find(id);
        }

        public void Remove(int id)
        {
            var category = _context.CouponCategories.Find(id);
            if (category != null)
            {
                _context.CouponCategories.Remove(category);
                _context.SaveChanges();
            }
        }

        public async Task<bool> UpdateAsync(CouponCategory category)
        {
            var existingCategory = await _context.CouponCategories.FindAsync(category.CategoryId);
            if (existingCategory != null)
            {
                _context.Entry(existingCategory).CurrentValues.SetValues(category);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<CouponCategory>> SearchAsync(string Name)
        {
            var query = _context.CouponCategories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(c => c.Name.Contains(Name));
            }

            return await query.ToListAsync();
        }

    }
}
