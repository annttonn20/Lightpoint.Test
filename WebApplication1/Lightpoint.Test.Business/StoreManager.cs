using Lightpoint.Test.Business.Exception_Messages;
using Lightpoint.Test.Business.Exceptions;
using Lightpoint.Test.Business.Interface;
using Lightpoint.Test.Business.Structure;
using Lightpoint.Test.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business
{
    public class StoreManager : IManager<StoreStruct>
    {
        private readonly DatabaseContext context;

        internal StoreManager(DatabaseContext context)
        {
            this.context = context ?? throw new ArgumentNullException();
        }

        public async Task<(bool, int)> AddAsync(StoreStruct storeStruct)
        {
            EntityEntry<StoresEntity> s =  await context.Stores.AddAsync(new StoresEntity
            {
                Name = storeStruct.Name,
                Address = storeStruct.Address,
                WorkingHours = storeStruct.WorkingHours
            });
            try
            {
                return ((await context.SaveChangesAsync()) > 0, s.Entity.Id);
            }
            catch (DbUpdateException dbe)
            {

                throw new ExistsInDBException(ExceptionMessages.CannotAddStore(), dbe);
            }
        }

        public async Task<IEnumerable<StoreStruct>> GetAllAsync()
        {
            List<StoreStruct> result = new List<StoreStruct>();
            List<StoresEntity> stores = await context.Stores.Include(c => c.StoreProduct).Select(p => p).ToListAsync();
            foreach (var storeEntity in stores)
            {
                result.Add(await CreatStoreStruct(storeEntity));
            }
            return result;
        }

        private async Task<StoreStruct> CreatStoreStruct(StoresEntity storeEntity)
        {
            StoreStruct storeStruct = new StoreStruct
            {
                Name = storeEntity.Name,
                Address = storeEntity.Address,
                WorkingHours = storeEntity.WorkingHours,
                Id = storeEntity.Id
            };
            if (storeStruct.Products == null)
            {
                storeStruct.Products = new List<ProductStruct>();
            }
            if (storeEntity.StoreProduct != null)
            {
                foreach (var product in storeEntity.StoreProduct)
                {
                    var productEntity = await context.Products.SingleOrDefaultAsync(p => p.Id == product.ProductId);
                    ProductStruct productStruct = new ProductStruct { Id = productEntity.Id, Name = productEntity.Name, Description = productEntity.Description };
                    storeStruct.Products.Add(productStruct);
                }
            }
            return storeStruct;

        }


        public async Task<StoreStruct> GetOneAsync(int id)
        {
            StoresEntity storesEntity = await context.Stores.Include(c => c.StoreProduct).SingleOrDefaultAsync(s => s.Id == id);
            return await CreatStoreStruct(storesEntity);
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }



}
