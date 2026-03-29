
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LB1OOP
{
    /// <summary>
    /// Главная форма приложения для отображения и редактирования информации о провайдере.
    /// </summary>
    public partial class MainForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
        private const uint MB_OK = 0x00000000;
        private const uint MB_ICONERROR = 0x00000010;
        private const uint MB_ICONINFORMATION = 0x00000040;

        public event Action addClicked;
        public event Action removeClicked;
        public event Action changeClicked;
        public event Action perfomanceTesterClicked;
        public event Action calcDensityClicked;
        public event Action<int> providerSelected;

        public MainForm()
        {
            InitializeComponent();
        }

        public void UpdateProviderInfo(string name, string userCount, string speedLimit,
           string area, string contract, string tarifName, string tarifCost, string hexCount)
        {
            NameTextBox.Text = name;
            userCountTextBox.Text = userCount;
            speedLimitTextBox.Text = speedLimit;
            areaTextBox.Text = area;
            contractNumberTextBox.Text = contract;
            tarifNameTextBox.Text = tarifName;
            tarifCoastTextBox.Text = tarifCost;
            UserCountHextextBox.Text = hexCount;
        }

        public void ClearProviderInfo()
        {
            UpdateProviderInfo("", "", "", "", "", "", "", "");
        }

        public void AddLog(string message)
        {
            string log = $"[{DateTime.Now:HH:mm:ss}] {message}";
            listBoxEvents.Items.Add(log);
        }

        public void ShowError(string message, string title = "Ошибка")
        {
            MessageBox(this.Handle, message, title, MB_OK | MB_ICONERROR);
        }
        public void ShowInfo(string message, string title = "Информация")
        {
            MessageBox(this.Handle, message, title, MB_OK | MB_ICONINFORMATION);
        }

        private void add_button_Click(object sender, EventArgs e) => addClicked?.Invoke();

        private void change_button_Click(object sender, EventArgs e) => changeClicked?.Invoke();

        private void remove_button_Click(object sender, EventArgs e) => removeClicked?.Invoke();

        private void test_button_Click(object sender, EventArgs e) => perfomanceTesterClicked?.Invoke();
        
        private void btnTestException_Click_1(object sender, EventArgs e) => calcDensityClicked?.Invoke();

        private void listBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProviders.SelectedItem != null)
            {
                int selectedIndex = listBoxProviders.SelectedIndex;
                providerSelected?.Invoke(selectedIndex);
            }
        }

        public void RefreshProviderList(IEnumerable<string> providerNames)
        {
            listBoxProviders.Items.Clear();
            foreach (var name in providerNames)
            {
                listBoxProviders.Items.Add(name);
            }
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
    }
}
