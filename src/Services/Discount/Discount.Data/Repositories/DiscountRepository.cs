using Discount.Data.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Discount.Data.Repositories
{
    //public class DiscountRepository : IDiscountRepositories
    //{
    //    private readonly IConfiguration _configuration;
    //    public DiscountRepository(IConfiguration configuration)
    //    {
    //        _configuration = configuration?? throw new ArgumentNullException(nameof(configuration));
           
    //    }
        
    //    public async Task<Coupon> GetDiscount(string productName)
    //    {
    //        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
    //        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * From Coupon Where ProductName= @ProductName", new { ProductName = productName});
    //        if (coupon == null)
    //        {
    //            return new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
    //        }
    //        return coupon;
    //    }

    //    public async Task<bool> CreateDiscount(Coupon coupon)
    //    {
    //        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

    //        var affected =
    //            await connection.ExecuteAsync
    //                ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
    //                        new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

    //        if (affected == 0)
    //            return false;

    //        return true;
    //    }

    //    public async Task<bool> UpdateDiscount(Coupon coupon)
    //    {
    //        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

    //        var affected = await connection.ExecuteAsync
    //                ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
    //                        new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

    //        if (affected == 0)
    //            return false;

    //        return true;
    //    }

    //    public async Task<bool> DeleteDiscount(string productName)
    //    {
    //        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

    //        var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
    //            new { ProductName = productName });

    //        if (affected == 0)
    //            return false;

    //        return true;
    //    }
    //}

    public class DiscountRepository : IDiscountRepositories
    {
        private readonly IDapperContext<DapperContext> _dapperContext;
        public DiscountRepository(IDapperContext<DapperContext> dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {

            var affected = await _dapperContext.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                 new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            return affected == 0 ? false : true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {

            var affected = await _dapperContext.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            return affected == 0 ? false : true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var coupon = await _dapperContext.QueryFirstAsync<Coupon>("SELECT * From Coupon Where ProductName= @ProductName", new { ProductName = productName });
            if (coupon == null)
            {
                return new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var affected = await _dapperContext.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, 
                                Id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
