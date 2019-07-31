using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Calculator.ViewModels;

namespace Calculator.Models
{
    public class Calculation : BindableBase, ICloneable
    {
        private List<decimal> _numberList = new List<decimal>();
        public List<decimal> NumberList
        {
            get
            {
                return _numberList;
            }
            set
            {
                SetProperty(ref _numberList, value);
            }
        }

        private List<string> _operatorList = new List<string>();
        public List<string> OperatorList
        {
            get
            {
                return _operatorList;
            }
            set
            {
                SetProperty(ref _operatorList, value);
            }
        }

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

        internal string _result = "0";
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
                    value = CalculationViewModel.ZERO;
                }
                else if (value.Contains("0."))
                {

                }
                else if (value.Length != 1 && value[0] == '0')
                {
                    value = value.Substring(1);
                }
                SetProperty(ref _result, value);
            }
        }

        public object Clone()
        {
            return new Calculation
            {
                NumberList = this.NumberList,
                OperatorList = this.OperatorList,
                Description = this.Description,
            };
        }
    }
}
