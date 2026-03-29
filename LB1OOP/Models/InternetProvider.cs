using LB1OOP.Interfaces;
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
    public class InternetProvider : IProvider
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


        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="InternetProvider"/> со значениями по умолчанию.
        /// </summary>
        public InternetProvider()
        {
            this._userCount = 0;
            this._speedLimit = 0;
            this._area = 100;
            this._contractNumber = 0;
            this._tarifName = "Не указан";
            this._tarifCoast = 100;
            this._name = "Не указан";
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="InternetProvider"/> с указанным названием.
        /// </summary>
        /// <param name="name">Название провайдера.</param>
        /// <exception cref="ArgumentException">Выбрасывается при пустом значении параметра.</exception>
        public InternetProvider(string name = "Не указан")
        {
            Validator.ValidateName(name);

            this._name = name;
            this._userCount = 0;
            this._speedLimit = 0;
            this._area = 100;
            this._contractNumber = 0;
            this._tarifName = "Не указан";
            this._tarifCoast = 100;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="InternetProvider"/> с указанным названием и стоимостью тарифа.
        /// </summary>
        /// <param name="name">Название провайдера.</param>
        /// <param name="tarifCoast">Стоимость тарифного плана.</param>
        /// <exception cref="ArgumentException">Выбрасывается при некорректных значениях параметров.</exception>
        public InternetProvider(string name = "Не указан", float tarifCoast = 100)
        {
            Validator.ValidateName(name);
            Validator.ValidateTarifCoast(tarifCoast);

            this._name = name;
            this._tarifCoast = tarifCoast;
            this._userCount = 0;
            this._speedLimit = 0;
            this._area = 100;
            this._contractNumber = 0;
            this._tarifName = "Не указан";
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="InternetProvider"/> с полным набором параметров.
        /// </summary>
        /// <param name="name">Название провайдера.</param>
        /// <param name="tarifCoast">Стоимость тарифного плана.</param>
        /// <param name="userCount">Количество абонентов.</param>
        /// <param name="speedLimit">Максимальная скорость интернета.</param>
        /// <param name="area">Площадь покрытия.</param>
        /// <param name="contractNumber">Номер договора.</param>
        /// <param name="tarifName">Название тарифа.</param>
        /// <exception cref="ArgumentException">Выбрасывается при некорректных значениях параметров.</exception>
        public InternetProvider(string name = "Не указан", float tarifCoast = 100,
            int userCount = 0, float speedLimit = 0, float area = 100,
            int contractNumber = 0, string tarifName = "Не указан")
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
        }

        /// <summary>
        /// Возвращает количество абонентов в шестнадцатеричном формате.
        /// </summary>
        /// <returns>Строковое представление числа в шестнадцатеричной системе счисления.</returns>
        public string GetUserCountInHex() => this.UserCount.ToString("X");


        /// <summary>
        /// Возвращает строковое представление объекта провайдера.
        /// </summary>
        /// <returns>Строка, содержащая имя провайдера.</returns>
        public override string ToString()
        {
            return $"{this.Name}";
        }

        public void Accept(IProviderVisitor visitor) { visitor.Visit(this); }
    }
}