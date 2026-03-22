using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace LB1OOP.Interfaces
{
    public interface IProvider
    {
        int UserCount { get; set; }
        float SpeedLimit { get; set; }
        float Area { get; set; }
        int ContractNumber { get; set; }
        string Name { get; set; }
        string TarifName { get; set; }
        float TarifCoast { get; set; }

        void Accept(IProviderVisitor visitor);
    }
}
