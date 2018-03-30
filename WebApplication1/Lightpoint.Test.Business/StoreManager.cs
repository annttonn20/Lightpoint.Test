using Lightpoint.Test.Business.Exception_Messages;
using Lightpoint.Test.Business.Exceptions;
using Lightpoint.Test.Business.Interface;
using Lightpoint.Test.Business.Structure;
using Lightpoint.Test.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business
{
    public class StoreManager : IStoreManager
    {
        private readonly DatabaseContext context;

        internal StoreManager(DatabaseContext context)
        {
            this.context = context ?? throw new ArgumentNullException();
        }

        public async Task<bool> AddAsync(StoreStruct storeStruct)
        {
            await context.Stores.AddAsync(new StoresEntity
            {
                Name = storeStruct.Name,
                Address = storeStruct.Address,
                WorkingHours = storeStruct.WorkingHour
            });

            try
            {
                return (await context.SaveChangesAsync()) > 0;
            }
            catch (DbUpdateException dbe)
            {

                throw new ExistsInDBException(ExceptionMessages.CannotAddStore(), dbe);
            }
        }

        public async Task<IEnumerable<StoreStruct>> GetAllAsync()
        {
            List<StoreStruct> result = new List<StoreStruct>();
            List<StoresEntity> stores = await context.Stores.Select(p => p).ToListAsync();
            foreach (var item in stores)
            {
                StoreStruct storeStruct = new StoreStruct
                {
                    Name = item.Name,
                    Address = item.Address,
                    WorkingHour = item.WorkingHours,
                    Id = item.Id
                };
                foreach (var product in item.StoreProduct)
                {
                    if (storeStruct.Products == null)
                    {
                        storeStruct.Products = new List<ProductStruct>();
                    }                   

                    var productEntity = await context.Products.SingleOrDefaultAsync(p => p.Id == product.ProductId);
                    ProductStruct productStruct = new ProductStruct { Id = productEntity.Id, Name = productEntity.Name, Description = productEntity.Description };
                    storeStruct.Products.Add(productStruct);
                }
            }
            return result;
        }

        public Task<StoreStruct> GetOneAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }



}
