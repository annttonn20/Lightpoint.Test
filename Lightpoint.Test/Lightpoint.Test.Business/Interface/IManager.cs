using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business.Interface
{
    public interface IManager
    {
        Task<bool> AddProductToStoreAsync(string storeName, string productName);
        Task<bool> RemoveProductFromStoreAsync(string storeName, string productName);
    }
}
