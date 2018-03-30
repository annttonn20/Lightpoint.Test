using AutoFixture.NUnit3;
using Lightpoint.Test.Business.Structure;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Lightpoint.Test.Business.Tests
{
    [TestFixture]
    public class ProductManagerTests
    {

        [Test]
        [AutoData]
        public async Task AddAsyncTestOkAsync(string databasename, ProductStruct productStruct)
        {
            InMemoryContext context = new InMemoryContext(databasename);
            ProductManager product = new ProductManager(context);

            bool result = await product.AddAsync(productStruct);

            Assert.IsTrue(result);

        }

    }
}
