using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP
{
    /// <summary>
    /// Предоставляет методы для валидации данных интернет-провайдера.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Проверяет корректность названия провайдера.
        /// </summary>
        /// <param name="name">Название провайдера.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если длина названия меньше 2 или больше 30 символов.
        /// </exception>
        public static void ValidateName(string name)
        {
            if (name.Length < 2 || name.Length > 30)
            {
                throw new ArgumentException("Название оператора должно содержать от 2 до 30 символов", nameof(name));
            }
        }

        /// <summary>
        /// Проверяет корректность названия тарифного плана.
        /// </summary>
        /// <param name="tarifName">Название тарифа.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если название не равно "Undefined" и его длина меньше 2 или больше 30 символов.
        /// </exception>
        /// <remarks>
        /// Значение "Undefined" является допустимым и используется по умолчанию.
        /// </remarks>
        public static void ValidateTarifName(string tarifName)
        {
            if (tarifName != "Undefined" && (tarifName.Length < 2 || tarifName.Length > 30))
            {
                throw new ArgumentException("Название тарифа должно содержать от 2 до 30 символов", nameof(tarifName));
            }
        }

        /// <summary>
        /// Проверяет корректность стоимости тарифного плана.
        /// </summary>
        /// <param name="tarifCoast">Стоимость тарифа в рублях.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если стоимость меньше 0 или больше 100 000 рублей.
        /// </exception>
        public static void ValidateTarifCoast(float tarifCoast)
        {
            if (tarifCoast < 0 || tarifCoast > 100_000)
            {
                throw new ArgumentOutOfRangeException(nameof(tarifCoast), "Стоимость тарифа должна быть от 0 до 100 000");
            }
        }

        /// <summary>
        /// Проверяет корректность количества абонентов.
        /// </summary>
        /// <param name="userCount">Количество пользователей.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если количество абонентов меньше 0 или больше 1 миллиарда.
        /// </exception>
        public static void ValidateUserCount(int userCount)
        {
            if (userCount < 0 || userCount > 1_000_000_000)
            {
                throw new ArgumentOutOfRangeException(nameof(userCount), "Количество пользователей должно быть от 0 до 1 миллиарда");
            }
        }

        /// <summary>
        /// Проверяет корректность максимальной скорости интернета.
        /// </summary>
        /// <param name="speedLimit">Скорость в Мбит/с.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если скорость меньше 0 или больше 10 000 Мбит/с.
        /// </exception>
        public static void ValidateSpeedLimit(float speedLimit)
        {
            if (speedLimit < 0 || speedLimit > 10_000)
            {
                throw new ArgumentOutOfRangeException(nameof(speedLimit), "Скорость должна быть от 0 до 10 000 Мбит/с");
            }
        }

        /// <summary>
        /// Проверяет корректность площади покрытия.
        /// </summary>
        /// <param name="area">Площадь покрытия в квадратных километрах.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если площадь меньше 0 или больше 100 000 000 кв. км.
        /// </exception>
        public static void ValidateArea(float area)
        {
            if (area < 0 || area > 100_000_000)
            {
                throw new ArgumentOutOfRangeException(nameof(area), "Площадь покрытия должна быть от 0 до 100 000 000 кв. км");
            }
        }

        /// <summary>
        /// Проверяет корректность номера договора.
        /// </summary>
        /// <param name="contractNumber">Номер договора.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если номер меньше 0 или больше 999 999 999.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если номер содержит больше 9 цифр.
        /// </exception>
        public static void ValidateContractNumber(int contractNumber)
        {
            if (contractNumber < 0 || contractNumber > 999_999_999)
            {
                throw new ArgumentOutOfRangeException(nameof(contractNumber), "Номер договора должен быть от 0 до 999 999 999");
            }

            if (contractNumber.ToString().Length > 9)
            {
                throw new ArgumentException("Номер договора не может содержать больше 9 цифр", nameof(contractNumber));
            }
        }
    }
}