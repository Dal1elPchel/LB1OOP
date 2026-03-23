using System;

namespace LB1OOP.interfaces
{
    /// <summary>
    /// Определяет контракт для работы с интернет-провайдером.
    /// </summary>
    public interface IProvider
    {
        string Name { get; set; }
        string TarifName { get; set; }
        float TarifCoast { get; set; }

        int UserCount { get; set; }
        float SpeedLimit { get; set; }
        float Area { get; set; }
        int ContractNumber { get; set; }

        string GetUserCountInHex();
        float CalculateUserDensity();

        string ToString();
    }
}