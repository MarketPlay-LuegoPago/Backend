using System.Collections.Generic;
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
        void CouponUpdate(int id, Coupon coupon);
        IEnumerable<Coupon> GetByOwnerId(int ownerId);
    }
}
