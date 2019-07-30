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
        #region Variables
        private string _expression;
        public string Expression
        {
            get
            {
                if (string.IsNullOrEmpty(_expression))
                {
                    return "0";
                }
                return _expression;
            }
            set
            {
                SetProperty(ref _expression, value);
            }
        }

        private string _result;
        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                SetProperty(ref _result, value);
            }
        }

        #endregion

        #region Commands
        public DelegateCommand<string> ClickBtnCommand { get; private set; }

        #endregion
        public CalculationViewModel()
        {
            ClickBtnCommand = new DelegateCommand<string>(Initaa);
        }

        public void Initaa(string content)
        {
            Console.WriteLine(content);
        }
    }
}
