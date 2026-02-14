namespace LB1OOP.Tests;

[TestClass]
public class ValidatorTests
{
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
            Assert.Fail("Исключение");
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ValidateName_TooShort_ThrowsException()
    {
        Validator.ValidateName("A");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ValidateName_TooLong_ThrowsException()
    {
        Validator.ValidateName(new string('A', 31));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ValidateTarifCoast_Negative_ThrowsException()
    {
        Validator.ValidateTarifCoast(-10);
    }

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
            Assert.Fail("Undefined должно быть допустимым значением");
        }
    }
}
