using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyListLibrary
{
    public class Node<T>
    {
        internal T item { get; }
        internal Node<T> next;
        internal Node<T> previous;
        public Node(T item)
        {
            this.item = item;
        }

    }
}
