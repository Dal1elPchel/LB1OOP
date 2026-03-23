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
    public partial class Create_Form : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
        private const uint MB_OK = 0x00000000;
        private const uint MB_ICONERROR = 0x00000010;

        public IProvider CreatedProvider { get; private set; }
        public Create_Form()
        {
            InitializeComponent();
            DisplayProviderInfo();
        }
        private void DisplayProviderInfo()
        {

            NameTextBox.Text = "Не задан";
            userCountTextBox.Text = "0";
            speedLimitTextBox.Text = "Не задан";
            areaTextBox.Text = "100";
            contractNumberTextBox.Text = "0";
            tarifNameTextBox.Text = "Не задан";
            tarifCoastTextBox.Text = "100";

        }

        private void create_button_Click(object sender, EventArgs e)
        {
            try
            {
                string name = NameTextBox.Text.Trim();
                int userCount = int.Parse(userCountTextBox.Text.Trim());
                float speedLimit = speedLimitTextBox.Text.Trim() == "Не задан" || speedLimitTextBox.Text.Trim() == "0"
                    ? 0 : float.Parse(speedLimitTextBox.Text.Trim());
                float area = float.Parse(areaTextBox.Text.Trim());
                int contract = int.Parse(contractNumberTextBox.Text.Trim());
                string tarif = tarifNameTextBox.Text.Trim();
                float tarifCoast = float.Parse(tarifCoastTextBox.Text.Trim());

                CreatedProvider = new Provider(name, tarifCoast, userCount, speedLimit, area, contract, tarif);

                if (checkBoxStaticIP.Checked) CreatedProvider = new StaticIpDecorators(CreatedProvider);
                if (checkBoxRouterRent.Checked) CreatedProvider = new RouterRentDecorator(CreatedProvider);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox(this.Handle, ex.Message, $"Ошибка при создании:\\n{ex.Message}", MB_OK | MB_ICONERROR);
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
