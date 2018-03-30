using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business.Interface
{
    public interface IManager
    {
        Task<bool> AddProductToStore(string storeName, string productName);
        Task<bool> RemoveProductFromStore(string storeName, string productName);
    }
}
