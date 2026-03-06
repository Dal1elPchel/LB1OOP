using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.Interfaces
{
    public interface IProviderFactory
    {

        IProvider CreateBasicProvider();
        IProvider CreateProviderWithName(string name);
        IProvider CreateProviderWithTarif(string name, float tarifCost);
        IProvider CreateProvider(
            string name = "Undefined",
            float tarifCoast = 0,
            int userCount = 0,
            float speedLimit = 0,
            float area = 0,
            int contractNumber = 0,
            string tarifName = "Undefined");

        IProviderCollection CreateProviderCollection();
    }
}
