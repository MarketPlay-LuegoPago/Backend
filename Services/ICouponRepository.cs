using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services
{
    public interface ICouponRepository
    {
        IEnumerable<Coupon> GetAll();
        Coupon GetById(int id);
        void Add(Coupon coupon);
        void Remove(int id);
        void Update(Coupon coupon);
        Task<IEnumerable<Coupon>> SearchByStatusAsync(string status);
    }
}