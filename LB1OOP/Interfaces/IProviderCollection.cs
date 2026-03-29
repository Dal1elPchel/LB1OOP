using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.Interfaces
{
    public interface IProviderCollection
    {
        int Count { get; }

        event ProviderCollectionChangedEventHandler providerAdded;
        event ProviderCollectionChangedEventHandler providerRemoved;

        void AddProvider(IProvider provider);
        bool RemoveProvider(IProvider provider);
        IEnumerable<IProvider> GetAll();
    }
}
