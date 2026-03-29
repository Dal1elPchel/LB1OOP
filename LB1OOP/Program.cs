using LB1OOP.Cntrollers;
using LB1OOP.Factories;
using LB1OOP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB1OOP
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IProviderFactory _defaultFactory = new DefaultProviderFactory();
            AppModel model = new AppModel(_defaultFactory);
            Dispatcher dp = new Dispatcher(model);

            dp.StartApplication();
        }
    }
}
