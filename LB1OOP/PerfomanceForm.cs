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
        public PerfomanceForm()
        {
            InitializeComponent();
        }

        private void run_button_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            var results = PerfomanceTester.RunTests();

            AddRow("Вставка 100k элементов", results.ListAddTime, results.ArrayAddTime);
            AddRow("Последовательное чтение", results.ListSeqReadTime, results.ArraySeqReadTime);
            AddRow("Случайное чтение", results.ListRandomReadTime, results.ArrayRandomReadTime);
        }

        private void AddRow(string op, long listTime, long arrayTime)
        {
            ListViewItem item = new ListViewItem(op);
            item.SubItems.Add(listTime.ToString());
            item.SubItems.Add(arrayTime.ToString());
            
            listView1.Items.Add(item);
        }
    }
}
