using LB1OOP.Controller;
using LB1OOP.Factories;
using LB1OOP.Interfaces;

namespace LB1OOP.Controllers
{
    public static class ControllerFactory
    {
        public static MainController CreateMainController()
        {
            var providerFactory = new DefaultProviderFactory();
            var providerCollection = providerFactory.CreateProviderCollection();

            var testProvider = providerFactory.CreateProviderWithName("МТС");
            providerCollection.AddProvider(testProvider);

            return new MainController(providerCollection, providerFactory);
        }
    }
}