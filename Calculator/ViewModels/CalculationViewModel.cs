using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModels
{
    public class CalculationViewModel
    {
        private ObservableCollection<string> aa = new ObservableCollection<string>();

        public DelegateCommand LoadedCommand { get; private set; }

        public CalculationViewModel()
        {
            Initaa();
            //LoadedCommand = new DelegateCommand(Initaa);
        }

        public void Initaa()
        {
            aa.Add("1");
            aa.Add("1");
            aa.Add("1");
            aa.Add("1");
            aa.Add("1");
            aa.Add("1");
        }
    }
}
