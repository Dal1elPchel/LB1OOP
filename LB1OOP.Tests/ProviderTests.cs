using LB1OOP;

namespace LB1OOP.Tests
{
    [TestClass]
    public sealed class ProviderTests
    {
        [TestMethod]
        public void Constructor()
        {
            Provider provider = new Provider();

            Assert.AreEqual("Undefined", provider.Name);
            Assert.AreEqual(0, provider.UserCount);
            Assert.AreEqual(0, provider.SpeedLimit);
            Assert.AreEqual(0, provider.Area);
            Assert.AreEqual(0, provider.ContractNumber);
            Assert.AreEqual("Undefined", provider.TarifName);
            Assert.AreEqual(0, provider.TarifCoast);
        }

        [TestMethod]
        public void Constructor_WithOneParam()
        {

            Provider provider = new Provider("МТС");

            Assert.AreEqual("МТС", provider.Name);
        }

        [TestMethod]
        public void Constructor_WithAllParameters()
        {

            Provider provider = new Provider(
                name: "Ростелеком",
                tarifCoast: 500,
                userCount: 1000,
                speedLimit: 100,
                area: 5000,
                contractNumber: 12345,
                tarifName: "Домашний"
            );

            Assert.AreEqual("Ростелеком", provider.Name);
            Assert.AreEqual(500, provider.TarifCoast);
            Assert.AreEqual(1000, provider.UserCount);
            Assert.AreEqual(100, provider.SpeedLimit);
            Assert.AreEqual(5000, provider.Area);
            Assert.AreEqual(12345, provider.ContractNumber);
            Assert.AreEqual("Домашний", provider.TarifName);
        }

        [TestMethod]
        public void GetUserCountInHex()
        {

            Provider provider = new Provider();
            provider.UserCount = 255;

            string result = provider.GetUserCountInHex();

            Assert.AreEqual("FF", result);
        }

        [TestMethod]
        public void ToString_ReturnsName()
        {

            Provider provider = new Provider("Мегафон");

            string result = provider.ToString();
            Assert.AreEqual("Мегафон", result);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UserCount_NegativeValue_ThrowsException()
        {
            Provider provider = new Provider();
            provider.UserCount = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TarifCoast_NegativeValue_ThrowsException()
        {
            Provider provider = new Provider();
            provider.TarifCoast = -10;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_EmptyString_ThrowsException()
        {
            Provider provider = new Provider();
            provider.Name = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_TooShort_ThrowsException()
        {
            Provider provider = new Provider();
            provider.Name = "A";
        }
    }
}
