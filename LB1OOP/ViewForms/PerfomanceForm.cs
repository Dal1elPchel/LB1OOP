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
    public partial class PerfomanceForm : Form
    {
        public event Action runClicked;
        public PerfomanceForm()
        {
            InitializeComponent();
        }

        private void run_button_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            runClicked?.Invoke();
        }

        public void AddRow(string op, double listTime, double arrayTime)
        {
            ListViewItem item = new ListViewItem(op);
            item.SubItems.Add(listTime.ToString());
            item.SubItems.Add(arrayTime.ToString());
            
            listView1.Items.Add(item);
        }
    }
}
