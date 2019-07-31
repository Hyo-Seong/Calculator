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

        private bool _operatorFlag = true;

        private string _tempOperator;

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

        private string _description = string.Empty;
        public string Description
        {
            get
            {
                return _description;     
            }
            set
            {
                SetProperty(ref _description, value);
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
                if (string.IsNullOrEmpty(value))
                {
                    value = ZERO;
                }
                if (value.Contains("0."))
                {

                }
                else if (value.Length != 1 && value[0] == '0')
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
            ClickBtnCommand = new DelegateCommand<string>(BtnClick);
        }

        public void BtnClick(string content)
        {
            // 0 ~ 9
            if (content.IsInt())
            {
                if (_cal.NumberList.Count != 0 && !String.IsNullOrEmpty(_tempOperator))
                {
                    _cal.OperatorList.Add(_tempOperator);
                    _operatorFlag = true;
                    _tempOperator = string.Empty;
                    Result = ZERO;
                }
                Result += content;
                return;
            }
            // + - × ÷
            else if(content.IsOperator() && !Result.Equals("0")) 
            {
                _tempOperator = content;
                
                if (_operatorFlag)
                {
                    _cal.NumberList.Add(Decimal.Parse(Result));
                    Description += Result + content;

                    _result = string.Empty;
                    _operatorFlag = false;
                    return;
                }
                Description = Description.Remove(Description.Length - 1);
                Description += _tempOperator;

                return;
            }
            // ＝ ← CE C .
            switch(content){
                case "＝":
                    Description = string.Empty;
                    _cal.NumberList.Add(Decimal.Parse(Result));
                    _tempOperator = string.Empty;
                    Result = Calculate().ToString();
                    _result = string.Empty;
                    //CalItems.Add();
                    break;
                case "←":
                    if (string.IsNullOrEmpty(_result))
                    {
                        break;
                    }
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
                    Result = string.Empty;
                    _cal = new Calculation();
                    Description = string.Empty;
                    _tempOperator = string.Empty;
                    _operatorFlag = true;
                    break;

                case ".":
                    if (Result.Contains('.'))
                    {
                        break;
                    }
                    Result += content;
                    break;
            }
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
                int count = 0;
                while (_cal.OperatorList.Count > count)
                {
                    if (_cal.OperatorList[count].Equals("×") || _cal.OperatorList[count].Equals("÷"))
                    {
                        _cal.NumberList[count] = Calculate(_cal.NumberList[count], _cal.NumberList[count + 1], _cal.OperatorList[count]);
                        _cal.NumberList.RemoveAt(count + 1);
                        _cal.OperatorList.RemoveAt(count);
                    }
                    count++;
                }
                count = 0;
                while (_cal.OperatorList.Count > count)
                {
                    if (_cal.OperatorList[count].Equals("＋") || _cal.OperatorList[count].Equals("－"))
                    {
                        _cal.NumberList[count] = Calculate(_cal.NumberList[count], _cal.NumberList[count + 1], _cal.OperatorList[count]);
                        _cal.NumberList.RemoveAt(count + 1);
                        _cal.OperatorList.RemoveAt(count);
                    }
                    count++;
                }
                
                calResult = _cal.NumberList[0];
            }
            _cal.NumberList = new List<decimal>();
            return Math.Round(calResult, 8);
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