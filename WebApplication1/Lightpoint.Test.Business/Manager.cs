using Lightpoint.Test.Business.Exception_Messages;
using Lightpoint.Test.Business.Exceptions;
using Lightpoint.Test.Business.Interface;
using Lightpoint.Test.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business
{
    public class Manager : IManager
    {
        private readonly DatabaseContext context;

        internal Manager(DatabaseContext databaseContext)
        {
            this.context = databaseContext ?? throw new ArgumentNullException();
        }



        public async Task<bool> AddProductToStore(string storeName, string productName)
        {
            StoresEntity stores = await context.Stores.FirstOrDefaultAsync(s => s.Name == storeName);
            if (stores == null)
            {
                throw new NoExistInDbException(ExceptionMessages.CannotAddProductToStoreSTORE());
            }
            ProductsEntity products = await context.Products.FirstOrDefaultAsync(p => p.Name == productName);
            if (stores == null)
            {
                throw new NoExistInDbException(ExceptionMessages.CannotAddProductToStorePRODUCT());
            }
            stores.StoreProduct = new List<ProductsAndStoresEntity>();

            stores.StoreProduct.Add(new ProductsAndStoresEntity { ProductId = products.Id });
            try
            {
                return (await context.SaveChangesAsync()) > 0;
            }

            catch (DbUpdateException dbe)
            {
                throw new ExistsInDBException(ExceptionMessages.CannotAddProductToStore(), dbe);
            }
        }


        public async Task<bool> RemoveProductFromStore(string storeName, string productName)
        {
            StoresEntity stores = await context.Stores.FirstOrDefaultAsync(s => s.Name == storeName);
            if (stores == null)
            {
                throw new NoExistInDbException(ExceptionMessages.CannotRemoveProductFromStoreSTORE());
            }
            ProductsEntity products = await context.Products.FirstOrDefaultAsync(p => p.Name == productName);
            if (stores == null)
            {
                throw new NoExistInDbException(ExceptionMessages.CannotRemoveProductFromStoreSTORE());
            }
            ProductsAndStoresEntity ps = stores.StoreProduct.Single(s => s.ProductId == products.Id && s.StoreId == stores.Id);
            stores.StoreProduct.Remove(ps);
            context.Stores.Update(stores);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
