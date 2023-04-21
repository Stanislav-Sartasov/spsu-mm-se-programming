using System;
using System.Collections;
using System.Collections.Generic;
namespace Task_2
{
    public class DoublyLinkedList<T> : IEnumerable<T>, IEnumerable
    {
        public Node<T> Beginning { get; private set; }
        public Node<T> Ending { get; private set; }
        public uint Length { get; private set; }

        public DoublyLinkedList(Node<T> beginning = null, Node<T> ending = null, uint len = 0)
        {
            Beginning = beginning;
            Ending = ending;
            Length = len;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumeratorGeneric();
        }

        private IEnumerator GetEnumeratorGeneric()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            DoublyLinkedListEnumerator<T> enumerator = new DoublyLinkedListEnumerator<T>(Beginning, Ending, Length);
            return enumerator;
        }

        public T Add(T element)
        {
            Length += 1;
            if (Beginning == null)
            {
                Beginning = new Node<T>(element);
                return Beginning.Value;
            }
            else if (Ending == null)
            {
                Ending = new Node<T>(element, Beginning);
                Beginning.SetNodes(null, Ending);
                return Ending.Value;
            }
            else
            {
                Node<T> newNode = new Node<T>(element, Ending);
                Ending.SetNodes(Ending.Previous, newNode);
                Ending = newNode;
                return Ending.Value;
            }
        }

        public void DeleteByIndex(int index)
        {
            if (index >= Length || index < (-1) * Length)
            {
                throw new ArgumentOutOfRangeException("Index was out of range");
            }
            if (index < 0)
            {
                index = (int)Length + index;
            }
            if (Length == 1)
            {
                Beginning = null;
                Ending = null;
                Length -= 1;
            }
            else if (Length == 2 && index == 1)
            {
                Ending = null;
                Beginning.SetNodes(null, null);
                Length -= 1;
            }
            else if (index == 0)
            {
                Beginning = Beginning.Next;
                Beginning.SetNodes(null);
                Length -= 1;
                return;
            }
            else if (index == Length - 1)
            {
                Ending = Ending.Previous;
                Ending.SetNodes(Ending.Previous, null);
                Length -= 1;
                return;
            }
            else
            {
                Node<T> currentNode = Beginning;
                while (index > 0)
                {
                    currentNode = currentNode.Next;
                    index -= 1;
                }
                currentNode.Previous.SetNodes(null, currentNode.Next);
                currentNode.Next.SetNodes(currentNode.Previous);
                Length -= 1;
            }
        }

        public byte DeleteByValue(T value)
        {
            Node<T> currentNode = Beginning;
            while (currentNode != null && !(currentNode.Value.Equals(value)))
            {
                currentNode = currentNode.Next;
            }
            if (currentNode == null)
            {
                return 1;
            }
            else
            {
                currentNode.Previous.SetNodes(null, currentNode.Next);
                currentNode.Next.SetNodes(currentNode.Previous);
                Length -= 1;
                return 0;
            }
        }

        public T GetByIndex(int index)
        {
            if (index >= Length || index < (-1) * Length)
            {
                throw new ArgumentOutOfRangeException("Index was out of range");
            }
            if (index < 0)
            {
                index = (int)Length + index;
            }
            if (index >= Length / 2 && Ending != null)
            {
                Node<T> currentNode = Ending;
                while (index < Length - 1)
                {
                    currentNode = currentNode.Previous;
                    index += 1;
                }
                return currentNode.Value;
            }
            else
            {
                Node<T> currentNode = Beginning;
                while (index > 0)
                {
                    currentNode = currentNode.Next;
                    index -= 1;
                }
                return currentNode.Value;
            }
        }
    }
}
