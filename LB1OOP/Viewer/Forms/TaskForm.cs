using LB1OOP.Controller;
using LB1OOP.Controllers;
using LB1OOP.Factories;
using LB1OOP.Interfaces;
using System;
using System.Windows.Forms;

namespace LB1OOP
{
    /// <summary>
    /// Стартовая форма приветствия, обеспечивающая переход к главной форме приложения.
    /// </summary>
    public partial class Hello_Form : Form
    {
        private Main_Form _mainForm;
        private MainController _controller;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Hello_Form"/>.
        /// </summary>
        public Hello_Form()
        {
            InitializeComponent();

            _controller = ControllerFactory.CreateMainController();

            _mainForm = new Main_Form(_controller);
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки перехода к главной форме.
        /// Отображает главную форму <see cref="Main_Form"/> и скрывает текущую форму приветствия.
        /// </summary>
        /// <param name="sender">Источник события - кнопка перехода.</param>
        /// <param name="e">Объект <see cref="EventArgs"/>, содержащий данные события.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            _mainForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Обрабатывает закрытие формы приветствия.
        /// Если закрывается форма, завершаем приложение.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_mainForm == null || _mainForm.IsDisposed)
            {
                Application.Exit();
            }
        }
    }
}