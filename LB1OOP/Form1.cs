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
    public partial class Hello_Form: Form
    {
        private Main_Form _form2;

        public Hello_Form()
        {
            InitializeComponent();
            _form2 = new Main_Form();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _form2.Show();
            this.Hide();
        }
    }
}
