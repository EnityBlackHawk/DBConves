using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DBTelegraph.Model
{
    public class Table
    {
        private string _name;

        public string Name
        {
            get { return _name.ToLower(); }
            set { _name = value; }
        }

        private List<Register> _registers;

        public List<Column> Columns { get; set; }



        public Table(string name, params Column[] columns)
        {
            Name = name;
            Columns = new List<Column>(columns);
            _registers = new List<Register>();


        }

        public dynamic this[int index]
        {
            get => _registers[index];
        }

        public void AddRegister(IDictionary<string, object> data)
        {
            Register r;
            if (_registers.Count == 0)
            {
                r = new Register();
                foreach (var c in Columns)
                {
                    (r as dynamic)[c.Name] = null;
                }
                r.Close();
            }
            else
            {
                r = new(_registers[0]);
            }

            foreach (var d in data)
            {
                (r as dynamic)[d.Key] = d.Value;
            }

            _registers.Add(r);
        }

    }
}
