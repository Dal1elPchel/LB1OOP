using LB1OOP.interfaces;

namespace LB1OOP
{
    /// <summary>
    /// Базовый класс декоратора. Реализует IProvider и пробрасывает все вызовы внутреннему объекту.
    /// </summary>
    public abstract class ProviderDecorator : IProvider
    {
        protected IProvider _wrapper;
        public IProvider InnerProvider => _wrapper;

        public ProviderDecorator(IProvider wrapper)
        {
            _wrapper = wrapper;
        }

        public virtual string Name
        {
            get => _wrapper.Name;
            set => _wrapper.Name = value;
        }

        public virtual string TarifName
        {
            get => _wrapper.TarifName;
            set => _wrapper.TarifName = value;
        }

        public virtual float TarifCoast
        {
            get => _wrapper.TarifCoast;
            set => _wrapper.TarifCoast = value;
        }

        public virtual int UserCount
        {
            get => _wrapper.UserCount;
            set => _wrapper.UserCount = value;
        }

        public virtual float SpeedLimit
        {
            get => _wrapper.SpeedLimit;
            set => _wrapper.SpeedLimit = value;
        }

        public virtual float Area
        {
            get => _wrapper.Area;
            set => _wrapper.Area = value;
        }

        public virtual int ContractNumber
        {
            get => _wrapper.ContractNumber;
            set => _wrapper.ContractNumber = value;
        }

        public virtual string GetUserCountInHex()
        {
            return _wrapper.GetUserCountInHex();
        }

        public virtual float CalculateUserDensity()
        {
            return _wrapper.CalculateUserDensity();
        }

        public override string ToString()
        {
            return _wrapper.ToString();
        }
    }
}