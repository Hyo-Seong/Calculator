using Calculator.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Util;

namespace Calculator.ViewModels
{
    public class CalculationViewModel : BindableBase
    {
        #region Variables
        private string _tempOperator;

        private List<string> _operatorList = new List<string>();

        private string _expression = "0";
        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                SetProperty(ref _expression, value);
            }
        }

        private string _result = "0";
        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                if (value.Length != 1 && value[0] == '0')
                {
                    value = value.Substring(1);
                }
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
            if (content.IsInt())
            {
                if (!String.IsNullOrEmpty(_tempOperator))
                {
                    _operatorList.Add(_tempOperator);
                    _tempOperator = string.Empty;
                }
                Result += content;
            } 
            else if(content.IsOperator()) 
            {
                _tempOperator = content;
            }
        }
    }
}
