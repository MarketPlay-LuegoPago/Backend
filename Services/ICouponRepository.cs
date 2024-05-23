using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services
{
    public interface ICouponRepository
    {
    object Coupons { get; }

    IEnumerable<Coupon> GetAll();
        Coupon GetById(int id);
        void Add(Coupon coupon);
        void Remove(int id);
        void Update(Coupon coupon);
         Task<IEnumerable<Coupon>> SearchAsync(string name, string description, string status, string use_type);
        Task<IEnumerable<Coupon>> GetAllWithCategoriesAndEmployeesAsync();
        Task<Coupon> GetByIdWithCategoryAndEmployeeAsync(int id);
    }
}