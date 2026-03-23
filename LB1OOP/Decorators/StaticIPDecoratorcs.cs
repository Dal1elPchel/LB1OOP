using LB1OOP.interfaces;

namespace LB1OOP
{
    public class StaticIpDecorators : ProviderDecorator
    {
        public StaticIpDecorators(IProvider wrapper) : base(wrapper) { }

        public override string TarifName
        {
            get => base.TarifName + " + [IP]";
            set => base.TarifName = value;
        }

        public override float TarifCoast
        {
            get => base.TarifCoast + 150;
            set => base.TarifCoast = value;
        }
    }
}