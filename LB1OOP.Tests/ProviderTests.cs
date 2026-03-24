using LB1OOP;
using LB1OOP.Interfaces;
using LB1OOP.Visitors;

namespace LB1OOP.Tests
{
    /// <summary>
    /// Содержит модульные тесты для проверки функциональности класса <see cref="InternetProvider"/>.
    /// </summary>
    [TestClass]
    public sealed class ProviderTests
    {
        /// <summary>
        /// Проверяет, что конструктор без параметров инициализирует все свойства значениями по умолчанию.
        /// </summary>
        [TestMethod]
        public void Constructor()
        {
            IProvider provider = new InternetProvider();

            Assert.AreEqual("Undefined", provider.Name);
            Assert.AreEqual(0, provider.UserCount);
            Assert.AreEqual(0, provider.SpeedLimit);
            Assert.AreEqual(0, provider.Area);
            Assert.AreEqual(0, provider.ContractNumber);
            Assert.AreEqual("Undefined", provider.TarifName);
            Assert.AreEqual(0, provider.TarifCoast);
        }

        /// <summary>
        /// Проверяет, что конструктор с одним параметром правильно устанавливает имя провайдера.
        /// </summary>
        [TestMethod]
        public void Constructor_WithOneParam()
        {
            IProvider provider = new InternetProvider("МТС");

            Assert.AreEqual("МТС", provider.Name);
        }

        /// <summary>
        /// Проверяет, что конструктор со всеми параметрами правильно инициализирует все свойства.
        /// </summary>
        [TestMethod]
        public void Constructor_WithAllParameters()
        {
            IProvider provider = new InternetProvider(
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

        /// <summary>
        /// Проверяет, что метод <see cref="InternetProvider.GetUserCountInHex"/> 
        /// правильно преобразует количество абонентов в шестнадцатеричный формат.
        /// </summary>
        [TestMethod]
        public void GetUserCountInHex()
        {
            IProvider provider = new InternetProvider();
            provider.UserCount = 255;
            IProviderVisitor densityVisitor = new DensityVisitor();
            densityVisitor.Visit(provider);
            string result = densityVisitor.Result.ToString();

            Assert.AreEqual("FF", result);
        }

        /// <summary>
        /// Проверяет, что метод <see cref="InternetProvider.ToString"/> возвращает имя провайдера.
        /// </summary>
        [TestMethod]
        public void ToString_ReturnsName()
        {
            IProvider provider = new InternetProvider("Мегафон");

            string result = provider.ToString();
            Assert.AreEqual("Мегафон", result);
        }

        /// <summary>
        /// Проверяет, что при установке отрицательного значения свойству <see cref="InternetProvider.UserCount"/>
        /// выбрасывается исключение <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UserCount_NegativeValue_ThrowsException()
        {
            IProvider provider = new InternetProvider();
            provider.UserCount = -1;
        }

        /// <summary>
        /// Проверяет, что при установке отрицательного значения свойству <see cref="InternetProvider.TarifCoast"/>
        /// выбрасывается исключение <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TarifCoast_NegativeValue_ThrowsException()
        {
            IProvider provider = new InternetProvider();
            provider.TarifCoast = -10;
        }

        /// <summary>
        /// Проверяет, что при установке пустой строки свойству <see cref="InternetProvider.Name"/>
        /// выбрасывается исключение <see cref="ArgumentException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_EmptyString_ThrowsException()
        {
            IProvider provider = new InternetProvider();
            provider.Name = "";
        }

        /// <summary>
        /// Проверяет, что при установке слишком короткого имени свойству <see cref="InternetProvider.Name"/>
        /// выбрасывается исключение <see cref="ArgumentException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_TooShort_ThrowsException()
        {
            IProvider provider = new InternetProvider();
            provider.Name = "A";
        }
    }
}