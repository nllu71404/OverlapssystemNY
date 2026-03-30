using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssystemDomain.Interfaces
{
    public interface IShoppingRepository
    {
        List<ShoppingModel> GetAllShopping();
        List<ShoppingModel> GetShoppingByResidentId(int residentId);
        int SaveNewShopping(ShoppingModel shopping);
        void DeleteShopping(int shoppingId);
        void UpdateShopping(ShoppingModel shopping);
    }
}
