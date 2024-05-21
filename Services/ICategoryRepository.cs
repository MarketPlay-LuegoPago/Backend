using System;
using System.Collections.Generic;
using System.Linq;
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
        void Update(CouponCategory category);
    }
}