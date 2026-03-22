using LB1OOP.Factories;
using LB1OOP.Interfaces;
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
    public partial class Main_Form : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
        private const uint MB_OK = 0x00000000;
        private const uint MB_ICONERROR = 0x00000010;
        private const uint MB_ICONINFORMATION = 0x00000040;

        private Create_Form create_Form;
        private Change_Form change_Form;

        public IProviderFactory _providerFactory;
        private IProvider _provider;
        private IProviderCollection _collection;

        public Main_Form()
        {
            InitializeComponent();

            _providerFactory = new DefaultProviderFactory();
            _collection = _providerFactory.CreateProviderCollection();

            _collection.providerAdded += (p, message) =>
            {
                listBoxProviders.Items.Add(p.Name);

                string log = $"[{DateTime.Now:HH:mm:ss}] {message}: {p.Name} (Тариф: {p.TarifName})";
                listBoxEvents.Items.Add(log);
            };

            _collection.providerRemoved += (p, action) =>
            {
                if (listBoxProviders.Items.Contains(p.Name))
                {
                    listBoxProviders.Items.Remove(p.Name);
                }

                string log = $"[{DateTime.Now:HH:mm:ss}] {action}: {p.Name}";
                listBoxEvents.Items.Add(log);
                if (_provider == p)
                {
                    _provider = null;
                }
            };

            _provider = _providerFactory.CreateProviderWithName("МТС");
            _collection.AddProvider(_provider);

            DisplayProviderInfo();
        }

        /// <summary>
        /// Отображает информацию о провайдере в текстовых полях формы.
        /// </summary>
        private void DisplayProviderInfo()
        {
            NameTextBox.Text = _provider.Name.Trim();
            userCountTextBox.Text = _provider.UserCount.ToString().Trim();
            speedLimitTextBox.Text = _provider.SpeedLimit == 0 ? "Не задан" : _provider.SpeedLimit.ToString().Trim();
            areaTextBox.Text = _provider.Area.ToString().Trim();
            contractNumberTextBox.Text = _provider.ContractNumber.ToString().Trim();
            tarifNameTextBox.Text = _provider.TarifName.ToString().Trim();
            tarifCoastTextBox.Text = _provider.TarifCoast.ToString().Trim();
            HexVisitor visitor = new HexVisitor();
            visitor.Visit(_provider);
            UserCountHextextBox.Text = visitor.Result.ToString().Trim();
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
        private void btnTestException_Click_1(object sender, EventArgs e)
        {
            try
            {
                DensityVisitor visitor = new DensityVisitor();
                visitor.Visit(_provider);
                float result = visitor.Result;
                MessageBox(this.Handle, $"Результат: {result:F2} абонентов/км²",
                    "Результат",
                    MB_OK | MB_ICONINFORMATION);

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
                if (listBoxProviders.SelectedItems == null)
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
    }
}
