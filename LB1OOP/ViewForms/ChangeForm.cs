using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB1OOP
{
    /// <summary>
    /// Форма для редактирования информации о провайдере.
    /// Позволяет изменять все свойства объекта <see cref="InternetProvider"/>.
    /// </summary>
    public partial class ChangeForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
        private const uint MB_OK = 0x00000000;
        private const uint MB_ICONERROR = 0x00000010;

        public event Action<string, float, int, float, float, int, string> ClickToSave;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ChangeForm"/>.
        /// </summary>
        public ChangeForm()
        {
            InitializeComponent();
        }

        public void UpdateProviderInfo(string name, string userCount, string speedLimit,
           string area, string contract, string tarifName, string tarifCost)
        {
            NameTextBox.Text = name;
            userCountTextBox.Text = userCount;
            speedLimitTextBox.Text = speedLimit;
            areaTextBox.Text = area;
            contractNumberTextBox.Text = contract;
            tarifNameTextBox.Text = tarifName;
            tarifCoastTextBox.Text = tarifCost;
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Назад".
        /// Закрывает текущую форму без сохранения изменений.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Объект <see cref="EventArgs"/>, содержащий данные события.</param>
        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Сохранить".
        /// Сохраняет изменения в объекте провайдера и закрывает форму.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Объект <see cref="EventArgs"/>, содержащий данные события.</param>
        private void save_button_Click(object sender, EventArgs e)
        {
            try
            {
                string name = NameTextBox.Text.Trim();
                int userCount = int.Parse(userCountTextBox.Text.Trim());
                float speedLimit = speedLimitTextBox.Text.Trim() != "Не указан" ? float.Parse(speedLimitTextBox.Text.Trim()) : 0;
                float area = float.Parse(areaTextBox.Text.Trim());
                int contract = int.Parse(contractNumberTextBox.Text.Trim());
                string tarif = tarifNameTextBox.Text.Trim();
                float tarifCoast = float.Parse(tarifCoastTextBox.Text.Trim());

                ClickToSave?.Invoke(name, tarifCoast, userCount, speedLimit, area, contract, tarif);
            }
            catch (Exception ex)
            {
                MessageBox(this.Handle, "Ошибка ввода данных: " + ex.Message, "Ошибка", MB_OK | MB_ICONERROR);
            }
        }

        /// <summary>
        /// Обрабатывает событие закрытия формы.
        /// </summary>
        /// <param name="e">Объект <see cref="FormClosingEventArgs"/>, содержащий данные события.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                
            }
        }
    }
}
