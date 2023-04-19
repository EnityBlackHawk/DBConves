using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect.Model
{
    public class Register: DynamicObject
    {

        private Dictionary<string, object?> _data = new();
        private bool isClosed = false;

        public Register()
        {
            
        }

        public Register(Register register)
        {
            foreach(string c in register.ColumnsName())
            {
                _data.Add(c, null);
            }
            Close();
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object? value)
        {
            string key = (string)indexes[0];
            if(_data.ContainsKey(key))
            {
                _data[key] = value;
                return true;
            }
            else if(!isClosed && !_data.ContainsKey(key))
            {
                _data.Add(key, value);
                return true;
            }
            return false;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
            => _data.TryGetValue((string)indexes.First(), out result);

        public List<string> ColumnsName()
        {
            return _data.Keys.ToList();
        }

        public void Close() => isClosed = true;
    }
}
