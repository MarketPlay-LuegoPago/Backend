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
        void Remove(int id);
        
        void CouponUpdate(int id, Coupon coupon);
        // object GetByOwnerId(int id ownerId);
        //Mostramos los cupones que crea un solo usuario
        IEnumerable<Coupon> GetByOwnerId(int? id);
        IEnumerable<Coupon> GetByOwnerId(object ownerId);
    }
}