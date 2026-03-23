using LB1OOP.interfaces;
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
    public partial class Main_Form: Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
        private const uint MB_OK = 0x00000000;
        private const uint MB_ICONERROR = 0x00000010;
        private const uint MB_ICONINFORMATION = 0x00000040;

        private Change_Form change_Form;

        private IProvider _provider;
        private ProviderCollection _collection;

        public Main_Form()
        {
            InitializeComponent();
            _collection = new ProviderCollection();
            _collection.ProviderAdded += (p, action) =>
            {
                listBoxProviders.Items.Add(p.Name);

                string log = $"[{DateTime.Now:HH:mm:ss}] {action}: {p.Name} (Тариф: {p.TarifName})";
                listBoxEvents.Items.Add(log);
            };
            _collection.ProviderRemoved += (p, action) =>
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
            _provider = new Provider("МТС");
            _collection.AddProvider(_provider);

            DisplayProviderInfo();
        }

        private void DisplayProviderInfo()
        {

            NameTextBox.Text = _provider.Name;
            userCountTextBox.Text = _provider.UserCount.ToString();
            speedLimitTextBox.Text = _provider.SpeedLimit != 0 ? _provider.SpeedLimit.ToString() : "Не задан";
            areaTextBox.Text = _provider.Area.ToString();
            contractNumberTextBox.Text = _provider.ContractNumber.ToString();
            tarifNameTextBox.Text = _provider.TarifName.ToString();
            tarifCoastTextBox.Text = _provider.TarifCoast.ToString();
            UserCountHexTextBox.Text = _provider.GetUserCountInHex();

        }

        private void change_button_Click(object sender, EventArgs e)
        {
            if (_provider == null)
            {
                MessageBox(this.Handle, "Выберите провайдера для изменения!", "Провайдер не выбран", MB_OK | MB_ICONERROR);
                return;
            }

            change_Form = new Change_Form(_provider);

            if (change_Form.ShowDialog() == DialogResult.OK)
            {
                IProvider newProvider = change_Form.UpdatedProvider;

                _collection.RemoveProvider(_provider);

                _collection.AddProvider(newProvider);

                _provider = newProvider;
                DisplayProviderInfo();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Application.Exit();
            }
        }

        private void btnTestException_Click_1(object sender, EventArgs e)
        {
            try
            {
                

                float result = _provider.CalculateUserDensity();

                MessageBox(this.Handle,
                                   $"Результат: {result:F2} абонентов/км²",
                                   "Результат",
                                   MB_OK | MB_ICONINFORMATION);
            }
            catch (CustomDivideByZeroException ex)
            {
                MessageBox(this.Handle, "Ошибка деления на ноль!", "Ошибка деления на ноль!", MB_OK | MB_ICONERROR);

            }
            catch (NullReferenceException ex)
            {
                MessageBox(this.Handle, "Выберите провайдера!", "Выберите провайдера!", MB_OK | MB_ICONERROR);

            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            using (Create_Form form = new Create_Form())
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
                    MessageBox(this.Handle, "Выберите провайдера для удаления!", "Ошибка удаления провайдера", MB_OK | MB_ICONERROR);
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
                MessageBox(this.Handle, "Выберите провайдера для удаления!", "Ошибка удаления провайдера", MB_OK | MB_ICONERROR);
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

        private void speedLimitLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
