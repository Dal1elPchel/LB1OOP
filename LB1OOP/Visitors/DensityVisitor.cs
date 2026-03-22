using LB1OOP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.Visitors
{
    public class DensityVisitor : IProviderVisitor
    {
        private float _result;

        public float Result {  get { return _result; } }

        /// <summary>
        /// Вычисляет плотность абонентов на квадратный километр.
        /// </summary>
        /// <returns>Количество абонентов на кв. км.</returns>
        /// <exception cref="CustomDivideByZeroException">
        /// Выбрасывается при попытке деления на ноль (когда площадь покрытия равна 0).
        /// </exception>
        public void Visit(IProvider provider)
        {
            if (provider.Area == 0)
            {
                throw new CustomDivideByZeroException(
                    $"Невозможно вычислить плотность абонентов: у провайдера {provider.Name} нет зоны покрытия");
            }

            _result = provider.UserCount / provider.Area;
        }
    }
}
