using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP
{
    public delegate void ProviderCollectionChangedEventHandler(Provider provider, string action);
    public class ProviderCollection
    {
        private List<Provider> _providers;

        public event ProviderCollectionChangedEventHandler providerAdded;
        public event ProviderCollectionChangedEventHandler providerRemoved;

        public ProviderCollection()
        {
            _providers = new List<Provider>();
        }
        public void AddProvider(Provider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            _providers.Add(provider);
            OnProviderAdd(provider);
        }

        public bool RemoveProvider(Provider provider)
        {
            if (provider == null) return false;
            bool result = _providers.Remove(provider);
            
            if (result) 
            {
                OnProviderRemove(provider);
            }

            return result;
        }

        protected virtual void OnProviderAdd(Provider provider)
        {
            providerAdded?.Invoke(provider, "Добавлен в коллекцию");
        }

        protected virtual void OnProviderRemove(Provider provider) 
        {
            providerRemoved?.Invoke(provider, "Удален из коллекции");
        }
        
        public IEnumerable<Provider> GetAll()
        {
            return _providers;
        }

        public List<Provider> GetInternalList()
        {
            return _providers;
        }

        public int Count => _providers.Count;
    }
}
