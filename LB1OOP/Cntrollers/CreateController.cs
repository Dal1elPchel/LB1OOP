using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.Cntrollers
{
    public class CreateController
    {
        private AppModel _model;
        private CreateForm _view;

        public CreateController(AppModel model, CreateForm view)
        {
            _model = model;
            _view = view;

            _view.SaveClicked += CreateProviderInModel;
        }

        public void CreateProviderInModel(string name, float tarifCoast, 
            int userCount, float speedLimit, float area, 
            int contract, string tarifName)
        {
            bool isSuccess = _model.CreateNewProvider(name, tarifCoast, userCount, 
                speedLimit, area, contract, tarifName);

            if (isSuccess)
            {
                _view.Close();
            }
            else
            {
                _view.ShowError("Не удалось создать провайдера!");
            }
        }
    }
}
