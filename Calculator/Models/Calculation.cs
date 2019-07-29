using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Calculation : BindableBase, ICloneable
    {
        private string _a;
        public string A
        {
            get 
            {
                return _a;
            }
            set
            {
                SetProperty(ref _a, value);
            }
        }

        private List<int> _numList;
        public List<int> NumList
        {
            get
            {
                return _numList;
            }
            set
            {
                SetProperty(ref _numList, value);
            }
        }

        private List<string> _operatorList;
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

        private decimal _result;
        public decimal Result
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

        public object Clone()
        {
            return new Calculation
            {
                A = this.A,
                NumList = this.NumList,
                OperatorList = this.OperatorList,
                Result = this.Result
            };
        }
    }
}
