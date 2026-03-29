using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.View.Interfaces
{
    interface IMainForm
    {
        string ProviderName { get; set; }
        int UserCount { get; set; }
        float SpeedLimit { get; set; }
        float Area { get; set; }
        int ContractNumber { get; set; }
        string TarifName { get; set; }
        float TarifCoast { get; set; }
        string UserCountDex { get; set; }
        float Dencity { get; set; }
        long VectorSum { get; set; }
        long ServerTime { get; set; }

        IEnumerable<string> ProviderNames { get; set; }
        IEnumerable<string> EventLogs { get; set; }
        string SelectedProviderName { get; }

        event EventHandler prAdded;
        event EventHandler prDeleted;
        event EventHandler prChanged;
        event EventHandler prSelected;
        event EventHandler testException;
        event EventHandler runTester;
        event EventHandler calculateDencity;
        event EventHandler calcVector;
        event EventHandler serverTime;

        void RefreshProviderList();
        void RefreshEventLog();
        void ClearProviderSelection();
        void ShowMessage(string message, bool isError = false);
        void UpdateProviderInfo();
    }
}
