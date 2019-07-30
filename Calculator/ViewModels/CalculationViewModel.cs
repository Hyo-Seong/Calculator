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

        private Calculation _calculation = new Calculation();

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
            ClickBtnCommand = new DelegateCommand<string>(BtnClick);
        }

        public void BtnClick(string content)
        {
            // 0 ~ 9
            if (content.IsInt())
            {
                if (_calculation.NumberList.Count != 0 && !String.IsNullOrEmpty(_tempOperator))
                {
                    _calculation.OperatorList.Add(_tempOperator);
                    _operatorFlag = true;
                    _tempOperator = string.Empty;
                    Result = ZERO;
                }
                Result += content;
                return;
            }
            // + - × ÷
            else if(content.IsOperator()) 
            {
                _tempOperator = content;
                
                if (_operatorFlag)
                {
                    _calculation.NumberList.Add(Decimal.Parse(Result));
                    Description += Result + content;
                    _operatorFlag = false;
                    return;
                }
                Description = Description.Remove(Description.Length - 1);
                Description += content;

                return;
            }
            
            switch(content){
                case "＝":
                    if (_operatorFlag)
                    {
                        Description += Result;
                    }
                    _calculation.NumberList.Add(Decimal.Parse(Result));
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
                    _calculation = new Calculation();
                    break;
                case ".":
                    Result += content;
                    break;
            }

        }

        private decimal Calculate()
        {
            if (_calculation.NumberList.Count == 1)
            {
                return _calculation.NumberList[0];
            }
            else
            {
                int count = 0;
                while (_calculation.OperatorList.Count > count)
                {
                    if (_calculation.OperatorList[count].Equals("×") || _calculation.OperatorList[count].Equals("÷"))
                    {
                        _calculation.NumberList[count] = Calculate(_calculation.NumberList[count], _calculation.NumberList[count + 1], _calculation.OperatorList[count]);
                        _calculation.NumberList.RemoveAt(count + 1);
                        _calculation.OperatorList.RemoveAt(count);
                    }
                    count++;
                }
                count = 0;
                while (_calculation.OperatorList.Count > count)
                {
                    if (_calculation.OperatorList[count].Equals("＋") || _calculation.OperatorList[count].Equals("－"))
                    {
                        _calculation.NumberList[count] = Calculate(_calculation.NumberList[count], _calculation.NumberList[count + 1], _calculation.OperatorList[count]);
                        _calculation.NumberList.RemoveAt(count + 1);
                        _calculation.OperatorList.RemoveAt(count);
                    }
                    count++;
                }
                
                return _calculation.NumberList[0];
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