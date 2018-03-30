using AutoFixture.NUnit3;
using Lightpoint.Test.Business.Exceptions;
using Lightpoint.Test.Business.Structure;
using Lightpoint.Test.Data;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business.Tests
{
    [TestFixture]
    [Category("Produc tManager")]
    public class ProductManagerTests
    {

        [Test]
        [AutoData]
        public async Task AddAsyncTestOkAsync(string databasename, string description, string name)
        {
            ProductStruct productStruct = new ProductStruct { Description = description, Name = name };
            InMemoryContext context = new InMemoryContext(databasename);
            ProductManager product = new ProductManager(context);

            bool result = await product.AddAsync(productStruct);

            Assert.IsTrue(result);
        }


        [Test]
        [AutoData]
        [Ignore("В памяти не возможно протестировать уникальность полей")]
        public async Task AddAsyncTestFaildAsync(string databasename, string description, string name)
        {
            ProductStruct productStruct = new ProductStruct { Description = description, Name = name };
            InMemoryContext context = new InMemoryContext(databasename);
            ProductManager product = new ProductManager(context);
            await context.Products.AddAsync(new ProductsEntity { Name = name });
            Assert.ThrowsAsync<ExistsInDBException>(async () => await product.AddAsync(productStruct));
        }

        [Test]
        [AutoData]
        public async Task GetAllAsyncOk1Async(string databasename, string description, string name)
        {
            InMemoryContext context = new InMemoryContext(databasename);
            ProductManager product = new ProductManager(context);
            await context.Products.AddAsync(new ProductsEntity { Name = name , Description = description});
            IEnumerable<ProductStruct> productStructs = await product.GetAllAsync();

            Assert.NotNull(productStructs);
        }

        [Test]
        [AutoData]
        public async Task GetAllAsyncOk2Async(string databasename, string description, string name)
        {
            InMemoryContext context = new InMemoryContext(databasename);
            ProductManager product = new ProductManager(context);
            IEnumerable<ProductStruct> productStructs = await product.GetAllAsync();

            Assert.NotNull(productStructs);
        }

        [Test]
        [AutoData]
        public async Task ConstructorOKAsync(string databasename)
        {
            InMemoryContext context = new InMemoryContext(databasename);
            ProductManager product = new ProductManager(context);
            
            Assert.NotNull(product);
        }

        [Test]
        public async Task ConstructoFaildAsync()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductManager(null));
        }
    }
}
