using Lightpoint.Test.Business.Structure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business.Interface
{
    public interface IProductManager
    {
        Task<IEnumerable<ProductStruct>> GetAllAsync();
        Task<ProductStruct> GetOneAsync(int id);
        Task<bool> AddAsync(ProductStruct productStruct);
        Task<bool> RemoveAsync(int id);
    }
}
