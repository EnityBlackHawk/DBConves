using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public struct TripleValue<T1, T2, T3>
    {
        public TripleValue(T1 value1, T2 value2, T3 value3)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }

        public T1 Value1 { get; set; }
        public T2 Value2 { get; set; }
        public T3 Value3 { get; set; }
    }


    public class Matrix3<T1, T2, T3> : IEnumerable<TripleValue<T1, T2, T3>>
    {
        private List<T1> _col1;
        private List<T2> _col2;
        private List<T3> _col3;

        public Matrix3()
        {
            _col1 = new List<T1>();
            _col2 = new List<T2>();
            _col3 = new List<T3>();
        }

        public virtual void Add(T1 value1, T2 value2, T3 value3)
        {
            _col1.Add(value1);
            _col2.Add(value2);
            _col3.Add(value3);
        }

        public virtual TripleValue<T1, T2, T3> GetByCol1(T1 value1)
        {
            int index = _col1.IndexOf(value1);
            return new(_col1[index], _col2[index], _col3[index]);
        }

        public virtual void SetCol3ByCol1(T1 value1, T3 value3)
        {
            int index = _col1.IndexOf(value1);
            _col3[index] = value3;
        }

        public IEnumerator<TripleValue<T1, T2, T3>> GetEnumerator()
        {
            for(int i = 0; i < _col1.Count; i++)
                yield return new TripleValue<T1, T2, T3>(_col1[i], _col2[i], _col3[i]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
