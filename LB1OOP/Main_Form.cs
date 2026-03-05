using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB1OOP
{
    /// <summary>
    /// Главная форма приложения для отображения и редактирования информации о провайдере.
    /// </summary>
    public partial class Main_Form : Form
    {
        private Change_Form _form3;
        private Provider provider;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Main_Form"/>.
        /// </summary>
        public Main_Form()
        {
            InitializeComponent();
            provider = new Provider("МТС");
            DisplayProviderInfo();
        }

        /// <summary>
        /// Отображает информацию о провайдере в текстовых полях формы.
        /// </summary>
        private void DisplayProviderInfo()
        {
            NameTextBox.Text = provider.ToString();
            userCountTextBox.Text = provider.UserCount.ToString();
            speedLimitTextBox.Text = provider.SpeedLimit.ToString();
            areaTextBox.Text = provider.Area.ToString();
            contractNumberTextBox.Text = provider.ContractNumber.ToString();
            tarifNameTextBox.Text = provider.TarifName.ToString();
            tarifCoastTextBox.Text = provider.TarifCoast.ToString();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки изменения данных провайдера.
        /// Открывает форму редактирования <see cref="Change_Form"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Объект <see cref="EventArgs"/>, содержащий данные события.</param>
        private void change_button_Click(object sender, EventArgs e)
        {
            _form3 = new Change_Form(provider);
            _form3.ShowDialog();
            DisplayProviderInfo();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки выхода из приложения.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Объект <see cref="EventArgs"/>, содержащий данные события.</param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Обрабатывает событие закрытия формы.
        /// Предотвращает закрытие формы при нажатии на крестик и завершает приложение.
        /// </summary>
        /// <param name="e">Объект <see cref="FormClosingEventArgs"/>, содержащий данные события.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Application.Exit();
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки тестирования исключения.
        /// Вычисляет плотность абонентов и обрабатывает возможное исключение <see cref="CustomDivideByZeroException"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Объект <see cref="EventArgs"/>, содержащий данные события.</param>
        private void btnTestException_Click_1(object sender, EventArgs e)
        {
            try
            {
                float result = provider.CalculateUserDensity();
                MessageBox.Show($"Результат: {result}");
            }
            catch (CustomDivideByZeroException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}