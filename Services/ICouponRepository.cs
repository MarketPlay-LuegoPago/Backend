using System;
using System.Collections.Generic;
using System.Linq;
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
         
        
        Task<respuesta> DeleteCouponAsync(int id, int userId);
        IEnumerable<Coupon> GetByOwnerId(int? id);
        IEnumerable<Coupon> GetByOwnerId(object ownerId);
        Task<respuesta> UpdateCouponAsync(int id, Coupon updatedCoupon, int userId);
        Task<IEnumerable<Coupon>> SearchAsync(string name, string description, string status, string use_type);
        Task<IEnumerable<Coupon>> GetAllWithCategoriesAndEmployeesAsync();
        Task<Coupon> GetByIdWithCategoryAndEmployeeAsync(int id);
       
           }
    }
