using Lightpoint.Test.Business.Structure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business.Interface
{
    public interface IStoreManager
    {
        Task<IEnumerable<StoreStruct>> GetAllAsync();
        Task<StoreStruct> GetOneAsync(int id);
        Task<bool> AddAsync(StoreStruct storeStruct);
        Task<bool> RemoveAsync(int id);
    }
}
