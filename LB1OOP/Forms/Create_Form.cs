using LB1OOP.Interfaces;
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
    public partial class Create_Form : Form
    {
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
                string name = NameTextBox.Text;
                int userCount = int.Parse(userCountTextBox.Text);
                float speedLimit = speedLimitTextBox.Text != "Не указан" ? float.Parse(speedLimitTextBox.Text) : 0;
                float area = float.Parse(areaTextBox.Text);
                int contract = int.Parse(contractNumberTextBox.Text);
                string tarif = tarifNameTextBox.Text;
                float tarifCoast = float.Parse(tarifCoastTextBox.Text);

                CreatedProvider = _factory.CreateProvider(name, tarifCoast, userCount, speedLimit, area, contract, tarif);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
