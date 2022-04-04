using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyListLibrary
{
    public class Node<T>
    {
        internal T Item { get; }
        internal Node<T> Next;
        internal Node<T> Previous;
        public Node(T item)
        {
            Item = item;
        }

    }
}
