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
    public partial class Change_Form: Form
    {
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
            speedLimitTextBox.Text = editProvider.SpeedLimit.ToString();
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
                editProvider.Name = NameTextBox.Text;
                editProvider.UserCount = int.Parse(userCountTextBox.Text);
                editProvider.SpeedLimit = float.Parse(speedLimitTextBox.Text);
                editProvider.Area = float.Parse(areaTextBox.Text);
                editProvider.ContractNumber = int.Parse(contractNumberTextBox.Text);
                editProvider.TarifName = tarifNameTextBox.Text;
                editProvider.TarifCoast = float.Parse(tarifCoastTextBox.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            } catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
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
