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
        public Provider CreatedProvider { get; private set; }
        public Create_Form()
        {
            InitializeComponent();
            DisplayProviderInfo();
        }
        private void DisplayProviderInfo()
        {

            NameTextBox.Text = "";
            userCountTextBox.Text = "0";
            speedLimitTextBox.Text = "0";
            areaTextBox.Text = "0";
            contractNumberTextBox.Text = "0";
            tarifNameTextBox.Text = "Undefined";
            tarifCoastTextBox.Text = "0";

        }

        private void create_button_Click(object sender, EventArgs e)
        {
            try
            {
                string name = NameTextBox.Text;
                int userCount = int.Parse(userCountTextBox.Text);
                float speedLimit = float.Parse(speedLimitTextBox.Text);
                float area = float.Parse(areaTextBox.Text);
                int contract = int.Parse(contractNumberTextBox.Text);
                string tarif = tarifNameTextBox.Text;
                float tarifCoast = float.Parse(tarifCoastTextBox.Text);

                CreatedProvider = new Provider(name, tarifCoast, userCount, speedLimit, area, contract, tarif);
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
