using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB1OOP.Cntrollers
{
    public class Dispatcher
    {
        private AppModel _model;

        public Dispatcher(AppModel model)
        {
            _model = model;
        }

        public void StartApplication()
        {
            using (var taskForm = new TaskForm())
            {
                if (taskForm.ShowDialog() == DialogResult.OK)
                {
                    var mainView = new MainForm();
                    var mainController = new MainController(this, _model, mainView);

                    Application.Run(mainView);
                }
                else
                {
                    Application.Exit();
                }
            }

        }

        public void OpenCreateForm()
        {
            var createForm = new CreateForm();
            CreateController createCtrl = new CreateController(_model, createForm);

            createForm.ShowDialog();
        }

        public bool OpenChangeForm()
        {
            var changeForm = new ChangeForm();
            ChangeController changeCtrl = new ChangeController(changeForm, _model);

            return changeForm.ShowDialog() == DialogResult.OK;
        }

        public void OpenPerfomanceForm()
        {
            PerfomanceForm testForm = new PerfomanceForm();
            TesterController testerCtrl = new TesterController(testForm);

            testForm.ShowDialog();
        }
    }
}
