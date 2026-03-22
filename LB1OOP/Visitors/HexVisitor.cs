using LB1OOP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.Visitors
{
    public class HexVisitor : IProviderVisitor
    {
        private string _result;
        public string Result { get { return _result; } }
        public void Visit(IProvider provider) => _result = provider.UserCount.ToString("X");
    }
}
