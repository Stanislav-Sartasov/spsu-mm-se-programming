using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleLinkedList
{

    //ElementType должен быть сравним, т.к. хранимые элементы хранятся отсортированными для упрощения поиска.
    public class DoubleLinkedList<ElementType> where ElementType : IComparable
    {
        public ElementType Element { get; }
        public DoubleLinkedList<ElementType>? Next { get; set; }
        public DoubleLinkedList<ElementType>? Previous { get; set; }

        public DoubleLinkedList(ElementType Data)
        {
            Element = Data;
            Next = null;
            Previous = null;
        }

        public void Add(ElementType Data)
        {
            if (Element.CompareTo(Data) == 0)
            {
                if (Next == null)
                {
                    Next = new DoubleLinkedList<ElementType>(Data);
                    Next.Previous = this;
                }
                else
                {
                    DoubleLinkedList<ElementType> temp = new DoubleLinkedList<ElementType>(Data);
                    temp.Next = Next;
                    temp.Previous = this;
                    Next.Previous = temp;
                    Next = temp;
                }
            }
            else if (Element.CompareTo(Data) > 0)
            {
                if (Previous == null)
                {
                    Previous = new DoubleLinkedList<ElementType>(Data);
                    Previous.Next = this;
                }
                else if (Previous.Element.CompareTo(Data) > 0)
                {
                    Previous.Add(Data);
                }
                else
                {
                    DoubleLinkedList<ElementType> temp = new DoubleLinkedList<ElementType>(Data);
                    temp.Next = this;
                    temp.Previous = Previous;
                    Previous.Next = temp;
                    Previous = temp;
                }
            }
            else
            {
                if (Next == null)
                {
                    Next = new DoubleLinkedList<ElementType>(Data);
                    Next.Previous = this;
                }
                else if (Next.Element.CompareTo(Data) < 0)
                {
                    Next.Add(Data);
                }
                else
                {
                    DoubleLinkedList<ElementType> temp = new DoubleLinkedList<ElementType>(Data);
                    temp.Next = Next;
                    temp.Previous = this;
                    Next.Previous = temp;
                    this.Next = temp;
                }
            }
        }

        //Возвращаем произвольный не удалённый элемент нового двусвязного списка, если операция прошла успешно или элемент не был найден
        //Возвращаем null, если удаляемый элемент - последний в списке.
        public DoubleLinkedList<ElementType>? Remove(ElementType Data)
        {

            //Если нет ни правого, ни левого элемента, то он в списке последний. 
            //В таком случае вернём null или текущий объект в зависимости от равенства.
            if (Next == null && Previous == null)
            {
                return (Element.CompareTo(Data) == 0 ? null : this);
            }

            if (Element.CompareTo(Data) == 0)
            {
                if (Next == null)
                {
                    Previous.Next = null;
                    return Previous;
                }
                else if (Previous == null)
                {
                    Next.Previous = null;
                    return Next;
                }
                else
                {
                    Previous.Next = Next;
                    Next.Previous = Previous;
                    return Next;
                }
                
            }

            if (Element.CompareTo(Data) > 0)
            {
                return Previous == null ? this : Previous.Remove(Data);
            }
            else
            {
                return Next == null ? this : Next.Remove(Data);
            }
        }

        //Вернёт true, если элемент был найден, и false, если не был найден.
        public bool Find(ElementType Data)
        {
            if (Next == null && Previous == null)
            {
                return Element.CompareTo(Data) == 0;
            }

            if (Element.CompareTo(Data) == 0)
            {
                return true;
            }

            if (Element.CompareTo(Data) > 0)
            {
                if (Previous != null)
                {
                    return Previous.Element.CompareTo(Data) >= 0 ? Previous.Find(Data) : false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (Next != null)
                {
                    return Next.Element.CompareTo(Data) <= 0 ? Next.Find(Data) : false;
                }
                else
                {
                    return false;
                }
            }
        }

        public void PrintElements()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            string result = "";
            DoubleLinkedList<ElementType> temp = this;
            while (temp.Previous != null)
            {
                temp = temp.Previous;
            }
            while (temp != null)
            {
                result += temp.Element.ToString();
                if (temp.Next != null)
                {
                    result += " ";
                }
                temp = temp.Next;
            }
            return result;
        }

    }
}
