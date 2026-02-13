using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP
{
    /// <summary>
    /// Представляет интернет-провайдера с информацией о тарифном плане и абонентах.
    /// </summary>
    public class Provider
    {
        private int _userCount;
        private float _speedLimit;
        private float _area;
        private int _contractNumber;
        private string _name;
        private string _tarifName;
        private float _tarifCoast;

        /// <summary>
        /// Получает или задает количество абонентов провайдера.
        /// </summary>
        /// <value>Неотрицательное целое число.</value>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается при отрицательном значении.</exception>
        public int UserCount
        {
            get => _userCount;
            set
            {
                Validator.ValidateUserCount(value);
                _userCount = value;
            }
        }

        /// <summary>
        /// Получает или задает максимальную скорость интернета в Мбит/с.
        /// </summary>
        /// <value>Положительное число.</value>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается при значении меньше или равном нулю.</exception>
        public float SpeedLimit
        {
            get => _speedLimit;
            set
            {
                Validator.ValidateSpeedLimit(value);
                _speedLimit = value;
            }
        }

        /// <summary>
        /// Получает или задает площадь покрытия провайдера в квадратных километрах.
        /// </summary>
        /// <value>Положительное число.</value>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается при значении меньше или равном нулю.</exception>
        public float Area
        {
            get => _area;
            set
            {
                Validator.ValidateArea(value);
                _area = value;
            }
        }

        /// <summary>
        /// Получает или задает номер договора провайдера.
        /// </summary>
        /// <value>Целое положительное число.</value>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается при значении меньше или равном нулю.</exception>
        /// <exception cref="ArgumentException">Выбрасывается при значении несоответсвующем длине в 9 цифр.</exception>
        public int ContractNumber
        {
            get => _contractNumber;
            set
            {
                Validator.ValidateContractNumber(value);
                _contractNumber = value;
            }
        }

        /// <summary>
        /// Получает или задает название провайдера.
        /// </summary>
        /// <value>Название не может быть пустым или состоять только из пробелов.</value>
        /// <exception cref="ArgumentException">Выбрасывается при пустом значении.</exception>
        public string Name
        {
            get => _name;
            set
            {
                Validator.ValidateName(value);
                _name = value;
            }
        }

        /// <summary>
        /// Получает или задает название тарифного плана.
        /// </summary>
        /// <value>Название не может быть пустым или состоять только из пробелов.</value>
        /// <exception cref="ArgumentException">Выбрасывается при пустом значении.</exception>
        public string TarifName
        {
            get => _tarifName;
            set
            {
                Validator.ValidateTarifName(value);
                _tarifName = value;
            }
        }

        /// <summary>
        /// Получает или задает стоимость тарифного плана в рублях.
        /// </summary>
        /// <value>Неотрицательное число.</value>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается при значении меньше 0 или больше 100_000.</exception>
        public float TarifCoast
        {
            get => _tarifCoast;
            set
            {
                Validator.ValidateTarifCoast(value);
                _tarifCoast = value;
            }
        }

        private static LinkedList<Provider> _providerCount = new LinkedList<Provider>();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Provider"/> со значениями по умолчанию.
        /// </summary>
        public Provider()
        {
            this._userCount = 0;
            this._speedLimit = 0;
            this._area = 0;
            this._contractNumber = 0;
            this._tarifName = "Undefined";
            this._tarifCoast = 0;
            this._name = "Undefined";

            AddNewProvider(this);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Provider"/> с указанным названием.
        /// </summary>
        /// <param name="name">Название провайдера.</param>
        /// <exception cref="ArgumentException">Выбрасывается при пустом значении параметра.</exception>
        public Provider(string name = "Undefined")
        {
            Validator.ValidateName(name);

            this._name = name;
            this._userCount = 0;
            this._speedLimit = 0;
            this._area = 0;
            this._contractNumber = 0;
            this._tarifName = "Undefined";
            this._tarifCoast = 0;

            AddNewProvider(this);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Provider"/> с указанным названием и стоимостью тарифа.
        /// </summary>
        /// <param name="name">Название провайдера.</param>
        /// <param name="tarifCoast">Стоимость тарифного плана.</param>
        /// <exception cref="ArgumentException">Выбрасывается при некорректных значениях параметров.</exception>
        public Provider(string name = "Undefined", float tarifCoast = 0)
        {
            Validator.ValidateName(name);
            Validator.ValidateTarifCoast(tarifCoast);

            this._name = name;
            this._tarifCoast = tarifCoast;
            this._userCount = 0;
            this._speedLimit = 0;
            this._area = 0;
            this._contractNumber = 0;
            this._tarifName = "Undefined";

            AddNewProvider(this);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Provider"/> с полным набором параметров.
        /// </summary>
        /// <param name="name">Название провайдера.</param>
        /// <param name="tarifCoast">Стоимость тарифного плана.</param>
        /// <param name="userCount">Количество абонентов.</param>
        /// <param name="speedLimit">Максимальная скорость интернета.</param>
        /// <param name="area">Площадь покрытия.</param>
        /// <param name="contractNumber">Номер договора.</param>
        /// <param name="tarifName">Название тарифа.</param>
        /// <exception cref="ArgumentException">Выбрасывается при некорректных значениях параметров.</exception>
        public Provider(string name = "Undefined", float tarifCoast = 0,
            int userCount = 0, float speedLimit = 0, float area = 0,
            int contractNumber = 0, string tarifName = "Undefined")
        {
            Validator.ValidateName(name);
            Validator.ValidateTarifName(tarifName);
            Validator.ValidateTarifCoast(tarifCoast);
            Validator.ValidateUserCount(userCount);
            Validator.ValidateSpeedLimit(speedLimit);
            Validator.ValidateArea(area);
            Validator.ValidateContractNumber(contractNumber);

            this._name = name;
            this._tarifCoast = tarifCoast;
            this._userCount = userCount;
            this._speedLimit = speedLimit;
            this._area = area;
            this._contractNumber = contractNumber;
            this._tarifName = tarifName;

            AddNewProvider(this);
        }

        /// <summary>
        /// Возвращает количество абонентов в шестнадцатеричном формате.
        /// </summary>
        /// <returns>Строковое представление числа в шестнадцатеричной системе счисления.</returns>
        public string GetUserCountInHex() => this.UserCount.ToString("X");

        /// <summary>
        /// Вычисляет плотность абонентов на квадратный километр.
        /// </summary>
        /// <returns>Количество абонентов на кв. км.</returns>
        /// <exception cref="CustomDivideByZeroException">
        /// Выбрасывается при попытке деления на ноль (когда площадь покрытия равна 0).
        /// </exception>
        public float CalculateUserDensity()
        {
            if (this.Area == 0)
            {
                throw new CustomDivideByZeroException(
                    $"Невозможно вычислить плотность абонентов: у провайдера {this.Name} нет зоны покрытия");
            }

            return this.UserCount / this.Area;
        }

        /// <summary>
        /// Возвращает строковое представление объекта провайдера.
        /// </summary>
        /// <returns>Строка, содержащая имя провайдера.</returns>
        public override string ToString()
        {
            return $"{this.Name}";
        }

        /// <summary>
        /// Добавляет провайдера в статический список всех созданных экземпляров.
        /// </summary>
        /// <param name="provider">Добавляемый провайдер.</param>
        private static void AddNewProvider(Provider provider) => _providerCount.AddLast(provider);
    }
}