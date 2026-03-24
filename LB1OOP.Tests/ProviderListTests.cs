using LB1OOP.Interfaces;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.Tests
{
    [TestClass]
    public class ProviderListTests
    {
        [TestMethod]
        public void AddProvider_ValidProvider_IncreasesCount()
        {
            var collection = new ProviderList();
            var provider = new InternetProvider("МТС");

            collection.AddProvider(provider);

            Assert.AreEqual(1, collection.Count);
        }

        [TestMethod]
        public void RemoveProvider_ExistingProvider_DecreasesCount()
        {
            var collection = new ProviderList();
            var provider = new InternetProvider("МТС");
            collection.AddProvider(provider);

            collection.RemoveProvider(provider);

            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddProvider_NullProvider_ThrowsException()
        {
            var collection = new ProviderList();

            collection.AddProvider(null);
        }


        [TestMethod]
        public void AddProvider_RaisesProviderAddedEvent()
        {
            var collection = new ProviderList();
            var provider = new InternetProvider("МТС");
            bool eventRaised = false;

            collection.providerAdded += (p, a) => eventRaised = true;

            collection.AddProvider(provider);

            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void RemoveProvider_RaisesProviderRemovedEvent()
        {
            var collection = new ProviderList();
            var provider = new InternetProvider("МТС");
            collection.AddProvider(provider);
            bool eventRaised = false;

            collection.providerRemoved += (p, a) => eventRaised = true;

            collection.RemoveProvider(provider);

            Assert.IsTrue(eventRaised);
        }


        [TestMethod]
        public void GetAll_ReturnsAllProviders()
        {
            var collection = new ProviderList();
            var provider = new InternetProvider("МТС");
            collection.AddProvider(provider);

            var all = collection.GetAll();

            Assert.IsNotNull(all);
            Assert.AreEqual(1, System.Linq.Enumerable.Count(all));
        }
    }
}
