namespace LB1OOP.Tests;

/// <summary>
/// Содержит модульные тесты для проверки функциональности класса <see cref="CustomDivideByZeroException"/>.
/// </summary>
[TestClass]
public class CustomDivideByZeroExceptionTests
{
    /// <summary>
    /// Проверяет, что метод <see cref="Provider.CalculateUserDensity"/> выбрасывает исключение 
    /// <see cref="CustomDivideByZeroException"/> при попытке вычисления плотности с нулевой площадью покрытия.
    /// </summary>
    [TestMethod]
    public void CalculateUserDensity_AreaZero_ThrowsCustomException()
    {
        Provider provider = new Provider("Тест");
        provider.Area = 0;
        provider.UserCount = 100;

        try
        {
            provider.CalculateUserDensity();
            Assert.Fail("Должно быть выброшено исключение");
        }
        catch (CustomDivideByZeroException ex)
        {
            StringAssert.Contains(ex.Message, "нет зоны покрытия");
        }
    }

    /// <summary>
    /// Проверяет, что метод <see cref="CustomDivideByZeroException.ToString"/> 
    /// возвращает правильно отформатированное сообщение.
    /// </summary>
    [TestMethod]
    public void CustomDivideByZeroException_ToString_ReturnsFormattedMessage()
    {
        var ex = new CustomDivideByZeroException("Тестовое сообщение");
        string result = ex.ToString();

        StringAssert.Contains(result, "Ошибка деления на 0");
        StringAssert.Contains(result, "Тестовое сообщение");
    }
}