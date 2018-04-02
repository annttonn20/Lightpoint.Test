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
    public class ProductManager : IManager<ProductStruct>
    {
        private readonly DatabaseContext context;

        internal ProductManager(DatabaseContext context)
        {
            this.context = context ?? throw new ArgumentNullException();
        }
        public async Task<(bool, int)> AddAsync(ProductStruct productStruct)
        {
            EntityEntry<ProductsEntity> pe = await context.Products.AddAsync(new ProductsEntity
            {
                Name = productStruct.Name,
                Description = productStruct.Description,
            });

            try
            {
                return ((await context.SaveChangesAsync()) > 0, pe.Entity.Id);
            }
            catch (DbUpdateException dbe)
            {

                throw new ExistsInDBException(ExceptionMessages.CannotAddProduct(), dbe);
            }
        }

        public async Task<IEnumerable<ProductStruct>> GetAllAsync()
        {
            List<ProductStruct> result = new List<ProductStruct>();
            List<ProductsEntity> products = await context.Products.Select(p => p).ToListAsync();
            foreach (var item in products)
            {
                result.Add(new ProductStruct
                {
                    Name = item.Name,
                    Description = item.Description,
                    Id = item.Id
                });
            }
            return result;
        }

        public async Task<ProductStruct> GetOneAsync(string name)
        {
            ProductsEntity productEntity = await context.Products.Include(c => c.ProductStore).SingleOrDefaultAsync(s => s.Name == name);
            if (productEntity == null)
            {
                throw new NoExistInDbException(ExceptionMessages.CannotFindStore());
            }
            return new ProductStruct
            {
                Name = productEntity.Name,
                Description = productEntity.Description,
                Id = productEntity.Id
            };
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
