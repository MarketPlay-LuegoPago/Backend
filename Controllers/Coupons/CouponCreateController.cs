using System;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Dto;



namespace Backend.Controllers.Coupons
{
    [ApiController]
    [Route("api/[controller]")]

{   

    public class CouponCreateController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponCreateController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateCouponDto createCouponDto)
        {
            if (createCouponDto == null)

        [Route("create")]
        public IActionResult Create([FromBody] Coupon coupon)
        {   
            if (coupon == null)

            {
                return BadRequest();
            }

            var coupon = new Coupon
            {
                name = createCouponDto.name,
                description = createCouponDto.description,
                creation_date = createCouponDto.creation_date,
                activation_date = createCouponDto.activation_date,
                expiration_date = createCouponDto.expiration_date,
                discount = createCouponDto.discount,
                Status = createCouponDto.status,
                use_type = createCouponDto.use_type,
                Quantity_uses = createCouponDto.quantity_uses,
                discount_type = createCouponDto.discount_type,
                EmployeeMarketingId = createCouponDto.EmployeeMarketing,
                CategoryId = createCouponDto.categoryId
            };

            try
            {
                _couponRepository.Add(coupon);

                // Recargar el cupón desde la base de datos para obtener las entidades relacionadas
                var createdCoupon = await _couponRepository.GetByIdWithCategoryAndEmployeeAsync(coupon.id);

                var couponDto = new CouponDto
                {
                    id = createdCoupon.id,
                    name = createdCoupon.name,
                    description = createdCoupon.description,
                    creation_date = createdCoupon.creation_date,
                    activation_date = createdCoupon.activation_date,
                    expiration_date = createdCoupon.expiration_date,
                    discount = createdCoupon.discount,
                    Status = createdCoupon.Status,
                    use_type = createdCoupon.use_type,
                    Quantity_uses = createdCoupon.Quantity_uses,
                    discount_type = createdCoupon.discount_type,
                    creator_employee_id = createdCoupon.EmployeeMarketingId,
                    categoryId = createdCoupon.CategoryId,
                    CreatorEmployeeName = createdCoupon.EmployeeMarketing?.name,
                    CategoryName = createdCoupon.Category?.Name
                };

                return CreatedAtAction(nameof(Create), new { id = couponDto.id }, couponDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
