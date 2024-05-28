using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<EmployeeMarketing> EmployeeMarketing { get; set; }
        public DbSet<CouponCategory> CouponCategories { get; set; }

        internal async Task ToList(object id)
        {
            throw new NotImplementedException();
        }
    }
}