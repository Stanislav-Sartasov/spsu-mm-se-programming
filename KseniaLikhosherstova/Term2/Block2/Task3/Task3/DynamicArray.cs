namespace Task3
{
    public class DynamicArray<T>
    {
        public T[]? Data;

        public int CountOfElements { get; set; }



        public DynamicArray()
        {
            Data = default;
            CountOfElements = 0;

        }

        public T this[int index]
        {
            get
            {
                if ((index >= 0) && (index <= Data.Length))
                    return Data[index];
                else
                    return default(T);

            }
        }



        public void Add(T value)
        {
            if (Data == default)
            {
                Data = new T[2];
            }
            else if (CountOfElements == Data.Length)
            {
                Resize(CountOfElements * 2);
            }
            Data[CountOfElements] = value;
            CountOfElements++;
        }

        public void Resize(int newSize)
        {
            if (newSize < CountOfElements)
            {
                throw new Exception("The new size is less than the allowed one");
            }
            T[] newArr = new T[newSize];

            Data.CopyTo(newArr, 0);

            Data = newArr;
        }


        public T GetItem(int index)
        {
            if (index < 0 || index > CountOfElements - 1)
                throw new IndexOutOfRangeException("Invalid index");

            return Data[index];
        }



        public int GetIndex(T value) //ЗАМЕНИТЬ ИЛИ ЧЕТ ДРУГОЕ
        {
            for (int i = 0; i <= CountOfElements; i++)
            {
                if (value.Equals(Data[i]))
                    return i;
            }
            return -1;
        }




        public void RemoveAt(int index)
        {
            if (index < 0 || index > CountOfElements - 1)
                throw new IndexOutOfRangeException("Invalid index");

            for (int i = index + 1; i < CountOfElements; i++)
                Data[i - 1] = Data[i];
            CountOfElements--;



        }

        public bool Remove(T value)
        {
            for (int i = 0; i < CountOfElements; i++)
            {
                if (Data[i].Equals(value))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

    }
}