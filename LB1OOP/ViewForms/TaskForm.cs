using LB1OOP.Factories;
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
    /// <summary>
    /// Стартовая форма приветствия, обеспечивающая переход к главной форме приложения.
    /// </summary>
    public partial class TaskForm : Form
    {
        public event EventHandler OkClicked;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TaskForm"/>.
        /// Создает экземпляр главной формы <see cref="MainForm"/> для последующего перехода.
        /// </summary>
        public TaskForm()
        {
            InitializeComponent();
            button1.Click += (sender, e) => OkClicked?.Invoke(this, EventArgs.Empty);
        }

    }
}