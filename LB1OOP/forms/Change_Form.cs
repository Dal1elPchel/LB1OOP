using LB1OOP.interfaces;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LB1OOP
{
    public partial class Change_Form : Form
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
        private const uint MB_OK = 0x00000000;
        private const uint MB_ICONERROR = 0x00000010;

        private IProvider _baseProvider;

        public IProvider UpdatedProvider { get; private set; }

        public Change_Form(IProvider provider)
        {
            InitializeComponent();
            UnpackProvider(provider);
            DisplayProviderInfo();
        }

        private void UnpackProvider(IProvider provider)
        {
            _baseProvider = provider;
            checkBoxStaticIP.Checked = false;
            checkBoxRouterRent.Checked = false;

            while (_baseProvider is ProviderDecorator decorator)
            {
                if (decorator is StaticIpDecorators) checkBoxStaticIP.Checked = true;

                if (decorator is RouterRentDecorator) checkBoxRouterRent.Checked = true;

                _baseProvider = decorator.InnerProvider;
            }
        }

        private void DisplayProviderInfo()
        {
            NameTextBox.Text = _baseProvider.Name;
            userCountTextBox.Text = _baseProvider.UserCount.ToString();
            speedLimitTextBox.Text = _baseProvider.SpeedLimit == 0 ? "Не задан" : _baseProvider.SpeedLimit.ToString();
            areaTextBox.Text = _baseProvider.Area.ToString();
            contractNumberTextBox.Text = _baseProvider.ContractNumber.ToString();
            tarifNameTextBox.Text = _baseProvider.TarifName;
            tarifCoastTextBox.Text = _baseProvider.TarifCoast.ToString();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            try
            {
                _baseProvider.Name = NameTextBox.Text.Trim();
                _baseProvider.UserCount = int.Parse(userCountTextBox.Text.Trim());
                _baseProvider.SpeedLimit = speedLimitTextBox.Text.Trim() == "Не задан" ||
                    speedLimitTextBox.Text.Trim() == "0" ? 0 : float.Parse(speedLimitTextBox.Text.Trim());
                _baseProvider.Area = float.Parse(areaTextBox.Text.Trim());
                _baseProvider.ContractNumber = int.Parse(contractNumberTextBox.Text.Trim());
                _baseProvider.TarifName = tarifNameTextBox.Text.Trim();
                _baseProvider.TarifCoast = float.Parse(tarifCoastTextBox.Text.Trim());

                UpdatedProvider = _baseProvider;

                if (checkBoxStaticIP.Checked)
                    UpdatedProvider = new StaticIpDecorators(UpdatedProvider);

                if (checkBoxRouterRent.Checked)
                    UpdatedProvider = new RouterRentDecorator(UpdatedProvider);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox(this.Handle, $"Ошибка при изменении:\n{ex.Message}", "Ошибка", MB_OK | MB_ICONERROR);
            }
        }

        private void back_button_Click(object sender, EventArgs e) => this.Close();
    }
}