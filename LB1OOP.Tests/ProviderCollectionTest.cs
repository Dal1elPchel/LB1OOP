using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LB1OOP.Tests
{
    [TestClass]
    public class ProviderCollectionTests
    {
        // ===== БАЗОВАЯ ФУНКЦИОНАЛЬНОСТЬ =====

        [TestMethod]
        public void AddProvider_ValidProvider_IncreasesCount()
        {
            // Arrange
            var collection = new ProviderCollection();
            var provider = new Provider("МТС");

            // Act
            collection.AddProvider(provider);

            // Assert
            Assert.AreEqual(1, collection.Count);
        }

        [TestMethod]
        public void RemoveProvider_ExistingProvider_DecreasesCount()
        {
            // Arrange
            var collection = new ProviderCollection();
            var provider = new Provider("МТС");
            collection.AddProvider(provider);

            // Act
            collection.RemoveProvider(provider);

            // Assert
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddProvider_NullProvider_ThrowsException()
        {
            // Arrange
            var collection = new ProviderCollection();

            // Act
            collection.AddProvider(null);
        }

        // ===== СОБЫТИЯ (только базовая проверка) =====

        [TestMethod]
        public void AddProvider_RaisesProviderAddedEvent()
        {
            // Arrange
            var collection = new ProviderCollection();
            var provider = new Provider("МТС");
            bool eventRaised = false;

            collection.ProviderAdded += (p, a) => eventRaised = true;

            // Act
            collection.AddProvider(provider);

            // Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void RemoveProvider_RaisesProviderRemovedEvent()
        {
            // Arrange
            var collection = new ProviderCollection();
            var provider = new Provider("МТС");
            collection.AddProvider(provider);
            bool eventRaised = false;

            collection.ProviderRemoved += (p, a) => eventRaised = true;

            // Act
            collection.RemoveProvider(provider);

            // Assert
            Assert.IsTrue(eventRaised);
        }

        // ===== ПОЛУЧЕНИЕ ДАННЫХ =====

        [TestMethod]
        public void GetAll_ReturnsAllProviders()
        {
            // Arrange
            var collection = new ProviderCollection();
            var provider = new Provider("МТС");
            collection.AddProvider(provider);

            // Act
            var all = collection.GetAll();

            // Assert
            Assert.IsNotNull(all);
            Assert.AreEqual(1, System.Linq.Enumerable.Count(all));
        }
    }
}