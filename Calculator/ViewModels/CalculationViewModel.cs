using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModels
{
    public class CalculationViewModel : BindableBase
    {
        public DelegateCommand LoadedCommand { get; private set; }

        public CalculationViewModel()
        {
            Initaa();
            //LoadedCommand = new DelegateCommand(Initaa);
        }

        public void Initaa()
        {

        }
    }
}
