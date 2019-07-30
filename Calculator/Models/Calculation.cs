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

        public object Clone()
        {
            return new Calculation
            {
                NumList = this.NumList,
                OperatorList = this.OperatorList,
            };
        }
    }
}
