using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services
{
    public interface ICategoryRepository
    {
        IEnumerable<CouponCategory> GetAll();
        CouponCategory GetById(int id);
        void Add(CouponCategory category);
        void Remove(int id);
        Task<bool> UpdateAsync(CouponCategory category);
        Task<IEnumerable<CouponCategory>> SearchAsync(string Name);
    }
}
