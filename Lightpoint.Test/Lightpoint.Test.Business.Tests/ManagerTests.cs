using AutoFixture.NUnit3;
using Lightpoint.Test.Business.Exceptions;
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
        public async Task AddProductToStoreAsyncTestOk1Async(string databasename, string description, string name, string address)
        {
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                Manager manager = new Manager(context);
                await context.Stores.AddAsync(new StoresEntity { Name = name, Address = address });
                await context.Products.AddAsync(new ProductsEntity { Name = name });
                await context.SaveChangesAsync();
                bool result = await manager.AddProductToStoreAsync(name, name);
                Assert.IsTrue(result);
            }
        }

        [Test]
        [AutoData]
        public async Task AddProductToStoreAsyncTestOk2Async(string databasename, string description, string name, string address, string name1)
        {
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                Manager manager = new Manager(context);
                StoreManager storeManager = new StoreManager(context);
                await context.Stores.AddAsync(new StoresEntity { Name = name, Address = address });
                await context.Products.AddAsync(new ProductsEntity { Name = name });
                context.SaveChanges();
                StoresEntity stores = await context.Stores.SingleOrDefaultAsync(s => s.Name == name);
                await manager.AddProductToStoreAsync(name, name);
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

        [Test]
        [AutoData]
        public async Task AddProductToStoreAsyncTestFaild1Async(string databasename, string description, string name, string address)
        {
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                Manager manager = new Manager(context);
                await context.Stores.AddAsync(new StoresEntity { Name = name, Address = address });
                await context.Products.AddAsync(new ProductsEntity { Name = name });

                Assert.ThrowsAsync<NoExistInDbException>(async () => await manager.AddProductToStoreAsync(name, name), "Не удается добавить продукт магазину. Такого магазина не существует");
            }
        }

        [Test]
        [AutoData]
        public async Task AddProductToStoreAsyncTestFaild2Async(string databasename, string description, string name, string address)
        {
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                Manager manager = new Manager(context);
                await context.Stores.AddAsync(new StoresEntity { Name = name, Address = address });
                await context.SaveChangesAsync();
                await context.Products.AddAsync(new ProductsEntity { Name = name });

                Assert.ThrowsAsync<NoExistInDbException>(async () => await manager.AddProductToStoreAsync(name, name), "Не удается добавить продукт магазину. Такого продукта не существует");
            }
        }


  


        [Test]
        [AutoData]
        public async Task RemoveProductFromStoreAsyncTestFaild1Async(string databasename, string description, string name, string address)
        {
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                Manager manager = new Manager(context);
                await context.Stores.AddAsync(new StoresEntity { Name = name, Address = address });
                await context.Products.AddAsync(new ProductsEntity { Name = name });

                Assert.ThrowsAsync<NoExistInDbException>(async () => await manager.RemoveProductFromStoreAsync(name, name), "Не удается добавить продукт магазину. Такого магазина не существует");
            }
        }

        [Test]
        [AutoData]
        public async Task RemoveProductFromStoreAsyncTestFaild2Async(string databasename, string description, string name, string address)
        {
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                Manager manager = new Manager(context);
                await context.Stores.AddAsync(new StoresEntity { Name = name, Address = address });
                await context.SaveChangesAsync();
                await context.Products.AddAsync(new ProductsEntity { Name = name });

                Assert.ThrowsAsync<NoExistInDbException>(async () => await manager.RemoveProductFromStoreAsync(name, name), "Не удается добавить продукт магазину. Такого продукта не существует");
            }
        }
    }
}
