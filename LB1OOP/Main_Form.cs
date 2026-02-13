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
    public partial class Main_Form: Form
    {
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

                MessageBox.Show($"Результат: {result}");
            }
            catch (CustomDivideByZeroException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
