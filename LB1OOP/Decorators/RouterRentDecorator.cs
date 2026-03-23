using LB1OOP.interfaces;

namespace LB1OOP
{
    public class RouterRentDecorator : ProviderDecorator
    {
        public RouterRentDecorator(IProvider wrapper) : base(wrapper) { }

        public override string TarifName
        {
            get => base.TarifName + " + [Роутер]";
            set => base.TarifName = value;
        }

        public override float TarifCoast
        {
            get => base.TarifCoast + 200;
            set => base.TarifCoast = value;
        }
    }
}