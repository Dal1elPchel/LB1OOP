using LB1OOP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.Factories
{
    public class DefaultProviderFactory : IProviderFactory
    {
        public IProvider CreateBasicProvider()
        {
            return new InternetProvider();
        }

        public IProvider CreateProviderWithName(string name)
        {
            return new InternetProvider(name);
        }

        public IProvider CreateProviderWithTarif(string name, float tarifCost)
        {
            return new InternetProvider(name, tarifCost);
        }

        public IProvider CreateProvider(
            string name = "Undefined",
            float tarifCoast = 0,
            int userCount = 0,
            float speedLimit = 0,
            float area = 0,
            int contractNumber = 0,
            string tarifName = "Undefined")
        {
            return new InternetProvider(name, tarifCoast, userCount, speedLimit, area, contractNumber, tarifName);
        }

        public IProviderCollection CreateProviderCollection()
        {
            return new ProviderList();
        }
    }
}
