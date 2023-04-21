using System;
using System.Collections;
using System.Collections.Generic;
namespace Task_2
{
    public class DoublyLinkedListEnumerator<T> : IEnumerator<T>, IEnumerator, IDisposable
    {
        public DoublyLinkedList<T> List { get; private set; }
        public int Position { get; private set; } = -1;
        public bool Disposed { get; private set; } = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
            {
                return;
            }
            if (disposing)
            {
                uint len = List.Length;
                for (int i = 0; i < len; i++)
                {
                    List.DeleteByIndex(0);
                }
            }
            Disposed = true;
        }

        ~DoublyLinkedListEnumerator()
        {
            Dispose(false);
        }

        public DoublyLinkedListEnumerator(Node<T> beginning, Node<T> ending, uint length)
        {

            List = new DoublyLinkedList<T>(beginning, ending, length);
        }

        public bool MoveNext()
        {
            if (Position < List.Length - 1)
            {
                Position += 1;
                return true;
            }
            return false;
        }

        private object CurrentGeneric
        {
            get { return this.Current; }
        }

        object IEnumerator.Current
        {
            get { return CurrentGeneric; }
        }

        public T Current
        {
            get
            {
                return List.GetByIndex(Position);
            }
        }

        public void Reset()
        {
            Position = -1;
        }
    }
}
