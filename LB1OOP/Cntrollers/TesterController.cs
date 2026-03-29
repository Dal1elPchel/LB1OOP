using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1OOP.Cntrollers
{
    public class TesterController
    {
        PerfomanceForm _view;

        public TesterController(PerfomanceForm view)
        {
            _view = view;

            _view.runClicked += runTests;
        }

        private void runTests()
        {
            var results = PerfomanceTester.RunTests();

            _view.AddRow("Вставка 100k элементов", results.ListAddTime, results.ArrayAddTime);
            _view.AddRow("Последовательное чтение", results.ListSeqReadTime, results.ArraySeqReadTime);
            _view.AddRow("Случайное чтение", results.ListRandomReadTime, results.ArrayRandomReadTime);
        }
    }
}
