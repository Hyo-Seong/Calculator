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
                if (Cal.EndCalFlag)
                {
                    Cal.Result = ZERO;
                }
                if (Cal.NumberList.Count != 0 && !String.IsNullOrEmpty(Cal.TempOperator))
                {
                    Cal.OperatorList.Add(Cal.TempOperator);

                    Cal.OperatorFlag = true;
                    Cal.TempOperator = string.Empty;
                    Cal.Result = ZERO;
                }
                Cal.EndCalFlag = false;
                Cal.Result += content;
                return;
            }
            // + - × ÷
            else if(content.IsOperator() && !Cal.Result.Equals("0")) 
            {
                Cal.TempOperator = content;
                
                if (Cal.OperatorFlag)
                {
                    Cal.NumberList.Add(Decimal.Parse(Cal.Result));
                    Cal.Description += Cal.Result + content;

                    Cal.OperatorFlag = false;
                    Cal.EndCalFlag = true;
                    return;
                }
                Cal.Description = Cal.Description.Remove(Cal.Description.Length - 1);
                Cal.Description += Cal.TempOperator;

                return;
            }
            // ＝ ← CE C .
            switch(content){
                case "＝":
                    ExcuteEqual();
                    break;
                case "←":
                    ExcuteRemove();
                    break;
                case "CE":
                    ExcuteCE();
                    break;
                case "C":
                    ExcuteC();
                    break;
                case ".":
                    ExcuteDot();
                    break;
            }
        }

        private void ExcuteDot()
        {
            if (!Cal.Result.Contains('.'))
            {
                Cal.Result += ".";
            }
        }

        private void ExcuteC()
        {
            InitVariables(new Calculation());
        }
        private void ExcuteCE()
        {
            Cal.Result = ZERO;
        }

        private void ExcuteRemove()
        {
            if (Cal.EndCalFlag)
            {
                return;
            }
            if (Cal.Result.Length <= 1)
            {
                Cal.Result = ZERO;
                return;
            }
            Cal.Result = Cal.Result.Remove(Cal.Result.Length - 1);
        }

        private void ExcuteEqual(){
            if (!Cal.EndCalFlag)
            {
                Cal.Description += Cal.Result;
                Cal.NumberList.Add(Decimal.Parse(Cal.Result));
            }
            else
            {
                Cal.Description = Cal.Description.Remove(Cal.Description.Length - 1);
            }

            Cal.Result += "=";

            Cal.Result = Calculate().ToString();
            CalLogItems.Add((Calculation)Cal.Clone());

            InitVariables(new Calculation { Result = Cal.Result });
        }

        private void InitVariables(Calculation cal)
        {
            Cal = cal;
        }

        private decimal Calculate()
        {
            decimal calResult = 0;
            if (Cal.NumberList.Count == 1)
            {
                calResult = Cal.NumberList[0];
            }
            else
            {
                CalByPriority("×", "÷");
                CalByPriority("＋", "－");
                
                calResult = Cal.NumberList[0];
            }
            return Math.Round(calResult, 8);
        }

        private void CalByPriority(string str1, string str2)
        {
            int count = 0;
            while (Cal.OperatorList.Count > count)
            {
                if (Cal.OperatorList[count].Equals(str1) || Cal.OperatorList[count].Equals(str2))
                {
                    Cal.NumberList[count] = Calculate(Cal.NumberList[count], Cal.NumberList[count + 1], Cal.OperatorList[count]);
                    Cal.NumberList.RemoveAt(count + 1);
                    Cal.OperatorList.RemoveAt(count);
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