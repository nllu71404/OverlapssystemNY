using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;

namespace OverlapssystemInfrastructure.Repositories
{
    internal class ShoppingRepository : IShoppingRepository
    {
        public void DeleteShopping(int shoppingId)
        {
            throw new NotImplementedException();
        }

        public List<ShoppingModel> GetAllShopping()
        {
            throw new NotImplementedException();
        }

        public List<ShoppingModel> GetShoppingByResidentId(int residentId)
        {
            throw new NotImplementedException();
        }

        public int SaveNewShopping(ShoppingModel shopping)
        {
            throw new NotImplementedException();
        }

        public void UpdateShopping(ShoppingModel shopping)
        {
            throw new NotImplementedException();
        }
    }
}
