using LB1OOP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.Cntrollers
{
    public class MainController
    {
        private Dispatcher _dp;
        private AppModel _model;
        private MainForm _view;

        public MainController(Dispatcher dp, AppModel model, MainForm view)
        {
            _dp = dp;
            _model = model;
            _view = view;

            _view.addClicked += () => _dp.OpenCreateForm();
            _view.removeClicked += ClickToRemove;
            _view.providerSelected += OnProviderSelected;
            _view.changeClicked += ClickToChange;
            _view.calcDensityClicked += ClickToDensity;
            _view.perfomanceTesterClicked += () => _dp.OpenPerfomanceForm();

            _model.Collection.providerAdded += OnProviderAddModel;
            _model.Collection.providerRemoved += OnProviderRemoveModel;

            var initialNames = _model.Collection.GetAll().Select(p => p.Name);
            _view.RefreshProviderList(initialNames);

            if (_model.CurrentProvider != null)
            {
                UpdateViewInfoWithProvider(_model.CurrentProvider);
            }
        }

        private void OnProviderSelected(int index)
        {
            IProvider selected = _model.GetProviderByIndex(index);

            if (selected != null)
            {
                _model.CurrentProvider = selected;

                UpdateViewInfoWithProvider(selected);
            }
        }

        private void ClickToRemove()
        {
            IProvider deletedProvider = _model.CurrentProvider;

            if (deletedProvider != null)
            {
                bool isSuccess = _model.RemoveProvider(deletedProvider);

                if (isSuccess)
                {
                    _view.ShowInfo($"Провайдер {deletedProvider.Name} успешно удален!");
                }
                else _view.ShowError($"Провайдера {deletedProvider.Name} не получилось удалить!");
            }
            else
            {
                _view.ShowError("Для удаления нужно выбрать провайдера!");
            }
        }

        private void OnProviderAddModel(IProvider provider, string message)
        {
            _view.AddLog($"{message}: {provider.Name}");

            UpdateViewInfoWithProvider(provider);

            var allNames = _model.Collection.GetAll().Select(p => p.Name);
            _view.RefreshProviderList(allNames);

        }
        
        private void OnProviderRemoveModel(IProvider provider, string message)
        {
            _view.AddLog($"{message}: {provider.Name}");
            _view.ClearProviderInfo();

            var allNames = _model.Collection.GetAll().Select(p => p.Name);
            _view.RefreshProviderList(allNames);
        }

        private void ClickToChange()
        {
            if (_model.CurrentProvider != null)
            {
                bool isSaved = _dp.OpenChangeForm();

                if (isSaved)
                {
                    _view.AddLog($"Изменены данные провайдера: {_model.CurrentProvider.Name}");

                    var allNames = _model.Collection.GetAll().Select(p => p.Name);
                    _view.RefreshProviderList(allNames);

                    UpdateViewInfoWithProvider(_model.CurrentProvider);
                }
            }
            else
            {
                _view.ShowError("Сначала выберите провайдера из списка для изменения!");
            }
        }

        private void UpdateViewInfoWithProvider(IProvider provider)
        {
            _view.UpdateProviderInfo(
                    provider.Name,
                    provider.UserCount.ToString(),
                    provider.SpeedLimit.ToString(),
                    provider.Area.ToString(),
                    provider.ContractNumber.ToString(),
                    provider.TarifName,
                    provider.TarifCoast.ToString(),
                    _model.GetUserCountInHEX(provider)
            );
        }

        private void ClickToDensity()
        {
            if (_model.CurrentProvider != null)
            {
                string density = _model.GetUserDensity(_model.CurrentProvider);

                _view.ShowInfo($"Плотность абонентов равна {density} чел. / кв. км.");
            }
            else
            {
                _view.ShowError("Сначала выберите провайдера из списка для рассчета плотности абонентов!");
            }
        }
    }
}
