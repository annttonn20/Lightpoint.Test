using Lightpoint.Test.Business.Structure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business.Interface
{
    public interface IManager<TStruct> where TStruct : struct
    {
        Task<IEnumerable<TStruct>> GetAllAsync();
        Task<TStruct> GetOneAsync(int id);
        Task<(bool, int)> AddAsync(TStruct productStruct);
        Task<bool> RemoveAsync(int id);
    }
}
