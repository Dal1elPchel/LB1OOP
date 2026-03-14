using LB1OOP.Interfaces;
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
    /// Форма для редактирования информации о провайдере.
    /// Позволяет изменять все свойства объекта <see cref="InternetProvider"/>.
    /// </summary>
    public partial class Change_Form : Form
    {
        private IProvider editProvider;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Change_Form"/>.
        /// </summary>
        /// <param name="provider">Объект провайдера для редактирования.</param>
        public Change_Form(IProvider provider)
        {
            InitializeComponent();
            editProvider = provider;
            DisplayProviderInfo();
        }

        /// <summary>
        /// Отображает текущую информацию о провайдере в текстовых полях формы.
        /// </summary>
        private void DisplayProviderInfo()
        {

            NameTextBox.Text = editProvider.Name;
            userCountTextBox.Text = editProvider.UserCount.ToString();
            speedLimitTextBox.Text = editProvider.SpeedLimit == 0 ? "Не задан" : editProvider.SpeedLimit.ToString();
            areaTextBox.Text = editProvider.Area.ToString();
            contractNumberTextBox.Text = editProvider.ContractNumber.ToString();
            tarifNameTextBox.Text = editProvider.TarifName.ToString();
            tarifCoastTextBox.Text = editProvider.TarifCoast.ToString();
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
                editProvider.Name = NameTextBox.Text;
                editProvider.UserCount = int.Parse(userCountTextBox.Text);
                editProvider.SpeedLimit = speedLimitTextBox.Text == "Не задан" ? 0 : float.Parse(speedLimitTextBox.Text);
                editProvider.Area = float.Parse(areaTextBox.Text);
                editProvider.ContractNumber = int.Parse(contractNumberTextBox.Text);
                editProvider.TarifName = tarifNameTextBox.Text;
                editProvider.TarifCoast = float.Parse(tarifCoastTextBox.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            } catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
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
