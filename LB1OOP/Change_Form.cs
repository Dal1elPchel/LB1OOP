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
        private Main_Form _form2;
        private Provider editprovider;

        public Change_Form(Provider provider)
        {
            InitializeComponent();
            editprovider = provider;
        }

        private void Change_Form_Load(object sender, EventArgs e)
        {

        }

        private void back_button_Click(object sender, EventArgs e)
        {
            _form2.Show();
            this.Hide();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            _form2.Show();
            this.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();

                Application.Exit();
            }
        }
    }
}
