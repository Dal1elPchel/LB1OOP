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
    public partial class Hello_Form : Form
    {
        private Main_Form _form2;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Hello_Form"/>.
        /// Создает экземпляр главной формы <see cref="Main_Form"/> для последующего перехода.
        /// </summary>
        public Hello_Form()
        {
            InitializeComponent();
            _form2 = new Main_Form();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки перехода к главной форме.
        /// Отображает главную форму <see cref="Main_Form"/> и скрывает текущую форму приветствия.
        /// </summary>
        /// <param name="sender">Источник события - кнопка перехода.</param>
        /// <param name="e">Объект <see cref="EventArgs"/>, содержащий данные события.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            _form2.Show();
            this.Hide();
        }
    }
}