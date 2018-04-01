using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business.Interface
{
    public interface IManager
    {
        Task<bool> AddProductToStoreAsync(int storeId, string productName);
        Task<bool> RemoveProductFromStoreAsync(string storeName, string productName);
    }
}
