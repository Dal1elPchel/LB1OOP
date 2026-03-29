using LB1OOP.Interfaces;
using LB1OOP.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP
{
    /// <summary>
    /// Представляет собой фасад бизнес-логики приложения (Модель в паттерне MVC).
    /// Управляет созданием, хранением и обработкой данных интернет-провайдеров.
    /// </summary>
    public class AppModel
    {
        /// <summary>
        /// Абстрактная фабрика для инстанцирования объектов провайдеров и коллекций.
        /// </summary>
        private IProviderFactory _factory;

        /// <summary>
        /// Получает или задает текущего выбранного интернет-провайдера.
        /// </summary>
        /// <value>Объект, реализующий интерфейс <see cref="IProvider"/>, или <c>null</c>, если провайдер не выбран.</value>
        public IProvider CurrentProvider { get; set; }

        /// <summary>
        /// Получает коллекцию всех созданных интернет-провайдеров.
        /// </summary>
        /// <value>Коллекция объектов, реализующая интерфейс <see cref="IProviderCollection"/>.</value>
        public IProviderCollection Collection { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AppModel"/>.
        /// Создает коллекцию и добавляет в нее провайдера по умолчанию.
        /// </summary>
        /// <param name="_newFactory">Фабрика для создания зависимостей.</param>
        public AppModel(IProviderFactory _newFactory)
        {
            _factory = _newFactory;
            Collection = _factory.CreateProviderCollection();
            CurrentProvider = _factory.CreateProviderWithName("МТС");
            Collection.AddProvider(CurrentProvider);
        }

        /// <summary>
        /// Создает нового интернет-провайдера с заданными параметрами, 
        /// добавляет его в коллекцию и устанавливает в качестве текущего.
        /// </summary>
        /// <param name="name">Название провайдера.</param>
        /// <param name="tarifCoast">Стоимость тарифного плана в рублях.</param>
        /// <param name="userCount">Количество подключенных абонентов.</param>
        /// <param name="speedLimit">Максимальная скорость интернета в Мбит/с.</param>
        /// <param name="area">Площадь покрытия сети в квадратных километрах.</param>
        /// <param name="contractNumber">Уникальный номер договора.</param>
        /// <param name="tarifName">Наименование тарифного плана.</param>
        public bool CreateNewProvider(string name = "Не указан", float tarifCoast = 100,
            int userCount = 0, float speedLimit = 0, float area = 100,
            int contractNumber = 0, string tarifName = "Не указан")
        {
            IProvider createdProvider = null;
            createdProvider = _factory.CreateProvider(name, tarifCoast, userCount, speedLimit,
                area, contractNumber, tarifName);

            if (createdProvider != null)
            {
                Collection.AddProvider(createdProvider);
                CurrentProvider = createdProvider;
                return true;
            }
            return false;
        }

        public IProvider GetProviderByIndex(int index)
        {
            var list = Collection.GetAll().ToList();
            if (index >= 0 && index < list.Count)
            {
                return list[index];
            }
            return null;
        }

        /// <summary>
        /// Удаляет указанного провайдера из коллекции.
        /// Если удаляемый провайдер является текущим, свойство <see cref="CurrentProvider"/> принимает значение <c>null</c>.
        /// </summary>
        /// <param name="removingProvider">Провайдер, подлежащий удалению.</param>
        public bool RemoveProvider(IProvider removingProvider)
        {
            if (removingProvider == null) return false;

            if (Collection.RemoveProvider(removingProvider))
            {
                if (CurrentProvider == removingProvider)
                {
                    CurrentProvider = null;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Вычисляет плотность абонентов (количество человек на квадратный километр) для указанного провайдера.
        /// Использует паттерн "Посетитель" (<see cref="DensityVisitor"/>).
        /// </summary>
        /// <param name="selectedProvider">Провайдер, для которого выполняется расчет.</param>
        /// <returns>Строковое представление вычисленной плотности.</returns>
        public string GetUserDensity(IProvider selectedProvider)
        {
            IProviderVisitor densityVisitor = new DensityVisitor();
            selectedProvider.Accept(densityVisitor);

            return densityVisitor.Result;
        }

        /// <summary>
        /// Преобразует количество абонентов указанного провайдера в шестнадцатеричную систему счисления.
        /// Использует паттерн "Посетитель" (<see cref="HexVisitor"/>).
        /// </summary>
        /// <param name="selectedProvider">Провайдер, данные которого необходимо преобразовать.</param>
        /// <returns>Строка, содержащая количество абонентов в шестнадцатеричном формате.</returns>
        public string GetUserCountInHEX(IProvider selectedProvider)
        {
            IProviderVisitor hexVisitor = new HexVisitor();
            selectedProvider.Accept(hexVisitor);

            return hexVisitor.Result;
        }

        /// <summary>
        /// Обновляет атрибуты существующего интернет-провайдера и устанавливает его в качестве текущего.
        /// </summary>
        /// <param name="provider">Объект провайдера для обновления. Если передан <c>null</c>, операция отменяется.</param>
        /// <param name="name">Новое название провайдера.</param>
        /// <param name="tarifCoast">Новая стоимость тарифа.</param>
        /// <param name="userCount">Новое количество абонентов.</param>
        /// <param name="speedLimit">Новая скорость интернета.</param>
        /// <param name="area">Новая площадь покрытия.</param>
        /// <param name="contractNumber">Новый номер договора.</param>
        /// <param name="tarifName">Новое название тарифа.</param>
        public void UpdateProvider(IProvider provider, string name, float tarifCoast,
            int userCount, float speedLimit, float area,
            int contractNumber, string tarifName)
        {
            if (provider == null) return;

            provider.Name = name;
            provider.TarifCoast = tarifCoast;
            provider.Area = area;
            provider.UserCount = userCount;
            provider.SpeedLimit = speedLimit;
            provider.ContractNumber = contractNumber;
            provider.TarifName = tarifName;

            CurrentProvider = provider;
        }
    }
}