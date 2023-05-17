using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    internal class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged
    {

        private IDictionary<TKey, TValue> _dictionary;

        public ICollection<TKey> Keys => new ObservableCollection<TKey>();

        public ICollection<TValue> Values => new ObservableCollection<TValue>();

        public int Count => Keys.Count;

        public bool IsReadOnly => false;

        public TValue this[TKey key] 
        { 
            get
            {
                return _dictionary[key];
            }
            set
            {
                TryUpdateOrAdd(key, value);
            }
        }

        public void TryUpdateOrAdd(TKey key, TValue value)
        {
            TValue oldValue;
            if(_dictionary.ContainsKey(key) && _dictionary.TryGetValue(key, out oldValue))
            {
                _dictionary[key] = value;
                NotifyUpdate(key, oldValue, value);
                return;
            }
            else
            {
                _dictionary.Add(key, value);
                NotifyAdd(key, value);
                return;
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void NotifyAdd(TKey key, TValue value)
        {
            CollectionChanged(
                this,
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    new KeyValuePair<TKey, TValue>(key, value)
                    )
                );
        }

        private void NotifyRemove(TKey key, TValue value)
        {
            CollectionChanged(
                this,
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove,
                    new KeyValuePair<TKey, TValue>(key, value)
                    )
                );
        }

        private void NotifyClear()
        {
            CollectionChanged(
                this,
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset,
                    null
                    )
                );
        }

        private void NotifyUpdate(TKey key, TValue oldValue, TValue newValue)
        {
            CollectionChanged(
                this,
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Replace,
                    new KeyValuePair<TKey, TValue>(key, oldValue),
                    new KeyValuePair<TKey, TValue>(key, newValue)
                    )
                );
        }

        public ObservableDictionary() {}

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
            NotifyAdd(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            TValue value;
            if (!_dictionary.TryGetValue(key, out value))
                return false;
            
            _dictionary.Remove(key);
            NotifyRemove(key, value);
            return true;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionary.Add(item);
            NotifyAdd(item.Key, item.Value);
        }

        public void Clear()
        {
            _dictionary.Clear();
            NotifyClear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
