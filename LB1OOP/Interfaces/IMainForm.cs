using System;
using System.Collections.Generic;

namespace LB1OOP.Viewer.Interfaces
{
    public interface IMainForm
    {
        int UserCount { get; set; }
        float SpeedLimit { get; set; }
        float Area { get; set; }
        int ContractNumber { get; set; }
        string ProviderName { get; set; }
        string TarifName { get; set; }
        float TarifCoast { get; set; }
        string UserCountHex { get; set; }

        IEnumerable<string> ProviderNames { set; }
        IEnumerable<string> EventLogs { set; }
        string SelectedProviderName { get; set; }

        event EventHandler CreateProviderRequested;
        event EventHandler EditProviderRequested;
        event EventHandler RemoveProviderRequested;
        event EventHandler DensityRequested;
        event EventHandler TestingRequested;
        event EventHandler<ProviderSelectedEventArgs> ProviderSelected;

        void RefreshDisplay();
        void ShowError(string message);
        void ShowInfo(string message);
        void CloseForm();
    }

    public class ProviderSelectedEventArgs : EventArgs
    {
        public string ProviderName { get; }

        public ProviderSelectedEventArgs(string providerName)
        {
            ProviderName = providerName;
        }
    }
}