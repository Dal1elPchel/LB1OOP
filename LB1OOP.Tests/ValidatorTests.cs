namespace LB1OOP.Tests;

/// <summary>
/// Содержит модульные тесты для проверки функциональности класса <see cref="Validator"/>.
/// </summary>
[TestClass]
public class ValidatorTests
{
    /// <summary>
    /// Проверяет, что метод <see cref="Validator.ValidateName"/> не выбрасывает исключение при корректном имени.
    /// </summary>
    [TestMethod]
    public void ValidateName()
    {
        string validName = "МТС";

        try
        {
            Validator.ValidateName(validName);
            Assert.IsTrue(true);
        }
        catch
        {
            Assert.Fail("Исключение не должно быть выброшено при корректном имени");
        }
    }

    /// <summary>
    /// Проверяет, что метод <see cref="Validator.ValidateName"/> выбрасывает исключение <see cref="ArgumentException"/>
    /// при слишком коротком имени.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ValidateName_TooShort_ThrowsException()
    {
        Validator.ValidateName("A");
    }

    /// <summary>
    /// Проверяет, что метод <see cref="Validator.ValidateName"/> выбрасывает исключение <see cref="ArgumentException"/>
    /// при слишком длинном имени.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ValidateName_TooLong_ThrowsException()
    {
        Validator.ValidateName(new string('A', 31));
    }

    /// <summary>
    /// Проверяет, что метод <see cref="Validator.ValidateTarifCoast"/> выбрасывает исключение 
    /// <see cref="ArgumentOutOfRangeException"/> при отрицательной стоимости.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ValidateTarifCoast_Negative_ThrowsException()
    {
        Validator.ValidateTarifCoast(-10);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="Validator.ValidateTarifName"/> не выбрасывает исключение
    /// при значении "Undefined".
    /// </summary>
    [TestMethod]
    public void ValidateTarifName_Undefined_DoesNotThrow()
    {
        try
        {
            Validator.ValidateTarifName("Undefined");
            Assert.IsTrue(true);
        }
        catch
        {
            Assert.Fail("Метод ValidateTarifName не должен выбрасывать исключение при значении 'Undefined'");
        }
    }
}