namespace LB1OOP.Tests;

[TestClass]
public class CustomDivideByZeroExceptionTests
{
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

    [TestMethod]
    public void CustomDivideByZeroException_ToString_ReturnsFormattedMessage()
    {

        var ex = new CustomDivideByZeroException("Тестовое сообщение");

        string result = ex.ToString();

        StringAssert.Contains(result, "Ошибка деления на 0");
        StringAssert.Contains(result, "Тестовое сообщение");
    }
}
