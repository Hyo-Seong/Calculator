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
        public const string ZERO = "0";

        private bool _operatorFlag = true;
        private bool _endCalFlag = true;

        private string _tempOperator = string.Empty;

        private ObservableCollection<Calculation> _calLogItems = new ObservableCollection<Calculation>();
        public ObservableCollection<Calculation> CalLogItems
        {
            get
            {
                return _calLogItems;
            }
            set
            {
                SetProperty(ref _calLogItems, value);
            }
        }

        private Calculation _cal = new Calculation();
        public Calculation Cal
        {
            get
            {
                return _cal;
            }
            set
            {
                SetProperty(ref _cal, value);
            }
        }

        #endregion

        #region Commands
        public DelegateCommand<string> ClickBtnCommand { get; private set; }

        #endregion
        
        public CalculationViewModel()
        {
            ClickBtnCommand = new DelegateCommand<string>(BtnClick);
        }

        public void BtnClick(string content)
        {
            // 0 ~ 9
            if (content.IsInt())
            {
                if (_endCalFlag)
                {
                    Cal.Result = ZERO;
                }
                if (_cal.NumberList.Count != 0 && !String.IsNullOrEmpty(_tempOperator))
                {
                    _cal.OperatorList.Add(_tempOperator);
                    _operatorFlag = true;
                    _tempOperator = string.Empty;
                    Cal.Result = ZERO;
                }
                _endCalFlag = false;
                Cal.Result += content;
                return;
            }
            // + - × ÷
            else if(content.IsOperator() && !Cal.Result.Equals("0")) 
            {
                _tempOperator = content;
                
                if (_operatorFlag)
                {
                    _cal.NumberList.Add(Decimal.Parse(Cal.Result));
                    Cal.Description += Cal.Result + content;

                    _operatorFlag = false;
                    _endCalFlag = true;
                    return;
                }
                Cal.Description = Cal.Description.Remove(Cal.Description.Length - 1);
                Cal.Description += _tempOperator;

                return;
            }
            // ＝ ← CE C .
            switch(content){
                case "＝":
                    if (!_endCalFlag)
                    {
                        _cal.NumberList.Add(Decimal.Parse(Cal.Result));
                    }
                    
                    CalLogItems.Add(_cal);

                    InitVariables(Calculate().ToString());
                    break;
                case "←":
                    if (_endCalFlag)
                    {
                        break;
                    }
                    if(Cal.Result.Length <= 1){
                        Cal.Result = ZERO;
                        break;
                    }
                    Cal.Result = Cal.Result.Remove(Cal.Result.Length - 1);
                    break;
                case "CE":
                    Cal.Result = ZERO;
                    break;
                case "C":
                    InitVariables();
                    break;
                case ".":
                    if (!Cal.Result.Contains('.'))
                    {
                        Cal.Result += content;
                    }
                    break;
            }
        }

        private void InitVariables(string result = ZERO)
        {
            Cal = new Calculation
            {
                Result = result
            };
            _tempOperator = string.Empty;
            _operatorFlag = true;
            _endCalFlag = true;
        }

        private decimal Calculate()
        {
            decimal calResult = 0;
            if (_cal.NumberList.Count == 1)
            {
                calResult = _cal.NumberList[0];
            }
            else
            {
                A("×", "÷");
                A("＋", "－");
                
                calResult = _cal.NumberList[0];
            }
            return Math.Round(calResult, 8);
        }

        private void A(string str1, string str2)
        {
            int count = 0;
            while (_cal.OperatorList.Count > count)
            {
                if (_cal.OperatorList[count].Equals(str1) || _cal.OperatorList[count].Equals(str2))
                {
                    _cal.NumberList[count] = Calculate(_cal.NumberList[count], _cal.NumberList[count + 1], _cal.OperatorList[count]);
                    _cal.NumberList.RemoveAt(count + 1);
                    _cal.OperatorList.RemoveAt(count);
                }
                count++;
            }
        }

        private decimal Calculate(decimal num1, decimal num2, string op)
        {
            decimal result = 0;
            switch (op)
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