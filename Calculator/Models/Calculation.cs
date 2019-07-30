using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Calculation : BindableBase, ICloneable
    {
        private List<decimal> _numberList;
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

        public object Clone()
        {
            return new Calculation
            {
                NumberList = this.NumberList,
                OperatorList = this.OperatorList,
            };
        }
    }
}
