using System;
using System.Collections;
using System.Collections.Generic;

namespace spp_lab9
{
    public class DynamicList<T> : IEnumerable<T>
    {

        private T[] Items { get; set; }
        public int Count { get; private set; }
        private const int Capacity = 16;
        

        public DynamicList()
        {
            Items = new T[Capacity];
        }

        public void Add(T element)
        {
            Resize();
            Items[Count++] = element;
        }

        public void Remove(T element)
        {
            RemoveAt(Array.IndexOf(Items, element));
        }

        public void RemoveAt(int index)
        {
            var newContainer = new T[Items.Length - 1];
            Array.Copy(Items, newContainer, index);
            Array.Copy(Items, index + 1, newContainer, index, Items.Length - index - 1);
            Items = newContainer;
            Count--;
        }

        public void Clear()
        {
            Items = Array.Empty<T>();
            Count = 0;
        }
        
        public T this[int index]
        {
            get => Items[index];
            set => Items[index] = value;
        }

        private void Resize()
        {
            if (Count != Items.Length)
            {
                return;
            }

            T[] newContainer = new T[Items.Length + Capacity];
            Array.Copy(Items, newContainer, Items.Length);
            Items = newContainer;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return Items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}