using LB1OOP.Factories;
using LB1OOP.Interfaces;
using LB1OOP.View.Interfaces;
using LB1OOP.Visitors;
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
    /// Главная форма приложения для отображения и редактирования информации о провайдере.
    /// </summary>
    public partial class Main_Form : Form, IMainForm
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
        private const uint MB_OK = 0x00000000;
        private const uint MB_ICONERROR = 0x00000010;
        private const uint MB_ICONINFORMATION = 0x00000040;

        public string ProviderName
        {
            get => NameTextBox.Text;
            set => NameTextBox.Text = value;
        }
        public int UserCount
        {
            get => int.TryParse(userCountTextBox.Text, out var val) ? val : 0;
            set => userCountTextBox.Text = value.ToString();
        }
        public int SpeedLimit
        {
            get => int.TryParse(speedLimitTextBox.Text, out var val) ? val : 0;
            set => speedLimitTextBox.Text = value == 0 ? "Не задан" : value.ToString();
        }
        public double Area
        {
            get => double.TryParse(areaTextBox.Text, out var val) ? val : 0;
            set => areaTextBox.Text = value.ToString();
        }
        public int ContractNumber
        {
            get => int.TryParse(contractNumberTextBox.Text, out var val) ? val : 0;
            set => contractNumberTextBox.Text = value.ToString();
        }
        public string TarifName
        {
            get => tarifNameTextBox.Text;
            set => tarifNameTextBox.Text = value;
        }

        public decimal TarifCoast
        {
            get => decimal.TryParse(tarifCoastTextBox.Text, out var val) ? val : 0;
            set => tarifCoastTextBox.Text = value.ToString();
        }

        public string UserCountHex
        {
            get => UserCountHextextBox.Text;
            set => UserCountHextextBox.Text = value;
        }

        public string DensityResult
        {
            get => DensityTextBox.Text;
            set => DensityTextBox.Text = value;
        }

        public long VectorSumResult
        {
            get => long.TryParse(sumTextBox.Text, out var val) ? val : 0;
            set => sumTextBox.Text = value.ToString();
        }

        public string ServerTimeResult
        {
            get => serverTimeTextBox.Text;
            set => serverTimeTextBox.Text = value;
        }

        public IEnumerable<string> ProviderNames
        {
            get => listBoxProviders.Items.Cast<string>();
            set
            {
                listBoxProviders.Items.Clear();
                foreach (var name in value)
                    listBoxProviders.Items.Add(name);
            }
        }

        public string SelectedProviderName
        {
            get => listBoxProviders.SelectedItem?.ToString();
        }

        public IEnumerable<string> EventLogs
        {
            get => listBoxEvents.Items.Cast<string>();
            set
            {
                listBoxEvents.Items.Clear();
                foreach (var log in value)
                    listBoxEvents.Items.Add(log);
            }
        }

        public event EventHandler prAdded;
        public event EventHandler prDeleted;
        public event EventHandler prChanged;
        public event EventHandler prSelected;
        public event EventHandler testException;
        public event EventHandler runTester;
        public event EventHandler calcVector;
        public event EventHandler serverTime;

        public Main_Form()
        {
            InitializeComponent();

            WireEvents();
        }

        private void WireEvents()
        {
            add_button.Click += (s, e) => prAdded?.Invoke(s, e);
            change_button.Click += (s, e) => prChanged?.Invoke(s, e);
            remove_button.Click += (s, e) => prDeleted?.Invoke(s, e);
            test_button.Click += (s, e) => runTester?.Invoke(s, e);
            SumButton.Click += (s, e) => calcVector?.Invoke(s, e);
            ServerTimeButton.Click += (s, e) => serverTime?.Invoke(s, e);
            btnTestException.Click += (s, e) => testException?.Invoke(s, e);
        }

        public void RefreshProviderList()
        {
            listBoxProviders.Refresh();
        }

        public void RefreshEventLog()
        {
            listBoxEvents.TopIndex = listBoxEvents.Items.Count - 1;
            listBoxEvents.Refresh();
        }

        public void ClearProviderSelection()
        {
            listBoxProviders.ClearSelected();
        }

        void ShowMessage(string title, string message, bool isError = false)
        {
            MessageBox(this.Handle, title,
                    message,
                    MB_OK | MB_ICONERROR);
        }
        void UpdateProviderInfo()
        {
            Refresh();
        }


        /// <summary>
        /// Обрабатывает нажатие кнопки изменения данных провайдера.
        /// Открывает форму редактирования <see cref="Change_Form"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Объект <see cref="EventArgs"/>, содержащий данные события.</param>
        private void change_button_Click(object sender, EventArgs e)
        {
            if (_provider == null)
            {
                MessageBox(this.Handle, $"Выберите провайдера из списка",
                    "Ошибка изменения",
                    MB_OK | MB_ICONERROR);
                return;
            }

            string oldName = _provider.Name;
            change_Form = new Change_Form(_provider);

            if (change_Form.ShowDialog() == DialogResult.OK)
            {
                DisplayProviderInfo();
                if (_provider.Name != oldName)
                {
                    int index = listBoxProviders.Items.IndexOf(oldName);
                    if (index != -1)
                    {
                        listBoxProviders.Items[index] = _provider.Name;
                        string log = $"[{DateTime.Now:HH:mm:ss}] Изменено: {oldName} -> {_provider.Name}";
                        listBoxEvents.Items.Add(log);
                    }
                }
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

        /// <summary>
        /// Обрабатывает нажатие кнопки тестирования исключения.
        /// Вычисляет плотность абонентов и обрабатывает возможное исключение <see cref="CustomDivideByZeroException"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Объект <see cref="EventArgs"/>, содержащий данные события.</param>
        private async void btnTestException_Click_1(object sender, EventArgs e)
        {
            try
            {
                float result = await getDensity();
                DensityTextBox.Text = $"{result:F2} абонентов/км²";

            }
            catch (CustomDivideByZeroException ex)
            {
                MessageBox(this.Handle, $"Невозможно рассчитать плотность абонентов, нет зоны покрытия!",
                    "Ошибка!",
                    MB_OK | MB_ICONERROR);
            }
            catch (NullReferenceException ex)
            {
                MessageBox(this.Handle, $"Выберите провайдера из списка!",
                    "Ошибка операции",
                    MB_OK | MB_ICONERROR);
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            using (Create_Form form = new Create_Form(_providerFactory))
            {
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    IProvider newProvider = form.CreatedProvider;
                    if (newProvider != null)
                    {
                        _collection.AddProvider(newProvider);
                        _provider = newProvider;
                        DisplayProviderInfo();
                    }
                }
            }
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxProviders.SelectedItem == null)
                {
                    MessageBox(this.Handle, $"Выберите провайдера из списка для удаления!",
                    "Ошибка удаления",
                    MB_OK | MB_ICONERROR);
                    return;
                }
                string selectedName = listBoxProviders.SelectedItem.ToString();

                var providerToRemove = _collection.GetAll().FirstOrDefault(p => p.Name == selectedName);

                _collection.RemoveProvider(providerToRemove);

                if (_provider == providerToRemove)
                {
                    _provider = null;
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox(this.Handle, $"Выберите провайдера из списка",
                    "Ошибка изменения",
                    MB_OK | MB_ICONERROR);
            }
            catch
            {
                MessageBox(this.Handle, $"Неизвестный тип ошибки!",
                    "Ошибка",
                    MB_OK | MB_ICONERROR);
            }
        }
        private void listBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBoxProviders.SelectedItem == null)
            {
                _provider = null;
                return;
            }

            string selectedName = listBoxProviders
                .SelectedItem.ToString();

            var foundProvider = _collection.GetAll().FirstOrDefault(p => p.Name == selectedName);

            if (foundProvider != null)
            {
                _provider = foundProvider;
                DisplayProviderInfo();
            }
        }

        private void test_button_Click(object sender, EventArgs e)
        {
            using (var form = new PerfomanceForm())
            {
                form.ShowDialog();
            }
        }

        private async void SumButton_Click(object sender, EventArgs e)
        {
            long sumTask = await getVectorSum();
            sumTextBox.Text = sumTask.ToString();
        }

        private async void ServerTimeButton_Click(object sender, EventArgs e)
        {
            string serverTask = await getServerTime();
            serverTimeTextBox.Text = serverTask;
        }

        private async Task<float> getDensity()
        {
            float result = await Task.Run(() =>
            {
                DensityVisitor visitor = new DensityVisitor();
                visitor.Visit(_provider);
                return visitor.Result;
            });
            return result;
        }

        private async Task<long> getVectorSum()
        {
            int size_vector = 10_000_000;
            int[] vector = new int[size_vector];
            Random rand = new Random();

            for (int i = 0; i < size_vector; i++)
            {
                vector[i] = rand.Next();
            }

            long sum = await Task.Run(() =>
            {
                long s = 0;
                for (int i = 0; i < size_vector; i++) { s += vector[i]; }
                return s;
            });
            return sum;
        }
        private async Task<string> getServerTime()
        {
            string serverTime = await Task.Run(() => DateTime.Now.ToString("HH:mm:ss"));
            return serverTime;
        }
    }
}
