using Discount.Data.Entities;
using System.Threading.Tasks;

namespace Discount.Data.Repositories
{
    public interface IDiscountRepositories
    {
        Task<Coupon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);
    } 
}
