using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP
{
    /// <summary>
    /// Представляет исключение, возникающее при попытке деления на ноль.
    /// Переопределенное наследование от DivideByZeroException.
    /// </summary>
    class CustomDivideByZeroException : DivideByZeroException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CustomDivideByZeroException"/>.
        /// </summary>
        CustomDivideByZeroException() : base() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CustomDivideByZeroException"/> с указанным сообщением об ошибке.
        /// </summary>
        /// <param name="message">Сообщение, описывающее ошибку.</param>
        CustomDivideByZeroException(string message) : base(message) { }

        /// <summary>
        /// Возвращает строковое представление исключения.
        /// </summary>
        /// <returns>Строка с детальной информацией об ошибке.</returns>
        public override string ToString()
        {
            return $"Ошибка деления на 0:\n{Message}";
        }
    }
}
