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
        private const string ZERO = "0";

        private string _tempOperator;

        private ObservableCollection<Calculation> _calItems = new ObservableCollection<Calculation>();
        public ObservableCollection<Calculation> CalItems
        {
            get
            {
                return _calItems;
            }
            set
            {
                SetProperty(ref _calItems, value);
            }
        }

        private List<string> _operatorList = new List<string>();
        private List<decimal> _numbers = new List<decimal>();

        private string _expression = string.Empty;
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

        private string _result = ZERO;
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
                if (_numbers.Count != 0 && !String.IsNullOrEmpty(_tempOperator))
                {
                    _operatorList.Add(_tempOperator);
                    _tempOperator = string.Empty;
                    Result = ZERO;
                }
                Result += content;
            } 
            else if(content.IsOperator()) 
            {
                if (string.IsNullOrEmpty(_tempOperator))
                {
                    _numbers.Add(Decimal.Parse(Result));
                }
                _tempOperator = content;
            }
            
            switch(content){
                case "＝":
                    _numbers.Add(Decimal.Parse(Result));
                    Result = Calculate().ToString();
                    //CalItems.Add();
                    break;
                case "←":
                    if(Result.Length <=1){
                        Result = ZERO;
                        break;
                    }
                    Result = Result.Remove(Result.Length-1);
                    break;
                case "CE":
                    Result = ZERO;
                    break;
                case "C":
                    Result = ZERO;
                    _numbers = new List<decimal>();
                    _operatorList = new List<string>();
                    break;
                case ".":
                    Result += content;
                    break;
            }

        }

        private decimal Calculate()
        {
            if (_numbers.Count == 1)
            {
                return _numbers[0];
            }
            else
            {
                int count = 0;
                while (_operatorList.Count > count)
                {
                    if (_operatorList[count].Equals("×") || _operatorList[count].Equals("÷"))
                    {
                        _numbers[count] = Calculate(_numbers[count], _numbers[count + 1], _operatorList[count]);
                        _numbers.RemoveAt(count + 1);
                        _operatorList.RemoveAt(count);
                    }
                    count++;
                }
                count = 0;
                while (_operatorList.Count > count)
                {
                    if (_operatorList[count].Equals("＋") || _operatorList[count].Equals("－"))
                    {
                        _numbers[count] = Calculate(_numbers[count], _numbers[count + 1], _operatorList[count]);
                        _numbers.RemoveAt(count + 1);
                        _operatorList.RemoveAt(count);
                    }
                    count++;
                }
                
                return _numbers[0];
            }
        }

        private decimal Calculate(decimal num1, decimal num2, string a)
        {
            decimal result = 0;
            switch (a)
            {
                case "＋":
                    result = num1 + num2;
                    break;
                case "－":
                    result = num1 - num2;
                    break;
                case "×":
                    result = num1 * num2;
                    break;
                case "÷":
                    result = num1 / num2;
                    break;
            }
            return result;
        }
    }
}
