using AutoFixture.NUnit3;
using Lightpoint.Test.Business.Exceptions;
using Lightpoint.Test.Business.Structure;
using Lightpoint.Test.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business.Tests
{
    [TestFixture]
    [Category("Store Manager")]
    class StoreManagerTests
    {
        [Test]
        [AutoData]
        public async Task AddAsyncTestOkAsync(string databasename, string address, string name)
        {
            StoreStruct storeStruct = new StoreStruct { Address = address, Name = name };
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                StoreManager store = new StoreManager(context);

                bool result = await store.AddAsync(storeStruct);

                Assert.IsTrue(result);
            }
        }


        [Test]
        [AutoData]
        [Ignore("В памяти не возможно протестировать уникальность полей")]
        public async Task AddAsyncTestFaildAsync(string databasename, string address, string name)
        {
            StoreStruct storeStruct = new StoreStruct { Address = address, Name = name };
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                StoreManager store = new StoreManager(context);
                await context.Stores.AddAsync(new StoresEntity { Name = name });
                Assert.ThrowsAsync<ExistsInDBException>(async () => await store.AddAsync(storeStruct));
            }
        }

        [Test]
        [AutoData]
        public async Task GetAllAsyncOk1Async(string databasename, string address, string name)
        {
            StoreStruct storeStruct = new StoreStruct { Address = address, Name = name };
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                StoreManager store = new StoreManager(context);
                await context.Stores.AddAsync(new StoresEntity { Name = name, Address = address });
                IEnumerable<StoreStruct> storeStructs = await store.GetAllAsync();

                Assert.NotNull(storeStructs);
            }
        }

        [Test]
        [AutoData]
        public async Task GetAllAsyncOk2Async(string databasename, string description, string name)
        {
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                StoreManager store = new StoreManager(context);
                IEnumerable<StoreStruct> storeStructs = await store.GetAllAsync();

                Assert.NotNull(storeStructs);
            }
        }

        [Test]
        [AutoData]
        public async Task ConstructorOKAsync(string databasename)
        {
            using (InMemoryContext context = new InMemoryContext(databasename))
            {
                StoreManager store = new StoreManager(context);

                Assert.NotNull(store);
            }
        }

        [Test]
        public async Task ConstructoFaildAsync()
        {
            Assert.Throws<ArgumentNullException>(() => new StoreManager(null));
        }
    }
}
