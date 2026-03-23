using LB1OOP.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP
{
    public delegate void ProviderCollectionChangedEventHandler(IProvider IProvider, string message);
    public class ProviderCollection
    {
        private List<IProvider> _IProviders;

        public event ProviderCollectionChangedEventHandler ProviderAdded;
        public event ProviderCollectionChangedEventHandler ProviderRemoved;

        public ProviderCollection()
        {
            _IProviders = new List<IProvider>();
        }
        public void AddProvider(IProvider IProvider)
        {
            if (IProvider == null) throw new ArgumentNullException(nameof(IProvider));
            _IProviders.Add(IProvider);
            OnProviderAdd(IProvider);
        }

        public bool RemoveProvider(IProvider IProvider)
        {
            if (IProvider == null) return false;
            bool result = _IProviders.Remove(IProvider);
            
            if (result) 
            {
                OnProviderRemove(IProvider);
            }

            return result;
        }

        protected virtual void OnProviderAdd(IProvider IProvider)
        {
            ProviderAdded?.Invoke(IProvider, "Добавлен в коллекцию");
        }

        protected virtual void OnProviderRemove(IProvider IProvider) 
        {
            ProviderRemoved?.Invoke(IProvider, "Удален из коллекции");
        }
        
        public IEnumerable<IProvider> GetAll()
        {
            return _IProviders;
        }

        public int Count => _IProviders.Count;
    }
}
