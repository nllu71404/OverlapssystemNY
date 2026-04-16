using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssytemApplication.Common;

namespace OverlapssytemApplication.Interfaces
{
    public interface IShoppingService
    {
        Task<Result<List<ShoppingModel>>> GetShoppingByResidentIdAsync(int residentId);
        Task <Result<int>>SaveNewShoppingAsync(ShoppingModel shoppingModel);
        Task <Result>UpdateShoppingAsync(ShoppingModel shopping);
        Task <Result>DeleteShoppingAsync(int shoppingId);

    }
}
