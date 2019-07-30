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

    public enum Operator
    {
        [StringValue("＋")]
        Addition,
        [StringValue("-")]
        Subtraction,
        [StringValue("×")]
        Multiplication,
        [StringValue("÷")]
        Division
    }

    public class StringValue : System.Attribute 
    {
        private string _value; 
        public StringValue(string value) 
        { 
            _value = value; 
        } 
        public string Value 
        { 
            get 
            {
                return _value;
            } 
        } 
    }

    public static class StringEnum 
    { 
        public static string GetStringValue(Operator value) 
        { 
            string output = null; 
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[]; 
            if(attrs.Length > 0) 
            { 
                output = attrs[0].Value; 
            }
            return output; 
        } 


    }


}
