using Discount.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Data
{
    public interface IUnitOfWork
    {
        IDiscountRepositories discountRepositories { get; }
        void BeginTransaction();
        void Rollback();
        void Commit();
        void Dispose();
    }
}
