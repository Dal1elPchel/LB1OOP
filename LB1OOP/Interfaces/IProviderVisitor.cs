using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.Interfaces
{
    public interface IProviderVisitor
    {
        string Result { get; }
        void Visit(IProvider provider);
    }
}
