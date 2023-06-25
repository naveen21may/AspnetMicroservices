using Discount.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DiscountRepository _discountRepository;
        private readonly IDapperContext<DapperContext> _discountDbContext;
        public IDiscountRepositories DiscountRepository
            => _discountRepository ??= new DiscountRepository(_discountDbContext);

        public IDiscountRepositories discountRepositories => throw new NotImplementedException();

        public UnitOfWork(IDapperContext<DapperContext> discountDbContext)
        {
            _discountDbContext = discountDbContext;
        }
        public void BeginTransaction()
        {
            _discountDbContext.BeginTransaction();
        }

        public void Commit()
        {
            _discountDbContext.Commit();
        }

        public void Dispose()
        {
            _discountDbContext.Dispose();
        }

        public void Rollback()
        {
            _discountDbContext.Rollback();
        }
    }
}
