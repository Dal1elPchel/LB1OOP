using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.Cntrollers
{
    public class ChangeController
    {
        private ChangeForm _view;
        private AppModel _model;

        public ChangeController(ChangeForm view, AppModel model)
        {
            _view = view;
            _model = model;

            var current = _model.CurrentProvider;

            if (current != null)
            {
                _view.UpdateProviderInfo(
                    current.Name,
                    current.UserCount.ToString(),
                    current.SpeedLimit.ToString(),
                    current.Area.ToString(),
                    current.ContractNumber.ToString(),
                    current.TarifName,
                    current.TarifCoast.ToString()
                );
            }

            _view.ClickToSave += HandleSave;
        }

        private void HandleSave(string name, float tarifCoast,
            int userCount, float speedLimit, float area,
            int contract, string tarifName)
        {
            _model.UpdateProvider(_model.CurrentProvider, name, tarifCoast, userCount, speedLimit, area, contract, tarifName);

            _view.DialogResult = System.Windows.Forms.DialogResult.OK;
            _view.Close();
        }
    }
}
