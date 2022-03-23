using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyListLibrary
{
    public class Node<T>
    {
        public T Item;
        public Node<T> Next;
        public Node<T> Previous;
        public Node(T item)
        {
            Item = item;
        }
    }
}
