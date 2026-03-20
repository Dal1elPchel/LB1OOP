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
    public partial class Change_Form: Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
        private const uint MB_OK = 0x00000000;
        private const uint MB_ICONERROR = 0x00000010;

        private Provider editProvider;

        public Change_Form(Provider provider)
        {
            InitializeComponent();
            editProvider = provider;
            DisplayProviderInfo();
        }

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

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            try
            {
                editProvider.Name = NameTextBox.Text.Trim();
                editProvider.UserCount = int.Parse(userCountTextBox.Text.Trim());
                editProvider.SpeedLimit = speedLimitTextBox.Text.Trim() == "Не задан" ||
                    speedLimitTextBox.Text.Trim() == "0" ? 0 : float.Parse(speedLimitTextBox.Text.Trim());
                editProvider.Area = float.Parse(areaTextBox.Text.Trim());
                editProvider.ContractNumber = int.Parse(contractNumberTextBox.Text.Trim());
                editProvider.TarifName = tarifNameTextBox.Text.Trim();
                editProvider.TarifCoast = float.Parse(tarifCoastTextBox.Text.Trim());
                this.DialogResult = DialogResult.OK;
                this.Close();
            } catch (Exception ex)
            {
                MessageBox(this.Handle, ex.Message, $"Ошибка при изменении:\\n{ex.Message}", MB_OK | MB_ICONERROR);

            }

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                
            }
        }
    }
}
