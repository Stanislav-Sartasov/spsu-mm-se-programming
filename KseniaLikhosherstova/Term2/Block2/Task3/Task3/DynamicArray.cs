namespace Task3
{
    public class DynamicArray<T>
    {
        private T[]? data;

        public int CountOfElements { get; private set; }


        public DynamicArray()
        {
            data = default;
            CountOfElements = 0;

        }

        public T this[int index]
        {
            get
            {
                if ((index >= 0) && (index <= data!.Length))
                    return data[index];
                else
                    return default(T)!;

            }
        }

        public void Add(T value)
        {
            if (data == default)
            {
                data = new T[2];
            }
            else if (CountOfElements == data.Length)
            {
                Resize(CountOfElements * 2);
            }
            data[CountOfElements] = value;
            CountOfElements++;
        }


         private void Resize(int newSize)
        {
            T[] newArr = new T[newSize];

            data!.CopyTo(newArr, 0);

            data = newArr;
        }

        public T GetByIndex(int index)
        {
            if (index < 0 || index > CountOfElements - 1)
                throw new IndexOutOfRangeException("Invalid index");

            return data![index];
        }

        public int GetIndex(T value)
        {
            for (int i = 0; i <= CountOfElements; i++)
            {
                if (value.Equals(data![i]))
                {
                    return i;
                }

                else if (value == null)
                {
                    throw new NullReferenceException("The entered value is undefined");
                }
            }

            return -1;
        }

        public void DeleteByIndex(int index)
        {
            if (index < 0 || index > CountOfElements - 1)
                throw new IndexOutOfRangeException("Invalid index");

            for (int i = index + 1; i < CountOfElements; i++)
                data![i - 1] = data[i];
            CountOfElements--;

        }

        public bool DeleteElement(T value)
        {
            for (int i = 0; i < CountOfElements; i++)
            {
                if (data[i].Equals(value))
                {
                    DeleteByIndex(i);
                    return true;
                }

                else if (value == null)
                {
                    throw new NullReferenceException("The entered value is undefined");
                }
            }
            return false;
        }

    }
}