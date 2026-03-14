using LB1OOP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP
{
    public delegate void ProviderCollectionChangedEventHandler(IProvider provider, string message);
    public class ProviderList : IProviderCollection
    {
        private List<IProvider> _providers;

        public event ProviderCollectionChangedEventHandler providerAdded;
        public event ProviderCollectionChangedEventHandler providerRemoved;

        public ProviderList()
        {
            _providers = new List<IProvider>();
        }
        public void AddProvider(IProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            _providers.Add(provider);
            OnProviderAdd(provider);
        }

        public bool RemoveProvider(IProvider provider)
        {
            if (provider == null) return false;
            bool result = _providers.Remove(provider);
            
            if (result) 
            {
                OnProviderRemove(provider);
            }

            return result;
        }

        protected virtual void OnProviderAdd(IProvider provider)
        {
            providerAdded?.Invoke(provider, "Добавлен в коллекцию");
        }

        protected virtual void OnProviderRemove(IProvider provider) 
        {
            providerRemoved?.Invoke(provider, "Удален из коллекции");
        }
        
        public IEnumerable<IProvider> GetAll()
        {
            return _providers;
        }

        public List<IProvider> GetInternalList()
        {
            return _providers;
        }

        public int Count => _providers.Count;
    }
}
