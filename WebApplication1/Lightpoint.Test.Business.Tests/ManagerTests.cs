using AutoFixture.NUnit3;
using Lightpoint.Test.Business.Structure;
using Lightpoint.Test.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business.Tests
{
    [TestFixture]
    [Category("Manger")]
    public class ManagerTests
    {
        [Test]
        [AutoData]
        public async Task AddAsyncTestOk1Async(string databasename, string description, string name, string address)
        {
            InMemoryContext context = new InMemoryContext(databasename);
            Manager manager = new Manager(context);
            await context.Stores.AddAsync(new StoresEntity { Name = name, Address = address });
            await context.Products.AddAsync(new ProductsEntity { Name = name });
            bool result = await manager.AddProductToStore(name, name);
            Assert.IsTrue(result);
        }

        [Test]
        [AutoData]
        public async Task AddAsyncTestOk2Async(string databasename, string description, string name, string address, string name1)
        {
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                Manager manager = new Manager(context);
                StoreManager storeManager = new StoreManager(context);
                await context.Stores.AddAsync(new StoresEntity { Name = name, Address = address });
                await context.Products.AddAsync(new ProductsEntity { Name = name });
                context.SaveChanges();
                StoresEntity stores = await context.Stores.SingleOrDefaultAsync(s => s.Name == name);
                await manager.AddProductToStore(name, name);
                IEnumerable<StoreStruct> storeStructs = await storeManager.GetAllAsync();
                foreach (var item in storeStructs)
                {
                    foreach (var product in item.Products)
                    {
                        Assert.AreEqual(product.Name, name);
                    }
                }
            }
        }
    }
}
