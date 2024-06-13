using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Dto;

namespace Backend.Services
{
    public interface ICouponRepository
    {
        object Coupons { get; }

        IEnumerable<Coupon> GetAll();
        Coupon GetById(int id);
        void Add(Coupon coupon);
        void Remove(int id);
        Task<Respuesta> DeleteCouponAsync(int id, int userId);
        Task<IEnumerable<Coupon>> GetAllWithCategoriesAndEmployeesAsync();
        Task<Coupon> GetByIdWithCategoryAndEmployeeAsync(int id);
        Task<IEnumerable<Coupon>> SearchAsync(string? name, string? description, string? status, string? use_type);
        IEnumerable<Coupon> GetByOwnerId(int ownerId);
        Task<Coupon> GetCouponByIdAsync(int id);
        Task<Respuesta> UpdateCouponAsync(int id, Coupon updatedCoupon, int userId);
         Task<int> SaveChangesAsync();
    }

    public class Respuesta
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
