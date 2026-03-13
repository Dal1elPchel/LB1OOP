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
        

        private Change_Form _form3;
        private Provider provider;
        public Main_Form()
        {
            InitializeComponent();
            provider = new Provider("МТС");
            DisplayProviderInfo();
        }

        

        private void DisplayProviderInfo()
        {

            NameTextBox.Text = provider.ToString();
            userCountTextBox.Text = provider.UserCount.ToString();
            speedLimitTextBox.Text = provider.SpeedLimit.ToString();
            areaTextBox.Text = provider.Area.ToString();
            contractNumberTextBox.Text = provider.ContractNumber.ToString();
            tarifNameTextBox.Text = provider.TarifName.ToString();
            tarifCoastTextBox.Text = provider.TarifCoast.ToString();
            UserCountTextBocx.Text = provider.GetUserCountInHex();

        }

        private void change_button_Click(object sender, EventArgs e)
        {
            _form3 = new Change_Form(provider);
            _form3.ShowDialog();
            DisplayProviderInfo();
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
                

                float result = provider.CalculateUserDensity();

                MessageBox(this.Handle,
                   $"Результат: {result:F2} абонентов/км²",
                   "Результат",
                   MB_OK | MB_ICONINFORMATION);
            }
            catch (CustomDivideByZeroException ex)
            {

                MessageBox(this.Handle,
                     ex.Message,
                     "Ошибка деления на ноль",
                     MB_OK | MB_ICONERROR);
            }
        }
    }
}
