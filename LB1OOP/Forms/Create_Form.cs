using LB1OOP.Interfaces;
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

        private readonly IProviderFactory _factory;
        public IProvider CreatedProvider { get; private set; }
        public Create_Form(IProviderFactory factory)
        {
            InitializeComponent();
            _factory = factory;
            DisplayProviderInfo();
        }
        private void DisplayProviderInfo()
        {

            NameTextBox.Text = "Не указан";
            userCountTextBox.Text = "0";
            speedLimitTextBox.Text = "Не указан";
            areaTextBox.Text = "100";
            contractNumberTextBox.Text = "0";
            tarifNameTextBox.Text = "Не указан";
            tarifCoastTextBox.Text = "100";

        }

        private void create_button_Click(object sender, EventArgs e)
        {
            try
            {
                string name = NameTextBox.Text.Trim();
                int userCount = int.Parse(userCountTextBox.Text.Trim());
                float speedLimit = speedLimitTextBox.Text.Trim() != "Не указан" ? float.Parse(speedLimitTextBox.Text.Trim()) : 0;
                float area = float.Parse(areaTextBox.Text.Trim());
                int contract = int.Parse(contractNumberTextBox.Text.Trim());
                string tarif = tarifNameTextBox.Text.Trim();
                float tarifCoast = float.Parse(tarifCoastTextBox.Text.Trim());

                CreatedProvider = _factory.CreateProvider(name, tarifCoast, userCount, speedLimit, area, contract, tarif);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox(this.Handle, ex.Message,
                                   "Ошибка",
                                   MB_OK | MB_ICONERROR);
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
